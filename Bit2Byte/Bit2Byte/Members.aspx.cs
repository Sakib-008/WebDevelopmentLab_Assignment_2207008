using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using Bit2Byte.Data;

namespace Bit2Byte
{
    public partial class Members : ProtectedPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindMembers();
            }

            var displayName = Convert.ToString(Session[SessionKeys.FullName]);
            var email = Convert.ToString(Session[SessionKeys.Email]);
            GreetingLiteral.Text = "<div class=\"card\"><p><strong>Welcome, " + HttpUtility.HtmlEncode(displayName ?? email) + "</strong></p><p>You are signed in and can view the member area.</p></div>";
            ProfileLink.Visible = Session[SessionKeys.Authenticated] is bool a && a;
        }

        private void BindMembers()
        {
            var members = new UserRepository().GetActiveMembers().ToList();
            MembersRepeater.DataSource = members;
            MembersRepeater.DataBind();
            EmptyMembersPanel.Visible = members.Count == 0;
        }

        protected string GetAvatarUrl(object avatarPath)
        {
            var path = Convert.ToString(avatarPath);
            return ResolveUrl(string.IsNullOrWhiteSpace(path) ? "~/Content/Images/default-avatar.svg" : path);
        }

        protected string FormatBio(object bio)
        {
            var text = Convert.ToString(bio)?.Trim();
            if (string.IsNullOrEmpty(text))
            {
                return "No bio yet.";
            }

            return text.Length <= 140 ? text : text.Substring(0, 137) + "...";
        }

        protected string FormatRole(object role)
        {
            var value = Convert.ToString(role)?.Trim();
            return string.IsNullOrEmpty(value) ? "Member" : value;
        }

        protected string FormatInterestTags(object interests)
        {
            var raw = Convert.ToString(interests);
            if (string.IsNullOrWhiteSpace(raw))
            {
                return string.Empty;
            }

            var tags = raw
                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(item => HttpUtility.HtmlEncode(item.Trim()))
                .Where(item => item.Length > 0)
                .Take(6)
                .Select(item => "<span class=\"member-interest-tag\">" + item + "</span>");

            var markup = string.Join(string.Empty, tags);
            return markup.Length == 0 ? string.Empty : "<div class=\"member-interest-tags\">" + markup + "</div>";
        }
    }
}
