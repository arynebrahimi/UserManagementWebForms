
using System;
using System.Web.UI;
using UserManagement.Application.Interfaces;
using UserManagement.Domain.Entities;
using UserManagement.WebForms.Services;

namespace UserManagement.WebForms
{
    public partial class EditUser : Page
    {
        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int id = Convert.ToInt32(Request.QueryString["id"]);

                var service = ServiceFactory.GetUserByIdService();
                var user = await service.GetByIdAsync(id);

                if (user == null)
                {
                    Response.Redirect("Users.aspx");
                    return;
                }

                hfID.Value = user.ID.ToString();
                txtUsername.Text = user.UserName;
                txtFullName.Text = user.FullName;
                txtEmail.Text = user.Email;
            }
        }

        protected async void btnUpdate_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(hfID.Value);

            var service = ServiceFactory.GetUserByIdService();
            var user = await service.GetByIdAsync(id);

            if (user == null)
            {
                lblMessage.Text = "User not found.";
                return;
            }

            user.UserName = txtUsername.Text.Trim();
            user.FullName = txtFullName.Text.Trim();
            user.Email = txtEmail.Text.Trim();

            var updateService = ServiceFactory.GetUpdateUserService();
            await updateService.UpdateAsync(user);

            Response.Redirect("Users.aspx");
        }
    }
}
