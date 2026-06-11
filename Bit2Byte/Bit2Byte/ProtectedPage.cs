using System;
using System.Web;
using System.Web.UI;

namespace Bit2Byte
{
    public abstract class ProtectedPage : Page
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsUserAuthenticated())
            {
                Response.Redirect(ResolveUrl("~/login.aspx"), true);
            }
        }

        protected bool IsUserAuthenticated()
        {
            return Session[SessionKeys.Authenticated] is bool authenticated && authenticated;
        }
    }
}
