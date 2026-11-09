
using System;
using System.Threading.Tasks;
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
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;
            string fullName = txtFullName.Text.Trim();
            string email = txtEmail.Text.Trim();

            if (username == "" || password == "" ||
                fullName == "" || email == "")
            {
                lblMessage.Text = "Please fill in all fields.";
                return;
            }

            User user = new User
            {
                UserName = username,
                Password = password,
                FullName = fullName,
                Email = email
            };

            IRegisterUser service = ServiceFactory.GetRegisterService();

            bool result = await service.RegisterAsync(user);

            if (result)
            {
                lblMessage.Text = "Registration successful!";

                txtUsername.Text = "";
                txtPassword.Text = "";
                txtFullName.Text = "";
                txtEmail.Text = "";
            }
            else
            {
                lblMessage.Text = "Username already exists.";
            }
        }
    }
}
