<%@ Page Language="C#" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Ext.NET SVG Help</title>
</head>
<body style="padding: 20px;">
    The error occured during exporting: <i><%= this.Request.QueryString["errorMessage"] %></i>
</body>
</html>
