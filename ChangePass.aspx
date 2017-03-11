<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangePass.aspx.cs" Inherits="QuranStudy.ChangePass" %>

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
     <div style="margin:50px 0px 150px 110px; text-align:center">
       
    <%=actionMsg %>

     <%if(UserID>0){ %>
        <form id="frmchangepass" name="frmchangepass" action="changepass.aspx" method="post">
            <input type="hidden" name="mode" value="changepass">
            <table>
                 <tr>
                    <td align="right">Your Email:</td>
                    <td>
                        <%=UserEmail %></td>
                </tr>
                <tr>
                    <td align="right">New Password:</td>
                    <td>
                        <input type="password" size="31" name="pword" maxlength="15" id="pword" value="" /> <span class="required">(required)</span></td>
                </tr>
                <tr>
                    <td align="right">Retype New Password:</td>
                    <td>
                        <input type="password" size="31" name="pwordConf" maxlength="15" id="pwordConf" value=""/> <span class="required">(required)</span></td>
                </tr>
                  
                
                <tr>
                    <td>&nbsp;</td>
                    <td>
                        <input style="font-size:100%;width:160px;" type="submit" value=" Change Password  " name="Change Password" size="25" id="Submit2" />
                    </td>
                </tr>
            </table>
        </form>
       <%}else{%>
         <a href="/Login.aspx">Log in to continue</a>
       <%}%>

 </div>

     <!--#include virtual="inc/footer.html" --> 

</body>
</html>
