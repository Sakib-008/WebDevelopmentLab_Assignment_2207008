using System;
using System.Web;
using System.Web.UI;

namespace Bit2Byte
{
    public partial class Members : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsUserAuthenticated())
            {
                Response.Redirect(ResolveUrl("~/login.aspx"), true);
                return;
            }

            var displayName = Convert.ToString(Session[SessionKeys.FullName]);
            var email = Convert.ToString(Session[SessionKeys.Email]);
            GreetingLiteral.Text = "<div class=\"card\"><p><strong>Welcome, " + HttpUtility.HtmlEncode(displayName ?? email) + "</strong></p><p>You are signed in and can view the member area.</p></div>";
        }

        private static bool IsUserAuthenticated()
        {
            return HttpContext.Current?.Session?[SessionKeys.Authenticated] is bool authenticated && authenticated;
        }
    }
}
