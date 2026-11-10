using System;
using System.Web.UI;
using UserManagement.Application.Services;
using UserManagement.Persistence.Data;

namespace UserManagement.WebForms
{
    public partial class Login : Page
    {
        protected async void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (username == "" || password == "")
            {
                lblMessage.Text = "Please enter username and password.";
                return;
            }

            using (var db = new AppDbContext())
            {
                var service = new GetUserByUsername(db);

                var user = await service.GetByUsernameAsync(username);

                if (user == null || user.Password != password)
                {
                    lblMessage.Text = "Invalid username or password.";
                    return;
                }

                Session["UserID"] = user.ID;
                Session["UserName"] = user.UserName;
                Session["Role"] = user.Role;
            }

            Response.Redirect("Users.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }
    }
}