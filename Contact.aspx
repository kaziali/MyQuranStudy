<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="QuranStudy.Contact" %>

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
     
     <div class="divCont"> 
   <h3>Contact Form</h3>
          Please inform us of any mistakes or let us know if you have any questions.<br /> <br />

         <div class="createFailed"> <%=failureMsg %></div>
   
         <div class="createSuccess"><%=successMsg %></div>
    

    <div style="background: #ccc; padding-top: 11px; padding-left: 12px; border: solid 5px #999; width: 600px; margin-left:20%;">

        <form id="frmContact" name="frmContact" action="contact.aspx" method="post">
            <input type="hidden" name="postmode" value="sending">
            <table>
                <tr>
                    <td align="right">Your Name:</td>
                    <td>
                        <input type="text" size="40" name="uname" maxlength="40" id="uname" value=""> <span class="required">(required)</span></td>
                </tr>

                <tr>
                    <td align="right">Your Email:</td>
                    <td>
                        <input type="text" size="40" name="uemail" maxlength="90" id="Text1" value="" /> <span class="required">(required)</span></td>
                </tr>
                 
                 <tr>
                     
                    <td colspan="2">
                     <hr />
                    </td>
                </tr>
                <tr>
                    <td align="right">Subject:</td>
                    <td>
                        <input type="text" size="66" name="subject" maxlength="100" id="subject" value="" /></td>
                </tr>
                <tr>
                    <td align="right">Message: <span class="required">(required)</span></td>
                    <td>
                       <textarea name="body" id="body" cols="50" rows="7"></textarea>
                </tr>               
                <tr>
                    <td colspan="2" align="center">
                        <input style="font-weight:bold;font-size:100%;width:130px" type="submit" value="  Send  " name="Sign Up - Free" size="25" />
                    </td>
                </tr>
            </table>
        </form>
         <br />
    </div>

     <div>
         <p></p>
        Download Quran Data file: <a href="/download/QuranAllWords.csv">All-Words-Arabic-English</a>
    </div>
         </div>

  
 <!--#include virtual="inc/footer.html" --> 
</body>
</html>

