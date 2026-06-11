using System;
using System.Web.UI;
using Bit2Byte.Data;

namespace Bit2Byte.MemberProfiles
{
    public partial class ConfirmEmailChange : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                return;
            }

            var token = Request.QueryString["token"];
            if (string.IsNullOrWhiteSpace(token))
            {
                ConfirmStatusLiteral.Text = "<div class=\"validation-error\">Missing confirmation token.</div>";
                return;
            }

            var repo = new UserRepository();
            if (repo.ConfirmEmailChange(token))
            {
                ConfirmStatusLiteral.Text = "<div class=\"success-message\">Your email address has been updated successfully. You can now <a href='" + ResolveUrl("~/login.aspx") + "'>log in</a> with the new email.</div>";
            }
            else
            {
                ConfirmStatusLiteral.Text = "<div class=\"validation-error\">The token is invalid or has expired.</div>";
            }
        }
    }
}
