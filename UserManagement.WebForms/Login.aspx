<%@ Page Language="C#" Async="true" AutoEventWireup="true"
    CodeBehind="Login.aspx.cs"
    Inherits="UserManagement.WebForms.Login" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Login</title>
</head>
<body>
<form id="form1" runat="server">

    <h2>Login</h2>

    <p>Username</p>
    <asp:TextBox ID="txtUsername" runat="server" />

    <p>Password</p>
    <asp:TextBox ID="txtPassword" runat="server"
        TextMode="Password" />

    <br /><br />

    <asp:Button ID="btnLogin" runat="server"
        Text="Login"
        OnClick="btnLogin_Click" />

    <br /><br />

    <asp:Label ID="lblMessage" runat="server" />

    <p>
        Don't have an account?
        <a href="Register.aspx">Register here</a>
    </p>

</form>
</body>
</html>