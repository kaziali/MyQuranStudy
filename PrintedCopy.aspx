<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintedCopy.aspx.cs" Inherits="QuranStudy.PrintedCopy" %>

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
     
<body <%if(!ShowFullVersion){ %> class="printerVer" <%} %> >
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
     <%if(ShowFullVersion){ %> 
    <!--#include virtual="inc/header.html" --><%} %>
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
         <%if(ShowFullVersion){ %>
        <div style="font-family: Arial; font-size: 95%;margin-left:10px">Learn Quran -  word for word meaning in English.</div>
        <div style="font-family: Arial; font-size: 10pt; height: 25px; padding: 10px 0 10px 100px; margin: 0px 0 0 0px; border: 0px solid red; background-color: #aaa">
            
            <%--<div style="zzposition: absolute;top: 32px; left: 430px;" id="outerFilterDiv" >--%>
            <input name="filterTextField" type="text" id="filterTextField" tabindex="2"  style="width: 90px;
    top: 1px; left: 1px; z-index: 2;border:none; color:rgba(0,0,128,0.5)" onkeyup="SearchSurah()" onmouseup="ClearDefaultVal()"  />
        <form id="frmSurahSel" name="frmSurahSel" style="display:inline">
           <select class="select-editable" name="Surah" id="Surah" onchange="SetAyatDD();" style="font-family: Arial; font-size: 10pt; font-weight: normal; width: 150px;"></select>
               <%-- </div>--%>
   
            <select name="AyahFrom" id="AyahFrom" onchange="$('[name=Page]').val('0'); return submit();" style="font-family: Arial; font-size: 10pt; font-weight: normal; width: 50px;"></select>
            
              &nbsp;  &nbsp;  &nbsp;  &nbsp; Juz:
            <select name="JuzDD" id="JuzDD" onchange="return submitJuz();" style="font-family: Arial; font-size: 10pt; font-weight: normal; width: 60px;">
                <option name="0" value="0">0</option>
                <%for(int pg=1; pg < 30; pg++){ %>
                <option name="<%=pg %>" value="<%=pg %>"><%=pg %></option>
                <%} %>
            </select>

            <input type="button" onclick="$('[name=Page]').val('0'); return submit();" name="Go" value="Go" />
             <!--<span style="float: none; padding-left: 20px">Next Page: 
            <select name="NumVerses" id="NumVerses" onchange="return submit();" style="font-family: Arial; font-size: 10pt; font-weight: normal; width: 40px;">
                <option name="5" value="5">5</option>
                <option  name="7a"  value="7">7</option>
                <option  name="10" value="10">10</option>
                <option  value="20">20</option>
                <option  value="30">30</option>
                <option value="40">40</option>
            </select>

            </span> --> 
              
             &nbsp; &nbsp;<a href="<%=prevVersesUrl %>" class="spNextPrevLinks"> Prev Page </a> &nbsp; 
            
            <span style="float: none; padding-left: 20px">  Page: 
            <select name="Page" id="Page" onchange="return submit();" style="font-family: Arial; font-size: 10pt; font-weight: normal; width: 60px;">
                <option name="0" value="0">0</option>
                <%for(int pg=1; pg < 605; pg++){ %>
                <option name="<%=pg %>" value="<%=pg %>"><%=pg %></option>
                <%} %>
            </select>
                
            </span>
             &nbsp; <a href="<%=nextVersesUrl %>" class="spNextPrevLinks"> Next Page</a> 
                <%-- <img src="images/next.png" width="30" style="padding:0;margin-bottom:-4px" border="0" />--%>

           

            <span style="margin-left:30px"><input type="checkbox" value="0" id="chkRuku" />Indo-Pak Font</span>
            <span>  <a href="Study.aspx?Surah=<%=surah%>&AyahFrom=<%=ayahFrom%>" class="spNextPrevLinks">[Old Study view, by verses]</a></span>

<%--   debug and print helpers          &nbsp;  &nbsp; // <a href="< %=currVersesUrl % >&print=true">Print</a>, 
            <a href="< %=currVersesUrl % >&linebr=true">LineBr</a>, --%>
    </form> 
        </div> <%} %>
        <%if(ShowFullVersion){ %><span class="font1" style="margin:4px; padding-left:50px;">Get meaning of each word - Click on a word for more.</span>
<div class="fb-like" data-href="http://www.myquranstudy.com/"  data-layout="standard" data-action="like" data-show-faces="false" data-share="true"></div><%} %>
      <%if(!ShowFullVersion){ %> <div  class="pagenum mushafPrinterPageNum"><span style="float:left">The Noble Quran</span>  <span style="float:right">Page:  <%= PageNum%></span></div><%} %>
    <div dir="rtl">
      
        <div class="mainLeft <%=mushafCss %> lineGap">
            <%=outhtml %>
        </div>
         <div id="re-login-div" style="visibility:hidden" dir="ltr">
             <h3>Please log in to track progress, insha'Allah</h3>
             You have not logged in or your session may have expired.<br /><br />
             [For testing purpose use - <br />
             Login: guest@myQuranStudy.com, Password: pass ]<br /><br />
             User Email:<input type="text"id="uname" value="" size="35" /><br />
             Password: &nbsp;&nbsp;<input type="password" id="pword" value="" size="36" /><br />
             <input id="btnReLogin" type="submit" value="Login" name="login" style="width:70px;color:blue;margin: 5px 5px 5px 150px;"  />
             <input id="cancel" type="button" value="Cancel" style="width:70px;color:blue;"  onclick="javascript:$('#close_x').click();  $('#re-login-div').css({ 'visibility': 'hidden' });" />
<%--             &nbsp;&nbsp;<span onclick="javascript:$('#close_x').click();  $('#re-login-div').css({ 'visibility': 'hidden' });" style="cursor:pointer;color:blue;margin: 5px 5px 5px 100px; font-size:large">Cancel</span>--%>

         </div>
        <div class="generalNoteTitle mushafAdjuster" dir="ltr">
             Take notes: <span id="saveGeneralNote">Save notes</span> <span class="spGenSavedMsg"></span><br />
             <textarea id="txtGeneralNote" style="width:100%;height:300px" maxlength="5000"><%=GeneralNote %></textarea>
            
        </div>
         <%--<div class="mainLeftEnTitle mushafAdjuster">
           English Translation - Sahih International
        </div>
          
        <div class="mainLeftEn mushafAdjuster">
             <%=sAllEngTrans %>
        </div>--%>
         <%if(ShowFullVersion){ %> 
        <div class="mainRight" style="margin-top:150px; zzvertical-align:central">
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
           <%-- 
            Add Line Break: <span  id="dvLineBreak" class="actionlink wordstate10">after this word </span>  <br />
            <span  id="dvLineBreakAfterNum" class="actionlink wordstate10">after ayah num |</span>   
            <span  id="dvLineBreakClear" class="actionlink wordstate10">Clear Line Break </span>  
            <hr />--%>
             My notes: <span id="saveLemmaNote">Save</span> <span class="spSavedMsg"></span><br />
                        
                <textarea id="txtLemmaNote" style="width:250px;height:40px" maxlength="50"></textarea>
             
            <hr /><span class="arRoot" style="direction:rtl;font-size:140%"></span> - 
            <span>Dictionary(Lane's Lexicon)</span>
             (<span id="rootLetters"></span>):<br /><span id="lexPage">00</span>

        </div>
        
        <div id="sectAyah">
           
            <b>Mark this ayah as:</b><br /><span id="spAyahMemod" class="actionlink wordstate11">Memorize</span> | <span id="spAyahUnderlined" class="actionlink wordstate12">Grammar Check</span>  | 
            <span id="spAyahHilited" class="actionlink wordstate13">Highlighted</span> | <span id="spAyahBoxed" class="actionlink wordstate14">&nbsp;Boxed </span>  | <span id="spAyahUnmarked" class="actionlink wordstate0">&nbsp;Unmark </span>
            <br />or <span  id="spAllKnown" class="actionlink wordstate1">Mark All words Known (this ayah)</span>


            <!-- thanks to flash-mp3-player.net/players/maxi/documentation/ for free mp3 player. May Allah guide them -->

<%--            http://www.handcutdesigns.com/21-free-music-players-for-your-website/--%>
            <div id="dvPlayer" style="margin-top:10px">
                Play this verse <span id="currAudioVerse"></span><br />
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
        <%} %>
    </div>
</div>
    <script type="text/javascript">
        var jSurah = <%= surah%>;
        var jAyahFrom = <%= ayahFrom%>;
        var jNumAyah = <%= numAyah%>;
        var jPageNum = <%= PageNum%>;

        
    </script>
         <script src="js/studymore.js"></script> 
     
     <%if(ShowFullVersion){ %> 
     <!--#include virtual="inc/footer-lite.html" --> <%} %>
</body>
</html>
