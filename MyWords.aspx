<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyWords.aspx.cs" Inherits="QuranStudy.MyWords" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Study Quran, learn word for word meanings, track progress</title>
    <meta name="keywords" content="quran, quran learning, online quran learning, learn quran online, quran word for word, quran word by word, quran meaning, quran study, my quran, learn quran, quran english, quran words, koran, coran, qur'an, Arabic roots" />
    <meta name="description" content="Study Quran, learn meaning of each word, underline and highlight words - track your progress." />
    <script src="http://code.jquery.com/jquery-1.9.1.js"></script>
    <script src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
    <script src="js/main.js"></script>

    <link href="css/main.css" type="text/css" rel="stylesheet" />
    <style type="text/css">
    </style>
</head>
<body>
   <%-- <table cellpadding="0" cellmargin="0" style="padding:0px;margin:0px;width:100%;border:1px red solid;">
        <tr><td> --%>          
   <!--#include virtual="inc/header.html" -->
      <%--</td></tr>
         <tr><td>  --%>
    <hr />
     <script type="text/javascript">
         $(document).ready(function () {
             $(".moreAyat").click(function (event) {
                 var details = $(event.target).data('word_all'); //HTML5 only? test!
                 //alert(details);
                 //var arrWord = details.split("|");
                 //var meaning = arrWord[0].replace('`', '\'');;
                 //var lemma = arrWord[1].replace('"', '\'');
                 //var lemmaID = arrWord[2];
                 //var root = arrWord[3];
                 //var arabicWord = arrWord[4];
                 //var lexpages = arrWord[5].split(",");
                 GetLemmaAyat(details, event);
                 return;
             });
        
         function GetLemmaAyat(lemmaID,event) {
            $.ajax({
                url: "GetHandler.aspx?reqType=getLemmaAyat&lemID=" + lemmaID,
                async: false,
                success: function (result) {
                    if (result.indexOf("SUCCESS|") > -1) {
                        $(event.target).siblings('.moreAyatSib').html(result.replace('SUCCESS|', ''));
                        $(event.target).siblings('.moreAyatHide').css('visibility', 'visible');
                        $(".moreAyatHide").click(function (event) {
                            $(event.target).siblings('.moreAyatSib').html('');
                        });
                    }
                    else
                        $(event.target).html('not found');

                }
            });
         }
         });
        

     </script>

   
    <%if(UserID > 0){ %>
    <div id="wordTypesLinks">       
        Show: <a href="/mywords.aspx?type=1">Known</a>&nbsp;&nbsp;|&nbsp;&nbsp;
        <a href="/mywords.aspx?type=2">Underlined</a>&nbsp;&nbsp;|&nbsp;&nbsp;
        <a href="/mywords.aspx?type=3">Highlited</a>&nbsp;&nbsp;|&nbsp;&nbsp;
        <a href="/mywords.aspx?type=4">Boxed</a>&nbsp;&nbsp;  
    </div>
    <div id="wordTypesTitle">My <%=WordTypeName %> words - <a href="/mywords.aspx?type=<%=WordType.ToString() %>&Shuffle=true"><span class="shuffleLink">[shuffle words]</span></a></div>
     <%} %>
    <div class="mywords">
            <%=outhtml %>
        </div>
    <div class="pageNumerbs"> 
   <%if (Shuffle == false){  %>
      
        Pages: 
      <%for (int i = 1; i <= TotalPages; i++) {%>
         
              <%if( i == PageNum){ %>
                 <%=i %><%if (i < TotalPages) { %>,  <%}  %>
              <%}else{ %>
                    <a href="/mywords.aspx?type=<%=WordType.ToString() %>&pageNum=<%=i.ToString()%>"><%=i %></a><%if (i < TotalPages) { %>,  <%}  %> 
                     

              <%} %>
      <%}%>
   
    <%}else{ %>
          (Random <%=WordsPerPage.ToString() %>  of  <%=TotalRecordsCount.ToString() %> words displayed.)                
            <a href="/mywords.aspx?type=<%=WordType.ToString() %>&Shuffle=true"><span class="shuffleLink">[re-shuffle]</span></a> 

    <%} %>
   </div>      
     <!--#include virtual="inc/footer.html" --> 
 
</body>
</html>