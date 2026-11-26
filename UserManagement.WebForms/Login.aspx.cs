using System;
using System.Web.UI;
using UserManagement.Application.DTOs;
using UserManagement.Application.BaseInfo.Users.Interfaces;

namespace UserManagement.WebForms
{
    public partial class Login : Page
    {
        private readonly IAuthenticateUser _authenticateUser;

        public Login(IAuthenticateUser authenticateUser)
        {
            _authenticateUser = authenticateUser;
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
                _authenticateUser.Authenticate(request, out string authUsername);

                Session["UserName"] = request.UserName;

                Response.Redirect("Users.aspx");
                return;
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
    }
}