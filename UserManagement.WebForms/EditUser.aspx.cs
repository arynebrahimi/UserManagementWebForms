using System;
using System.Web.UI;
using UserManagement.Application.BaseInfo.Users;
using UserManagement.Application.DTOs;

namespace UserManagement.WebForms
{
    public partial class EditUser : Page
    {
        private readonly IGetUserById _getUserById;
        private readonly IUpdateUser _updateUser;

        public EditUser(
            IGetUserById getUserById,
            IUpdateUser updateUser)
        {
            _getUserById = getUserById;
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
                Response.Write("Invalid user ID.");
                return;
            }

            var request = new UpdateUserDto
            {
                Id = id,
                UserName = txtUsername.Text.Trim(),
                FullName = txtFullName.Text.Trim(),
                Email = txtEmail.Text.Trim()
            };

            try
            {
                _updateUser.Update(request);

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
