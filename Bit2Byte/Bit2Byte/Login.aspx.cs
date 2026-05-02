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
            var registeredEmail = Convert.ToString(Session[SessionKeys.Email]);
            var registeredPassword = Convert.ToString(Session[SessionKeys.Password]);

            var hasStoredAccount = !string.IsNullOrWhiteSpace(registeredEmail) && !string.IsNullOrWhiteSpace(registeredPassword);
            var matchesStoredAccount = hasStoredAccount && string.Equals(email, registeredEmail, StringComparison.OrdinalIgnoreCase) && password == registeredPassword;
            var demoLogin = !hasStoredAccount && email.EndsWith("@kuet.ac.bd", StringComparison.OrdinalIgnoreCase) && password.Length >= 8;

            if (matchesStoredAccount || demoLogin)
            {
                Session[SessionKeys.Email] = email;
                Session[SessionKeys.Authenticated] = true;
                Response.Redirect(ResolveUrl("~/members.aspx"), true);
                return;
            }

            StatusLabel.Text = "<div class=\"validation-error\">Invalid credentials. Register first or use the same KUET email and password you registered with.</div>";
        }

        private bool IsUserAuthenticated()
        {
            return Session[SessionKeys.Authenticated] is bool authenticated && authenticated;
        }
    }
}
