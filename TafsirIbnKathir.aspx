<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TafsirIbnKathir.aspx.cs" Inherits="QuranStudy.TafsirIbnKathir" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Tafsir Ibn Kathir</title>
    <link rel="stylesheet" type="text/css" href="css_tafsir/normalize.css">
    <link rel="stylesheet" type="text/css" href="css_tafsir/demo.css">
    <link rel="stylesheet" type="text/css" href="css_tafsir/component.css">
        <link href="css/main.css" type="text/css" rel="stylesheet" />

    <!--[if IE]>
    <script src="http://html5shiv.googlecode.com/svn/trunk/html5.js"></script>
    <![endif]-->
</head>
<body>
     <!--#include virtual="inc/header.html" -->
   <%-- <form id="form1" runat="server">
        <div>--%>
             
    <div class="container">
        <!-- Top Navigation --> 

        <section class="color-3">
          <%--  <b>Tafsir Ibn Kathir</b>--%>
            <nav class="nav-circlepop">                 
                <a class="prev" href="/TafsirIbnKathir.aspx?fn=<%=pdfPagePrev %>">
                    <span class="icon-wrap"></span><b>Prev</b>
                </a>
                <div class="nav-iframe"><iframe id="pdfframe" src="<%= ifrm_src %>" style="width:80%;" height="820px"></iframe></div>
                <a class="next" href="/TafsirIbnKathir.aspx?fn=<%=pdfPageNext %>">
                    <span class="icon-wrap"></span><b>Next</b>
                </a>
                <%--<a id="nextbtn" class="next" href="#">
                    <span class="icon-wrap"></span>
                </a>--%>
            </nav>
        </section>
      
    </div><!-- /container -->
    <script>
        var el = document.getElementById("nextbtn");
            el.addEventListener('click',
                function (ev)
                {
                    //ev.preventDefault();
                    //location.href = 'http://localhost:81/TafsirIbnKathir.aspx?fn=001_38.pdf';
                    document.getElementById("pdfframe").src = 'http://localhost:81/tafsir-ibn-kathir/splitPdf/001_38.pdf';
                });
         
			//// For Demo purposes only
			//[].slice.call( document.querySelectorAll('nav > a') ).forEach( function(el) {
			//    el.addEventListener('click',
            //        function (ev)
            //        {
            //            //ev.preventDefault();
            //            //location.href = 'http://localhost:81/TafsirIbnKathir.aspx?fn=001_38.pdf';
            //        });
			//} );
    </script>

<%--            <div style="width: 100%;">
                <div style="float: left; width: 200px;"><span style='color: #bbb; font-weight: bold; font-size: 24px'><%=pagePrev %></span></div>
                <div style="float: left; width: 800px; margin: 0 10px 0 20px"><iframe id="pdfframe" src="<%= ifrm_src %>" style="width:700px"></iframe></div>
                <div style="float: left; width: 200px;"> <span style='color: #bbb; font-weight: bold; font-size: 24px'><%=pageNext %></span></div>
                <br style="clear: left;" />
            </div> 
            <script language="javascript">
                pdfframe.height = screen.height - 210;
            </script>--%>
        </div>
    </form>
</body>
</html>
