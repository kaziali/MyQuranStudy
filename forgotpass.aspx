<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="forgotpass.aspx.cs" Inherits="QuranStudy.forgotpass" %>

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
    .forgotpass  
    {
	    padding:20px;
	    background: #ccc;
	    margin-top: 7px;
	    margin-bottom: 3px;
	    border:solid 5px #999;
	    width:440px; 
     }
      
	h1 
    {
	    font-family: Verdana, Georgia, serif;
	    font-weight: normal;
	    font-size: 18px;
	    color: #369;
    }

    h2 
    {
	    font-family: Verdana, Georgia, serif;
	    font-weight: normal;
	    font-size: 18px;
	    color: #B54B1A;
    }
    .divCont {
         margin-left:10%;
         font-family: Verdana, Georgia, serif;
         font-size:90%;
         zzborder:1px solid red;
         width:510px
    }
 </style>
</head>
<body>
   <!--#include virtual="inc/header.html" -->
 
    <div class="divCont" >
    <!--main content-->
 <%
if(postmode=="submitted" && sResultMsg =="" )
{%>
 		We have sent you an email with your password. 
		Please open your email from us to retrieve it. 
		<br /><br /><br />
 		 
<%}
else
{
%>
			<h1>Forgot your account password?</h1>
			Please enter your signup email address and click on submit.  
 			<div class="forgotpass">
			<div style="color:red"><%=sResultMsg%></div>
			<form id="frmSignup" name="frmSignup" action="forgotpass.aspx" method="post">
				<input type="hidden" name="postmode" value="submitted" id="postmode" />
				<div>
 				<br /><span class="spnSignup">Email Address: </span><input type="text" size="40" name="uemail" maxlength="90" id="uemail" value="<%=uemail%>" />
 				</div><br />
				<div style="text-align:center"><input type="submit" value="  Submit  " name="signup" size=35 id="signup" /> </div> 
			</form><br />
  			&nbsp; &nbsp;<a href="/signup.aspx">New user - Sign up here</a> &nbsp;  
  			&nbsp;<a href="/login.aspx">Existing user - login here!</a>
  			
  	</div>
  	<%}%>   
    
     <!--/main content-->
 </div>


     <!--#include virtual="inc/footer.html" --> 

</body>
</html>


