using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace QuranStudy
{
    public partial class ChangePass : System.Web.UI.Page
    {
        protected int UserID = -1;//-1=not logged in, 1=guest, 2=kazi
        protected string UserName = "<a href=\"/login.aspx\">sign in</a>";

        protected string connStr = ConfigurationManager.ConnectionStrings["ConnectionStr"].ToString();

        protected string actionMsg = "";
         
        protected string actionMsgCss = "createFailed";
        protected string UserEmail = "";
        string password = "";
        string passwordConf = ""; 
        string mode = "";


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] != null)
                UserName = Session["UserName"].ToString();
            if (Session["UserID"] != null)
                UserID = Convert.ToInt32(Session["UserID"].ToString());
            if (Session["UserEmail"] != null)
                UserEmail = Session["UserEmail"].ToString();

            if (!string.IsNullOrEmpty(Request.Form["mode"]))
                mode = Request.Form.Get("mode").ToString();

            if (mode.Contains("changepass"))
            {
                password = Request.Form["pword"].ToString();
                passwordConf = Request.Form["pwordConf"].ToString();
                if (password != passwordConf)
                {
                    actionMsg = "<div class=\"" + actionMsgCss + "\">" + actionMsg + " Conform Password by retyping it!.</div>";
                    return;
                }
                
                if (FunctionLib.IsValidPassword(password, ref actionMsg) == false)
                {
                    actionMsg = "<div class=\"" + actionMsgCss + "\">" + actionMsg + " Password update Failed.</div>";
                    return;
                }

                if(ChangePassword(UserID, password)==false)
                    actionMsg = "<div class=\"" + actionMsgCss + "\">" + actionMsg + " Password update Failed.</div>";
                else
                    actionMsg = "<div class=\"" + actionMsgCss + "\"> Password updated successfully.</div>";
            }
        }

        private bool ChangePassword(int userID, string password)//,   ref string loginMsg)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd = new SqlCommand("[SP_UpdateUserPassword]", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@userID", SqlDbType.Int).Value = userID;
                    cmd.Parameters.Add("@UserPassword", SqlDbType.VarChar).Value = password;
                     
                    cmd.ExecuteNonQuery();
                    actionMsgCss = "createSuccess";
                    return true;
                } 

            }
            catch (Exception ex)
            {
                Response.Write("<div class=\"createFailed\">Failed. Error - " + ex.Message + "</div>");
                return false;
            }


        }

    }
}