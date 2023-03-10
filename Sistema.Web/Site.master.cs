using System;
using System.Web.UI;
using DevExpress.Web;
using System.Web;

public partial class Site : MasterPage
{

    private bool Login { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {
        string usuario = Request.Cookies["User"] != null ? Request.Cookies["User"].Value : "";
        string password = Request.Cookies["Password"] != null ? Request.Cookies["Password"].Value : "";
        bool valido = ClsConsulta.Login(usuario, password);
        pcLogin.ShowOnPageLoad = !valido;
    }

    protected void ThemeSelector_DataBound(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ASPxComboBox themeSelector = (ASPxComboBox)sender;
            var item = themeSelector.Items.FindByValue(Page.Theme);
            if (item != null)
                themeSelector.SelectedItem = item;
        }
    }
    protected void btnAceptar_Click(object sender, EventArgs e)
    {

    }
}
