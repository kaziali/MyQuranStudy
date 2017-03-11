using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
namespace QuranStudy
{
    public partial class forgotpass : System.Web.UI.Page
    {
        //protected int iResultError=0;
        protected string sResultMsg = "";
        protected string uemail = "";
        protected string postmode = "";
        private void Page_Load(object sender, System.EventArgs e)
        {
            try
            {
                postmode = Request.Form.Get("postmode").ToString().Trim();
                uemail = Request.Form.Get("uemail").ToString().Trim();
            }
            catch { }

            if (postmode == "submitted")
            {
                if (FunctionLib.IsValidEmail(uemail, ref sResultMsg) == false)
                {
                    sResultMsg = "Error!!<br>" + sResultMsg;
                    return;
                }
                else
                    SendPasswordEmail();
            }

        }

        private void SendPasswordEmail()
        {
            try
            {
                string connStr = ConfigurationManager.ConnectionStrings["ConnectionStr"].ToString();
                string password = "";
                string unameFromDB = "";//reset and get from DB
                using (SqlConnection oConn = new SqlConnection(connStr))
                {
                    SqlCommand oCmd = new SqlCommand();
                    oConn.Open();
                    oCmd = new SqlCommand("[SP_ForgotPassword]", oConn);
                    oCmd.CommandType = CommandType.StoredProcedure;
                    oCmd.Parameters.Add("@useremail", SqlDbType.VarChar).Value = uemail;
                    SqlDataReader oReader = null;
                    oReader = oCmd.ExecuteReader();
                    //int errNum=-1;

                    while (oReader.Read())
                    {
                        //isActive = Convert.ToBoolean(oReader["isActive"].ToString());
                        password = oReader["UserPassword"].ToString();
                        //actCode = oReader["ActivationCode"].ToString();
                        unameFromDB = oReader["username"].ToString();
                    }
                }
                //if (oConn.State == ConnectionState.Open) oConn.Close();
                //if(uname <>"") 
                if(password.Trim() != "")
                SendEmail(unameFromDB, password, uemail);
                else
                    sResultMsg = "No user is found with Email address: " + uemail;

            }
            catch//(Exception ex)
            {
                //sResultMsg=ex.Message + ex.StackTrace + ex.Source ;
                sResultMsg = "Signup error!! Some unknown technical error has occurred. Please open a new browser window and try again.";
            }

        }

        private void SendEmail(string sName, string password, string toEmail)
        {
            //return;
            try
            {
                
                string msg = "Assalamu alaikum " + sName + ":";
                msg += @" 
We have received a password re-send request to login to www.MyQuranStudy.com

Here is your login email and password.

";
msg += "\n\nLogin email: " + toEmail;
msg += "\nPassword: " + password;
msg += @"


If you have not initiated this sign up with MyQuranStudy.com, please ignore this email or notify us and we will delete your account.

If you have any questions, please contact us via http://www.MyQuranStudy.com/ .

Thank you,
Automatic email from MyQuranStudy.com
(on behalf of Kazi Nasrat Ali
Issaquah WA, USA)
   
 
";

                System.Net.Mail.SmtpClient smtpClient = new System.Net.Mail.SmtpClient();
                System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
                message.From = new MailAddress("myquran365@gmail.com", "MyQuranStudy.com");
                //message.From = new MailAddress(fromEmail, fromEmail);

                message.Subject = "MyQuranStudy.com - Password recovery";
                message.Body = msg;
                message.To.Add(toEmail);
                message.Bcc.Add("kaziali@hotmail.com");

                var client = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential("myquran365@gmail.com", "xxxx"),
                    EnableSsl = true
                };
                client.Send(message); 

            }
            catch(Exception ex)
            {
                Response.Write("<font color=#dddddd>TECHNICAL ERROR: Could not send email to: " + toEmail + "<br> Please contact from http://www.MyQuranStudy.com/  <br>" + ex.Message + "</font><hr>");
                sResultMsg = "TECHNICAL ERROR: Could not send email to: " + toEmail + "<br> Please contact from http://www.MyQuranStudy.com/ <br> <hr>";
            }

        }

  
 
    }
}