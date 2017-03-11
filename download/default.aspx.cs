using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
namespace QuranStudy.download
{
    public partial class _default : System.Web.UI.Page
    {
        protected string connStr = ConfigurationManager.ConnectionStrings["ConnectionStr"].ToString();

        protected string signupMsg = "";

        int userid = 0;
        string userEmail = "";
        string userName = "";
        string heardFrom = "";
        string arabicNative = "";
        string mode = "";

        //protected string downloadHtml = "";
        protected string downloaLink = "http://www.myquranstudy.com/download/QuranStudyDownload.zip";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.Form["postmode"]))
                mode = Request.Form.Get("postmode").ToString();

            if (mode.Contains("signup"))
            {
                userEmail = Request.Form["uemail"].ToString();
                userName = Request.Form["uname"].ToString();
                heardFrom = Request.Form["heardFrom"].ToString();
                arabicNative = Request.Form["arabicNative"].ToString();

                if (IsInputValid() == false)
                {
                    signupMsg = "Download error!!<br>" + signupMsg;
                    signupMsg = "<div class=\"createFailed\">" + signupMsg + "</div>";

                    return;
                }

                signupMsg = "success";

                //if (CreateNewUser(userEmail, password, userName, heardFrom, arabicNative, ref userid, ref signupMsg))
                //{
                //    Session["UserID"] = userid;
                //    Session["UserName"] = userName;
                //    Session["UserEmail"] = userEmail;
                //    //Response.Redirect("/default.aspx");
                //    //SendEmail(userName, password, userEmail);
                //    // Application["aa"] = "";
                //    Task.Factory.StartNew(() => SendEmail(userName, password, userEmail));
                //}
                //else
                //{
                //    //loginMsg = "Login failed";
                //    Session["UserID"] = null;
                //    Session["UserName"] = null;
                //    Session["UserEmail"] = null;
                //}

                string msg = "";
                if (TrackDownload(ref msg) == false)
                     Response.Write(msg);
                 

            }
            
        }

        private bool IsInputValid()
        {
            if (userEmail.Trim().Length == 0 || userName.Trim().Length == 0)// || heardFrom.Trim().Length == 0)
            {
                signupMsg = "Please enter Your Name, Email!";
                return false;
            }

           
            if (FunctionLib.IsValidEmail(userEmail, ref signupMsg) == false)
            {
                return false;
            }

            return true;
        }

        private bool TrackDownload(ref string msg)
        {
            string ipUser = string.Empty;
            string ipISP = string.Empty;
            try
            {
                ipISP = Request.UserHostAddress.Trim();
                if (!string.IsNullOrEmpty(Request.ServerVariables.Get("HTTP_X_FORWARDED_FOR")))
                    ipUser = Request.ServerVariables.Get("HTTP_X_FORWARDED_FOR").Trim();
            }
            catch
            {

            }

            if (string.IsNullOrEmpty(ipUser))
            {
                ipUser = ipISP;// Request.ServerVariables.Get("REMOTE_ADDR");
            }

            if (ipUser.IndexOf(",") > 1)
                ipUser = ipUser.Split(",".ToCharArray())[0];

            //REMOTE_ADDR does not always provide the users IP but rather the ISPs' IP address so 
            //first test HTTP_X_FORWARDED_FOR as this one is the real user IP.

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd = new SqlCommand("[SP_AddDownloadData]", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@userEmail", SqlDbType.VarChar).Value = userEmail;
                    cmd.Parameters.Add("@UserName", SqlDbType.VarChar).Value = userName;
                    cmd.Parameters.Add("@HeardFrom", SqlDbType.VarChar).Value = heardFrom;
                    cmd.Parameters.Add("@IsArabicNative", SqlDbType.VarChar).Value = arabicNative;
                    cmd.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = ipUser;// Request.UserHostAddress.Trim();
                    if (ipUser != ipISP)
                        cmd.Parameters.Add("@IP_ISP", SqlDbType.VarChar).Value = ipISP;

                    SqlDataReader reader = null;
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        userid = Convert.ToInt32(reader["NewUserId"].ToString());
                    }

                   // cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                msg = "Please report this error. Error - " + ex.Message;
                return false;
            }

            return true;
        }
        private bool zzTrackDownload(ref string msg)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    // SqlConnection oConn = new SqlConnection(Global.gConnString);
                    conn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd = new SqlCommand("[SP_AddDownloadData]", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = "";
                    cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = "";
                    cmd.Parameters.Add("@ipaddress", SqlDbType.VarChar).Value = Request.UserHostAddress.Trim();
                    //SqlDataReader reader = null;
                     cmd.ExecuteNonQuery(); 
                }
            }
            catch (Exception ex)
            {
                msg = "Download failed. Error - " + ex.Message;
                return false;
            }

            return true;
        }
    }
}