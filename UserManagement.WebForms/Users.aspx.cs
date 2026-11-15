using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using UserManagement.Application.BaseInfo.Users.Interfaces;

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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadUsers();
            }
        }

        private void LoadUsers()
        {
            gvUsers.DataSource = _getAllUsers.GetAll();
            gvUsers.DataBind();
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