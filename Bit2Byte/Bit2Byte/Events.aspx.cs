using System;
using System.Web.UI;
using Bit2Byte.Data;

namespace Bit2Byte
{
    public partial class Events : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Load events into ViewState for possible display in the markup
            var repo = new EventRepository();
            try
            {
                var events = repo.GetAll();
                ViewState["EventsList"] = events;
            }
            catch
            {
                // ignore DB errors for now; the page will fall back to static content
            }
        }
    }
}
