using System;
using System.Data.Entity.Infrastructure;
using System.Web.UI;
using UserManagement.Application.Interfaces;
using UserManagement.WebForms.Services;

namespace UserManagement.WebForms
{
    public partial class EditUser : Page
    {
        protected async void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null ||
                Session["Role"]?.ToString() != "Admin")
            {
                Response.Redirect("Users.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
                return;
            }

            if (!IsPostBack)
            {
                if (!int.TryParse(Request.QueryString["id"], out int id))
                {
                    Response.Redirect("Users.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                    return;
                }

                var service = ServiceFactory.GetUserByIdService();
                var user = await service.GetByIdAsync(id);

                if (user == null)
                {
                    Response.Redirect("Users.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
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
            if (Session["UserID"] == null ||
                Session["Role"]?.ToString() != "Admin")
            {
                Response.Redirect("Login.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
                return;
            }

            if (!int.TryParse(hfID.Value, out int id))
            {
                lblMessage.Text = "Invalid user ID.";
                return;
            }

            string username = txtUsername.Text.Trim();

            if (string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(txtFullName.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                lblMessage.Text = "Please fill in all fields.";
                return;
            }

            var userService = ServiceFactory.GetUserByIdService();
            var user = await userService.GetByIdAsync(id);

            if (user == null)
            {
                lblMessage.Text = "User not found.";
                return;
            }

            var usernameService = ServiceFactory.GetUsernameExistsService();

            bool exists = await usernameService.ExistsAsync(username, id);

            if (exists)
            {
                lblMessage.Text = "This username is already used by another user.";
                return;
            }

            user.UserName = username;
            user.FullName = txtFullName.Text.Trim();
            user.Email = txtEmail.Text.Trim();

            try
            {
                var updateService = ServiceFactory.GetUpdateUserService();

                await updateService.UpdateAsync(user);

                Response.Redirect("Users.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (DbUpdateException)
            {
                lblMessage.Text = "Username already exists or database error occurred.";
            }
        }
    }
}