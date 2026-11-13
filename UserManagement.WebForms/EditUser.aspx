
<%@ Page Language="C#" Async="true" AutoEventWireup="true"
    CodeBehind="EditUser.aspx.cs"
    Inherits="UserManagement.WebForms.EditUser" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Edit User</title>
</head>
<body>
<form Id="form1" runat="server">

    <h2>Edit User</h2>

    <asp:HIddenField Id="hfId" runat="server" />

    <p>Username</p>
    <asp:TextBox Id="txtUsername" runat="server" />

    <p>Full Name</p>
    <asp:TextBox Id="txtFullName" runat="server" />

    <p>Email</p>
    <asp:TextBox Id="txtEmail" runat="server" />

    <br /><br />

    <asp:Button Id="btnUpdate" runat="server"
        Text="Update"
        OnClick="btnUpdate_Click" />

    <br /><br />

    <asp:Label Id="lblMessage" runat="server" />

</form>
</body>
</html>
