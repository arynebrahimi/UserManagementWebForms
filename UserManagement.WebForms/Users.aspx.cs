using System;
using System.Web.UI;
using UserManagement.Application.BaseInfo.Users.Interfaces;

namespace UserManagement.WebForms
{
	public partial class Users : Page
	{
		private readonly IGetAllUsers _getAllUsers;

		public Users(IGetAllUsers getAllUsers)
		{
			_getAllUsers = getAllUsers;
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				LoadUsers();
			}
		}

		private void LoadUsers()
		{
			gvUsers.DataSource = _getAllUsers.GetAll();

			gvUsers.DataBind();
		}
	}
}