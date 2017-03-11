using System;

namespace QuranStudy
{
    public partial class LanesLex : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(Request.QueryString["p"]))
                Response.Redirect(Request.QueryString["p"].ToString());

        }
    }
}