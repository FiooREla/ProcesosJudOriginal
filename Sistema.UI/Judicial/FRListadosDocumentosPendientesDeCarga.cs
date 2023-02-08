using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Base.UI;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrinting.Preview;
using DevExpress.XtraScheduler;
using Sistema.Model;
using Sistema.Query;
using Sistema.UI.Otros;
using Sistema.UI.Persona;

namespace Sistema.UI.Judicial
{
    public partial class FRListadosDocumentosPendientesDeCarga : FBaseForm
    {

        public enum EstadoDeFiltro {   SinArchivos , PendientesDeCarga , Cargados };
        private const int numeroDeDocumentos = 20;
        private List<Documento> listaDocumentos = new List<Documento>();
        private List<Documento> listaDocumentosCreados=new List<Documento>();
        public EstadoDeFiltro estadoDeFiltroActual =  EstadoDeFiltro.PendientesDeCarga;
        private ContextoModelo CtxModelo = new ContextoModelo();
        public FRListadosDocumentosPendientesDeCarga()
        {
            InitializeComponent();
            wbtnBorrar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            wbtnEditar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            wbtnNuevo.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            rbtnVisualizar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            CargarFiltroDeBusqueda();

        }
        private void CargarFiltroDeBusqueda() {

            ComboBoxItemCollection coll = cmbFiltro.Properties.Items;
            coll.BeginUpdate();
            try
            {
                coll.Add(new ComboItem() { Codigo = EstadoDeFiltro.Cargados, Valor = EstadoDeFiltro.Cargados.ToString() });
                coll.Add(new ComboItem() { Codigo = EstadoDeFiltro.PendientesDeCarga, Valor = EstadoDeFiltro.PendientesDeCarga.ToString() });
                coll.Add(new ComboItem() { Codigo = EstadoDeFiltro.SinArchivos, Valor = EstadoDeFiltro.SinArchivos.ToString() });

            }
            finally
            {
                coll.EndUpdate();
            }
            cmbFiltro.SelectedIndex = 1;
        }

        public override void FnImprimir()
        {
            PrintPreviewRibbonFormEx pViewEx = new PrintPreviewRibbonFormEx();
            pViewEx.PrintingSystem = printingSystem1;
            printableComponentLink1.CreateDocument();
            pViewEx.Show();
        }

        private void GenerarDocumentos()
        {

            ComboItem valorComboBox = (ComboItem)cmbFiltro.SelectedItem;
            EstadoDeFiltro estadoDeFiltro = (EstadoDeFiltro)Enum.Parse(typeof(EstadoDeFiltro), valorComboBox.Valor);
            try
            {
                if (estadoDeFiltro == EstadoDeFiltro.Cargados)
                {
                    listaDocumentos = CtxModelo.Documento.Where(x => x.RutaPc != null && x.Documento1 != null).Take(numeroDeDocumentos).ToList();
                }
                else if (estadoDeFiltro == EstadoDeFiltro.SinArchivos)
                {
                    listaDocumentos = CtxModelo.Documento.Where(x => x.Documento1 == null).ToList();
                }
                else if (estadoDeFiltro == EstadoDeFiltro.PendientesDeCarga)
                {

                    listaDocumentos = CtxModelo.Documento.Where(x => x.RutaPc == null && x.Documento1 != null).Take(numeroDeDocumentos).ToList();

                }

                bsLista.DataSource = listaDocumentos;
                listaDocumentosCreados = new List<Documento>();
                estadoDeFiltroActual = estadoDeFiltro;
            }
            catch (Exception ex)
            {

                listaDocumentosCreados = new List<Documento>();
            }

            
            
        }

        private void btnMostrar_Click(object sender, EventArgs e)
        {
            
            GenerarDocumentos();
        }

        private void btnCargarDocumentos_Click(object sender, EventArgs e)
        {
           GestionDeDocumentos.CargarDocumentos(listaDocumentos,ref listaDocumentosCreados);
           GuardarRutasDeArchivo();
        }

        /// <summary>
        /// Carga documentos segun la variable cantidad 
        /// </summary>
       

        private void GuardarRutasDeArchivo() {

            CtxModelo.SaveChanges();
            GenerarDocumentos();

        }

     
        private void cmbFiltro_RightToLeftChanged(object sender, EventArgs e)
        {
         

        }
    }
}
