using System;
using System.Web.UI;
using UserManagement.Application.Interfaces;
using UserManagement.Domain.Entities;
using UserManagement.WebForms.Services;

namespace UserManagement.WebForms
{
    public partial class Register : Page
    {
        protected async void btnRegister_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text) ||
                string.IsNullOrWhiteSpace(txtFullName.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                lblMessage.Text = "Please fill in all fields.";
                return;
            }

            User user = new User
            {
                UserName = txtUsername.Text.Trim(),
                Password = txtPassword.Text,
                FullName = txtFullName.Text.Trim(),
                Email = txtEmail.Text.Trim()
            };

            IRegisterUser service = ServiceFactory.GetRegisterService();

            bool result = await service.RegisterAsync(user);

            lblMessage.Text = result
                ? "Registration successful!"
                : "Username already exists.";
        }
    }
}