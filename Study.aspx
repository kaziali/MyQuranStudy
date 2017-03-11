<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Study.aspx.cs" Inherits="QuranStudy.Study" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Learn Quran online, translation of quran word for word</title>
    <meta name="keywords" content="quran, quran learning, online quran learning, translation of quran, learn quran online, quran word for word, quran word by word, quran meaning, quran study, my quran, learn quran, quran english, quran words, koran, coran, qur'an, Arabic roots, quran tracker" />
    <meta name="description" content="Study Quran, learn meaning of each word, underline and highlight words - track with progress tracker." />
    <script src="http://code.jquery.com/jquery-1.9.1.js"></script>
    <script src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
    <script src="js/main.js"></script>
    <script src="js/quran-metadata.js"></script>
    <link href="css/main.css" type="text/css" rel="stylesheet" />
    <style type="text/css">
        
  .select-editable { position:relative; background-color:white; border:solid grey 1px;  width:120px; height:18px; }
  .select-editable select { position:absolute; top:0px; left:0px; font-size:14px; border:none; width:120px; margin:0; }
  .select-editable input { position:absolute; top:0px; left:0px; width:100px; padding:1px; font-size:12px; border:none; }
  .select-editable select:focus, .select-editable input:focus { outline:none; }
     </style>
</head>
<body>
   
    <!-- ref: 
     Response.Write(Request.UrlReferrer.ToString());
    -->
<%--    <div style="position: absolute; top: 32px; left: 430px;" id="outerFilterDiv">
        <input name="filterTextField" type="text" id="filterTextField" tabindex="2" style="width: 140px; position: absolute; top: 1px; left: 1px; z-index: 2; border: none;" />
        <div style="position: absolute;" id="filterDropdownDiv">
            <select name="filterDropDown" id="filterDropDown" tabindex="1000"
                onchange="DropDownTextToBox(this,'filterTextField');" style="position: absolute; top: 0px; left: 0px; z-index: 1; width: 165px;">
                <option value="-1" selected="selected" disabled="disabled">11</option>
                <option value="-1" selected="selected" disabled="disabled">22</option>
                <option value="-1" selected="selected" disabled="disabled">-- Select Column Name --</option>
            </select>
        </div>
    </div>--%>

    <!--#include virtual="inc/header.html" -->
    <div id="dvOverlay"></div>
     

    <script type="text/javascript">
        var  WordHoverDiagHeight = "40";
       <%=jsArrayTrans%>
        //var UserID = < %=UserID % >;
        var jsArrayIDs = new Array(<%=jsArrayIDs%>);
        var jsArrayNames = new Array(<%=jsArrayNames%>);
        var jsArrayAyat = new Array(<%=jsArrayAyat%>);
        var numAyah = 7;
    </script>
    <script src="js/study.js"></script>

    <div>
        <div style="font-family: Arial; font-size: 95%;margin-left:10px">Learn Quran -  word for word meaning in English.</div>
        <div style="font-family: Arial; font-size: 10pt; height: 25px; padding: 10px 0 10px 100px; margin: 0px 0 0 0px; border: 0px solid red; background-color: #aaa">
            Surah/Ayah:
            <%--<div style="zzposition: absolute;top: 32px; left: 430px;" id="outerFilterDiv" >--%>
            <input name="filterTextField" type="text" id="filterTextField" tabindex="2"  style="width: 90px;
    top: 1px; left: 1px; z-index: 2;border:none; color:rgba(0,0,128,0.5)" onkeyup="SearchSurah()" onmouseup="ClearDefaultVal()"  />
        <form id="frmSurahSel" name="frmSurahSel" style="display:inline">
           <select class="select-editable" name="Surah" id="Surah" onchange="SetAyatDD();" style="font-family: Arial; font-size: 10pt; font-weight: normal; width: 150px;"></select>
               <%-- </div>--%>
   
            <select name="AyahFrom" id="AyahFrom" onchange="return submit();" style="font-family: Arial; font-size: 10pt; font-weight: normal; width: 50px;"></select>
            <input type="button" onclick="return submit();" name="Go" value="Go" />
            <span style="float: none; padding-left: 20px">Number of verses: 
            <select name="NumVerses" id="NumVerses" onchange="return submit();" style="font-family: Arial; font-size: 10pt; font-weight: normal; width: 40px;">
                <option value="5">5</option>
                <option value="7">7</option>
                <option value="10">10</option>
                <option value="20">20</option>
                <option value="30">30</option>
                <option value="40">40</option>
            </select>

            </span> <a href="<%=nextVersesUrl %>" id="spNextVerses"> Next <img src="images/next.png" width="30" style="padding:0;margin-bottom:-4px" border="0" /></a>
            <span style="margin-left:30px"><input type="checkbox" value="0" id="chkRuku" />Indo-Pak Font</span>
            <span> <a href="PrintedCopy.aspx?Surah=<%=surah%>&AyahFrom=<%=ayahFrom%>" class="spNextPrevLinks">[Read as Printer copy (Mushaf)]</a></span>
    </form> 
        </div>
    <span class="font1" style="margin:4px; padding-left:50px;">Get meaning of each word - Click on a word for more.</span>
<div class="fb-like" data-href="http://www.myquranstudy.com/"  data-layout="standard" data-action="like" data-show-faces="false" data-share="true"></div>
    <div dir="rtl">
        <div class="mainLeft">
            <%=outhtml %>
        </div>
         <div class="mainLeftEnTitle">
           English Translation - Sahih International
        </div>
        
        <div class="mainLeftEn">
             <%=sAllEngTrans %>
        </div>
        <div class="mainRight">
            <span class="myProg">My Progress</span><br />
            Name: <%=UserName %><br />
            <br /><span class="countTitle">Known Word count</span><br /> As found in Quran:<br /> <span id="spAgKnown"> <%=AggrtKnown %> (<%=AggrtKnown*100/77429 %>%)</span><br />
            <br /><span class="countTitle">Unique word count</span><br />
            Known:<span id="spUniqKnown"> <%=UniqueKnown %></span><br />
            Underlined:<span id="spUniqUnderline">  <%=UniqueUnderlined %></span><br />
            Highlighted:<span id="spUniqHilited">  <%=UniqueHilited %></span><br />
            Boxed:<span id="spUniqBoxed">  <%=UniqueBoxed %> </span><br />


    <div id="dialog" dir="ltr" style="visibility: hidden">
        <a id="close_x" class="close sprited" href="#">close</a>
        <div id="sectWord">
            <div id="mean"></div>
            <div id="lemma"></div>
            <hr />
            <b>Mark this word as:</b><br /> <span id="dvKnown" class="actionlink wordstate1">Known</span> | <span id="dvUnderlined" class="actionlink wordstate2">Underlined</span>
             | <span id="dvHilited" class="actionlink wordstate3">Highlighted</span> | <span id="dvBoxed" class="actionlink wordstate4">&nbsp;Boxed </span>
            <br />or <span  id="dvUnmarked" class="actionlink wordstate0"><b>Unmark It</b></span> 
            <hr />
             My notes:<span id="saveLemmaNote">Save</span> <span class="spSavedMsg"></span>
            <div>              
                <textarea id="txtLemmaNote" style="width:240px;height:30px" maxlength="50"></textarea>
            </div>
            <hr /><span class="arRoot" style="direction:rtl;font-size:140%"></span> - 
            <span>Dictionary(Lane's Lexicon)</span> 
             (<span id="rootLetters"></span>):<br /><span id="lexPage">00</span>

        </div>
        <div id="sectAyah">
           
            <b>Mark this ayah as:</b><br /><span id="spAyahMemod" class="actionlink wordstate11">&nbsp;Memorize&nbsp;</span> | <span id="spAyahUnderlined" class="actionlink wordstate12">&nbsp;Grammar Check&nbsp;</span>  | 
            <span id="spAyahHilited" class="actionlink wordstate13">&nbsp;Highlighted&nbsp;</span> | <span id="spAyahBoxed" class="actionlink wordstate14">&nbsp;Boxed&nbsp;</span>  | <span id="spAyahUnmarked" class="actionlink wordstate0">&nbsp;Unmark </span>
            <br />or <span  id="spAllKnown" class="actionlink wordstate1">Mark All words Known (this ayah)</span>


            <!-- thanks to flash-mp3-player.net/players/maxi/documentation/ for free mp3 player. May Allah guide them -->

<%--            http://www.handcutdesigns.com/21-free-music-players-for-your-website/--%>
            <div id="dvPlayer" style="margin-top:10px">
                Play this verse  <span id="currAudioVerse"></span><br />
                <%--<object id="flPlayerObj" type="application/x-shockwave-flash" data="player_mp3_multi.swf" width="200" height="50">
    <param name="movie" value="player_mp3_multi.swf" />
    <param name="bgcolor" value="#ffffff" />
    <param name="FlashVars" value="mp3=/audio/002002.mp3|/audio/002003.mp3&amp;height=50" />
</object>--%>
                <audio id="audioayah" src="/audio/002000.mp3" controls style="width:280px;height:30px" />
                <%--<object id="flPlayerObj" type="application/x-shockwave-flash" data="player_mp3_maxi.swf" width="200" height="20">
                    <param name="movie" value="player_mp3_maxi.swf" />
                    <param name="bgcolor" value="#ffffff" />
                    <param name="FlashVars" value="mp3=/audio/002000.mp3&amp;showstop=1&amp;showvolume=1&amp;bgcolor1=888888&amp;bgcolor2=999999" />
                </object> --%>
                
                <br />(Reciter: Mishary Rashid)
            </div> 
             My notes:<span id="saveAyahNote">Save</span> <span class="spSavedMsg"></span>
            <div>              
                <textarea id="txtAyahNote" style="width:240px;height:30px" maxlength="50"></textarea>
            </div>
            <span id="tafsirPage"></span>

        </div>
       <%-- <a href="http://localhost:62958/default.aspx" target="_blank">test1</a> <br />
        <a href="http://www.msn.com/" target="test">test</a>
        <div onclick="OpenInNewTab('http://localhost:62958/default.aspx');">Something To Click On</div>--%>
    </div>

            <div id="diagHover" dir="ltr" style="visibility: hidden">
                <div id="meanHover"></div>
            </div>
        </div> 
    </div>
</div>
    <script type="text/javascript">
        var jSurah = <%= surah%>;
        var jAyahFrom = <%= ayahFrom%>;
        var jNumAyah = <%= numAyah%>;

        
    </script>
         <script src="js/studymore.js"></script>


     <!--#include virtual="inc/footer.html" --> 
</body>
</html>
