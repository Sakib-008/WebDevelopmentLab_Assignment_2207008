using System;
using System.Web.UI;
using Bit2Byte.Data;

namespace Bit2Byte
{
    public partial class Events : ProtectedPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var repo = new EventRepository();
            try
            {
                var events = repo.GetAll();
                var list = events == null ? new System.Collections.Generic.List<Data.Models.EventItem>() : new System.Collections.Generic.List<Data.Models.EventItem>(events);
                if (list.Count == 0)
                {
                    EventsStatus.Text = "<div class=\"panel\"><h3>No upcoming events</h3><p>There are currently no scheduled events. Check back later or contact an administrator to add events.</p></div>";
                    EventsRepeater.DataSource = null;
                    EventsRepeater.DataBind();
                }
                else
                {
                    EventsStatus.Text = string.Empty;
                    EventsRepeater.DataSource = list;
                    EventsRepeater.DataBind();
                }
            }
            catch (Exception ex)
            {
                EventsStatus.Text = "<div class=\"validation-error\">Error loading events.</div>";
                System.Diagnostics.Trace.TraceError("Events load error: " + ex);
            }
        }
    }
}
