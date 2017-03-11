using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
namespace QuranStudy
{
    public partial class GetHandler : System.Web.UI.Page
    {
        protected string outhtml = "";//<p><div id='rrr' class = \"maina\">"  ;

        protected string reqType="";//getLex
        protected string connStr = ConfigurationManager.ConnectionStrings["ConnectionStr"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["reqType"] != null)
            {
                reqType = Request.QueryString["reqType"].ToString();
            }
            else
            {
                Response.Write("\nError reqType required.");
                return;
            }

            if (reqType == "getLex")
            {
                Response.Write(GetLexPages(Request.QueryString["root"].ToString()));
            }

            if (reqType == "getLemmaAyat")
            {
                Response.Write(GetAyatFromLemmaID(Request.QueryString["lemID"].ToString()));
            } 

        }

        private string GetLexPages(string letters)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    // SqlConnection oConn = new SqlConnection(Global.gConnString);
                    SqlCommand cmd = new SqlCommand();
                    conn.Open();
                    cmd = new SqlCommand("[SP_GetLexiconRef]", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@letters", SqlDbType.VarChar).Value = letters; 
                    SqlDataReader reader = null;
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        return "SUCCESS" + "|" + reader["volume"].ToString() + "|" + reader["pages"].ToString();
                    }
                    return "";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }


        }

        private string GetAyatFromLemmaID(string letters)
        {
            string surahId = "";
            string ayahId = "";
            string ayahLink = "<a href=\"/study.aspx?Surah={0}&AyahFrom={1}\">{0}:{1}</a>]";
            string spaceNeeded = "&nbsp; ";

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    SqlCommand cmd = new SqlCommand();
                    conn.Open();
                    cmd = new SqlCommand("[SP_GetLemmaAyat]", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@LemmaID", SqlDbType.VarChar).Value = letters;
                    SqlDataReader reader = null;
                    reader = cmd.ExecuteReader();
                    int count = 1;
                    while (reader.Read())
                    {
                        surahId = reader["surahID"].ToString();
                        ayahId = reader["ayahID"].ToString();
                        string ayahLinkNow = string.Format(ayahLink,surahId,ayahId);
                        //***VERY IMP NOTE: read in default.aspx.cs
                        outhtml += "<div data-word_all='" + reader["Meaning"].ToString().Replace("'", "`") + "|" + reader["Lemma"].ToString().Replace("'", "\"") + "|" + reader["LemmaID"].ToString() + "|" + reader["root"].ToString() + "|" + reader["wordText"].ToString() + "' class='lemmaAyah'>";
                        outhtml += count.ToString() + ". <span class='hilite'>" + spaceNeeded + reader["wordText"].ToString() + "</span> <span> - " + reader["Meaning"].ToString() + "</span>] - <span class='showAyah'>" + ayahLinkNow + spaceNeeded + reader["AyahText"].ToString() + "</span></div><hr />"; //spit out as js array and use jQuery to add link and ajax
                        count++;
                    } 
                }
                return "SUCCESS|" + outhtml;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }


        }

 
    }
}