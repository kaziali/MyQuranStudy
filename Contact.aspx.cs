using System;
using System.Net;
using System.Net.Mail;
namespace QuranStudy
{
    public partial class Contact : System.Web.UI.Page
    {

        protected string failureMsg = "";
        protected string successMsg = "";
        string userEmail = "";
        string userName = "";
        string subject = "";
        string body = "";
        string mode = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.Form["postmode"]))
                mode = Request.Form.Get("postmode").ToString();

            if (mode.Contains("sending"))
            {
                userEmail = Request.Form["uemail"].ToString();
                userName = Request.Form["uname"].ToString();
                subject = Request.Form["subject"].ToString();
                body = Request.Form["body"].ToString();
                if (userEmail.Trim().Length == 0 || userName.Trim().Length == 0 || body.Trim().Length == 0)
                {
                    failureMsg = "Please enter Your Name, Email and message!";
                    return  ;
                }
                if (FunctionLib.IsValidEmail(userEmail, ref failureMsg) == false)
                {
                    return  ;
                }

                SendEmail(userName, userEmail, subject, body);

                if (!string.IsNullOrEmpty(failureMsg)) successMsg = "";
            }

        }

        private void SendEmail(string name, string email, string sub, string body)
        {
            try
            {
                System.Net.Mail.SmtpClient smtpClient = new System.Net.Mail.SmtpClient();
                System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
                message.From = new MailAddress("myquran365@gmail.com", "MyQuranStudy Contact");
                //message.From = new MailAddress(fromEmail, fromEmail);

                message.Subject = subject;
                message.Body = "From: " + name + " | " +  email + " [Sent from IP address:" + Request.UserHostAddress.Trim() + " ]\n";
                message.Body += "\nSubject: " + sub;
                message.Body += "\nMessage: " + body;
                //message.To.Add(new MailAddress(email, name));
                message.To.Add("kaziali@hotmail.com");
                message.To.Add(email);
                message.ReplyToList.Add(email);
                //message.ReplyTo.

                var client = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential("myquran365@gmail.com", "xxxx"),
                    EnableSsl = true
                };

                client.Send(message);

                successMsg = "Message sent successfully, Alhamdulillah!";

            }
            catch(Exception ex)
            {
                failureMsg += "TECHNICAL ERROR: Could not send message: Please email directly to: myquran365@gmail.com  ";
            }

        }

    }

}