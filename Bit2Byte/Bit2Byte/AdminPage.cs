using System;

namespace Bit2Byte
{
    public abstract class AdminPage : ProtectedPage
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            var userIdObj = Session[SessionKeys.UserId];
            if (!(userIdObj is int userId))
            {
                Response.Redirect(ResolveUrl("~/login.aspx"), true);
                return;
            }

            var repo = new Data.UserRepository();
            var user = repo.GetById(userId);
            if (user == null || !string.Equals(user.Role, "admin", StringComparison.OrdinalIgnoreCase))
            {
                Response.Redirect(ResolveUrl("~/"), true);
                return;
            }
        }
    }
}
