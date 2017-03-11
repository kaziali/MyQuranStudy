using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
namespace QuranStudy
{
    public partial class MyWords : System.Web.UI.Page
    {
        protected int UserID = -1;//-1=not logged in, 1=guest, 2=kazi
        protected int WordType = 3;//hilited
        protected string WordTypeName = "";//hilited
        //protected string UserName = "[<a href=\"/login.aspx\">sign in</a>]";
        protected string outhtml = "";//<p><div id='rrr' class = \"maina\">"  ;
        protected Dictionary<string, string> dictTrans = new Dictionary<string, string>();
        protected int PageNum = 1;
        protected int WordsPerPage = 10;
        protected int TotalPages = 1 ;
        protected double TotalRecordsCount = 10;
        protected bool Shuffle = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            InitBuckwalterTransliteration();
            if (Session["UserID"] != null)
            {
                UserID = Convert.ToInt32(Session["UserID"].ToString());
                if (Request.QueryString["type"] != null)
                {
                    WordType =  Convert.ToInt32(Request.QueryString["type"].ToString());
                }

                if (Request.QueryString["PageNum"] != null)
                {
                    PageNum = Convert.ToInt32(Request.QueryString["PageNum"].ToString());
                }
                switch (WordType)
                {
                    case 1: WordTypeName = "Known"; break;
                    case 2: WordTypeName = "Underlined"; break;
                    case 3: WordTypeName = "Highlighted"; break;
                    case 4: WordTypeName = "Boxed (Special)"; break;
                    default: WordTypeName = "Known"; break;
                }
                if (Request.QueryString["Shuffle"] != null)
                    Shuffle = Convert.ToBoolean(Request.QueryString["Shuffle"].ToString());
                GetMyWords(UserID, WordType, PageNum, Shuffle);

              

            }
            else
                outhtml = "Please <a href=\"/login.aspx\">login</a> to see your words";
        }
        protected string GetArabicLemma(string lemma)
        {
            if(lemma=="-") return "<span class=\"averageFont\">Huruf - Pronouns, prepositions etc.</span>";
             string outString = "";
          //  string[] arr = lemma.Split("".ToCharArray());
             char[] arr = lemma.ToCharArray();
            for (int i = 0; i < arr.Length; i++)
            {
                 //if (list[arr[i]] != 'undefined')
                if(dictTrans.ContainsKey(arr[i].ToString()))
                outString += dictTrans[arr[i].ToString()];
            }
            return outString;
        }
        protected  void InitBuckwalterTransliteration()
        {
            // select 'dictTrans.Add("' + ASCIIVal + '", ' + '"\' + hex + '"); '  from dbo.BuckwalterEx 

            dictTrans.Add("'", "\u0621"); 
            dictTrans.Add(">", "\u0623"); 
            dictTrans.Add("&", "\u0624"); 
            dictTrans.Add("<", "\u0625"); 
            dictTrans.Add("}", "\u0626"); 
            dictTrans.Add("A", "\u0627"); 
            dictTrans.Add("b", "\u0628"); 
            dictTrans.Add("p", "\u0629"); 
            dictTrans.Add("t", "\u062A"); 
            dictTrans.Add("v", "\u062B"); 
            dictTrans.Add("j", "\u062C"); 
            dictTrans.Add("H", "\u062D"); 
            dictTrans.Add("x", "\u062E"); 
            dictTrans.Add("d", "\u062F"); 
            dictTrans.Add("*", "\u0630"); 
            dictTrans.Add("r", "\u0631"); 
            dictTrans.Add("z", "\u0632"); 
            dictTrans.Add("s", "\u0633"); 
            dictTrans.Add("$", "\u0634"); 
            dictTrans.Add("S", "\u0635"); 
            dictTrans.Add("D", "\u0636"); 
            dictTrans.Add("T", "\u0637"); 
            dictTrans.Add("Z", "\u0638"); 
            dictTrans.Add("E", "\u0639"); 
            dictTrans.Add("g", "\u063A"); 
            dictTrans.Add("_", "\u0640"); 
            dictTrans.Add("f", "\u0641"); 
            dictTrans.Add("q", "\u0642"); 
            dictTrans.Add("k", "\u0643"); 
            dictTrans.Add("l", "\u0644"); 
            dictTrans.Add("m", "\u0645"); 
            dictTrans.Add("n", "\u0646"); 
            dictTrans.Add("h", "\u0647"); 
            dictTrans.Add("w", "\u0648"); 
            dictTrans.Add("Y", "\u0649"); 
            dictTrans.Add("y", "\u064A"); 
            dictTrans.Add("F", "\u064B"); 
            dictTrans.Add("N", "\u064C"); 
            dictTrans.Add("K", "\u064D"); 
            dictTrans.Add("a", "\u064E"); 
            dictTrans.Add("u", "\u064F"); 
            dictTrans.Add("i", "\u0650"); 
            dictTrans.Add("~", "\u0651"); 
            dictTrans.Add("o", "\u0652"); 
            dictTrans.Add("^", "\u0653"); 
            dictTrans.Add("#", "\u0654"); 
            dictTrans.Add("`", "\u0670"); 
            dictTrans.Add("{", "\u0671"); 
            dictTrans.Add(":", "\u06DC"); 
            dictTrans.Add("@", "\u06DF"); 
            dictTrans.Add("\"", "\u06E0"); 
            dictTrans.Add("[", "\u06E2"); 
            dictTrans.Add(";", "\u06E3"); 
            dictTrans.Add(",", "\u06E5"); 
            dictTrans.Add(".", "\u06E6"); 
            dictTrans.Add("!", "\u06E8"); 
            dictTrans.Add("-", "\u06EA"); 
            dictTrans.Add("+", "\u06EB"); 
            dictTrans.Add("%", "\u06EC"); 
            dictTrans.Add("]", "\u06ED"); 

        }
        protected void GetMyWords(int userID, int type, int pageNum, bool bShuffle)
        {
            string connStr = ConfigurationManager.ConnectionStrings["ConnectionStr"].ToString();
            //double TotalRecordsCount = 10;
            string spName = "[SP_GetMyWords]";
            if (bShuffle == true)
                spName = "[SP_GetMyWords_Shuffle]";
           // Response.Write(bShuffle);
           // Response.Write(spName);
            //string surahId = "";
            //string ayahId = "";
            //string ayahLink = "<a href=\"/study.aspx?Surah={0}&AyahFrom={1}\">({0}:{1})</a>";
            //string spaceNeeded = "&nbsp; ";
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand();
                conn.Open();
                cmd = new SqlCommand(spName, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@userID", SqlDbType.Int).Value = userID;
                cmd.Parameters.Add("@TypeID", SqlDbType.Int).Value = type;
                cmd.Parameters.Add("@pageNum", SqlDbType.Int).Value = pageNum;
                //cmd.Parameters.Add("@Shuffle", SqlDbType.Bit).Value = Shuffle;
                SqlDataReader reader = null;
                reader = cmd.ExecuteReader();
                int count = (PageNum - 1) * WordsPerPage + 1;
                while (reader.Read())
                {
                   TotalRecordsCount = Convert.ToInt32(reader["TotalCount"].ToString());
                   //outhtml += "<div class=\"myWordList\" data-word_all='" + reader["LemmaID"].ToString() + "|" + reader["root"].ToString() + "'>" + count.ToString() + ". " + GetArabicLemma(reader["Lemma"].ToString()) + " <span class=\"myWordsLex\">" + GetLexiconLinks(reader["volume"].ToString(), reader["pages"].ToString()) + "</span></div><div><div class='moreAyatSib'></div><span dir='ltr' class='moreAyatHide' style='visibility:hidden'>hide</span> <span  dir='ltr' class='moreAyat' data-word_all='" + reader["LemmaID"].ToString() + "'>[Show verses]</span></div><hr class='hrMain'>";
                   outhtml += "<div class=\"myWordList\" data-word_all='" + reader["LemmaID"].ToString() + "|" + reader["root"].ToString() + "'>" + count.ToString() + ". " + GetArabicLemma(reader["Lemma"].ToString()) + " <span class=\"myWordsLex\">" + GetLexiconLinks(reader["volume"].ToString(), reader["pages"].ToString()) + "</span></div><div> <span dir='ltr' class='moreAyatHide' style='visibility:hidden'>hide</span> <span  dir='ltr' class='moreAyat' data-word_all='" + reader["LemmaID"].ToString() + "'>[Show verses]</span><div class='moreAyatSib'></div></div><hr class='hrMain'>";
                   count++;
                }
               
                if (conn.State == ConnectionState.Open) conn.Close();

            }


            if (TotalRecordsCount < WordsPerPage) WordsPerPage = (int)TotalRecordsCount;
            if (TotalRecordsCount % WordsPerPage == 0)
                TotalPages = Convert.ToInt32(Math.Floor(TotalRecordsCount / WordsPerPage));
            else
                TotalPages = Convert.ToInt32(Math.Floor(TotalRecordsCount / WordsPerPage)) + 1;

          


        }

        private string GetLexiconLinks(string vol, string pages)
        {
            if (string.IsNullOrEmpty(pages.Trim()) || string.IsNullOrEmpty(vol.Trim()))
                return "";

            string htmlCont="";
            string[] arrPg=pages.Split(",".ToCharArray());
            for (int i = 0; i < arrPg.Length; i++)
            {
                string p = arrPg[i].Trim();
                //string url = "http://www.studyquran.org/LaneLexicon/Volume" + vol + "/" + p.PadLeft(8, '0') + ".pdf"; //http://www.studyquran.org/LaneLexicon/Volume3/00000114.pdf
                string url = "/LaneLexicon/V" + vol + "/" + p.PadLeft(8, '0') + ".pdf";  
                htmlCont += "<a target='_blank' href='LanesLex.aspx?p=" + url + "'>" + p + "</a>, ";
            }
            htmlCont = htmlCont.Trim();
            if (htmlCont.EndsWith(","))
            {
                htmlCont = htmlCont.Substring(0, htmlCont.Length - 1);
                htmlCont =  "[ Lane's Lexicon: " + htmlCont + " ]";
            }
            //else
            return htmlCont;
        }

    }
}