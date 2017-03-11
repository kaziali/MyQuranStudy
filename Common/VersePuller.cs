using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace QuranStudy.Common
{
    public class VersePuller
    {
        protected Dictionary<string, string> dicTrans = new Dictionary<string, string>();

        public void LoadWords(int surah, int ayahFrom, int numAyah, ref string outhtml, ref string nextVersesUrl, int UserID, string langCode)
        {
            string connStr = ConfigurationManager.ConnectionStrings["ConnectionStr"].ToString();
            string ayahId = ayahFrom.ToString(); 
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
                cmd.Parameters.Add("@langCode", SqlDbType.VarChar).Value = langCode;
                SqlDataReader reader = null;
                reader = cmd.ExecuteReader();

                outhtml = "<span>";//<p><div id='rrr' class = \"maina\">"  ;
                //outhtml = "";//<p><div id='rrr' class = \"maina\">"  ;
                //outArab = "''";
                string stateID = "";
                string ayahStateClass = "";
                string stateClass = "";
                //bool IsNewAyah = false;
                while (reader.Read())
                {
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

                    string oneWord = "<span data-word_all='" + reader["meaning"].ToString().Replace("'", "`") + "|" + reader["Lemma"].ToString().Replace("'", "\"") + "|" + reader["LemmaID"].ToString() + "|" + reader["root"].ToString() + "|" + reader["wordText"].ToString() + "|" + reader["LemmaNote"].ToString().Replace("'", "`") + "' title='" + "" + "' class='word " + stateClass + "'>" + spaceNeeded + reader["wordText"].ToString() + "</span>"; //spit out as js array and use jQuery to add link and ajax

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
                        outhtml += "<span data-ayah_all=" + surah.ToString() + ":" + ayahId + " class='ayahNum " + ayahStateClass + "'> (" + ayahId + ") </span></span>";
                        ayahId = reader["ayahId"].ToString();
                        outhtml += "<span>" + oneWord;
                    }

                }
                outhtml += "<span data-ayah_all=" + surah.ToString() + ":" + ayahId + " class='ayahNum " + ayahStateClass + "'> " + ayahId + " </span> </span>";
                nextVersesUrl = "/{0}.aspx?Surah=" + nextSura + "&AyahFrom=" + nextAyah + "&NumVerses=" + numAyah.ToString();
                if (langCode == "ur")
                    nextVersesUrl = string.Format(nextVersesUrl, "Quran-Urdu");
                else if (langCode == "bn")
                    nextVersesUrl = string.Format(nextVersesUrl, "Quran-Bengali");
                else  
                    nextVersesUrl = string.Format(nextVersesUrl, "study");

                if (conn.State == ConnectionState.Open) conn.Close();

            }
        }
        public void LoadTranslation(int surah, int ayahFrom, int numAyah, ref string sAllEngTrans, ref string jsArrayTrans, string langCode)
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
                cmd.Parameters.Add("@langCode", SqlDbType.VarChar).Value = langCode;
                SqlDataReader reader = null;
                reader = cmd.ExecuteReader();

                //outArab = "''";
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

        public void LoadSurahNames(ref string jsArrayIDs, ref string jsArrayNames, ref string jsArrayAyat)
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

        public void Editor_LoadWords(int surah, int ayahFrom, int numAyah, ref string outhtml, ref string nextVersesUrl, int editorIDCurr, string langCode)
        {
            string outhtml2 = "";
            string connStr = ConfigurationManager.ConnectionStrings["ConnectionStr"].ToString();
            string ayahId = ayahFrom.ToString();
            string spaceNeeded = " ";
            string nextSura = "";
            string nextAyah = "";
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand();
                conn.Open();
                cmd = new SqlCommand("[EditorSP_GetVersesWithUserVocab]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.Add("@userID", SqlDbType.Int).Value = UserID;
                cmd.Parameters.Add("@SurahID", SqlDbType.Int).Value = surah;
                cmd.Parameters.Add("@AyahFrom", SqlDbType.Int).Value = ayahFrom;
                cmd.Parameters.Add("@AyahCount", SqlDbType.Int).Value = numAyah;
                cmd.Parameters.Add("@langCode", SqlDbType.VarChar).Value = langCode;
                SqlDataReader reader = null;
                reader = cmd.ExecuteReader();

                outhtml = "<span>";//<p><div id='rrr' class = \"maina\">"  ;
                //outhtml = "";//<p><div id='rrr' class = \"maina\">"  ;
                //outArab = "''";
                string stateID = "";
                string ayahStateClass = "";
                string stateClass = "";
                //bool IsNewAyah = false;
                while (reader.Read())
                {

                    nextSura = reader["nextSurah"].ToString();
                    nextAyah = reader["nextAyah"].ToString(); 
                    stateID = "1";
                    if (string.IsNullOrEmpty(stateID))
                        stateClass = "wordstate0";
                    else
                        stateClass = "wordstate" + stateID;
                     
                    //******HTML5 --> use of data attribute: data-wordDetails=
                    //string oneWord = "<span data-word_all='" + reader["meaning"].ToString().Replace("'", "`") + "|" + reader["Lemma"].ToString().Replace("'", "\"") + "|" + reader["LemmaID"].ToString() + "|" + reader["root"].ToString() + "|" + reader["wordText"].ToString() + "|" + reader["LemmaNote"].ToString().Replace("'", "`") + "' title='" + "" + "' class='word " + stateClass + "'>" + spaceNeeded + reader["wordText"].ToString() + "</span>"; //spit out as js array and use jQuery to add link and ajax
                   // string oneWord = "<div class='verifyWordDiv'>";
                    
                   // oneWord += "<span class='verifyArabicFont'>" + spaceNeeded + reader["wordText"].ToString() + spaceNeeded +   "</span>";
                   // oneWord += "<textarea class='txtVerify' maxlength='150'>" + reader["meaningbn"].ToString() + "</textarea>";
                    //oneWord += " <span class='lnkSaveCorrection'>save1</span> ";
                   
                    //oneWord += "<span class='spnVerifyAction'> " + reader["meaningbn"].ToString() + " </span>";
                    //oneWord += "<span> [" + reader["meaning"].ToString() + "] </span>";
                    //oneWord += "</div>";

                    string oneWord2 = "<tr class='verifyWordDiv'>";

                    oneWord2 += "<td class='verifyArabicFont'>" + spaceNeeded + reader["wordText"].ToString() + spaceNeeded + "</td>";
                    string meaningLoc = reader["meaningLoc"].ToString();
                    string transState = ""; ;
                    int editorID = 0;
                    if (!string.IsNullOrEmpty(reader["editorID"].ToString()))
                    editorID = Convert.ToInt32(reader["editorID"].ToString());

                    int editorID_OW = 0;
                    if (!string.IsNullOrEmpty(reader["editorIDOverwrite"].ToString()))
                        editorID_OW = Convert.ToInt32(reader["editorIDOverwrite"].ToString());

                    if (string.IsNullOrEmpty(meaningLoc))
                    {
                        meaningLoc = reader["meaningLocAuto"].ToString();
                        transState = "<img width=\"40\" src=\"/images/exclaimMark.png\" title=\"Please verify and save.\"  />";
                    }
                    else
                    {
                        if (editorID > 1000)
                            transState = "<img width=\"70\" src=\"/images/tickMarkAttn.png\" title=\"Needs your attention. Verify and save\" />";
                        else
                            transState = "<img width=\"40\" src=\"/images/tickMark.png\" title=\"Verified by editor " + editorID + "\" />";

                    }

                    if (langCode == "verify_en")
                    {
                        string iddups = reader["idDups"].ToString();
                        string id2 = reader["ID"].ToString();
                        if (id2 == iddups)
                            transState = "<img width=\"70\" src=\"/images/tickMarkAttn.png\" title=\"ID=" + id2 + "\" />" + id2;
                        else
                            transState = "<img width=\"40\" src=\"/images/tickMark.png\" title=\"ID= " + id2 + "\" />";

                    }


                    string saveBtn = "";
                    if ((editorID == 1 || editorID_OW ==1 ) && (editorIDCurr > 1) && langCode == "verify_bn")  //kazi
                        saveBtn = " <span class='lnkSaveLocked'>[Locked]</span>";
                    else if (editorIDCurr == editorID || editorID > 1000 || editorID==0) 
                        saveBtn = " <span class='lnkSaveCorrection'>save</span>";
                    else
                        saveBtn = " <span data-id='" + editorID.ToString() + "' class='lnkSaveOverwrite'>[Overwrite]</span>";

                    oneWord2 += "<td class='EditTransCol'><textarea class='txtVerify' maxlength='150'>" + meaningLoc + "</textarea>";
                    oneWord2 += saveBtn + " <span class='wordID' style='display:none'>" + reader["ID"].ToString() + "</span> <span class='done'>" + transState + "</span></td>";

                    oneWord2 += "<td class='spnFont" + langCode.ToUpper() + "'> " + reader["meaningLocAuto"].ToString() + " </td>";
                    oneWord2 += "<td class='verifyEnMeaning'>" + reader["meaning"].ToString() + "</td>";
                    oneWord2 += "</tr>"; 


                    if (ayahId == reader["ayahId"].ToString())
                    {
                        outhtml2 += oneWord2;
                        //outhtml += oneWord;
                        //IsNewAyah = false;
                        ayahStateClass = "wordstate1";// +reader["ayahStateid"].ToString();
                    }
                    else
                    {
                        //IsNewAyah = true;
                        // outhtml += " </span>";
                        outhtml += "<span data-ayah_all=" + surah.ToString() + ":" + ayahId + " class='ayahNum " + ayahStateClass + "'> " + ayahId + " </span></span>";
                       // ayahId = reader["ayahId"].ToString();
                        //outhtml += "<span>" + oneWord;

                        outhtml2 += "<tr><td colspan=3 class='spnFont" + langCode.ToUpper() + "'>" + dicTrans[surah + "_" + ayahId] + "</td><td><span data-ayah_all=" + surah.ToString() + ":" + ayahId + " zzclass='ayahNum " + ayahStateClass + "'> [" + surah.ToString() + ":" + ayahId + "] </span></td></tr>";
                        outhtml2 += "<tr><td colspan=4><div class='ayahMarkerBg'>&nbsp;</div></td></tr>";
                        ayahId = reader["ayahId"].ToString();
                        outhtml2 += "<span>" + oneWord2;

                    }

                }
                outhtml += "<span data-ayah_all=" + surah.ToString() + ":" + ayahId + " class='ayahNum " + ayahStateClass + "'> " + ayahId + " </span> </span>";
                outhtml2 += "<tr><td colspan=3 class='spnFont" + langCode.ToUpper() + "'>" + dicTrans[surah + "_" + ayahId] + "</td><td><span data-ayah_all=" + surah.ToString() + ":" + ayahId + " zzclass='ayahNum " + ayahStateClass + "'> [" + surah.ToString() + ":" + ayahId + "] </span></td></tr>";
                //outhtml2 += "<tr><td colspan=4><span data-ayah_all=" + surah.ToString() + ":" + ayahId + " class='ayahNum " + ayahStateClass + "'> " + ayahId + " </span> </span></td></tr>";
                nextVersesUrl = "/VerifyTranslation/Bengali.aspx?Surah=" + nextSura + "&AyahFrom=" + nextAyah + "&NumVerses=" + numAyah.ToString();

                if (conn.State == ConnectionState.Open) conn.Close();

            }
            outhtml = "<table id='mainTbl'><tr class='header1stTr'><td>Arabic</td><td class='EditTransCol'>Edit or Verify</td><td>Auto Translation</td><td>English</td></tr>" + outhtml2 + "</table>";
           // outhtml = "<table>" + outhtml2 + "</table>";
        }

        public void Editor_LoadTranslation(ref int surah, ref int ayahFrom, int numAyah, ref string sAllEngTrans, ref string jsArrayTrans, string langCode)
        {
            string connStr = ConfigurationManager.ConnectionStrings["ConnectionStr"].ToString();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand();
                conn.Open();
                cmd = new SqlCommand("[EditorSP_GetTranslation]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@SurahID", SqlDbType.Int).Value = surah;
                cmd.Parameters.Add("@AyahFrom", SqlDbType.Int).Value = ayahFrom;
                cmd.Parameters.Add("@AyahCount", SqlDbType.Int).Value = numAyah;
                cmd.Parameters.Add("@langCode", SqlDbType.VarChar).Value = langCode;
                SqlDataReader reader = null;
                reader = cmd.ExecuteReader();

                //outArab = "''";
                string sura = "";
                string ayah = "";
                string ayahTrans = "";

                while (reader.Read())
                {
                    surah = Convert.ToInt32(reader["CurrSurah"].ToString());
                    ayahFrom = Convert.ToInt32(reader["CurrAyah"].ToString());

                    sura = reader["surahId"].ToString();
                    ayah = reader["ayahId"].ToString();
                    ayahTrans = reader["ayahTextEn"].ToString().Replace("'", "`");

                    dicTrans.Add(sura + "_" + ayah, ayahTrans);
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

        public void Editor_LoadProgress(int EditorID, ref int yourwords, ref int yourGrandTotal, ref int yourpercent, ref int siteprogress, string langcode)
        {
            string connStr = ConfigurationManager.ConnectionStrings["ConnectionStr"].ToString();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand();
                conn.Open();
                cmd = new SqlCommand("[EditorSP_GetEditorProgress]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@EditorID", SqlDbType.Int).Value = EditorID;
                cmd.Parameters.Add("@langcode", SqlDbType.VarChar).Value = langcode;
                SqlDataReader reader = null;
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    yourwords = Convert.ToInt32(reader["wordsUniq"].ToString());
                    yourGrandTotal = Convert.ToInt32(reader["wordsGrand"].ToString());
                    yourpercent = Convert.ToInt32(reader["wordsGrand"].ToString()) * 100 / 77429;
                    siteprogress = Convert.ToInt32(reader["wordsSiteGrand"].ToString()) * 100 / 77429;
                }
            }
        }


    }
}