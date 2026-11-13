using System;
using System.Threading.Tasks;
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

		protected async void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				await LoadUsers();
			}
		}

		private async Task LoadUsers()
		{
			lnkCreate.Visible = true;

			// Hide edit and delete actions until
			// user authentication and role management are implemented.
			gvUsers.Columns[5].Visible = false;

			gvUsers.DataSource = await _getAllUsers.GetAllAsync();

			gvUsers.DataBind();
		}
	}
}