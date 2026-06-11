using System;
using System.Web;
using System.Web.UI;

namespace Bit2Byte
{
    public partial class Members : ProtectedPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var displayName = Convert.ToString(Session[SessionKeys.FullName]);
            var email = Convert.ToString(Session[SessionKeys.Email]);
            GreetingLiteral.Text = "<div class=\"card\"><p><strong>Welcome, " + HttpUtility.HtmlEncode(displayName ?? email) + "</strong></p><p>You are signed in and can view the member area.</p></div>";
        }
    }
}
