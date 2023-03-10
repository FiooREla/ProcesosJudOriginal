using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using DevExpress.Web;
using Sistema.Model;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        Utils.ApplyTheme(this);
    }

    protected void Page_Load(object s, EventArgs e)
    {
        if (ShouldBingGrid())
        {
            DateTime fecha = DateTime.Now;
            DateTime fInicio = new DateTime(fecha.Year, fecha.Month == 1 ? 1 : fecha.Month - 1, 1);
            ASPxDateEdit deInicio = MailMenu.Items.FindByName("Fecha1").FindControl("ASPxDateEdit1") as ASPxDateEdit;
            ASPxDateEdit deFin = MailMenu.Items.FindByName("Fecha2").FindControl("ASPxDateEdit2") as ASPxDateEdit;
            if (deInicio.Value == null) deInicio.Value = fInicio;
            if (deFin.Value == null) deFin.Value = fecha;
            DateTime inicio = Convert.ToDateTime(deInicio.Value);
            DateTime fin = Convert.ToDateTime(deFin.Value);
            MailGrid.DataSource = ClsConsulta.GlviewExpediente(inicio, fin);
            MailGrid.DataBind();
        }
    }

    bool ShouldBingGrid()
    {
        return !IsCallback || MailGrid.IsCallback || MailPanel.IsCallback;
    }

    protected void MailGrid_FocusedRowChanged(object sender, EventArgs e)
    {
        viewExpediente entidad = (viewExpediente)MailGrid.GetRow(MailGrid.FocusedRowIndex);
        if (entidad == null) return;
        gvDetalles1.DataSource = ClsConsulta.GlActoProcesalContenido(entidad.IdExpediente);
        gvDetalles1.DataBind();
        gvDetalles2.DataSource = ClsConsulta.GlviewExpedienteInstancia(entidad.IdExpediente);
        gvDetalles2.DataBind();
    }

    protected void gvDetalles1_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        try
        {
            if (String.IsNullOrEmpty(e.Parameters)) gvDetalles1.DataSource = null;
            else
            {
                int id = Convert.ToInt32(e.Parameters);
                gvDetalles1.DataSource = ClsConsulta.GlActoProcesalContenido(id);
                gvDetalles1.DataBind();
            }
        }
        catch
        {
        }
    }

    protected void gvDetalles2_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        try
        {
            if (String.IsNullOrEmpty(e.Parameters)) gvDetalles2.DataSource = null;
            else
            {
                int id = Convert.ToInt32(e.Parameters);
                gvDetalles2.DataSource = ClsConsulta.GlviewExpedienteInstancia(id);
                gvDetalles2.DataBind();
            }
        }
        catch
        {
        }
    }
}
