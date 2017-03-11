using System;

namespace QuranStudy
{
    public partial class FbSignup : System.Web.UI.Page
    {
        protected int IsNewFBUser = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                IsNewFBUser = Convert.ToInt32(Session["FBUserSignup"].ToString());
                Session["FBUserSignup"] = null;
            }
            catch
            {
                IsNewFBUser = 0;
                Session["FBUserSignup"] = null;
            }

        }
    }
}