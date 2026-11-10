
<%@ Page Language="C#" Async="true" AutoEventWireup="true"
    CodeBehind="EditUser.aspx.cs"
    Inherits="UserManagement.WebForms.EditUser" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Edit User</title>
</head>
<body>
<form id="form1" runat="server">

    <h2>Edit User</h2>

    <asp:HiddenField ID="hfID" runat="server" />

    <p>Username</p>
    <asp:TextBox ID="txtUsername" runat="server" />

    <p>Full Name</p>
    <asp:TextBox ID="txtFullName" runat="server" />

    <p>Email</p>
    <asp:TextBox ID="txtEmail" runat="server" />

    <br /><br />

    <asp:Button ID="btnUpdate" runat="server"
        Text="Update"
        OnClick="btnUpdate_Click" />

    <br /><br />

    <asp:Label ID="lblMessage" runat="server" />

</form>
</body>
</html>
