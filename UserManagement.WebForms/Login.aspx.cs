using System;
using System.Web.UI;
using UserManagement.Application.BaseInfo.Users;
using UserManagement.Application.DTOs;

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

            try
            {
                _validateLogin.Validate(request);

                Response.Redirect("Users.aspx", false);
                return;
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
    }
}
