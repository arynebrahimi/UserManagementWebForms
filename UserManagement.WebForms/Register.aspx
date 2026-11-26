<%@ Page Language="C#" AutoEventWireup="true"
    CodeBehind="Register.aspx.cs"
    Inherits="UserManagement.WebForms.Register" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Register</title>
    <link href="Content/Site.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">

        <h2>Register</h2>

        <div>
            <label>Username</label>
            <asp:TextBox ID="txtUsername" runat="server" />
        </div>

        <div>
            <label>Password</label>
            <asp:TextBox ID="txtPassword" runat="server"
                TextMode="Password" />
        </div>

        <div>
            <label>Full Name</label>
            <asp:TextBox ID="txtFullName" runat="server" />
        </div>

        <div>
            <label>Email</label>
            <asp:TextBox ID="txtEmail" runat="server" />
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail"
                ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                Display="Dynamic" ErrorMessage="Invalid email address" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEmail"
                ForeColor="Red" Display="Dynamic" ErrorMessage="Required" />
        </div>

        <br />

        <asp:Button ID="btnRegister" runat="server"
            Text="Register"
            OnClick="btnRegister_Click" />

        <br /><br />

        <asp:Label ID="lblMessage" runat="server" />

        <p>
            Already have an Account?
            <a href="Login.aspx">Login</a>
        </p>

    </form>
</body>
</html>