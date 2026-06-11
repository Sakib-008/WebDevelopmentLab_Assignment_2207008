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
            bool isAdmin = false;
            var userIdObj = Session[SessionKeys.UserId];
            if (userIdObj is int uid)
            {
                var repo = new Data.UserRepository();
                var user = repo.GetById(uid);
                isAdmin = user != null && string.Equals(user.Role, "admin", StringComparison.OrdinalIgnoreCase);
            }
            AdminNavItem.Visible = isAdmin;
        }
    }
}