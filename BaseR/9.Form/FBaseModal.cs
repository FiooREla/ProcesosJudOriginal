using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BaseR.Ctrls;
using BaseR.Fns;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraDataLayout;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.DXErrorProvider;
using DevExpress.XtraEditors.Mask;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;

namespace BaseR
{
    public partial class FBaseModal : XtraForm
    {
        public DataLayoutControl DLControl;
        public LayoutControl DLControl2;

        private readonly bool SeCargo = false;
        public EnumEdicion TipoEdicion;

        public FBaseModal()
        {
            InitializeComponent();
        }

        public FnFiltro ClsFiltro { get; set; }
        public bool SeGrabo { get; set; }
        public object EntidadActual { get; set; }
        public string TipoInterno { get; set; }
        public Control FirstControl { get; set; }
        public object EntidadForm { get; set; }
        private bool SeMostroFormulario { get; set; }

        private void FBaseModal_Load(object sender, EventArgs e)
        {
            if (SeCargo) return;
            FnLoadBasic();
            FnControl();
        }

        public void FnLoadBasic()
        {
            if (DLControl != null || DLControl2 != null) return;
            try
            {
                var ctrl = Controls.Find("dlcData", true).FirstOrDefault();
                if (ctrl is DataLayoutControl) DLControl = (DataLayoutControl) ctrl;
                if (ctrl is LayoutControl) DLControl2 = (LayoutControl) ctrl;
            }
            catch (Exception)
            {
            }
        }

        public virtual void FnLoadForm()
        {
        }

        public void FnDialog(EnumEdicion tipo)
        {
            SeMostroFormulario = false;
            TipoEdicion = tipo;
            if (tipo == EnumEdicion.Nuevo || tipo == EnumEdicion.Editar || tipo == EnumEdicion.Borrar)
            {
                if (DLControl != null) DLControl.OptionsView.IsReadOnly = DefaultBoolean.False;
                if (DLControl2 != null) DLControl2.OptionsView.IsReadOnly = DefaultBoolean.False;
                GrupoDatos.Visible = true;
                GrupoOpcion.Visible = false;
            }
            else if (tipo == EnumEdicion.Visualizar)
            {
                if (DLControl != null) DLControl.OptionsView.IsReadOnly = DefaultBoolean.True;
                if (DLControl2 != null) DLControl2.OptionsView.IsReadOnly = DefaultBoolean.True;
                GrupoDatos.Visible = false;
                GrupoOpcion.Visible = true;
            }

            SeGrabo = false;
            ShowDialog();
        }

        public void FnLuegoEdicion(EnumOperacion tipo)
        {
            if (tipo == EnumOperacion.Grabar)
            {
                if (Tag == null)
                {
                    var msg = "Los datos son conformes.?";
                    if (TipoEdicion == EnumEdicion.Borrar) msg = "Seguro que desea borrar el registro.?";
                    if (FormFiltro) msg = "Aplicar los filtros establecidos.?";
                    var result = XtraMessageBox.Show(msg, "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.No || !FnGrabar()) return;
                    SeGrabo = true;
                }
                else
                {
                    if (!FnGrabar()) return;
                    SeGrabo = true;
                }
            }
            else if (tipo == EnumOperacion.Carcelar)
            {
                FnCancelar();
                SeGrabo = false;
            }

            Close();
        }

        public virtual void FnMostrar(object arg1 = null, object arg2 = null)
        {
        }

        public virtual bool FnMostrarForm()
        {
            return true;
        }

        public virtual bool FnGrabar()
        {
            return true;
        }

        public virtual void FnCancelar()
        {
        }

        private bool FnValidar()
        {
            return DxValidacion.Validate();
        }

        private void btnGrabar_ItemClick(object sender, ItemClickEventArgs e)
        {
            SendKeys.SendWait("{TAB}");
            var valido = FnValidar();
            if (!valido) return;
            FnLuegoEdicion(EnumOperacion.Grabar);
        }

        private void btnCancelar_ItemClick(object sender, ItemClickEventArgs e)
        {
            FnLuegoEdicion(EnumOperacion.Carcelar);
        }

        private void btnCerrar_ItemClick(object sender, ItemClickEventArgs e)
        {
            Close();
        }

        private void FBaseModal_Activated(object sender, EventArgs e)
        {
            if (!SeMostroFormulario && FirstControl != null) FirstControl.Focus();
            SeMostroFormulario = true;
        }

        #region Filtro

        private bool _FormFiltro;

        public bool FormFiltro
        {
            get => _FormFiltro;
            set
            {
                _FormFiltro = value;
                if (_FormFiltro)
                {
                    btnGrabar.Caption = "Aceptar";
                    btnGrabar.LargeImageIndex = 4;
                    btnGrabar.Hint = "Aceptar";
                }
            }
        }

        #endregion

        #region Datos por defecto y validaciones

        private readonly DXValidationProvider DxValidacion = new DXValidationProvider();

        public void FnControl()
        {
            if (DLControl == null && DLControl2 == null) return;
            var grids2 = new List<GridControl>();
            if (DLControl != null) grids2 = ExtControls.FnGetControls<GridControl>(DLControl);
            if (DLControl2 != null) grids2 = ExtControls.FnGetControls<GridControl>(DLControl2);
            foreach (var item in grids2)
            {
                item.EmbeddedNavigator.Buttons.Append.Visible = false;
                item.EmbeddedNavigator.Buttons.Edit.Visible = false;
                item.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
                item.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
                item.EmbeddedNavigator.Buttons.Remove.Visible = false;
                foreach (var rpi in item.RepositoryItems)
                    if (rpi.GetType().Name == "RepositoryItemGridLookUpEdit")
                        Glue.FnGlueBase(rpi);

                foreach (var viewItem in item.Views)
                {
                    var view = viewItem as GridView;
                    view.OptionsNavigation.EnterMoveNextColumn = true;
                }
            }

            var glues = new List<GridLookUpEdit>();
            if (DLControl != null) glues = ExtControls.FnGetControls<GridLookUpEdit>(DLControl);
            if (DLControl2 != null) glues = ExtControls.FnGetControls<GridLookUpEdit>(DLControl2);

            foreach (var item in glues)
            {
                item.Properties.AppearanceDisabled.ForeColor = Color.Black;
                item.Properties.AppearanceDisabled.Options.UseForeColor = true;
                Glue.FnGlueBase(item);
                if (item.Tag != null) DxValidacion.SetValidationRule(item, new ValidationRuleControl(item));
            }

            var dates = new List<DateEdit>();
            if (DLControl != null) dates = ExtControls.FnGetControls<DateEdit>(DLControl);
            if (DLControl2 != null) dates = ExtControls.FnGetControls<DateEdit>(DLControl2);
            foreach (var item in dates)
            {
                item.Properties.Mask.MaskType = MaskType.DateTimeAdvancingCaret;
                item.Properties.TextEditStyle = TextEditStyles.Standard;
                item.Properties.ValidateOnEnterKey = true;
                item.Properties.AppearanceDisabled.ForeColor = Color.Black;
                item.Properties.AppearanceDisabled.Options.UseForeColor = true;
            }

            var txts = new List<TextEdit>();
            if (DLControl != null) txts = ExtControls.FnGetControls<TextEdit>(DLControl);
            if (DLControl2 != null) txts = ExtControls.FnGetControls<TextEdit>(DLControl2);
            foreach (var item in txts)
            {
                if (item.Tag != null) DxValidacion.SetValidationRule(item, new ValidationRuleControl(item));
                item.EnterMoveNextControl = true;
                item.Properties.ValidateOnEnterKey = true;
                item.Properties.AppearanceDisabled.ForeColor = Color.Black;
                item.Properties.AppearanceDisabled.Options.UseForeColor = true;
            }

            var mees = new List<MemoEdit>();
            if (DLControl != null) mees = ExtControls.FnGetControls<MemoEdit>(DLControl);
            if (DLControl2 != null) mees = ExtControls.FnGetControls<MemoEdit>(DLControl2);
            foreach (var item in mees)
            {
                if (item.Tag != null) DxValidacion.SetValidationRule(item, new ValidationRuleControl(item));
                //item.EnterMoveNextControl = true;
                item.Properties.ValidateOnEnterKey = true;
                item.Properties.AppearanceDisabled.ForeColor = Color.Black;
                item.Properties.AppearanceDisabled.Options.UseForeColor = true;
            }
        }

        #endregion
    }
}