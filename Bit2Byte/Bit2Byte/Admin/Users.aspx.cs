using System;
using Bit2Byte.Data;

namespace Bit2Byte.Admin
{
    public partial class Users : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) LoadUsers();
        }

        private void LoadUsers()
        {
            var repo = new UserRepository();
            UsersGrid.DataSource = repo.GetAll();
            UsersGrid.DataBind();
        }

        protected void UsersGrid_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            var id = Convert.ToInt32(UsersGrid.DataKeys[e.RowIndex].Value);
            var repo = new UserRepository();
            repo.Delete(id);
            StatusLiteral.Text = "User deleted.";
            LoadUsers();
        }

        protected void UsersGrid_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            UsersGrid.EditIndex = e.NewEditIndex;
            LoadUsers();
        }

        protected void UsersGrid_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
        {
            UsersGrid.EditIndex = -1;
            LoadUsers();
        }

        protected void UsersGrid_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
        {
            var id = Convert.ToInt32(UsersGrid.DataKeys[e.RowIndex].Value);
            var row = UsersGrid.Rows[e.RowIndex];
            var username = ((System.Web.UI.WebControls.TextBox)row.Cells[1].Controls[0]).Text.Trim();
            var email = ((System.Web.UI.WebControls.TextBox)row.Cells[2].Controls[0]).Text.Trim();
            var role = ((System.Web.UI.WebControls.TextBox)row.Cells[3].Controls[0]).Text.Trim();

            var repo = new UserRepository();
            var user = repo.GetById(id);
            if (user != null)
            {
                user.Username = username;
                user.Email = email;
                user.Role = role;
                repo.Update(user);
                StatusLiteral.Text = "User updated.";
            }

            UsersGrid.EditIndex = -1;
            LoadUsers();
        }
    }
}
