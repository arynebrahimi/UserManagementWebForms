<%@ Page Language="C#" Async="true" AutoEventWireup="true"
    CodeBehind="Login.aspx.cs"
    Inherits="UserManagement.WebForms.Login" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Login</title>
</head>
<body>
<form Id="form1" runat="server">

    <h2>Login</h2>

    <p>Username</p>
    <asp:TextBox Id="txtUsername" runat="server" />

    <p>Password</p>
    <asp:TextBox Id="txtPassword" runat="server"
        TextMode="Password" />

    <br /><br />

    <asp:Button Id="btnLogin" runat="server"
        Text="Login"
        OnClick="btnLogin_Click" />

    <br /><br />

    <asp:Label Id="lblMessage" runat="server" />

    <p>
        Don't have an account?
        <a href="Register.aspx">Register here</a>
    </p>

</form>
</body>
</html>