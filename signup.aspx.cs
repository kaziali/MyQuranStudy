using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
namespace QuranStudy
{
    public partial class signup : System.Web.UI.Page
    {
        protected string connStr = ConfigurationManager.ConnectionStrings["ConnectionStr"].ToString();

        protected string signupMsg = "";

            int userid = 0;
            string userEmail = "";
            string password = "";
            string passwordConf = "";
            string userName ="";
            string heardFrom="";
            string arabicNative = "";
            string mode = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.Form["postmode"]))
                mode = Request.Form.Get("postmode").ToString();

            if (mode.Contains("signup"))
            {
                userEmail = Request.Form["uemail"].ToString();
                password = Request.Form["pword"].ToString();
                passwordConf = Request.Form["pwordConf"].ToString();
                userName = Request.Form["uname"].ToString();
                heardFrom = Request.Form["heardFrom"].ToString();
                arabicNative = Request.Form["arabicNative"].ToString();

                if (IsInputValid(passwordConf) == false)
                {
                    signupMsg = "Signup error!!<br>" + signupMsg;
                    signupMsg = "<div class=\"createFailed\">" + signupMsg  + "</div>";

                    return;
                }
                 

                if (CreateNewUser(userEmail, password, userName, heardFrom, arabicNative, ref userid, ref signupMsg))
                {
                    Session["UserID"] = userid;
                    Session["UserName"] = userName;
                    Session["UserEmail"] = userEmail;
                    //Response.Redirect("/default.aspx");
                    //SendEmail(userName, password, userEmail);
                   // Application["aa"] = "";
                    Task.Factory.StartNew(() => SendEmail(userName, password, userEmail));
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

        private void SendEmail(string sName, string password, string toEmail)
        {
           
            try
            {               
                string msg = "Assalamu alaikum " + sName + ":";
                msg += @" 

Thank you for signing up with 
MyQuranStudy.com - a tool to help you learn the word-for-word meaning of Quran. 

Please inform your relatives and friends about our website - this is our gentle request.

Your account has been created and activated.
You can login now from http://www.MyQuranStudy.com ";
                msg += "\n\nHere is your login info - \n";
                msg += "\nLogin email: " + toEmail;
                msg += "\nPassword: " + password;

                msg += "\n\n[Heard From - " + heardFrom + "]\n";


                msg += @"

May Allah grant you authentic Islamic knowledge, and make you successful in this life and the hereafter. Ameen.

If you have any questions, please contact us via http://www.MyQuranStudy.com/ 

Again, our gentle request - Please share our website address with others. May Allah reward you!!


Thank you,
Automatic email 
from MyQuranStudy.com
(on behalf of Kazi Nasrat Ali,
Washington state, USA)
   
 
";

                //[If you have not initiated this sign up with MyQuranStudy.com, please ignore this email or notify us and we will delete your account, insha'Allah.]

                    //and DO NOT reply to this email.
                System.Net.Mail.SmtpClient smtpClient = new System.Net.Mail.SmtpClient();
                System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
                message.From = new MailAddress("myquran365@gmail.com", "MyQuranStudy.com");
                //message.From = new MailAddress(fromEmail, fromEmail);

                message.Subject = "Welcome to MyQuranStudy.com";
                message.Body = msg;
                message.To.Add(toEmail);
                message.Bcc.Add("kaziali@hotmail.com"); 
                message.ReplyToList.Add("kaziali@hotmail.com"); 


                var client = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential("myquran365@gmail.com", "xxxx"),
                    EnableSsl = true
                };
                client.Send(message);

                 
            }
            catch 
            {
            }

        }


        private bool IsInputValid(string strpass)
        {
            if (userEmail.Trim().Length == 0 || userName.Trim().Length == 0 || password.Trim().Length == 0)// || heardFrom.Trim().Length == 0)
            {
                signupMsg = "Please enter Your Name, Email, Password and Heard-From!";
                return false;
            }

            if (password.Trim().Length < 4)
            {
                signupMsg = "Please enter a Password with at least 4 letters/numbers!";
                return false;
            }

            if (password != passwordConf)
            {
                signupMsg = "Conform Password by retyping it!";
                return false;
            }
            if (FunctionLib.IsValidEmail(userEmail, ref signupMsg) == false)
            {
                return false;
            }
            if (FunctionLib.IsValidPassword(password, ref signupMsg) == false)
            {
                return false;
            }

            return true;
        }

        private string GetLocation(string ipAddrs)
        {
            string location = ""; //US;United States;Washington;Redmond
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("svc-uri-here");
                request.Method = "GET";
                request.Timeout = 5000;

                // WebRequest request = WebRequest.Create(url);
                //request.Method = "GET";
                WebResponse response = request.GetResponse();
                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream);
                location = reader.ReadToEnd();
                reader.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                // Response.Write("<br />" + ipAdd + ":" + ex.Message + " - " + url + " down ???");
                return "";
            }

            return location;

        }

        private bool CreateNewUser(string userEmail, string password, string userName, string heardFrom, string arabicNative, ref int userid, ref string loginMsg)
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


           // string[] locArry=GetLocation(ipUser).Split(';'); //US;United States;Washington;Redmond

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    // SqlConnection oConn = new SqlConnection(Global.gConnString);
                    conn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd = new SqlCommand("[SP_AddQuranUser]", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@userEmail", SqlDbType.VarChar).Value = userEmail;
                    cmd.Parameters.Add("@UserPassword", SqlDbType.VarChar).Value = password;
                    cmd.Parameters.Add("@UserName", SqlDbType.VarChar).Value = userName;
                    cmd.Parameters.Add("@HeardFrom", SqlDbType.VarChar).Value = heardFrom;
                    cmd.Parameters.Add("@IsArabicNative", SqlDbType.VarChar).Value = arabicNative;
                    cmd.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = ipUser; 
                    if(ipUser != ipISP)
                        cmd.Parameters.Add("@IP_ISP", SqlDbType.VarChar).Value = ipISP;

                    //if (locArry.Length > 3)
                    //{
                    //    cmd.Parameters.Add("@IPCountry", SqlDbType.VarChar).Value = locArry[1]; 
                    //    cmd.Parameters.Add("@IPState", SqlDbType.VarChar).Value = locArry[2]; 
                    //    cmd.Parameters.Add("@IPCity", SqlDbType.VarChar).Value = locArry[3]; 
                    //}
                    SqlDataReader reader = null;
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        userid = Convert.ToInt32(reader["NewUserId"].ToString());
                    }
                   // signupMsg = "Log in failed.";
                    //return false;
                }

                if (userid == 0)
                {
                    signupMsg = "<div class=\"createFailed\">Failed to create user. User exists with this email address.</div>";
                    return false;
                }

                if (userid > 0)
                {
                    signupMsg = "<div class=\"createSuccess\">User created successfully.</div>";
                    return true;
                }
                else
                {
                    signupMsg = "<div class=\"createFailed\">Failed to create user. Please try again!</div>";
                    return false;
                }

            }
            catch (Exception ex)
            {
                signupMsg = "<div class=\"createFailed\">Failed. Error - " + ex.Message +"</div>";
                return false;
            }


        }

    }
}