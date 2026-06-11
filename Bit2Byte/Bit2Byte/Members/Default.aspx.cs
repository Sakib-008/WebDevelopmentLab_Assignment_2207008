using System;
using System.Web;

namespace Bit2Byte.MemberProfiles
{
    public partial class Default : ProtectedPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var displayName = Convert.ToString(Session[SessionKeys.FullName]);
            var email = Convert.ToString(Session[SessionKeys.Email]);

            WelcomeLiteral.Text = HttpUtility.HtmlEncode(displayName ?? email ?? "Member");
        }
    }
}