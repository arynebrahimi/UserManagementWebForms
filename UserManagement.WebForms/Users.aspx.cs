using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UserManagement.Application.BaseInfo.Persons.Interfaces;
using UserManagement.Application.BaseInfo.Users.Interfaces;

namespace UserManagement.WebForms
{
    public partial class Users : Page
    {
        private readonly ISearchUsers _searchUsers;
        private readonly IDeleteUser _deleteUser;

        public Users()
        {
            _searchUsers = (ISearchUsers)
                HttpRuntime.WebObjectActivator.GetService(typeof(ISearchUsers));

            _deleteUser = (IDeleteUser)
                HttpRuntime.WebObjectActivator.GetService(typeof(IDeleteUser));
        }

        public Users(ISearchUsers searchUsers, IDeleteUser deleteUser)
        {
            _searchUsers = searchUsers;
            _deleteUser = deleteUser;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadUsers();
        }

        private void LoadUsers()
        {
            var request = new ISearchUsers.Request
            {
                Username = txtSearch.Text.Trim(),

                Person = new ISearchPersons.Request
                {
                    Keyword = txtSearch.Text.Trim()
                }
            };

            gvUsers.DataSource = _searchUsers.Execute(request).ToList();
            gvUsers.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadUsers();
        }

        protected void btnShowAll_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            LoadUsers();
        }

        protected void gvUsers_RowCommand(
            object sender,
            GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteUser")
            {
                int id = Convert.ToInt32(e.CommandArgument);

                _deleteUser.Delete(id);

                LoadUsers();
            }
        }
    }
}