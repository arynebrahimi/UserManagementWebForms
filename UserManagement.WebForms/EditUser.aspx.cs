using System;
using System.Data.Entity.Infrastructure;
using System.Web.UI;
using UserManagement.Application.DTOs;
using UserManagement.Application.BaseInfo.Users.Interfaces;

namespace UserManagement.WebForms
{
    public partial class EditUser : Page
    {
        private readonly IGetUserById _getUserById;
        private readonly IUsernameExists _usernameExists;
        private readonly IUpdateUser _updateUser;

        public EditUser(
            IGetUserById getUserById,
            IUsernameExists usernameExists,
            IUpdateUser updateUser)
        {
            _getUserById = getUserById;
            _usernameExists = usernameExists;
            _updateUser = updateUser;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!int.TryParse(Request.QueryString["id"], out int id))
                {
                    Response.Redirect("Users.aspx", false);
                    return;
                }

                var user = _getUserById.GetById(id);

                if (user == null || user.Person == null)
                {
                    Response.Redirect("Users.aspx", false);
                    return;
                }

                hfId.Value = user.Id.ToString();
                txtUsername.Text = user.UserName;
                txtFullName.Text = user.Person.FullName;
                txtEmail.Text = user.Person.Email;
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(hfId.Value, out int id))
            {
                lblMessage.Text = "Invalid user ID.";
                return;
            }

            var request = new UpdateUserDto
            {
                Id = id,
                UserName = txtUsername.Text.Trim(),
                FullName = txtFullName.Text.Trim(),
                Email = txtEmail.Text.Trim()
            };

            if (string.IsNullOrWhiteSpace(request.UserName) ||
                string.IsNullOrWhiteSpace(request.FullName) ||
                string.IsNullOrWhiteSpace(request.Email))
            {
                lblMessage.Text = "Please fill in all fields.";
                return;
            }

            var user = _getUserById.GetById(id);

            if (user == null || user.Person == null)
            {
                lblMessage.Text = "User not found.";
                return;
            }

            bool exists = _usernameExists.Exists(request.UserName, id);

            if (exists)
            {
                lblMessage.Text =
                    "This username is already used by another user.";
                return;
            }

            try
            {
                _updateUser.Update(request);

                Response.Redirect("Users.aspx", false);
            }
            catch (DbUpdateException)
            {
                lblMessage.Text =
                    "Username already exists or database error occurred.";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                lblMessage.Text = "Update failed.";
            }
        }
    }
}
