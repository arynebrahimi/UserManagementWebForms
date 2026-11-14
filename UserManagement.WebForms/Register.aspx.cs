using System;
using System.Web.UI;
using System.Data.Entity.Infrastructure;
using UserManagement.Domain.Entities;
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
			string username = txtUsername.Text.Trim();

			if (string.IsNullOrWhiteSpace(username) ||
				string.IsNullOrWhiteSpace(txtPassword.Text) ||
				string.IsNullOrWhiteSpace(txtFullName.Text) ||
				string.IsNullOrWhiteSpace(txtEmail.Text))
			{
				lblMessage.Text = "Please fill in all fields.";
				return;
			}

			User user = new User
			{
				UserName = username,
				Password = txtPassword.Text,
				FullName = txtFullName.Text.Trim(),
				Email = txtEmail.Text.Trim()
			};

			try
			{
				bool result = _registerUser.Register(user);

				if (!result)
				{
					lblMessage.Text = "Username already exists.";
					return;
				}

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
				lblMessage.Text = "Registration failed.";
			}
		}
	}
}