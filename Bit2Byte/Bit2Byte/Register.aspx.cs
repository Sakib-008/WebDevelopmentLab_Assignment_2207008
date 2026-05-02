using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;

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

            Session[SessionKeys.FullName] = FullNameTextBox.Text.Trim();
            Session[SessionKeys.Email] = EmailTextBox.Text.Trim();
            Session[SessionKeys.Password] = PasswordTextBox.Text;
            Session[SessionKeys.Year] = YearList.SelectedValue;
            Session[SessionKeys.Interests] = string.Join(",", selectedInterests);
            Session[SessionKeys.Role] = RoleDropDown.SelectedValue;
            Session[SessionKeys.Message] = MessageTextBox.Text.Trim();
            Session[SessionKeys.Authenticated] = true;
            Session[SessionKeys.Registered] = true;

            Response.Redirect(ResolveUrl("~/members.aspx"), true);
        }

        protected void InterestValidator_ServerValidate(object source, System.Web.UI.WebControls.ServerValidateEventArgs args)
        {
            args.IsValid = InterestList.Items.Cast<System.Web.UI.WebControls.ListItem>().Any(item => item.Selected);
        }
    }
}
