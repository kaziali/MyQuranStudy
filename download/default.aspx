<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="QuranStudy.download._default" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Study Quran, learn word for word meanings, track progress</title>
    <meta name="keywords" content="quran, quran learning, online quran learning, learn quran online, quran word for word, quran word by word, quran meaning, quran study, my quran, learn quran, quran english, quran words, koran, coran, qur'an, Arabic roots, quran tracker" />
    <meta name="description" content="Study Quran, learn meaning of each word, underline and highlight words - track with progress tracker." />
    <script src="http://code.jquery.com/jquery-1.9.1.js"></script>
    <script src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
    <link href="/css/main.css" type="text/css" rel="stylesheet" />
    <style type="text/css">
    </style>
</head>
<body>
   <!--#include virtual="/inc/header.html" -->
   
      <%--<h3 style="text-align:center">Download - Free! No payment required... only your duas and spreading of the word.</h3> --%>
    <div class="divCont2"> 
    <h3 style="margin-left: 40%;"><b>FREE Download</b></h3>
        
        The downloaded offline version cannot track progress or highlight words. 
        <br />You can register and use online version to track your progress, insha'Allah.
    </div>
 
  <div>
      <div style="padding-top: 5px; padding-left: 12px; border: solid 0px #999; width: 500px; margin-left:20%;"> 
       
      </div>
       <%if (signupMsg.IndexOf("success") > -1)
         { %>
        <div  style="text-align:center; margin-left:280px;""> 
        <span style="zztext-align:right; zzmargin-left:280px; background-color:#121cd5;width:280px;padding:11px"> 
            <a style="font-size:130%;color:white" href="<%=downloaLink %>">Click To Download Now!</a> 

        </span>
         
            <h3>Jazakallahu khair - May Allah reward you!</h3>    
        </div>
      <%} else { %>

       <div style="padding-top: 5px; padding-left: 12px; border: solid 0px #999; width: 500px; margin-left:20%;"> 
       <%=signupMsg %>
      </div>

  <div id="divSignupLeft"style="display:inline-block;float:left;width:60%; border:0px solid red;">
    <div style="background: #ccc; padding-top: 11px; padding-left: 12px; border: solid 5px #999; width: 500px; margin-left:20%;">
   

        <form id="frmDownload" name="frmDownload" action="default.aspx" method="post">
           <%-- <input type="hidden" name="mode" value="download">--%>
                        <input type="hidden" name="postmode" value="signup" ID="Hidden1" />
            <table>
                <tr>
                    <td align="right">Your Name:</td>
                    <td>
                        <input type="text" size="30" name="uname" maxlength="40" id="uname" value=""> <span class="required">(required)</span></td>
                </tr>

                <tr>
                    <td align="right">Your Email:</td>
                    <td>
                        <input type="text" size="30" name="uemail" maxlength="90" id="Text1" value="" /> <span class="required">(required)</span></td>
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
                    <td colspan="2" align="center"><div style="font:10px verdana"><br />[Note: Your email address will be kept private. We may send ocasional newsletters/articles with optout option.]</div></td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>
                        <input style="font-weight:bold;font-size:111%;width:200px" type="submit" value="  Download  " name="Sign Up - Free" size="35" id="Submit2">
                    </td>
                </tr>
            </table>
        </form>
        <div style="text-align:center"><br />
         <a href="/signup.aspx">Rather, Let me Sign Up</a></div><br />
    </div>
    <div style="text-align:center;color:red">
     
</div>
  </div>
 

       <%} %>
</div>

     <!--#include virtual="/inc/footer.html" --> 

     
</body>
</html>
