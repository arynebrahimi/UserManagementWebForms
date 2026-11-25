using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using UserManagement.Application.BaseInfo.Users;
using UserManagement.Application.DTOs;

namespace UserManagement.WebForms
{
    public partial class Users : Page
    {
        private readonly IGetAllUsers _getAllUsers;
        private readonly IDeleteUser _deleteUser;

        public Users(
            IGetAllUsers getAllUsers,
            IDeleteUser deleteUser)
        {
            _getAllUsers = getAllUsers;
            _deleteUser = deleteUser;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            if (Session["UserName"] == null)
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadUsers();
        }

        private void LoadUsers()
        {
            var request = new UserSearchDto
            {
                Keyword = txtSearch.Text.Trim()
            };

            gvUsers.DataSource = _getAllUsers.GetAll(request);
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

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();

            Response.Redirect("Login.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }
    }
}