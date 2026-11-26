using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UserManagement.Application.BaseInfo.Users;
using UserManagement.Application.DTOs;

namespace UserManagement.WebForms
{
    public partial class Users : Page
    {
        private const int PageSize = 10;

        private readonly IGetAllUsers _getAllUsers;
        private readonly IDeleteUser _deleteUser;

        public Users()
            : this(
                (IGetAllUsers)HttpRuntime.WebObjectActivator.GetService(
                    typeof(IGetAllUsers)),
                (IDeleteUser)HttpRuntime.WebObjectActivator.GetService(
                    typeof(IDeleteUser)))
        {
        }

        public Users(
            IGetAllUsers getAllUsers,
            IDeleteUser deleteUser)
        {
            _getAllUsers = getAllUsers;
            _deleteUser = deleteUser;
        }

        private int CurrentPage
        {
            get
            {
                return ViewState["PageNumber"] == null
                    ? 1
                    : (int)ViewState["PageNumber"];
            }
            set
            {
                ViewState["PageNumber"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (!IsPostBack)
                LoadUsers();
        }

        private void LoadUsers()
        {
            var request = new UserSearchDto
            {
                Keyword = txtSearch.Text.Trim(),
                PageNumber = CurrentPage
            };

            int count = _getAllUsers.GetCount(request);

            int totalPages = Math.Max(
                1,
                (int)Math.Ceiling((double)count / PageSize));

            if (CurrentPage > totalPages)
                CurrentPage = totalPages;

            request.PageNumber = CurrentPage;

            gvUsers.DataSource = _getAllUsers.GetAll(request);
            gvUsers.DataBind();

            lblPageInfo.Text = "Page " + CurrentPage +
                               " of " + totalPages;

            btnPrevious.Enabled = CurrentPage > 1;
            btnNext.Enabled = CurrentPage < totalPages;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            CurrentPage = 1;
            LoadUsers();
        }

        protected void btnShowAll_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            CurrentPage = 1;
            LoadUsers();
        }

        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            if (CurrentPage > 1)
                CurrentPage--;

            LoadUsers();
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            CurrentPage++;
            LoadUsers();
        }

        protected void gvUsers_RowCommand(
            object sender,
            GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteUser")
            {
                int id = Convert.ToInt32(e.CommandArgument);

                _deleteUser.Delete(id);

                LoadUsers();
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();

            Response.Redirect("Login.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }
    }
}
