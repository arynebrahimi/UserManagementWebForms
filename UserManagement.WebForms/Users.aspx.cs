using System;
using System.Web.UI;
using UserManagement.Application.Interfaces;
using UserManagement.WebForms.Services;

namespace UserManagement.WebForms
{
    public partial class Users : Page
    {
        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                await LoadUsers();
        }

        private async System.Threading.Tasks.Task LoadUsers()
        {
            IGetAllUsers service = ServiceFactory.GetAllUsersService();
            gvUsers.DataSource = await service.GetAllAsync();
            gvUsers.DataBind();
        }

        protected async void gvUsers_RowCommand(object sender,
            System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteUser")
            {
                int id = Convert.ToInt32(e.CommandArgument);

                var service = ServiceFactory.GetDeleteUserService();
                await service.DeleteAsync(id);

                await LoadUsers();
            }
        }
    }
}