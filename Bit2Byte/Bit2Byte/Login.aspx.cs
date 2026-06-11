using System;
using System.Web.UI;

namespace Bit2Byte
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsUserAuthenticated())
            {
                Response.Redirect(ResolveUrl("~/members.aspx"), true);
            }
        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (!Page.IsValid)
            {
                StatusLabel.Text = string.Empty;
                return;
            }

            var email = EmailTextBox.Text.Trim();
            var password = PasswordTextBox.Text;

            var repo = new Bit2Byte.Data.UserRepository();
            var user = repo.GetByEmail(email);

            if (user != null && Bit2Byte.Data.PasswordHelper.VerifyPassword(password, user.PasswordHash) && user.IsActive)
            {
                Session[SessionKeys.UserId] = user.Id;
                Session[SessionKeys.Authenticated] = true;
                Session[SessionKeys.Role] = user.Role;
                Response.Redirect(ResolveUrl("~/members.aspx"), true);
                return;
            }

            StatusLabel.Text = "<div class=\"validation-error\">Invalid credentials. Make sure you registered and used the correct password.</div>";
        }

        private bool IsUserAuthenticated()
        {
            return Session[SessionKeys.Authenticated] is bool authenticated && authenticated;
        }
    }
}
