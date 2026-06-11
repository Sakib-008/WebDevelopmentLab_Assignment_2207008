using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Bit2Byte
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var isAuthenticated = Session[SessionKeys.Authenticated] is bool authenticated && authenticated;

            MembersNavItem.Visible = isAuthenticated;
            LogoutNavItem.Visible = isAuthenticated;
            LoginNavItem.Visible = !isAuthenticated;
            RegisterNavItem.Visible = !isAuthenticated;
        }
    }
}