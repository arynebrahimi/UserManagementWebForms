using System;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;
using UserManagement.WebForms.Services;

namespace UserManagement.WebForms
{
    public partial class Users : Page
    {
        protected async void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Write("Session is empty!");
                return;
            }

            Response.Write("Welcome: " + Session["UserName"]);

            if (!IsPostBack)
                await LoadUsers();
        }

        private async Task LoadUsers()
        {
            bool isAdmin = Session["Role"]?.ToString() == "Admin";

            gvUsers.Columns[5].Visible = isAdmin;
            lnkCreate.Visible = isAdmin;

            var service = ServiceFactory.GetAllUsersService();

            gvUsers.DataSource = await service.GetAllAsync();
            gvUsers.DataBind();
        }

        protected async void gvUsers_RowCommand(
            object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteUser")
            {
                if (Session["Role"]?.ToString() != "Admin")
                {
                    Response.Redirect("Users.aspx");
                    return;
                }

                int id = Convert.ToInt32(e.CommandArgument);

                await ServiceFactory.GetDeleteUserService()
                    .DeleteAsync(id);

                await LoadUsers();
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();

            Response.Redirect("Login.aspx");
        }
    }
}