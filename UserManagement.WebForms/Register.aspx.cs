using System;
using System.Web.UI;
using System.Data.Entity.Infrastructure;
using UserManagement.Application.Interfaces;
using UserManagement.Domain.Entities;
using UserManagement.WebForms.Services;

namespace UserManagement.WebForms
{
    public partial class Register : Page
    {
        protected async void btnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();

            if (string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(txtPassword.Text) ||
                string.IsNullOrWhiteSpace(txtFullName.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                lblMessage.Text = "Please fill in all fields.";
                return;
            }

            User user = new User
            {
                UserName = username,
                Password = txtPassword.Text,
                FullName = txtFullName.Text.Trim(),
                Email = txtEmail.Text.Trim()
            };

            try
            {
                IRegisterUser service =
                    ServiceFactory.GetRegisterService();

                bool result = await service.RegisterAsync(user);

                if (!result)
                {
                    lblMessage.Text = "Username already exists.";
                    return;
                }

                Session["UserID"] = user.ID;
                Session["UserName"] = user.UserName;
                Session["Role"] = user.Role;

                Response.Redirect("Users.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (DbUpdateException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());

                lblMessage.Text =
                    "Username already exists or database error occurred.";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());

                lblMessage.Text = "Registration failed.";
            }
        }
    }
}