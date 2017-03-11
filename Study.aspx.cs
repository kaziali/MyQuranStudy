using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using QuranStudy.Common;
namespace QuranStudy
{
    public partial class Study : System.Web.UI.Page
    {
        protected int UserID = -1;//-1=not logged in, 1=guest, 2=kazi
        protected string UserName = "<a href=\"/login.aspx\">sign in</a>";
        protected string nextVersesUrl = "";

        protected string sAllEngTrans = "";
        protected string jsArrayTrans = "";
        protected string outhtml;
        protected string outArab;
        protected string outEng;
        protected int surah = 1;
        protected int ayahFrom = 1;
        protected int numAyah = 7;
        //protected int ayahTo = 1;

        protected string jsArrayIDs = "";
        protected string jsArrayNames = "";
        protected string jsArrayAyat = "";

        protected int AggrtKnown = 0;
        protected int UniqueKnown = 0;
        protected int UniqueUnderlined = 0;
        protected int UniqueHilited = 0;
        protected int UniqueBoxed = 0;
        //protected string WordHoverDiagHeight = "40"; 

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["UserName"] != null)
                UserName = Session["UserName"].ToString();
            if (Session["UserID"] != null)
                UserID = Convert.ToInt32(Session["UserID"].ToString());

            surah = Convert.ToInt32(Request.Params["surah"]);
            ayahFrom = Convert.ToInt32(Request.Params["AyahFrom"]);
            numAyah = Convert.ToInt32(Request.Params["NumVerses"]);

            string loc = Request.Params[0].ToString();
            if(loc.Contains(":"))
            {
                try
                {

                    string[] tmp = loc.Split(":".ToCharArray());
                    surah = Convert.ToInt32(tmp[0]);
                    ayahFrom = Convert.ToInt32(tmp[1]);
                    numAyah = 5;
                }
                catch { }
            } 

            if (surah < 1) surah = 1;
            if (ayahFrom < 1) ayahFrom = 1;
            if (numAyah < 1) numAyah = 7;
            //if (ayahTo < 1) ayahTo = 3;
            VersePuller vp=new VersePuller();
            vp.LoadSurahNames(ref jsArrayIDs, ref jsArrayNames, ref jsArrayAyat);
            vp.LoadWords(surah, ayahFrom, numAyah, ref outhtml, ref nextVersesUrl, UserID, "en");
            vp.LoadTranslation(surah, ayahFrom, numAyah, ref   sAllEngTrans, ref   jsArrayTrans, "en");
            LoadUserProgress();

            //http://localhost:62958/default.aspx?Surah=1&AyahFrom=8&NumWords=5
            //nextVersesUrl = "/default.aspx?Surah=" + surah.ToString() + "&AyahFrom=" + (ayahFrom + 7).ToString() + "&NumVerses=7";
        }

        /// <summary>
        /// load user's known word count, % achieved etc.
        /// </summary>
        protected void LoadUserProgress()
        {
           // Response.Write(UserID.ToString());
            string connStr = ConfigurationManager.ConnectionStrings["ConnectionStr"].ToString();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand();
                conn.Open();
                cmd = new SqlCommand("[SP_GetUserVocabCount]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@userID", SqlDbType.Int).Value = UserID;

                SqlDataReader reader = null;
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AggrtKnown = Convert.ToInt32(reader["agKnown"].ToString());

                    UniqueKnown = Convert.ToInt32(reader["uKnown"].ToString());
                    UniqueUnderlined = Convert.ToInt32(reader["uUnderlined"].ToString());
                    UniqueHilited = Convert.ToInt32(reader["uHilited"].ToString());
                    UniqueBoxed = Convert.ToInt32(reader["uBoxed"].ToString());
                     
                }
                if (conn.State == ConnectionState.Open) conn.Close();

            }
        }
        protected void zzLoadSurahNames()
        {
            string connStr = ConfigurationManager.ConnectionStrings["ConnectionStr"].ToString();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                // SqlConnection oConn = new SqlConnection(Global.gConnString);
                SqlCommand cmd = new SqlCommand();
                conn.Open();
                cmd = new SqlCommand("[SP_GetSurahNames]", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = null;
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    jsArrayIDs += "," + reader["SurahID"].ToString();
                    jsArrayNames += ",\"" + reader["SurahID"].ToString() + " - " + reader["TName"].ToString() + "\"";
                    jsArrayAyat += "," + reader["AyatTotal"].ToString();
                }
                if (conn.State == ConnectionState.Open) conn.Close();
                jsArrayIDs = jsArrayIDs.Substring(1);
                jsArrayNames = jsArrayNames.Substring(1);
                jsArrayAyat = jsArrayAyat.Substring(1);
            }
        }
        protected void zzLoadVerses(int surah, int ayahFrom, int numAyah)
        {
            string connStr = ConfigurationManager.ConnectionStrings["ConnectionStr"].ToString();
            string ayahId =   ayahFrom.ToString();// "";
            string spaceNeeded = " ";
            string nextSura = "";
            string nextAyah = "";
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand();
                conn.Open();
                cmd = new SqlCommand("[SP_GetVersesWithUserVocab]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@userID", SqlDbType.Int).Value = UserID;
                cmd.Parameters.Add("@SurahID", SqlDbType.Int).Value = surah;
                cmd.Parameters.Add("@AyahFrom", SqlDbType.Int).Value = ayahFrom;
                cmd.Parameters.Add("@AyahCount", SqlDbType.Int).Value = numAyah;
                SqlDataReader reader = null;
                reader = cmd.ExecuteReader();

                outhtml = "<span>";//<p><div id='rrr' class = \"maina\">"  ;
                //outhtml = "";//<p><div id='rrr' class = \"maina\">"  ;
                outArab = "''";
                string stateID = "";
                string ayahStateClass = "";
                string stateClass = "";
                //bool IsNewAyah = false;
                while (reader.Read())
                {
                    //if (ayahId == reader["ayahId"].ToString())
                    //{
                    //    IsNewAyah = false;
                    //}
                    //else
                    //{
                    //    //ayahStateClass = "wordstate" + reader["ayahStateid"].ToString();
                    //    IsNewAyah = true;
                    //    outhtml += " </span>";
                    //    //ayahId = reader["ayahId"].ToString();
                    //    //outhtml += " <span><span data-word_all=" + surah.ToString() + ":" + ayahId + " class='ayahNum " + ayahStateClass + "'>" + ayahId + "</span> ";
                    //}
                    nextSura = reader["nextSurah"].ToString();
                    nextAyah = reader["nextAyah"].ToString();
                    stateID = reader["stateid"].ToString();
                    if (string.IsNullOrEmpty(stateID))
                        stateClass = "wordstate0";
                    else
                        stateClass = "wordstate" + stateID;
                    #region notes
                    /***VERY IMP NOTE: 
                     * SQL table.Column: Words.Meaning has 18 single quotes, 3942 dbl quotes and 0 ` symbols---->no `
                     * SQL table/col: Lemma.lemma has 114 single quotes, 0 dbl quotes and 600 ` symbols--------->no "
                     * 
                     * THE TEST SQL:
                     * select * from words where charindex(CHAR(39),meaning)>0  --single quote:18
                     * select * from words where charindex('"',meaning)>0  --dbl quote: 3942
                     * select * from words where charindex('`',meaning)>0  --tilde 0
                     * 
                     * select * from lemma where charindex(CHAR(39),lemma)>0  --single quote:114
                     * select * from lemma where charindex('"',lemma)>0  --dbl quote:0
                     * select * from lemma where charindex('`',lemma)>0  --dbl quote:lots
                     * 
                     * 
                     * REPLACE HERE:
                     *      outhtml sanitization for Words.Meaning ->Replace("'", "`")
                     *      outhtml sanitization for Lemma.Lemma ->Replace("'", "\"")
                     * THEN REPLACE BACK in js :
                     *      var meaning = arrWord[0].replace('`', '\'');;
                     *      var lemma = arrWord[1].replace('"','\'');
                     *                      
                     *****IMP NOTE:*/
                    #endregion 
                    //******HTML5 --> use of data attribute: data-wordDetails=
                    string oneWord = "<span data-word_all='" + reader["meaningbengali"].ToString().Replace("'", "`") + "|" + reader["Lemma"].ToString().Replace("'", "\"") + "|" + reader["LemmaID"].ToString() + "|" + reader["root"].ToString() + "|" + reader["wordText"].ToString() + "|" + reader["LemmaNote"].ToString().Replace("'", "`") + "' title='" + "" + "' class='word " + stateClass + "'>" + spaceNeeded + reader["wordText"].ToString() + "</span>"; //spit out as js array and use jQuery to add link and ajax
                    
                    if (ayahId == reader["ayahId"].ToString())
                    {
                        outhtml += oneWord;
                        //IsNewAyah = false;
                         ayahStateClass = "wordstate" + reader["ayahStateid"].ToString();
                   }
                    else
                    {
                        //IsNewAyah = true;
                       // outhtml += " </span>";
                        outhtml += "<span data-ayah_all=" + surah.ToString() + ":" + ayahId + " class='ayahNum " + ayahStateClass + "'> " + ayahId + " </span></span>";
                        ayahId = reader["ayahId"].ToString();
                        outhtml += "<span>" + oneWord;
                    }

                }
                outhtml += "<span data-ayah_all=" + surah.ToString() + ":" + ayahId + " class='ayahNum " + ayahStateClass + "'> " + ayahId + " </span> </span>";
                nextVersesUrl = "/Study.aspx?Surah=" + nextSura + "&AyahFrom=" + nextAyah + "&NumVerses=" + numAyah.ToString();

                if (conn.State == ConnectionState.Open) conn.Close();

            }
        }
        protected void zzLoadTranslation(int surah, int ayahFrom, int numAyah)
        {
            string connStr = ConfigurationManager.ConnectionStrings["ConnectionStr"].ToString();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand();
                conn.Open();
                cmd = new SqlCommand("[SP_GetTranslation]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@SurahID", SqlDbType.Int).Value = surah;
                cmd.Parameters.Add("@AyahFrom", SqlDbType.Int).Value = ayahFrom;
                cmd.Parameters.Add("@AyahCount", SqlDbType.Int).Value = numAyah;
                SqlDataReader reader = null;
                reader = cmd.ExecuteReader();

                outArab = "''";
                string sura = "";
                string ayah = "";
                string ayahTrans = "";
                
                while (reader.Read())
                {

                    sura = reader["surahId"].ToString();
                    ayah = reader["ayahId"].ToString();
                    ayahTrans = reader["ayahTextEn"].ToString().Replace("'", "`");
                    jsArrayTrans += "var tran" + sura + "_" + ayah + "='" + ayahTrans + "';";

                    sAllEngTrans += "(" + sura + ":" + ayah + ") " + ayahTrans + " ";
                    /***VERY IMP NOTE: 
                     * SQL table.Column: Words.Meaning has 18 single quotes, 3942 dbl quotes and 0 ` symbols---->no `
                     * SQL table/col: Lemma.lemma has 114 single quotes, 0 dbl quotes and 600 ` symbols--------->no "
                     * 
                     * THE TEST SQL:
                        select * from translationEn where charindex(CHAR(39),ayahtextEn)>0  --single quote:290
                        select * from translationEn where charindex('"',ayahtextEn)>0  --dbl quote: 1717
                        select * from translationEn where charindex('`',ayahtextEn)>0  --tilde 0
                     * 
                     * REPLACE HERE:
                     *      outhtml sanitization for Words.Meaning ->Replace("'", "`")
                     *      outhtml sanitization for Lemma.Lemma ->Replace("'", "\"")
                     * THEN REPLACE BACK in js :
                     *      var meaning = arrWord[0].replace('`', '\'');;
                     *      var lemma = arrWord[1].replace('"','\'');
                     *                      
                     *****IMP NOTE:*/
                    //******HTML5 --> use of data attribute: data-wordDetails=
                    //outhtml += "<span data-word_all='" + reader["Meaning"].ToString().Replace("'", "`") + "|" + reader["Lemma"].ToString().Replace("'", "\"") + "|" + reader["LemmaID"].ToString() + "|" + reader["root"].ToString() + "|" + reader["wordText"].ToString() + "|41,http://www.studyquran.org/LaneLexicon/Volume1/00000041.pdf' title='" + reader["Meaning"].ToString() + "' class='word" + stateClass + "'>" + spaceNeeded + reader["wordText"].ToString() + "</span>"; //spit out as js array and use jQuery to add link and ajax
                }
                if (conn.State == ConnectionState.Open) conn.Close();
                sAllEngTrans = "<span>" + sAllEngTrans + "</span>";
            }
        }
    }
}