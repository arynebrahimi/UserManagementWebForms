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
                Response.Redirect("Login.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
                return;
            }

            if (!IsPostBack)
            {
                await LoadUsers();
            }
        }

        private async Task LoadUsers()
        {
            bool isAdmin = Session["Role"]?.ToString() == "Admin";

            lnkCreate.Visible = isAdmin;
            gvUsers.Columns[5].Visible = isAdmin;

            var service = ServiceFactory.GetAllUsersService();

            gvUsers.DataSource = await service.GetAllAsync();
            gvUsers.DataBind();
        }

        protected async void gvUsers_RowCommand(
            object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteUser")
            {
                if (Session["UserID"] == null ||
                    Session["Role"]?.ToString() != "Admin")
                {
                    Response.Redirect("Login.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                    return;
                }

                if (!int.TryParse(e.CommandArgument.ToString(), out int id))
                    return;

                await ServiceFactory.GetDeleteUserService()
                    .DeleteAsync(id);

                await LoadUsers();
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