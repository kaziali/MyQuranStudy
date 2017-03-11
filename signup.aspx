<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="signup.aspx.cs" Inherits="QuranStudy.signup" %>

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
   
      <h3 style="text-align:center">Sign up - Free! No payment required... only your duas and spreading of the word.</h3> 
    <div class="divCont2"> As a registered user, you will be able to track your progress,insha'Allah, as you continue your efforts in learning the meaning of Quranic words. 
        For every word you will find its root words, and Lane's Lexicon reference for further research.
    </div>
    <h3 style="margin-left: 40%;"><b>FREE SIGN UP</b></h3>
 
  <div>
      <div style="padding-top: 11px; padding-left: 12px; border: solid 0px #999; width: 500px; margin-left:20%;"> 
       <%=signupMsg %><br />
      </div>
       <%if (signupMsg.IndexOf("success") > -1)
         { %>
        <div class=""> 
        <span style="text-align:right; margin-left:280px; background-color:#121cd5;width:280px;padding:11px"> <a style="font-size:130%;color:white" href="Study.aspx">Continue to Quranic Word Study</a> </span>
        </div>
      <%} else { %>
  <div id="divSignupLeft"style="display:inline-block;float:left;width:60%; border:0px solid red;">
    <div style="background: #ccc; padding-top: 11px; padding-left: 12px; border: solid 5px #999; width: 500px; margin-left:20%;">
   

        <form id="frmLogin" name="frmLogin" action="signup.aspx" method="post">
            <input type="hidden" name="mode" value="login">
            <table>
                <tr>
                    <td align="right">Your Name:</td>
                    <td>
                        <input type="text" size="30" name="uname" maxlength="40" id="uname" value=""> <span class="required">(required)</span></td>
                </tr>

                <tr>
                    <td align="right">Your Email:</td>
                    <td>
                        <input type="hidden" name="postmode" value="signup" ID="Hidden1" />
                        <input type="text" size="30" name="uemail" maxlength="90" id="Text1" value="" /> <span class="required">(required)</span></td>
                </tr>
                <tr>
                    <td align="right">Password:</td>
                    <td>
                        <input type="password" size="31" name="pword" maxlength="15" id="pword" value="" /> <span class="required">(required)</span></td>
                </tr>
                <tr>
                    <td align="right">Retype Password:</td>
                    <td>
                        <input type="password" size="31" name="pwordConf" maxlength="15" id="pwordConf" value=""/> <span class="required">(required)</span></td>
                </tr>
                 <tr>
                     
                    <td colspan="2">
                     <hr />
                    </td>
                </tr>
                <tr>
                    <td align="right">How did you find us? <br />(Friend, Google, etc.)</td>
                    <td>
                        <input type="text" size="30" name="heardFrom" maxlength="50" id="heardFrom" value=""></td>
                </tr>
                <tr>
                    <td align="right">Is Arabic your native language?</td>
                    <td>
                        <select name="arabicNative"  id="arabicNative">
                            <option value=""></option>
                            <option value="Yes">Yes</option>
                            <option value="No">No</option>
                        </select>
                        <%--<span class="optional">(optional)</span>--%></td>
                </tr>
               
                <tr>
                    <td>&nbsp;</td>
                    <td>
                        <input style="font-weight:bold;font-size:111%;width:200px" type="submit" value="  Sign Up  " name="Sign Up - Free" size="35" id="Submit2">
                    </td>
                </tr>
                 <tr>
                    <td colspan="2" align="center"><div style="font:10px verdana"><br />[Note: Your email address will be kept private. We may send occasional newsletters/articles with optout option.]</div></td>
                </tr>
            </table>
        </form>
        <div style="text-align:center"><br />
         <a href="/forgotpass.aspx">Forgot Password?</a></div><br />
    </div>
    <div style="text-align:center;color:red">
     
</div>
  </div><%-- Facebook signup - 7/26/2016 capture email address then enable
  <div id="divSignupRight"style="display:inline-block;border:0px solid red;">
        <h3>OR, Sign up using Facebook</h3>
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
               // alert(response.authResponse);
                var accessToken = response.authResponse.accessToken;
                // TODO: Handle the access token -- below done
                var form = document.createElement("form");
                form.setAttribute("method", 'post');
                form.setAttribute("action", '/ActionHandler.aspx');


                //var field = document.createElement("input");
                //field.setAttribute("type", "hidden");
                //field.setAttribute("name", 'accessToken');
                //field.setAttribute("value", accessToken);
                //form.appendChild(field);

                //document.body.appendChild(form);
                //form.submit();

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
                $('#fbContinue').html(lbl + form.outerHTML);

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
        },{scope: 'email'});
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
    </div>
--%>
       <%} %>
</div>

     <!--#include virtual="inc/footer.html" --> 



       <%if (signupMsg.IndexOf("success") > -1)
         { %>
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

</body>
</html>
