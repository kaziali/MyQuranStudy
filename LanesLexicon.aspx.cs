using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.SqlServer.Server;

namespace QuranStudy
{
    public partial class LanesLexicon : System.Web.UI.Page
    {
        protected string pageNav = "Navigate pages: ";
        protected string ifrm_src = "";
 

        protected void Page_Load(object sender, EventArgs e)
        {
            int vol = Convert.ToInt32(Request.QueryString["v"].ToString());
            int page = Convert.ToInt32(Request.QueryString["p"].ToString());
            int range1 =0;
            int range2=0;


            //e.g http://www.studyquran.org/LaneLexicon/Volume6/00000189.pdf
            //string locExt = "http://www.studyquran.org/LaneLexicon/Volume";
            string locExt = "/LaneLexicon/V";
            string locInt = "/LanesLexicon.aspx?"; 

            try
            {
                range1 = Convert.ToInt32(Request.QueryString["range1"].ToString());
                range2 = Convert.ToInt32(Request.QueryString["range2"].ToString());
            }
            catch
            {
                
            }

            ifrm_src = locExt + vol.ToString() + "/" + page.ToString().PadLeft(8, '0') + ".pdf";
            int pgSpan = range2 - range1 < 4 ? 5: range2 - range1 + 2;
            for (int pg = page - pgSpan; pg < page + pgSpan; pg++)
            {
                if (pg > 0)
                {
                    if (pg == page)
                    {
                        pageNav += " <span style='color:#bbb;font-weight:bold;font-size:24px'>" + pg.ToString() +
                                   "</span>,  ";

                    }
                    else if (range1 > 0 && pg >= range1 && pg <= range2)
                    {
                        pageNav += " <a style='color:#b22222;font-weight:bold;font-size:24px' href=\"" + locInt + "v=" +
                                   vol + "&p=" + pg.ToString() + "&range1=" +
                                   range1 + "&range2=" + range2 + "\">" + pg.ToString() + "</a>,  ";

                    }
                    else
                        pageNav += "<a href=\"" + locInt + "v=" + vol + "&p=" + pg.ToString() + "\">" + pg.ToString() +
                                   "</a>, ";
                }
            }
              
        }
    }
}