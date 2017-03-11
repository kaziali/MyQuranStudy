<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LanesLexicon.aspx.cs" Inherits="QuranStudy.LanesLexicon" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Lanes Lexicon - Arabic-English Dictionary</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    Lanes Lexicon - Arabic-English Dictionary - <%=pageNav %>
        <div>
            <iframe id="pdfframe" src="<%= ifrm_src %>" style="width:100%"></iframe>
        </div>
        <script language="javascript">
            pdfframe.height = screen.height - 210;
        </script>
    </div>
    </form>
</body>
</html>
