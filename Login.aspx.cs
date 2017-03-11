using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
namespace QuranStudy
{
    public partial class Login : System.Web.UI.Page
    {
        protected string connStr = ConfigurationManager.ConnectionStrings["ConnectionStr"].ToString();

        protected string loginMsg = "";
        protected string retPage = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            string userEmail = "";
            string password = "";
            string mode = "";

            if (Request.UrlReferrer != null)
            {
                if (Request.UrlReferrer.Host.ToLower().Contains("myquranstudy.com"))
                    retPage = Request.UrlReferrer.AbsoluteUri;
                else
                    retPage = "/study.aspx";

                if (Request.UrlReferrer.AbsolutePath.Length < 5)
                    retPage = "/study.aspx";

            }
            else
                retPage = "/study.aspx";

            if (!string.IsNullOrEmpty(Request.QueryString.Get("lo")))
            {
                Session["UserID"] = null;
                Session["UserName"] = null;
                Session["UserEmail"] = null;
                Session.Abandon();
                Response.Redirect(retPage);// ("/");
                 return;
            }
           

            if (!string.IsNullOrEmpty(Request.Form["mode"])) 
                mode = Request.Form.Get("mode").ToString();

            if (mode.Contains("loginClicked"))
            {
                userEmail = Request.Form["uemail"].ToString();
                password = Request.Form["pword"].ToString();
                retPage = Request.Form["retPage"].ToString();
                // if (username == "kaziali@" && password == "aaaa")
                int userid = -1;
                string name = "";
                if (VerifyLogin(userEmail, password, ref userid, ref name, ref loginMsg))
                {
                    Session["UserID"] = userid;
                    Session["UserName"] = name;
                    Session["UserEmail"] = userEmail;
                   // if (userEmail.ToLower()=="kaziali@oz.net")
                   //     Response.Redirect(retPage == "/study.aspx" ? "/mymushaf.aspx?page=540" : retPage);
                   // else
                    Response.Redirect(retPage);//"/study.aspx");
                }
                else
                {
                    //loginMsg = "Login failed";
                    Session["UserID"] = null;
                    Session["UserName"] = null;
                    Session["UserEmail"] = null;
                }
            }
            else
            {
               // if (Session["UserID"] == null) 
            
            }
             
            //else if (mode.Contains("loginClicked"))
             //   Response.Redirect("/default.aspx");
        }

        public bool VerifyLogin(string userEmail, string password, ref int userid, ref string name, ref string loginMsg)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    // SqlConnection oConn = new SqlConnection(Global.gConnString);
                    conn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd = new SqlCommand("[SP_GetUser]", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@userEmail", SqlDbType.VarChar).Value = userEmail;
                    cmd.Parameters.Add("@UserPassword", SqlDbType.VarChar).Value = password; 
                    SqlDataReader reader = null;
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        userid = Convert.ToInt32(reader["userId"].ToString());
                        name = reader["userName"].ToString();
                        return true;
                    }
                    loginMsg = "Log in failed.";
                    return false;
                }
            }
            catch (Exception ex)
            {
                loginMsg = "Log in failed. Error - " + ex.Message;
                return false;
            }


        }


    }
}