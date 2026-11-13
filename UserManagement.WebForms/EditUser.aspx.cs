using System;
using System.Data.Entity.Infrastructure;
using System.Web.UI;
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

		protected async void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				if (!int.TryParse(Request.QueryString["id"], out int id))
				{
					Response.Redirect("Users.aspx", false);
					return;
				}

				var user = await _getUserById.GetByIdAsync(id);

				if (user == null)
				{
					Response.Redirect("Users.aspx", false);
					return;
				}

				hfId.Value = user.Id.ToString();
				txtUsername.Text = user.UserName;
				txtFullName.Text = user.FullName;
				txtEmail.Text = user.Email;
			}
		}

		protected async void btnUpdate_Click(object sender, EventArgs e)
		{
			if (!int.TryParse(hfId.Value, out int id))
			{
				lblMessage.Text = "Invalid user ID.";
				return;
			}

			string username = txtUsername.Text.Trim();

			if (string.IsNullOrWhiteSpace(username) ||
				string.IsNullOrWhiteSpace(txtFullName.Text) ||
				string.IsNullOrWhiteSpace(txtEmail.Text))
			{
				lblMessage.Text = "Please fill in all fields.";
				return;
			}

			var user = await _getUserById.GetByIdAsync(id);

			if (user == null)
			{
				lblMessage.Text = "User not found.";
				return;
			}

			bool exists = await _usernameExists.ExistsAsync(username, id);

			if (exists)
			{
				lblMessage.Text =
					"This username is already used by another user.";
				return;
			}

			user.UserName = username;
			user.FullName = txtFullName.Text.Trim();
			user.Email = txtEmail.Text.Trim();

			try
			{
				await _updateUser.UpdateAsync(user);

				Response.Redirect("Users.aspx", false);
				return;
			}
			catch (DbUpdateException)
			{
				lblMessage.Text =
					"Username already exists or database error occurred.";
			}
		}
	}
}