using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.SqlServer.Server;

namespace QuranStudy
{
    public partial class TafsirIbnKathir : System.Web.UI.Page
    {
        protected string pdfPagePrev = "";
        protected string pdfPageNext = "";
        protected string ifrm_src = "";
 

        protected void Page_Load(object sender, EventArgs e)
        {
            string fn = "001_25.pdf";
            try
            {
                fn = Request.QueryString["fn"].ToString();
            }
            catch { }
            string locExt = "/tafsir-ibn-kathir/splitPdf/";
            string sura_pg = fn.Replace(".pdf", "");
            int surah = Convert.ToInt32(sura_pg.Split('_')[0]);
            int page = Convert.ToInt32(sura_pg.Split('_')[1]);
            GetTafsirNavLinks(surah, page);
            ifrm_src = locExt + fn; 
        }

        private void GetTafsirNavLinks(int sur, int pg)
        {
            try
            {
                string connStr = ConfigurationManager.ConnectionStrings["ConnectionStr"].ToString();
                using (SqlConnection conn = new SqlConnection(connStr))
                {

                    SqlCommand cmd = new SqlCommand();
                    conn.Open();
                    cmd = new SqlCommand("[SP_GetTafsirNextPrev]", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@surahid", SqlDbType.Int).Value = sur;
                    cmd.Parameters.Add("@pageID", SqlDbType.Int).Value = pg;
                    SqlDataReader reader = null;
                    reader = cmd.ExecuteReader();
                    string PrevSurah = string.Empty;
                    string PrevPage = string.Empty;
                    string NextSurah = string.Empty;
                    string NextPage = string.Empty;

                    while (reader.Read())
                    {
                        if (reader["PrevSurah"] != null) PrevSurah = reader["PrevSurah"].ToString();
                        if (reader["PrevPage"] != null) PrevPage = reader["PrevPage"].ToString();
                        if (reader["NextSurah"] != null) NextSurah = reader["NextSurah"].ToString();
                        if (reader["NextPage"] != null) NextPage = reader["NextPage"].ToString();


                        //pagePrev = reader["FNPrev"] != null ? reader["FNPrev"].ToString(): "";
                        //pageNext = reader["FNNext"] != null ? reader["FNNext"].ToString() : "";

                    }
                    pdfPagePrev = PrevSurah.PadLeft(3, '0') + "_" + PrevPage + ".pdf";
                    pdfPageNext = NextSurah.PadLeft(3, '0') + "_" + NextPage + ".pdf"; 

                    //if (!string.IsNullOrEmpty(pageNext))
                    //    pageNext = "<a href=\"/TafsirIbnKathir.aspx?fn=" + pageNext + ".pdf\">Next</a>";
                    //if (!string.IsNullOrEmpty(pagePrev))
                    //    pagePrev = "<a href=\"/TafsirIbnKathir.aspx?fn=" + pagePrev + ".pdf\">Prev</a>";
                }
            }
            catch (Exception ex)
            {
              //  return ex.Message;
            }


        }

    }
}
 