using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
namespace QuranStudy
{
    public partial class MyVerses : System.Web.UI.Page
    {
        protected int UserID = -1;//-1=not logged in, 1=guest, 2=kazi
        protected int verseType = 13;//hilited
        protected string verseTypeName = "Hilighted";//hilited
        //protected string UserName = "[<a href=\"/login.aspx\">sign in</a>]";
        protected string outhtml = "";//<p><div id='rrr' class = \"maina\">"  ;

        protected void Page_Load(object sender, EventArgs e)
        {
            //InitBuckwalterTransliteration();
            if (Session["UserID"] != null)
            {
                UserID = Convert.ToInt32(Session["UserID"].ToString());
                if (Request.QueryString["type"] != null)
                {
                    verseType = Convert.ToInt32(Request.QueryString["type"].ToString());
                }

                switch (verseType)
                {
                    case 11: verseTypeName = "Memorize"; break;
                    case 12: verseTypeName = "Grammar"; break;
                    case 13: verseTypeName = "Highlighted"; break;
                    case 14: verseTypeName = "Boxed (Special)"; break;
                    default: verseTypeName = "Memorize"; break;
                }
                GetMyVerses(UserID, verseType);
            }
            else
                outhtml = "Please <a href=\"/login.aspx\">login</a> to see your words";
        }
        protected void GetMyVerses(int userID, int type)
        {
            string connStr = ConfigurationManager.ConnectionStrings["ConnectionStr"].ToString();
            string surahId = "";
            string ayahId = "";
            string ayahLink = "<a href=\"/study.aspx?Surah={0}&AyahFrom={1}\">({0}:{1})</a>";
            string spaceNeeded = "&nbsp; ";
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand();
                conn.Open();
                cmd = new SqlCommand("[SP_GetMyVerses]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@userID", SqlDbType.Int).Value = userID;
                cmd.Parameters.Add("@TypeID", SqlDbType.Int).Value = type;
                SqlDataReader reader = null;
                reader = cmd.ExecuteReader();
                int count = 1;
                while (reader.Read())
                {
                    surahId = reader["surahID"].ToString();
                    ayahId = reader["ayahID"].ToString();
                    string ayahLinkNow = string.Format(ayahLink, surahId, ayahId);
                    //***VERY IMP NOTE: read in default.aspx.cs
                    //outhtml += "<div class='lemmaAyah'>";
                    outhtml += "<div>" + count.ToString() + ". <span class='showAyah'>" + ayahLinkNow + spaceNeeded + reader["AyahText"].ToString() + "</span></div><hr />"; //spit out as js array and use jQuery to add link and ajax
                    count++;
                    //outhtml += "<div class=\"myWordList\" data-word_all='" + reader["LemmaID"].ToString() + "|" + reader["root"].ToString() + "'>" + count.ToString() + ". " + GetArabicLemma(reader["Lemma"].ToString()) + " <span class=\"myWordsLex\">" + GetLexiconLinks(reader["volume"].ToString(), reader["pages"].ToString()) + "</span></div><div><div class='moreAyatSib'></div><span dir='ltr' class='moreAyatHide' style='visibility:hidden'>hide</span> <span  dir='ltr' class='moreAyat' data-word_all='" + reader["LemmaID"].ToString() + "'>[Show verses]</span></div><hr class='hrMain'>";
                    //count++;
                }

                if (conn.State == ConnectionState.Open) conn.Close();

            }
        }
         

    }
}