using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace QuranStudy
{
    public class Global : System.Web.HttpApplication
    {
        protected List<string> Surahs;// = new List<string>();
        protected void Application_Start(object sender, EventArgs e)
        {
            Application["surahs"] = Surahs;
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            Session["VocabExcludeIds"] = "";
            Session["VocabLang"] = "ar";
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}