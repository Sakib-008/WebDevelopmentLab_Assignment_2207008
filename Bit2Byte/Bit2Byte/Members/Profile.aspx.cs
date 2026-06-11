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

        protected void RequestEmailChangeButton_Click(object sender, EventArgs e)
        {
            if (!(Session[SessionKeys.UserId] is int uid))
            {
                Response.Redirect(ResolveUrl("~/login.aspx"), true);
                return;
            }

            var newEmail = NewEmailTextBox.Text.Trim();
            var password = EmailCurrentPasswordTextBox.Text;

            if (string.IsNullOrWhiteSpace(newEmail) || !Regex.IsMatch(newEmail, @"^[^\s@]+@[^\s@]+\.[^\s@]+$"))
            {
                EmailChangeLinkLiteral.Text = "<div class=\"validation-error\">Enter a valid email address.</div>";
                return;
            }

            var repo = new UserRepository();
            var user = repo.GetById(uid);
            if (user == null)
            {
                EmailChangeLinkLiteral.Text = "<div class=\"validation-error\">User not found.</div>";
                return;
            }

            if (!PasswordHelper.VerifyPassword(password, user.PasswordHash))
            {
                EmailChangeLinkLiteral.Text = "<div class=\"validation-error\">Current password is incorrect.</div>";
                return;
            }

            if (string.Equals(newEmail, user.Email, StringComparison.OrdinalIgnoreCase))
            {
                EmailChangeLinkLiteral.Text = "<div class=\"validation-error\">New email must be different from the current one.</div>";
                return;
            }

            if (repo.IsEmailInUse(newEmail, user.Id))
            {
                EmailChangeLinkLiteral.Text = "<div class=\"validation-error\">That email is already in use.</div>";
                return;
            }

            var token = Guid.NewGuid().ToString("N");
            var expiresUtc = DateTime.UtcNow.AddHours(24);
            if (repo.RequestEmailChange(user.Id, newEmail, token, expiresUtc))
            {
                var confirmUrl = ResolveUrl("~/Members/ConfirmEmailChange.aspx?token=" + HttpUtility.UrlEncode(token));
                EmailChangeLinkLiteral.Text = "<div class=\"success-message\">Confirmation link generated. Open this link to confirm your new email: <a href='" + confirmUrl + "'>" + confirmUrl + "</a></div>";
                BindProfile();
            }
            else
            {
                EmailChangeLinkLiteral.Text = "<div class=\"validation-error\">Unable to create email change request.</div>";
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

            if (!string.IsNullOrWhiteSpace(user.PendingEmail))
            {
                EmailChangeLinkLiteral.Text = "<div class=\"validation-summary\">Pending email change: <strong>" + HttpUtility.HtmlEncode(user.PendingEmail) + "</strong></div>";
            }
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
