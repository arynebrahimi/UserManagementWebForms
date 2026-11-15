<%@ Page Language="C#" AutoEventWireup="true"
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
            DataKeyNames="Id"
            OnRowCommand="gvUsers_RowCommand">

            <Columns>

                <asp:BoundField
                    DataField="Id"
                    HeaderText="ID" />

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

    </form>
</body>
</html>