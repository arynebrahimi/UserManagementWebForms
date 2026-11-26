<%@ Page Language="C#" AutoEventWireup="true"
    CodeBehind="Users.aspx.cs"
    Inherits="UserManagement.WebForms.Users" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Users</title>
    <link href="Content/Site.css" rel="stylesheet" />
</head>

<body>
    <form id="form1" runat="server">

        <h2>Users List</h2>

        <asp:Button ID="btnLogout" runat="server"
            Text="Logout"
            OnClick="btnLogout_Click" />

        <asp:HyperLink ID="lnkCreate" runat="server"
            NavigateUrl="Register.aspx"
            Text="Add New User" />

        <br />
        <br />

        <asp:TextBox ID="txtSearch" runat="server"
            placeholder="Search by username, name or email" />

        <asp:Button ID="btnSearch" runat="server"
            Text="Search"
            OnClick="btnSearch_Click" />

        <asp:Button ID="btnShowAll" runat="server"
            Text="Show All"
            OnClick="btnShowAll_Click" />

        <br />
        <br />

        <asp:GridView ID="gvUsers" runat="server"
            CssClass="users-table"
            AutoGenerateColumns="False"
            DataKeyNames="Id"
            OnRowCommand="gvUsers_RowCommand">

            <Columns>

                <asp:BoundField
                    DataField="UserName"
                    HeaderText="Username" />

                <asp:TemplateField HeaderText="Full Name">
                    <ItemTemplate>
                        <%# Eval("Person.FullName") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Email">
                    <ItemTemplate>
                        <%# Eval("Person.Email") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField
                    DataField="Role"
                    HeaderText="Role" />

                <asp:TemplateField HeaderText="Actions">
                    <ItemTemplate>

                        <asp:HyperLink runat="server"
                            Text="Edit"
                            NavigateUrl='<%# "EditUser.aspx?id=" + Eval("Id") %>' />

                        &nbsp;

                        <asp:LinkButton runat="server"
                            Text="Delete"
                            CommandName="DeleteUser"
                            CommandArgument='<%# Eval("Id") %>'
                            OnClientClick="return confirm('Delete this user?');" />

                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>

        </asp:GridView>

        <br />

        <asp:Button ID="btnPrevious" runat="server"
            Text="Previous"
            OnClick="btnPrevious_Click"
            Enabled="False" />

        <asp:Label ID="lblPageInfo" runat="server"
            Text="Page 1 of 1" />

        <asp:Button ID="btnNext" runat="server"
            Text="Next"
            OnClick="btnNext_Click"
            Enabled="False" />

    </form>
</body>
</html>
