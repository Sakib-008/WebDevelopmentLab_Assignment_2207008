using System;
using Bit2Byte.Data;
using Bit2Byte.Data.Models;

namespace Bit2Byte.Admin
{
    public partial class Events : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) LoadEvents();
        }

        private void LoadEvents()
        {
            var repo = new EventRepository();
            EventsGrid.DataSource = repo.GetAll();
            EventsGrid.DataBind();
        }

        protected void AddEventButton_Click(object sender, EventArgs e)
        {
            var title = NewTitle.Text.Trim();
            DateTime date;
            if (!DateTime.TryParse(NewDate.Text, out date))
            {
                StatusLiteral.Text = "<div class=\"validation-error\">Enter a valid date.</div>";
                return;
            }

            var userId = Session[SessionKeys.UserId] is int uid ? (int?)uid : null;
            var repo = new EventRepository();
            var ev = new EventItem
            {
                Title = title,
                Description = NewDescription.Text.Trim(),
                EventDate = date,
                CreatedByUserId = userId
            };
            repo.Create(ev);
            StatusLiteral.Text = "Event added.";
            NewTitle.Text = string.Empty;
            NewDate.Text = string.Empty;
            NewDescription.Text = string.Empty;
            LoadEvents();
        }

        protected void EventsGrid_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            var id = Convert.ToInt32(EventsGrid.DataKeys[e.RowIndex].Value);
            var repo = new EventRepository();
            repo.Delete(id);
            StatusLiteral.Text = "Event deleted.";
            LoadEvents();
        }

        protected void EventsGrid_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            EventsGrid.EditIndex = e.NewEditIndex;
            LoadEvents();
        }

        protected void EventsGrid_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
        {
            EventsGrid.EditIndex = -1;
            LoadEvents();
        }

        protected void EventsGrid_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
        {
            var id = Convert.ToInt32(EventsGrid.DataKeys[e.RowIndex].Value);
            var row = EventsGrid.Rows[e.RowIndex];
            var title = ((System.Web.UI.WebControls.TextBox)row.Cells[1].Controls[0]).Text.Trim();
            var dateStr = ((System.Web.UI.WebControls.TextBox)row.Cells[2].Controls[0]).Text.Trim();
            DateTime date;
            if (!DateTime.TryParse(dateStr, out date))
            {
                StatusLiteral.Text = "<div class=\"validation-error\">Enter a valid date.</div>";
                return;
            }

            var repo = new EventRepository();
            var ev = repo.GetById(id);
            if (ev != null)
            {
                ev.Title = title;
                ev.EventDate = date;
                repo.Update(ev);
                StatusLiteral.Text = "Event updated.";
            }

            EventsGrid.EditIndex = -1;
            LoadEvents();
        }
    }
}
