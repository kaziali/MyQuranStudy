<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyAccount.aspx.cs" Inherits="QuranStudy.MyAccount" %>

<!DOCTYPE html>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Study Quran, learn word for word meanings, track progress</title>
    <meta name="keywords" content="quran, quran learning, online quran learning, learn quran online, quran word for word, quran word by word, quran meaning, quran study, my quran, learn quran, quran english, quran words, koran, coran, qur'an, Arabic roots" />
    <meta name="description" content="Study Quran, learn meaning of each word, underline and highlight words - track your progress." />
    <script src="http://code.jquery.com/jquery-1.9.1.js"></script>
    <script src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
    <link href="css/main.css" type="text/css" rel="stylesheet" />
    <style type="text/css">
    </style>
</head>
<body>
   <!--#include virtual="inc/header.html" -->
    <hr />
     <div style="margin:50px 0px 150px 110px;">
           <%if(UserID > 0){ %>
        Name: <%=UserName %><br />
       <%  if (Session["UserEmail"].ToString().IndexOf("@_fb_") > -1) { %> 
                (You logged in using Facebook)
       
       <%}else{ %>
         Email: <%=UserEmail %> <br />
         <a href="changepass.aspx">Change password</a>

        <%}%>
       
         
          <br /> <br /> <br />
        More content will come later, insha'Allah 
           <%}else{ %>
                  <a href="/Login.aspx">Log in to continue</a>

           <%}%>
    </div>

     <!--#include virtual="inc/footer.html" --> 

</body>
</html>
