<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyMushafDownload.aspx.cs" Inherits="QuranStudy.MyMushafDownload" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Learn Quran online, translation of quran word for word</title>
    <meta name="keywords" content="quran, quran learning, online quran learning, translation of quran, learn quran online, quran word for word, quran word by word, quran meaning, quran study, my quran, learn quran, quran english, quran words, koran, coran, qur'an, Arabic roots, quran tracker" />
    <meta name="description" content="Study Quran, learn meaning of each word, underline and highlight words - track with progress tracker." />
    <script src="js/jquery-1.9.1.js"></script>
    <script src="js/jquery-ui.js"></script>
    <script src="js/main_dnld.js"></script>
    <script src="js/metadata_dnld.js"></script>
    <link href="css/main_dnld.css" type="text/css" rel="stylesheet" />
    <style type="text/css">
        
  .select-editable { position:relative; background-color:white; border:solid grey 1px;  width:120px; height:18px; }
  .select-editable select { position:absolute; top:0px; left:0px; font-size:14px; border:none; width:120px; margin:0; }
  .select-editable input { position:absolute; top:0px; left:0px; width:100px; padding:1px; font-size:12px; border:none; }
  .select-editable select:focus, .select-editable input:focus { outline:none; }
   div.mainLeft .ayahNum {
            background-image: url(images/ayah.png) !important;
    }

    </style>
</head>
     
<body <%if(!ShowFullVersion){ %> class="printerVer" <%} %> >
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
     
    <!--#include virtual="inc/header_dnld.html" --> 
    <div id="dvOverlay"></div>
     

    <script type="text/javascript">
        var  WordHoverDiagHeight = "50";
        <%=jsArrayTrans%>       
       
    </script>
        <script src="js/study_dnld.js"></script>

    <div>
         <%if(ShowFullVersion){ %>
        <div style="font-family: Arial; font-size: 95%;margin-left:10px">Word for word meaning in English.</div>
        <div style="font-family: Arial; font-size: 10pt; height: 25px; padding: 10px 0 10px 100px; margin: 0px 0 0 0px; border: 0px solid red; background-color: #aaa">
            
            <%--<div style="zzposition: absolute;top: 32px; left: 430px;" id="outerFilterDiv" >--%>
           <%-- <input name="filterTextField" type="text" id="filterTextField" tabindex="2"  style="width: 90px;
    top: 1px; left: 1px; z-index: 2;border:none; color:rgba(0,0,128,0.5)" onkeyup="SearchSurah()" 
                onmouseup="ClearDefaultVal()"  />--%>

        <form id="frmSurahSel" name="frmSurahSel" style="display:inline;margin: 0px 0 0 40px; ">
           <select class="select-editable" name="Surah" id="Surah" onchange="SetAyatDD();" style="font-family: Arial; font-size: 10pt; font-weight: normal; width: 150px;"></select>
               <%-- </div>--%>
   
            <select name="AyahFrom" id="AyahFrom"
                onchange="$('[name=Page]').val('0'); SubmitGo();" style="font-family: Arial; font-size: 10pt; font-weight: normal; width: 50px;"></select>
            <input type="button" onclick="$('[name=Page]').val('0'); SubmitGo();" name="Go" value="Go" />
             &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;<a href="<%=prevVersesUrl %>" class="spNextPrevLinks">Prev Page</a> &nbsp; 
            
            <span style="float: none; padding-left: 10px">Page: 
            <select name="Page" id="Page" 
                onchange="window.document.location.href=this.options[this.selectedIndex].value;" 
                style="font-family: Arial; font-size: 10pt; font-weight: normal; width: 60px;">
               <%-- <%for(int pg=1; pg < 605; pg++){ %>
                <option name="<%=pg %>" value="<%=pg %>.htm"><%=pg %></option>
                <%} %>--%>
            </select>
                
            </span>
             &nbsp;&nbsp; <a href="<%=nextVersesUrl %>" class="spNextPrevLinks">Next Page</a> 
                <%-- <img src="images/next.png" width="30" style="padding:0;margin-bottom:-4px" border="0" />--%>
            <%--<span style="margin-left:30px"><input type="checkbox" value="0" id="chkRuku" />Show Ruku marks</span>
            
             &nbsp;  &nbsp; // <a href="<%=currVersesUrl %>&print=true">Print</a>, 
            <a href="<%=currVersesUrl %>&linebr=true">LineBr</a>, --%>
    </form> 
             
        </div> <%} %>       

<%--  <div  class="pagenum mushafPrinterPageNum"><span style="float:none">The Noble Quran</span>  <span style="float:right">Page:  <%= PageNum%></span></div>--%>
    <div dir="rtl">
      
        <div class="mainLeft <%=mushafCss %> lineGap">
            <%=outhtml %>
        </div>
         <div class="mainLeftEnTitle mushafAdjuster">
           English Translation - Sahih International
        </div>
          
        <div class="mainLeftEn mushafAdjuster">
             <%=sAllEngTrans %>
        </div>
         <%if(ShowFullVersion){ %> 
        <%--<div class="mainRight">
            <span class="myProg">My Progress</span><br />
            Name: <%=UserName %><br />
            <br /><span class="countTitle">Known Word count</span><br /> As found in Quran:<br /> <span id="spAgKnown"> <%=AggrtKnown %> (<%=AggrtKnown*100/77429 %>%)</span><br />
            <br /><span class="countTitle">Unique word count</span><br />
            Known:<span id="spUniqKnown"> <%=UniqueKnown %></span><br />
            Underlined:<span id="spUniqUnderline">  <%=UniqueUnderlined %></span><br />
            Highlighted:<span id="spUniqHilited">  <%=UniqueHilited %></span><br />
            Boxed:<span id="spUniqBoxed">  <%=UniqueBoxed %> </span><br /> 
        </div> --%>
          
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
             
            <hr />Root: <span class="arRoot" style="direction:rtl;font-size:140%"></span> 
           <%-- <span>Lane's Lexicon</span> 
             (<span id="rootLetters"></span>):<span id="lexPage">00</span>--%>

        </div>
        <div id="sectAyah">
           
            <b>Mark this ayah as:</b><hr /><span id="spAyahMemod" class="actionlink wordstate11">Memorize</span> | <span id="spAyahUnderlined" class="actionlink wordstate12">Grammar Check</span>  | 
            <span id="spAyahHilited" class="actionlink wordstate13">Highlighted</span> | <span id="spAyahBoxed" class="actionlink wordstate14">&nbsp;Boxed </span>  | <span id="spAyahUnmarked" class="actionlink wordstate0">&nbsp;Unmark </span>
            <br />or <span  id="spAllKnown" class="actionlink wordstate1">Mark All words Known (this ayah)</span>
<hr />
            
             My notes:<span id="saveAyahNote">Save</span> <span class="spSavedMsg"></span>
            <div>              
                <textarea id="txtAyahNote" style="width:240px;height:60px" maxlength="50"></textarea>
            </div>
             <span id="tafsirPage"></span>

        </div>
      
    </div>

            <div id="diagHover" dir="ltr" style="visibility: hidden">
                <div id="meanHover"></div>
            </div>
        
        <%} %>
    </div>
</div>
    <script type="text/javascript">
        var jSurah = <%= surah%>;
        var jAyahFrom = <%= ayahFrom%>;
        var jPageNum = <%= PageNum%>;

        
    </script>
         <script src="js/studymore_dnld.js"></script> 
     
   
     <!--#include virtual="inc/footer_dnld.html" -->  
</body>
</html>
