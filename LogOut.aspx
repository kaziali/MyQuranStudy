<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogOut.aspx.cs" Inherits="QuranStudy.LogOut" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Study Quran, learn word for word meanings, track progress</title>
    <meta name="keywords" content="quran, quran learning, online quran learning, learn quran online, quran word for word, quran word by word, quran meaning, quran study, my quran, learn quran, quran english, quran words, koran, coran, qur'an, Arabic roots" />
    <meta name="description" content="Study Quran, learn meaning of each word, underline and highlight words - track your progress." />
    <script src="http://connect.facebook.net/en_US/all.js"></script>

</head>
<body>
       <!--#include virtual="inc/header.html" -->
    <br />
    <br />
    <br />
 <h3>Sign out from Facebook</h3>
       
    <div id="fb-root"></div>
<script type="text/javascript">
    window.fbAsyncInit = function () {
        FB.init({
            appId: '534441243342482', // App ID
            status: true, // check login status
            cookie: true, // enable cookies to allow the server to access the session
            xfbml: true  // parse XFBML
        });
         
        FB.getLoginStatus(function (response) {
            if (response && response.status === 'connected') {
                FB.logout(function (response) {
                });
            }
             //logout from mQS anyways
            //var form = document.createElement("form");
            //form.setAttribute("method", 'post');
            //form.setAttribute("action", '/login.aspx?lo=out');
            //document.body.appendChild(form);
            //form.submit();
            //document.location.href = "/login.aspx?lo=out";
       });
     
    };

    //var form = document.createElement("form");
    //form.setAttribute("method", 'post');
    //form.setAttribute("action", '/login.aspx?lo=out');
    //document.body.appendChild(form);
    //form.submit();
    document.location.href = "/login.aspx?lo=out";

</script>
<%--    <div class="fb-login-button" data-max-rows="2" data-size="xlarge" data-show-faces="false" data-auto-logout-link="true"></div>--%>
  
    
     <!--#include virtual="inc/footer.html" -->

</body>
</html>
