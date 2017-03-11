using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Facebook;

namespace QuranStudy
{
    public partial class ActionHandler : System.Web.UI.Page
    {
        protected int lemmaid = 0;
        protected int uid = 0;
        protected int editorid = 0;
        protected int stateid = -1;//0=known,1=unknown,2=hilite,3=all-words-in-ayah,4=hilite-ayah
        protected int ayahid = 0;
        protected int surahid = 0;
        protected string reqType = "";//getLex
        protected string connStr = ConfigurationManager.ConnectionStrings["ConnectionStr"].ToString();
        protected int IsNewFBUser = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
           // Response.Write("test001aa"); return;
            if (Request["reqType"] != null)
            {
                reqType = Request["reqType"].ToString();
            }
            else
            {
                Response.Write("\nError- reqType required.");
                return;
            }

            //fblogin----
            if (reqType == "fblogin")
            {
                var accessToken = Request["accessToken"];
                Session["AccessToken"] = accessToken;

                //var accessToken = Session["AccessToken"].ToString();
                var client = new FacebookClient(accessToken);
                //dynamic result = client.Get("me", new { fields = "name,id,email" });
                //var parameters = new Dictionary<string, object>();
               // parameters["scope"] = "email";


                dynamic result = client.Get("me");
                string name = result.name;
                //string ee = result.email;
                string useremail_fb = result.id + "@_fb_" ; ;//later on just append the actual email address, just in SQL SP do a like search for '123456789@_fb_'
                // Response.Write(result);

                int userID = CreateOrGet_FacebookUserID(useremail_fb, name);
                if (userID == -1) Response.Redirect("/"); ;
                Session["UserID"] = userID;
                Session["UserName"] = name ;
                Session["UserEmail"] = useremail_fb ;
                Session["FBUserSignup"] = IsNewFBUser.ToString();
                if(IsNewFBUser == 1)
                    Response.Redirect("/FbSignup.aspx");
                else
                    Response.Redirect("/study.aspx"); 

            }
            if (reqType == "fbsignupques")
            {
                 int userID = Convert.ToInt32(Session["UserID"].ToString());
                 string HeardFrom = Request.Form["HeardFrom"].ToString();
                 using (SqlConnection conn = new SqlConnection(connStr))
                 {
                     SqlCommand cmd = new SqlCommand();
                     conn.Open();
                     cmd = new SqlCommand("[SP_UpdateAnswer_FBUser]", conn);
                     cmd.CommandType = CommandType.StoredProcedure;
                     cmd.Parameters.Add("@userID", SqlDbType.VarChar).Value = userID;
                     cmd.Parameters.Add("@heardFrom", SqlDbType.VarChar).Value = HeardFrom;  
                     cmd.ExecuteNonQuery(); //try-catch??
                 }

                Response.Redirect("/study.aspx");

            }

            if (reqType == "getTafsirLink")
            {
                surahid = Convert.ToInt32(Request.QueryString["surahID"]);
                ayahid = Convert.ToInt32(Request.QueryString["ayahID"]);
                Response.Write(GetTafsirLink(surahid, ayahid));
                return;
            }

            if (reqType == "clickReLogin")
            {
                string uname = Request.Params["uname"].ToString().Trim();
                string pword = Request.Params["pword"].ToString().Trim();
                Response.Write(UserReLogin(uname, pword));
                return;
            }
            if (reqType == "updateStatusMsaVocab" || reqType == "updatePriorityMsaVocab" || reqType == "updateClearMsaVocab")
            {
                ProcessMsaVocabAction(reqType);
                return;
            }
          
            if (Session["EditorID"] != null)
            {
                editorid = Convert.ToInt32(Session["EditorID"].ToString());
                uid = 0;

            }
            else
            {
                if (Session["UserID"] != null)
                {
                    uid = Convert.ToInt32(Session["UserID"].ToString());

                }
                else
                {
                    if (reqType != "getUserAyahNote")
                        Response.Write("SESSION_EXPIRED");
                        //Response.Write("\n1010 Please log in to track progress, insha'Allah!\n\nFor testing purpose use -\n\nLogin: guest@myQuranStudy.com\nPassword: pass");
                    else
                        //Response.Write(GetTafsirLink(surahid, ayahid));
                        Response.Write("SUCCESS|");
                    return;
                }
            }
            
            Response.ContentType = "text/plain";
            //mark one word only
            if (reqType == "markLemma")
            {
                lemmaid = Convert.ToInt32(Request.QueryString["lemmaID"]);
                stateid = Convert.ToInt32(Request.QueryString["stateID"]);
                Response.Write(AddWord(lemmaid, uid, stateid));
                return;
            }

            //mark all words in one ayah or hilite ayah or box ayah
            if (reqType == "markAyahLemmas")
            {
                surahid = Convert.ToInt32(Request.QueryString["surahID"]);
                ayahid = Convert.ToInt32(Request.QueryString["ayahID"]);
                stateid = Convert.ToInt32(Request.QueryString["stateID"]);
                Response.Write(AddAyahWords(surahid, ayahid, uid, stateid));
                return;
            }
            if (reqType == "getUserAyahNote")
            {
                surahid = Convert.ToInt32(Request.QueryString["surahID"]);
                ayahid = Convert.ToInt32(Request.QueryString["ayahID"]);
                Response.Write(GetUserAyahNote(uid, surahid, ayahid));
                return;
            }

            if (reqType == "addUserLemmaNote")
            {
                lemmaid = Convert.ToInt32(Request.QueryString["LemmaID"]);
                string note = Request.Params["LemmaNote"].Trim();
                Response.Write(AddUserLemmaNote(uid, lemmaid, note));
                return;
            }

            if (reqType == "addUserAyahNote")
            {
                surahid = Convert.ToInt32(Request.QueryString["surahID"]);
                ayahid = Convert.ToInt32(Request.QueryString["ayahID"]);
                string note = Request.Params["ayahNote"].Trim();
                Response.Write(AddUserAyahNote(uid, surahid, ayahid, note));
                return;
            }
            if (reqType == "saveUserGeneralNote")
            {

               // Session["UserID"] = null;
                string note = Request.Params["generalNote"].ToString().Trim();
                Response.Write(AddUserGeneralNote(uid, note));
                return;
            }
            
            if (reqType == "EditorUpdateTranslationBn")
            {
                int wid = Convert.ToInt32(Request.QueryString["wid"].Trim());
                string meaningEn = Request.QueryString["meaningEn"].Trim();
                string meaningLoc = Request.QueryString["meaningLoc"].Trim();
                Response.Write(UpdateTranslationEditor(wid, meaningEn, meaningLoc,"bn"));
                return;
            }

            if (reqType == "EditorUpdateTranslationEn")
            {
                int wid = Convert.ToInt32(Request.QueryString["wid"].Trim());
                string meaningEn = Request.QueryString["meaningEn"].Trim();
                //string meaningLoc = Request.QueryString["meaningLoc"].Trim();
                Response.Write(UpdateTranslationEditor(wid, meaningEn, meaningEn, "en"));
                return;
            }
            if (reqType == "EditorUpdateTranslationUr")
            {
                int wid = Convert.ToInt32(Request.QueryString["wid"].Trim());
                string meaningEn = Request.QueryString["meaningEn"].Trim();
                string meaningLoc = Request.QueryString["meaningLoc"].Trim();
                Response.Write(UpdateTranslationEditor(wid, meaningEn, meaningLoc,"ur"));
                return;
            }
        }

        private void ProcessMsaVocabAction(string reqType)
        {
            string data = "";
            string val = "";
            string id = "";

            switch (reqType)
            {
                case "updateStatusMsaVocab":
                case "updatePriorityMsaVocab":
                case "updateClearMsaVocab":
                    data = Request.Params["dat"].ToString().Trim(); // action|wordId =>known|12
                    val = data.Split("|".ToCharArray())[0];
                    id = data.Split("|".ToCharArray())[1];
                    using (SqlConnection conn = new SqlConnection(connStr))
                    {
                        SqlCommand cmd = new SqlCommand();
                        conn.Open();
                        cmd = new SqlCommand("[SP_MSA_UpdateWordStatus]", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id", SqlDbType.Int).Value = Convert.ToInt32( id);
                        switch (reqType)
                        {
                            case "updateStatusMsaVocab":
                                cmd.Parameters.Add("@Status", SqlDbType.VarChar).Value = val;
                                cmd.Parameters.Add("@Priority", SqlDbType.VarChar).Value = "";
                                break;
                            case "updatePriorityMsaVocab":
                                cmd.Parameters.Add("@Status", SqlDbType.VarChar).Value = "";
                                cmd.Parameters.Add("@Priority", SqlDbType.VarChar).Value = val;
                                break;
                            case "updateClearMsaVocab":
                                cmd.Parameters.Add("@Status", SqlDbType.VarChar).Value ="";
                                cmd.Parameters.Add("@Priority", SqlDbType.VarChar).Value = "";
                                break;
                        }
                        
                        cmd.ExecuteNonQuery(); //try-catch??
                        Response.Write("SUCCESS");
                    }
                    break;
  
                default:
                    Response.Write("FAILURE");

                    break;
            }
            //ProcessMsaVocabAction(reqType);
            //string uname = Request.Params["uname"].ToString().Trim();
            //string pword = Request.Params["pword"].ToString().Trim();
            //Response.Write(UserReLogin(uname, pword));

        }

        private int CreateOrGet_FacebookUserID(string useremail_fb, string name)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {

                    SqlCommand cmd = new SqlCommand();
                    conn.Open();
                    cmd = new SqlCommand("[SP_AddorGetFacebookUser]", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@UserFbEmail", SqlDbType.VarChar).Value = useremail_fb;
                    cmd.Parameters.Add("@UserName", SqlDbType.VarChar).Value = name;
                    cmd.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = Request.UserHostAddress.Trim();

                    SqlDataReader reader = null;
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        IsNewFBUser = Convert.ToInt32(reader["isNewUser"].ToString()); 
                        return Convert.ToInt32(reader["UserID"].ToString()); 
                    }
                    return -1;
                }
            }
            catch 
            {
                return -1;
            }


        }
        private string GetTafsirLink(int surahid, int ayahid)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {

                    SqlCommand cmd = new SqlCommand();
                    conn.Open();
                    cmd = new SqlCommand("[SP_GetTafsirLink]", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@surahid", SqlDbType.Int).Value = surahid;
                    cmd.Parameters.Add("@AyahID", SqlDbType.Int).Value = ayahid;
                    SqlDataReader reader = null;
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        return "SUCCESS" + "|" + reader["TafsirFilename"].ToString() +"|" + reader["TafsirLink"].ToString();
                    }
                    return "SUCCESS|";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }


        }
        private string GetUserAyahNote(int userID, int surahid, int ayahid)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {

                    SqlCommand cmd = new SqlCommand();
                    conn.Open();
                    cmd = new SqlCommand("[SP_GetUserNoteForAyah]", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;
                    cmd.Parameters.Add("@surahid", SqlDbType.Int).Value = surahid;
                    cmd.Parameters.Add("@AyahID", SqlDbType.Int).Value = ayahid;
                    SqlDataReader reader = null;
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        return "SUCCESS" + "|" + reader["AyahNote"].ToString().Replace("|", ":") + "|" + reader["DateUpdated"].ToString();// +"|" + reader["TafsirLink"].ToString();
                    }
                    return "SUCCESS|";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }


        }
        private string AddUserAyahNote(int userID, int surahid, int ayahid, string note)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {

                    SqlCommand cmd = new SqlCommand();
                    conn.Open();
                    cmd = new SqlCommand("[SP_AddUserNoteForAyah]", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;
                    cmd.Parameters.Add("@surahid", SqlDbType.Int).Value = surahid;
                    cmd.Parameters.Add("@AyahID", SqlDbType.Int).Value = ayahid;
                    cmd.Parameters.Add("@AyahNote", SqlDbType.NVarChar).Value = note;
                    SqlDataReader reader = null;
                    reader = cmd.ExecuteReader();
                    //while (reader.Read())
                    //{
                    //    return "SUCCESS" + "|" + reader["AyahNote"].ToString() + "|" + reader["DateUpdated"].ToString();
                    //}
                    return "SUCCESS|";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }


        }
        private string AddUserGeneralNote(int userID, string note)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {

                    SqlCommand cmd = new SqlCommand();
                    conn.Open();
                    cmd = new SqlCommand("[SP_AddUserGeneralNote]", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;
                    cmd.Parameters.Add("@Note", SqlDbType.NVarChar).Value = note;
                    SqlDataReader reader = null;
                    reader = cmd.ExecuteReader();
                    //while (reader.Read())
                    //{
                    //    return "SUCCESS" + "|" + reader["AyahNote"].ToString() + "|" + reader["DateUpdated"].ToString();
                    //}
                    return "SUCCESS|";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }


        }

        private string UserReLogin(string userEmail, string password)
        {
            int userid = -1;
            string name = "";
            string loginMsg = "";//QuranStudy
            Login lg = new Login();
            if (lg.VerifyLogin(userEmail, password, ref userid, ref name, ref loginMsg))
            {
                Session["UserID"] = userid;
                Session["UserName"] = name;
                Session["UserEmail"] = userEmail;
                return "SUCCESS|";
            }
            else
            {
                //loginMsg = "Login failed";
                Session["UserID"] = null;
                Session["UserName"] = null;
                Session["UserEmail"] = null;
                return "Login failed for email:" + userEmail;
            }
        }
        private string AddUserLemmaNote(int userID, int lemmaid, string note)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {

                    SqlCommand cmd = new SqlCommand();
                    conn.Open();
                    cmd = new SqlCommand("[SP_AddUserNoteForLemma]", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;
                    cmd.Parameters.Add("@LemmaID", SqlDbType.Int).Value = lemmaid;
                    cmd.Parameters.Add("@LemmaNote", SqlDbType.NVarChar).Value = note;
                    SqlDataReader reader = null;
                    reader = cmd.ExecuteReader();
           
                    return "SUCCESS|";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }


        }

        private string AddWord(int lemmaID, int userID, int stateID)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    // SqlConnection oConn = new SqlConnection(Global.gConnString);
                    SqlCommand cmd = new SqlCommand();
                    conn.Open();
                    cmd = new SqlCommand("[SP_AddUserVocab]", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;
                    cmd.Parameters.Add("@LemmaID", SqlDbType.Int).Value = lemmaID;
                    cmd.Parameters.Add("@StateID", SqlDbType.Int).Value = stateID;
                    SqlDataReader reader = null;
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        return "SUCCESS" + "|" + reader["agKnown"].ToString() + "|" + reader["uKnown"].ToString() + "|" + reader["uUnderlined"].ToString() + "|" + reader["uHilited"].ToString() + "|" + reader["uBoxed"].ToString();
                    }
                    return "User progress report not found!";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }


        }

        private string AddAyahWords(int surahid, int ayahid, int userID, int stateID)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    // SqlConnection oConn = new SqlConnection(Global.gConnString);
                    SqlCommand cmd = new SqlCommand();
                    conn.Open();
                    cmd = new SqlCommand("[SP_AddUserAyah]", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;
                    cmd.Parameters.Add("@surahid", SqlDbType.Int).Value = surahid;
                    cmd.Parameters.Add("@AyahID", SqlDbType.Int).Value = ayahid;
                    cmd.Parameters.Add("@StateID", SqlDbType.Int).Value = stateID;
                    SqlDataReader reader = null;
                    reader = cmd.ExecuteReader();
                    reader.Read();
                    if (stateID == 1) // known AddAyahWords added  
                        return "SUCCESS" + "|" + reader["agKnown"].ToString() + "|" + reader["uKnown"].ToString() + "|" + reader["uUnderlined"].ToString() + "|" + reader["uHilited"].ToString() + "|" + reader["uBoxed"].ToString();
                    else
                    {
                        if (Convert.ToInt32(reader["success"].ToString()) > -1)
                            return "SUCCESS|" ;//+ "|" + reader["agKnown"].ToString() + "|" + reader["uKnown"].ToString() + "|" + reader["uUnderlined"].ToString() + "|" + reader["uHilited"].ToString() + "|" + reader["uBoxed"].ToString();
                        else
                            return "Failed!";
                    }
                   
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private string UpdateTranslationEditor(int wid, string meaningEn, string meaningLoc, string lang)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {

                    SqlCommand cmd = new SqlCommand();
                    conn.Open();
                    cmd = new SqlCommand("[EditorSP_UpdateTranslation]", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@editorId", SqlDbType.Int).Value = editorid;
                    cmd.Parameters.Add("@wordId", SqlDbType.Int).Value = wid;
                    cmd.Parameters.Add("@meaningEn", SqlDbType.VarChar).Value = meaningEn;
                    cmd.Parameters.Add("@meaningLoc", SqlDbType.NVarChar).Value = meaningLoc;
                    cmd.Parameters.Add("@LangCode", SqlDbType.NVarChar).Value = lang;
                    cmd.ExecuteNonQuery();
                    //SqlDataReader reader = null;
                    //reader = cmd.ExecuteReader();
                    //while (reader.Read())
                    //{
                    //    return "SUCCESSzz" + "|" + meaningBn + reader["result"].ToString();// +"|" + reader["DateUpdated"].ToString();
                    //}
                    return "SUCCESS|";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }


        }

        private string AddLineBreak(int wID, int typeID)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {

                    SqlCommand cmd = new SqlCommand();
                    conn.Open();
                    cmd = new SqlCommand("[SP_AddLineBreakOnWord]", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@wID", SqlDbType.Int).Value = wID;
                    cmd.Parameters.Add("@typeID", SqlDbType.Int).Value = typeID;
                    
                    SqlDataReader reader = null;
                    reader = cmd.ExecuteReader();
                    //while (reader.Read())
                    //{
                    //    return "SUCCESS" + "|" + reader["AyahNote"].ToString() + "|" + reader["DateUpdated"].ToString();
                    //}
                    return "SUCCESS|";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }


        }
 
    }
}