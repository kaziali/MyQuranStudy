using System;

namespace QuranStudy
{
    public partial class MyAccount : System.Web.UI.Page
    {
        protected int UserID = -1;//-1=not logged in, 1=guest, 2=kazi
        protected string UserEmail = "<a href=\"/login.aspx\">sign in</a>";
        protected string UserName = "<a href=\"/login.aspx\">sign in</a>";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] != null)
                UserName = Session["UserName"].ToString();
            if (Session["UserEmail"] != null)
                UserEmail = Session["UserEmail"].ToString();
            if (Session["UserID"] != null)
                UserID = Convert.ToInt32(Session["UserID"].ToString());

        }
    }
}