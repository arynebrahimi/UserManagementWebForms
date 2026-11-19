using System;
using System.Web.UI;
using System.Data.Entity.Infrastructure;
using UserManagement.Application.DTOs;
using UserManagement.Application.BaseInfo.Users.Interfaces;

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

            if (string.IsNullOrWhiteSpace(request.UserName) ||
                string.IsNullOrWhiteSpace(request.Password) ||
                string.IsNullOrWhiteSpace(request.FullName) ||
                string.IsNullOrWhiteSpace(request.Email))
            {
                lblMessage.Text = "Please fill in all fields.";
                return;
            }

            try
            {
                bool result = _registerUser.Register(request);

                if (!result)
                {
                    lblMessage.Text = "Username already exists.";
                    return;
                }

                Response.Redirect("Users.aspx", false);
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