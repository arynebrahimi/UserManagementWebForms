using System;
using System.Web.UI;
using UserManagement.Application.BaseInfo.Users;
using UserManagement.Application.DTOs;

namespace UserManagement.WebForms
{
    public partial class Register : Page
    {
        private readonly IRegisterUser _registerUser;

        public Register(IRegisterUser registerUser)
        {
            _registerUser = registerUser;
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            var request = new RegisterUserDto
            {
                UserName = txtUsername.Text.Trim(),
                Password = txtPassword.Text,
                FullName = txtFullName.Text.Trim(),
                Email = txtEmail.Text.Trim()
            };

            try
            {
                _registerUser.Register(request);

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
