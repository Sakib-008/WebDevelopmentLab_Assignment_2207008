using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using Bit2Byte.Data;
using Bit2Byte.Data.Models;

namespace Bit2Byte.MemberProfiles
{
    public partial class Profile : ProtectedPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindProfile();
            }
        }

        protected void SaveProfileButton_Click(object sender, EventArgs e)
        {
            if (!(Session[SessionKeys.UserId] is int uid))
            {
                Response.Redirect(ResolveUrl("~/login.aspx"), true);
                return;
            }

            var repo = new UserRepository();
            var user = repo.GetById(uid);
            if (user == null)
            {
                StatusLiteral.Text = "<div class=\"validation-error\">User not found.</div>";
                return;
            }

            user.Username = UsernameTextBox.Text.Trim();
            user.Bio = BioTextBox.Text.Trim();
            user.Interests = string.Join(",", GetSelectedInterests());

            if (AvatarUpload.HasFile)
            {
                var avatarPath = SaveAvatar(user.Id);
                if (avatarPath == null)
                {
                    return;
                }
                user.AvatarPath = avatarPath;
            }

            // Handle password change if requested
            var current = CurrentPassword.Text;
            var np = NewPassword.Text;
            var cp = ConfirmPassword.Text;
            if (!string.IsNullOrEmpty(np) || !string.IsNullOrEmpty(cp) || !string.IsNullOrEmpty(current))
            {
                if (np != cp)
                {
                    StatusLiteral.Text = "<div class=\"validation-error\">New passwords do not match.</div>";
                    return;
                }
                if (string.IsNullOrEmpty(current) || !PasswordHelper.VerifyPassword(current, user.PasswordHash))
                {
                    StatusLiteral.Text = "<div class=\"validation-error\">Current password is incorrect.</div>";
                    return;
                }

                user.PasswordHash = PasswordHelper.HashPassword(np);
            }

            try
            {
                repo.Update(user);
                Session[SessionKeys.FullName] = user.Username;
                EmailTextBox.Text = user.Email;
                RoleTextBox.Text = user.Role;
                BindAvatar(user);
                StatusLiteral.Text = "<div class=\"success-message\">Profile updated.</div>";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError("Profile save error: " + ex);
                StatusLiteral.Text = "<div class=\"validation-error\">Failed to save profile.</div>";
            }
        }

        private void BindProfile()
        {
            if (!(Session[SessionKeys.UserId] is int uid))
            {
                Response.Redirect(ResolveUrl("~/login.aspx"), true);
                return;
            }

            var repo = new UserRepository();
            var user = repo.GetById(uid);
            if (user == null)
            {
                Response.Redirect(ResolveUrl("~/login.aspx"), true);
                return;
            }

            UsernameTextBox.Text = user.Username;
            EmailTextBox.Text = user.Email;
            RoleTextBox.Text = user.Role;
            BioTextBox.Text = user.Bio ?? string.Empty;
            BindInterests(user.Interests);
            BindAvatar(user);
     
        }

        private void BindAvatar(Data.Models.User user)
        {
            var avatar = string.IsNullOrWhiteSpace(user.AvatarPath) ? ResolveUrl("~/Content/Images/default-avatar.svg") : ResolveUrl(user.AvatarPath);
            AvatarImage.ImageUrl = avatar;
        }

        private void BindInterests(string interests)
        {
            var selected = new HashSet<string>((interests ?? string.Empty).Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()), StringComparer.OrdinalIgnoreCase);
            foreach (System.Web.UI.WebControls.ListItem item in InterestsCheckBoxList.Items)
            {
                item.Selected = selected.Contains(item.Value);
            }
        }

        private IEnumerable<string> GetSelectedInterests()
        {
            return InterestsCheckBoxList.Items.Cast<System.Web.UI.WebControls.ListItem>()
                .Where(item => item.Selected)
                .Select(item => item.Value);
        }

        private string SaveAvatar(int userId)
        {
            try
            {
                var extension = Path.GetExtension(AvatarUpload.FileName).ToLowerInvariant();
                var allowed = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { ".png", ".jpg", ".jpeg", ".gif", ".webp", ".svg" };
                if (!allowed.Contains(extension))
                {
                    StatusLiteral.Text = "<div class=\"validation-error\">Unsupported avatar file type.</div>";
                    return null;
                }

                var folder = Server.MapPath("~/Uploads/avatars");
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                var fileName = "user_" + userId + "_" + Guid.NewGuid().ToString("N") + extension;
                var filePath = Path.Combine(folder, fileName);
                AvatarUpload.SaveAs(filePath);
                return "~/Uploads/avatars/" + fileName;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError("Avatar save error: " + ex);
                StatusLiteral.Text = "<div class=\"validation-error\">Unable to save avatar.</div>";
                return null;
            }
        }
    }
}