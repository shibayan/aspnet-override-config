<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SampleApp.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h3>App Settings</h3>
        <ul>
            <% foreach (var key in ConfigurationManager.AppSettings.AllKeys)
               { %>
            <li><%: key %>: <%: ConfigurationManager.AppSettings[key] %></li>
            <% } %>
        </ul>
        <h3>Connection String</h3>
        <ul>
            <% foreach (ConnectionStringSettings setting in ConfigurationManager.ConnectionStrings)
               { %>
                <li><%: setting.Name %>: <%: setting.ConnectionString %></li>
            <% } %>
        </ul>
    </div>
    </form>
</body>
</html>
