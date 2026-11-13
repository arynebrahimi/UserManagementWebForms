using System;
using System.Web.UI;
using UserManagement.Application.BaseInfo.Users;

namespace UserManagement.WebForms
{
	public partial class Login : Page
	{
		private readonly IValidateLogin _ValidateLogin;

		public Login(IValidateLogin ValidateLogin)
		{
			_ValidateLogin = ValidateLogin;
		}

		protected async void btnLogin_Click(object sender, EventArgs e)
		{
			string username = txtUsername.Text.Trim();
			string password = txtPassword.Text;

			if (string.IsNullOrWhiteSpace(username) ||
				string.IsNullOrWhiteSpace(password))
			{
				lblMessage.Text = "Please enter username and password.";
				return;
			}

			bool isValid = await _ValidateLogin.ValidateAsync(
				username, password);

			if (!isValid)
			{
				lblMessage.Text = "Invalid username or password.";
				return;
			}

			Response.Redirect("Users.aspx");
		}
	}
}