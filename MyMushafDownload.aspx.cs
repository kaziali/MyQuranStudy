using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
namespace QuranStudy
{
    public partial class MyMushafDownload : System.Web.UI.Page
    {
        protected bool VerifyLineBreak = false; //otherwise printer version
        protected bool ShowFullVersion = true; //otherwise printer version
        protected bool IsDownload = true; //otherwise printer version
        protected string mushafCss = "mushafAdjuster"; //otherwise printer version

        protected int UserID = -1;//-1=not logged in, 1=guest, 2=kazi
        protected string UserName = "<a href=\"/login.aspx\">sign in</a>";
        protected string nextVersesUrl = "";
        protected string prevVersesUrl = "";
        protected string currVersesUrl = "";

        protected string sAllEngTrans = "";
        protected string jsArrayTrans = "";
        protected string outhtml;
        protected string outArab;
        protected string outEng;
        protected int surah = 0;
        protected int ayahFrom = 0;
        protected int numAyah = 0;
        protected int PageNum = 0;

        //protected int ayahTo = 1;

        protected string jsArrayIDs = "";
        protected string jsArrayNames = "";
        protected string jsArrayAyat = "";

        protected int AggrtKnown = 0;
        protected int UniqueKnown = 0;
        protected int UniqueUnderlined = 0;
        protected int UniqueHilited = 0;
        protected int UniqueBoxed = 0; 

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["UserName"] != null)
                UserName = Session["UserName"].ToString();
            if (Session["UserID"] != null)
                UserID = Convert.ToInt32(Session["UserID"].ToString());

            surah = Convert.ToInt32(Request.Params["surah"]);
            ayahFrom = Convert.ToInt32(Request.Params["AyahFrom"]);
            numAyah = 5;// Convert.ToInt32(Request.Params["NumVerses"]);
            PageNum = Convert.ToInt32(Request.Params["Page"]);

            if (Request.QueryString["print"] != null)
            {
                IsDownload = true;
                ShowFullVersion = true;// !Convert.ToBoolean(Request.Params["print"].ToString());
                if (!ShowFullVersion) mushafCss = "mushafPrinter";
            }

            if (Request.QueryString["linebr"] != null)
            {
                VerifyLineBreak = Convert.ToBoolean(Request.Params["linebr"].ToString());                
            }

            if (PageNum <= 0)
            {
                if (surah < 1) surah = 1;
                if (ayahFrom < 1) ayahFrom = 1;
                if (numAyah < 1) numAyah = 7;
            }
           
            LoadSurahNames();
            LoadVerses(ref surah, ref ayahFrom, numAyah, ref PageNum);
            LoadTranslation(surah, ayahFrom, numAyah, PageNum);
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
        protected void LoadSurahNames()
        {
            bool addSurahs=false;
            List<string> surahs;// = new List<string>();
            surahs = (List<string>)Application["surahs"];
            if (surahs == null)
            {
                addSurahs = true;
                surahs  = new List<string>();
            }
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
                    //jsArrayIDs += "," + reader["SurahID"].ToString();
                    //jsArrayNames += ",\"" + reader["SurahID"].ToString() + " - " + reader["TName"].ToString() + "\"";
                    //jsArrayAyat += "," + reader["AyatTotal"].ToString();
                    if(addSurahs == true) 
                        surahs.Add(reader["TName"].ToString());
                }
                if (addSurahs == true) 
                    Application["surahs"] = surahs;
                if (conn.State == ConnectionState.Open) conn.Close();
                //jsArrayIDs = jsArrayIDs.Substring(1);
                //jsArrayNames = jsArrayNames.Substring(1);
                //jsArrayAyat = jsArrayAyat.Substring(1);
            }
        }
        protected void LoadVerses(ref int surah, ref int ayahFrom, int numAyah, ref int PageNum)
        {
            List<string> surahs;// = new List<string>();
            surahs = (List<string>)Application["surahs"];

            string connStr = ConfigurationManager.ConnectionStrings["ConnectionStr"].ToString();
            string ayahId =   ayahFrom.ToString();// "";
            string spaceNeeded = " ";
           // string nextSura = "";
            //string nextAyah = "";
            string nextPage="";
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand();
                conn.Open();
                cmd = new SqlCommand("[SP_GetVersesWithUserVocab_Mushaf]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@userID", SqlDbType.Int).Value = UserID;
                cmd.Parameters.Add("@SurahFrom", SqlDbType.Int).Value = surah;
                cmd.Parameters.Add("@AyahFrom", SqlDbType.Int).Value = ayahFrom;
                cmd.Parameters.Add("@AyahCount", SqlDbType.Int).Value = numAyah;
                cmd.Parameters.Add("@PageNum", SqlDbType.Int).Value = PageNum;
                SqlDataReader reader = null;
                reader = cmd.ExecuteReader();

                outhtml = "<span>";//<p><div id='rrr' class = \"maina\">"  ;
                //outhtml = "";//<p><div id='rrr' class = \"maina\">"  ;
                outArab = "''";
                string stateID = "";
                string ayahStateClass = "";
                string stateClass = "";
                int LineBreakType = -1;
                string newlineAfterWord = "";
                string newlineAfterAyahNum = "";

                string spnAyahNum = "";
                bool IsFirstRec = true;
                string prevSurah = "";
                string currSurah = "";
                string currAyah = "";

                while (reader.Read())
                {
                    if (IsFirstRec)
                    {
                        surah = Convert.ToInt32(reader["SurahId"].ToString());
                        currSurah = surah.ToString();
                        ayahFrom = Convert.ToInt32(reader["AyahId"].ToString());
                    }
                    //IsFirstRec = false;

                    prevSurah = currSurah;
                    currSurah = reader["SurahId"].ToString();
                    currAyah = reader["AyahId"].ToString();

                    newlineAfterWord = "";
                    //memoLineNumHtml = "";
                    LineBreakType = -1;
                    if (!string.IsNullOrEmpty(reader["LineBreakType"].ToString()))
                    {
                        LineBreakType = Convert.ToInt32(reader["LineBreakType"].ToString());
                        if (LineBreakType == 1)
                            newlineAfterWord = "<br />";
                        if (LineBreakType == 2 && ayahId == currAyah)
                            newlineAfterAyahNum = "<br />";
                    }
                    nextPage = reader["nextPage"].ToString();
                   // nextAyah = reader["nextAyah"].ToString();
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
                    string breakTypeString = "";
                    string breakTypeInline = "";
                    if (LineBreakType == 1)
                    { 
                       // breakTypeString = "title = br-Word";
                        breakTypeInline = "<span style=\"font-size:50%;color:#0094ff\"> (1) </span>";
                    }
                    else if (LineBreakType == 2)
                    {
                       // breakTypeString = "title = br-Num";
                        breakTypeInline = "<span style=\"font-size:50%;color:#ff00dc\"> [2] </span>";
                    }
                    else
                        breakTypeInline = "";

                    if(!VerifyLineBreak)  breakTypeInline = "";

                    string spnWord = "<span " + breakTypeString + " data-word_all='" + reader["Meaning"].ToString().Replace("'", "`") + "|" + reader["Lemma"].ToString().Replace("'", "\"") + "|" + reader["LemmaID"].ToString() + "|" + reader["root"].ToString() + "|" + reader["wordText"].ToString() + "|" + reader["LemmaNote"].ToString().Replace("'", "`") + "|" + reader["ID"].ToString() + "' title='" + "" + "' class='word " + stateClass + "'>" + spaceNeeded + reader["wordText"].ToString() + "</span>" + breakTypeInline + newlineAfterWord; //spit out as js array and use jQuery to add link and ajax
                    spnAyahNum = "<span data-ayah_all='{0}:{1}' class='ayahNum {2}'>{1}</span>";

                    if (IsFirstRec) 
                        if (currAyah == "1" & reader["WordID"].ToString() == "1")
                        {
                            outhtml += " <div class='suraName'>Surah " + currSurah + " : " + surahs[Convert.ToInt32(currSurah) - 1] + "</div>";
                            if (currSurah != "9" & currSurah != "1")
                                outhtml += "<div class='suraBasmala'><img src='images/SurahBasmala.jpg' /></div> ";
                        }
                    IsFirstRec = false;

                    if (ayahId == currAyah)
                    {
                        outhtml += spnWord;
                    }
                    else
                    {
                        ayahStateClass = "wordstate" + reader["ayahStateid"].ToString();
                        if (ayahId == "0")
                        {
                            // outhtml += string.Format(spnAyahNum, ayahId) + newlineAfterAyahNum + "</span>";
                        }
                        else
                        {
                            outhtml += string.Format(spnAyahNum, prevSurah, ayahId, ayahStateClass) + newlineAfterAyahNum + "</span>";

                            if (!IsFirstRec) 
                                if (currAyah == "1" & reader["WordID"].ToString() == "1")
                                {
                                    outhtml += " <div class='suraName'>Surah " + currSurah + " : " + surahs[Convert.ToInt32(currSurah) - 1] + "</div>";
                                    if (currSurah != "9" & currSurah != "1")
                                        outhtml += "<div class='suraBasmala'><img src='images/SurahBasmala.jpg' /></div> ";
                                }
                           // if (currAyah == "1" & reader["WordID"].ToString() == "1")
                            //    outhtml += "<hr /> Surah: <span id='suraName'>" + currSurah + "</span><hr />";
                        }

                        ayahId = currAyah;
                        outhtml += "<span>" + spnWord;
                        newlineAfterAyahNum = "";
                    }


                }
                outhtml += string.Format(spnAyahNum, currSurah, currAyah, ayahStateClass) + newlineAfterAyahNum + "</span>";
               // outhtml += " </span>";
                int prePg=Convert.ToInt32(nextPage) - 2;
                prePg = prePg < 1 ? 1 : prePg;
                prevVersesUrl = prePg.ToString() + ".htm";
                nextVersesUrl = nextPage.ToString() + ".htm";
                currVersesUrl = "/MyMushafDownload.aspx?Page=" + (prePg + 1).ToString();

                PageNum = Convert.ToInt32(nextPage) - 1;
                if (conn.State == ConnectionState.Open) conn.Close();

            }
        }
        protected void LoadTranslation(int surah, int ayahFrom, int numAyah, int PageNum)
        {
            //Response.Write(PageNum);
            string connStr = ConfigurationManager.ConnectionStrings["ConnectionStr"].ToString();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand();
                conn.Open();
                cmd = new SqlCommand("[SP_GetTranslation_Mushaf]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@SurahFrom", SqlDbType.Int).Value = surah;
                cmd.Parameters.Add("@AyahFrom", SqlDbType.Int).Value = ayahFrom;
                cmd.Parameters.Add("@AyahCount", SqlDbType.Int).Value = numAyah;
                cmd.Parameters.Add("@PageNum", SqlDbType.Int).Value = PageNum;
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

            }
        }
    }
}