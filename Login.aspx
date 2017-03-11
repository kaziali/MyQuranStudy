<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="QuranStudy.Login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Study Quran, learn word for word meanings, track progress</title>
    <meta name="keywords" content="quran, quran learning, online quran learning, learn quran online, quran word for word, quran word by word, quran meaning, quran study, my quran, learn quran, quran english, quran words, koran, coran, qur'an, Arabic roots, quran tracker" />
    <meta name="description" content="Study Quran, learn meaning of each word, underline and highlight words - track with progress tracker." />
    <script src="http://code.jquery.com/jquery-1.9.1.js"></script>
    <script src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
    <link href="css/main.css" type="text/css" rel="stylesheet" />
    <style type="text/css">
    </style>
</head>
<body>
   <!--#include virtual="inc/header.html" -->
 
<%--     <h3 style="text-align:center">Sign up! No payment required, only your duas and spreading the word.</h3>--%>
   
   <div class="divCont"> Login to track your progress as you continue your efforts in learning the meaning of Quranic words. 
        For every word you will find its root words, and Lane's Lexicon reference for further research. </div>
    
    <div>
    <div id="divSignupLeft"style="display:inline-block;float:left;width:60%; border:0px solid red;">

     <div style="background: #ccc; padding-top: 20px; padding-left: 12px; border: solid 6px #999; width: 370px; margin-left: 220px; margin-top: 18px;">
        <form id="frmLogin" name="frmLogin" action="Login.aspx" method="post">
            <input type="hidden" name="mode" value="login" />
            <input type="hidden" name="retPage" value="<%=retPage %>" />
            <table>
                <tr>
                    <td align="right">User Email:</td>
                    <td>
                        <input type="hidden" name="mode" id="mode" value="loginClicked" />
                        <input type="text" size="35" name="uemail" maxlength="90" id="Text1" value=""></td>
                </tr>
                <tr>
                    <td align="right">Password:</td>
                    <td>
                        <input type="password" size="36" name="pword" maxlength="90" id="pword" value=""></td>
                </tr>
                <tr>
                    <td align="right">&nbsp;</td>
                    <td align="right">
                        <input type="submit" value="  Login  " name="login" size="35" id="Submit2">
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center"><a href="signup.aspx">Sign up! - Free</a> | <a href="/forgotpass.aspx">Forgot Password?</a>
                    </td>
                </tr>
            </table>       

        </form>
        <br />
    </div>

        </div>
  
<%-- Facebook signup - 7/26/2016 capture email address then enable
   <div id="divSignupRight"style="display:inline-block;border:0px solid red;">
        <h3>OR, Log in using Facebook</h3>
      <div id="fbContinue"></div><br /> 
    <div id="fb-root"></div>
<script type="text/javascript">
    window.fbAsyncInit = function () {
        FB.init({
            appId: '534441243342482', // App ID
            status: true, // check login status
            cookie: true, // enable cookies to allow the server to access the session
            xfbml: true  // parse XFBML
        });

        // Additional initialization code here: below done
        FB.Event.subscribe('auth.authResponseChange', function (response) {
            //  alert('lmml');
            if (response.status === 'connected') {
                // the user is logged in and has authenticated your
                // app, and response.authResponse supplies
                // the user's ID, a valid access token, a signed
                // request, and the time the access token 
                // and signed request each expire
                 

                var uid = response.authResponse.userID;
                var accessToken = response.authResponse.accessToken; 

                // TODO: Handle the access token -- below done
                var form = document.createElement("form");
                form.setAttribute("method", 'post');
                form.setAttribute("action", '/ActionHandler.aspx');

                var typeFld = document.createElement("input");
                typeFld.setAttribute("type", "hidden");
                typeFld.setAttribute("name", 'reqType');
                typeFld.setAttribute("value", 'fblogin');
                form.appendChild(typeFld);

                var field = document.createElement("input");
                field.setAttribute("type", "hidden");
                field.setAttribute("name", 'accessToken');
                field.setAttribute("value", accessToken);
                form.appendChild(field);

                var buttn = document.createElement("input");
                buttn.setAttribute("type", "submit");
                buttn.setAttribute("name", 'Continue with Facebook');
                buttn.setAttribute("value", "Launch Quran Study using Facebook");
                form.appendChild(buttn);

                var lbl = "Already logged in to Facebook<br>";
                $('#fbContinue').css({ 'height': '60px' });
                $('#fbContinue').html(lbl+form.outerHTML);
               // document.body.fbContinue.appendChild(form);
                //form.submit();

            } else if (response.status === 'not_authorized') {
                // the user is logged in to Facebook, 
                // but has not authenticated your app
                // document.location.href = "http://www. .com";
            } else {
                // the user isn't logged in to Facebook.
                var form = document.createElement("form");
                form.setAttribute("method", 'post');
                form.setAttribute("action", '/login.aspx?lo=out');
                document.body.appendChild(form);
                form.submit();
            }
        });
    };

    // Load the SDK Asynchronously
    (function (d) {
        var js, id = 'facebook-jssdk', ref = d.getElementsByTagName('script')[0];
        if (d.getElementById(id)) { return; }
        js = d.createElement('script'); js.id = id; js.async = true;
        js.src = "//connect.facebook.net/en_US/all.js";
        ref.parentNode.insertBefore(js, ref);
    }(document));
</script>
    <div class="fb-login-button" data-scope="basic_info" data-max-rows="2" data-size="xlarge" data-show-faces="false" data-auto-logout-link="true"></div>
              <!-- later:  data-scope="basic_info,email" -->
       <br /><br /> 
        
    </div>
--%>
</div>
    <div style="text-align:center;color:red">
    <%=loginMsg %>
</div>

<%--     <div id="postToFBWall">Post to FB </div>
        <script>
           

            $('#postToFBWall').click(function (event) {
                FB.ui(
                  {
                      method: 'feed',
                      name: 'Quran Learning word for word',
                      link: 'http://www.myquranstudy.com/',
                      picture: 'http://www.myquranstudy.com/images/logo_221bg_154_fb.png',
                      caption: 'Get meaning of each word - Underline words, track progress!',
                      description: 'Quran meaning word for word. Free sign up!',
                      message: 'Quran learning made easy, InshaAllah!'
                  },
                  function (response) {
                      if (response && response.post_id) {
                          alert('Post was published.');
                      } else {
                          alert('Post was not published.');
                      }
                  }
                );
            });
        </script>
     --%>
   
     <!--#include virtual="inc/footer.html" --> 

</body>
</html>

 