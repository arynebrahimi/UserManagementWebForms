<%@ Page Language="C#" Async="true" AutoEventWireup="true"
    CodeBehind="Users.aspx.cs"
    Inherits="UserManagement.WebForms.Users" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Users</title>
</head>
<body>
<form id="form1" runat="server">

    <h2>Users List</h2>

    <asp:HyperLink ID="lnkCreate" runat="server"
        NavigateUrl="Register.aspx"
        Text="Add New User" />

    <br /><br />

    <asp:GridView ID="gvUsers" runat="server"
        AutoGenerateColumns="False"
        DataKeyNames="ID"
        OnRowCommand="gvUsers_RowCommand">

        <Columns>
            <asp:BoundField DataField="ID" HeaderText="ID" />
            <asp:BoundField DataField="UserName" HeaderText="Username" />
            <asp:BoundField DataField="FullName" HeaderText="Full Name" />
            <asp:BoundField DataField="Email" HeaderText="Email" />
            <asp:BoundField DataField="Role" HeaderText="Role" />

            <asp:TemplateField HeaderText="Actions">
                <ItemTemplate>
                    <asp:HyperLink runat="server"
                        Text="Edit"
                        NavigateUrl='<%# "EditUser.aspx?id=" + Eval("ID") %>' />

                    <asp:LinkButton runat="server"
                        Text="Delete"
                        CommandName="DeleteUser"
                        CommandArgument='<%# Eval("ID") %>'
                        OnClientClick="return confirm('Delete this user?');" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:LinkButton ID="btnLogout" runat="server"
    Text="Logout"
    OnClick="btnLogout_Click" />
</form>
</body>
</html>