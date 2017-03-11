using System;
using System.IO;
using System.Net;
using System.Xml;

namespace QuranStudy
{
    public partial class Terms : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           // Test2();
        }
        protected void Test2()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:200/api/polls/view/camp/drwAOrR7Dhu08cK5RYjpBqxKzY2tvEICe9t81IUtInQ=");
            request.Credentials = CredentialCache.DefaultCredentials;
            request.Referer = "http://www.msn.com";
            string responseFromServer = "aa";
            WebResponse response = request.GetResponse ();
           // Console.WriteLine (((HttpWebResponse)response).StatusDescription);
            Stream dataStream = response.GetResponseStream ();
            StreamReader reader = new StreamReader (dataStream);
            responseFromServer = reader.ReadToEnd ();
            Console.WriteLine (responseFromServer);
            reader.Close ();
            response.Close ();
        

        }
        protected void Test1()
        {
            XmlDocument xmlDoc = new XmlDocument(); // Create an XML document object
            xmlDoc.Load(@"E:\my\web\QStudy\quranMetadata.xml"); // Load the XML document from the specified file

            // Get elements
            XmlNodeList nodes = xmlDoc.GetElementsByTagName("pages");
            //XmlNodeList girlAge = xmlDoc.GetElementsByTagName("gAge");
            //XmlNodeList girlCellPhoneNumber = xmlDoc.GetElementsByTagName("gPhone");

            // Display the results
            // foreach (XmlNodeList c in nodes[0].ChildNodes)
            for (int k = 0; k < nodes[0].ChildNodes.Count; k++)
            {
                // string page = nodes[0].ChildNodes[0].Attributes["index"].Value;
                // string sura = nodes[0].ChildNodes[0].Attributes["sura"].Value;
                // string ayah = nodes[0].ChildNodes[0].Attributes["ayah"].Value;

                string page = nodes[0].ChildNodes[k].Attributes["index"].Value;
                string sura = nodes[0].ChildNodes[k].Attributes["sura"].Value;
                string ayah = nodes[0].ChildNodes[k].Attributes["aya"].Value;

                // Response.Write(page );
                Response.Write("insert into Page1(Page, Surah, Ayah) values (" + page + "," + sura + "," + ayah + ")<br>");
            }
            //Console.WriteLine("Age: " + girlAge[0].InnerText);
            //Console.WriteLine("Phone Number: " + girlCellPhoneNumber[0].InnerText);
        }
    }
}