using System;
using System.Web.UI;
using UserManagement.Application.DTOs;
using UserManagement.Application.BaseInfo.Users.Interfaces;

namespace UserManagement.WebForms
{
    public partial class Login : Page
    {
        private readonly IValidateLogin _validateLogin;

        public Login(IValidateLogin validateLogin)
        {
            _validateLogin = validateLogin;
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            var request = new LoginDto
            {
                UserName = txtUsername.Text.Trim(),
                Password = txtPassword.Text
            };

            if (string.IsNullOrWhiteSpace(request.UserName) ||
                string.IsNullOrWhiteSpace(request.Password))
            {
                lblMessage.Text = "Please enter username and password.";
                return;
            }

            bool isValid = _validateLogin.Validate(request);

            if (!isValid)
            {
                lblMessage.Text = "Invalid username or password.";
                return;
            }

            Response.Redirect("Users.aspx", false);
        }
    }
}