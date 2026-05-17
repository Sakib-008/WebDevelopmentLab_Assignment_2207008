using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Diagnostics;
using Bit2Byte.Data;
using Bit2Byte.Data.Models;

namespace Bit2Byte
{
    public partial class Register : Page
    {
        protected void SubmitRegistrationButton_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (!Page.IsValid)
            {
                StatusLabel.Text = "";
                return;
            }

            var selectedInterests = InterestList.Items.Cast<System.Web.UI.WebControls.ListItem>()
                .Where(item => item.Selected)
                .Select(item => item.Value)
                .ToArray();

            // Create user in database
            var repo = new UserRepository();
            var user = new User
            {
                Username = FullNameTextBox.Text.Trim(),
                Email = EmailTextBox.Text.Trim(),
                PasswordHash = PasswordHelper.HashPassword(PasswordTextBox.Text),
                IsActive = true
            };

            try
            {
                int userId = repo.Create(user);
                // store minimal session info
                Session[SessionKeys.UserId] = userId;
                Session[SessionKeys.FullName] = user.Username;
                Session[SessionKeys.Email] = user.Email;
                Session[SessionKeys.Authenticated] = true;
                Session[SessionKeys.Registered] = true;

                Response.Redirect(ResolveUrl("~/members.aspx"), true);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError("Registration error: " + ex);
                StatusLabel.Text = "Registration failed. Please try again later.";
            }
        }

        protected void InterestValidator_ServerValidate(object source, System.Web.UI.WebControls.ServerValidateEventArgs args)
        {
            args.IsValid = InterestList.Items.Cast<System.Web.UI.WebControls.ListItem>().Any(item => item.Selected);
        }
    }
}
