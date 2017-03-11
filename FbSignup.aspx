<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FbSignup.aspx.cs" Inherits="QuranStudy.FbSignup" %>

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
 
    <div style="margin:50px 50px 50px 500px">
 
    <h4>One last question to help us!</h4>
    <form id="frmLogin" name="frmLogin" action="actionhandler.aspx?reqType=fbsignupques" method="post">
     <input type="hidden" name="mode" value="fbsignup">
        How did you find our website? (Friend, Google or else?)<br />
     <input type="text" size="50" name="heardFrom" maxlength="50" id="heardFrom" value=""> <br />
     <input style="font-weight:bold;font-size:88%;width:110px" type="submit" value=" Finish & Go " name="Sign Up - Free" size="35" id="Submit2">

   </form>
   <%if (IsNewFBUser == 1)
         {
             Response.Write("g-track");
         %>
       <!-- Google Code for QuranStudy-signUp Conversion Page -->
<script type="text/javascript">
    /* <![CDATA[ */
    var google_conversion_id = 1071450768;
    var google_conversion_language = "en";
    var google_conversion_format = "1";
    var google_conversion_color = "ffffff";
    var google_conversion_label = "ZcTzCN7NwAgQkJX0_gM";
    var google_remarketing_only = false;
    /* ]]> */
</script>
<script type="text/javascript" src="//www.googleadservices.com/pagead/conversion.js">
</script>
<noscript>
<div style="display:inline;">
<img height="1" width="1" style="border-style:none;" alt="" src="//www.googleadservices.com/pagead/conversion/1071450768/?label=ZcTzCN7NwAgQkJX0_gM&amp;guid=ON&amp;script=0"/>
</div>
</noscript>
 <!-- /Google Code for QuranStudy-signUp Conversion Page -->
       <%} %>
    </div>

     <!--#include virtual="inc/footer.html" --> 
        
</body>
</html>
