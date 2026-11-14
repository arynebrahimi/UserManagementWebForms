using System;
using System.Web.UI;
using UserManagement.Application.BaseInfo.Users.Interfaces;

namespace UserManagement.WebForms
{
	public partial class Login : Page
	{
		private readonly IValidateLogin _validateLogin;

		public Login(IValidateLogin validateLogin)
		{
			_validateLogin = validateLogin;
		}

		protected void btnLogin_Click(object sender, EventArgs e)
		{
			string username = txtUsername.Text.Trim();
			string password = txtPassword.Text;

			if (string.IsNullOrWhiteSpace(username) ||
				string.IsNullOrWhiteSpace(password))
			{
				lblMessage.Text = "Please enter username and password.";
				return;
			}

			bool isValid = _validateLogin.Validate(username, password);

			if (!isValid)
			{
				lblMessage.Text = "Invalid username or password.";
				return;
			}

			Response.Redirect("Users.aspx", false);
		}
	}
}