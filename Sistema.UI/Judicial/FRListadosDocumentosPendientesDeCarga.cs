using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Base.UI;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrinting.Preview;
using DevExpress.XtraScheduler;
using Sistema.Model;
using Sistema.Query;
using Sistema.UI.Persona;

namespace Sistema.UI.Judicial
{
    public partial class FRListadosDocumentosPendientesDeCarga : FBaseForm
    {

        public enum EstadoDeFiltro {   Todos , PendientesDeCarga , Cargados };

        public EstadoDeFiltro _EstadoDeFiltro =  EstadoDeFiltro.PendientesDeCarga;
        ContextoModelo CtxModelo = new ContextoModelo();
        public FRListadosDocumentosPendientesDeCarga()
        {
            InitializeComponent();
            wbtnBorrar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            wbtnEditar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            wbtnNuevo.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            rbtnVisualizar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
          
            }

        public override void FnImprimir()
        {
            PrintPreviewRibbonFormEx pViewEx = new PrintPreviewRibbonFormEx();
            pViewEx.PrintingSystem = printingSystem1;
            printableComponentLink1.CreateDocument();
            pViewEx.Show();
        }

        private void GenerarDocumentos(EstadoDeFiltro estadoDeFiltro )
        {
            List<Documento> listaDocumentos = new List<Documento>();

            if (estadoDeFiltro == EstadoDeFiltro.Cargados)
            {
                listaDocumentos = CtxModelo.Documento.Where(x => x.RutaPc != null).Take(20).ToList();
            }
            else if (estadoDeFiltro == EstadoDeFiltro.Todos) {

               listaDocumentos = CtxModelo.Documento.Take(20).ToList();
            }
            else if (estadoDeFiltro == EstadoDeFiltro.PendientesDeCarga) {

                listaDocumentos = CtxModelo.Documento.Where(x => x.RutaPc == null).Take(20).ToList();

            }

            bsLista.DataSource = listaDocumentos;

        }




        private void btnMostrar_Click(object sender, EventArgs e)
        {
            GenerarDocumentos(EstadoDeFiltro.PendientesDeCarga);
        }


    }
}
