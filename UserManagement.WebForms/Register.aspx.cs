using System;
using System.Web.UI;
using UserManagement.Application.DTOs;
using UserManagement.Application.BaseInfo.Users.Interfaces;
using UserManagement.Domain.Entities;

namespace UserManagement.WebForms
{
    public partial class Register : Page
    {
        private readonly IAddUser _addUser;

        public Register(IAddUser addUser)
        {
            _addUser = addUser;
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                var request = new RegisterUserDto
                {
                    UserName = txtUsername.Text.Trim(),
                    Password = txtPassword.Text,
                    FullName = txtFullName.Text.Trim(),
                    Email = txtEmail.Text.Trim()
                };
                _addUser.Add(request, out User user);
                Session["UserName"] = user.UserName;

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
