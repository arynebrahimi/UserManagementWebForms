
<%@ Page Language="C#" Async="true" AutoEventWireup="true"
    CodeBehind="Register.aspx.cs"
    Inherits="UserManagement.WebForms.Register" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Register</title>
</head>
<body>
    <form Id="form1" runat="server">

        <h2>Register</h2>

        <div>
            <label>Username</label>
            <asp:TextBox Id="txtUsername" runat="server" />
        </div>

        <div>
            <label>Password</label>
            <asp:TextBox Id="txtPassword" runat="server"
                TextMode="Password" />
        </div>

        <div>
            <label>Full Name</label>
            <asp:TextBox Id="txtFullName" runat="server" />
        </div>

        <div>
            <label>Email</label>
            <asp:TextBox Id="txtEmail" runat="server" />
        </div>

        <br />

        <asp:Button Id="btnRegister" runat="server"
            Text="Register"
            OnClick="btnRegister_Click" />

        <br /><br />

        <asp:Label Id="lblMessage" runat="server" />
        <p>
            Already have an Account?
            <a href="Login.aspx">Login</a>
        </p>

    </form>
</body>
</html>
