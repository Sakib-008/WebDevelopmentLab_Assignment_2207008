<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Bit2Byte.Register" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main class="page-section container">
        <h2 class="section-title">Register for Bit2Byte</h2>
        <div class="form-wrapper">
            <asp:ValidationSummary ID="RegisterValidationSummary" runat="server" CssClass="validation-summary" HeaderText="Please fix the following:" />
            <asp:Label ID="StatusLabel" runat="server" EnableViewState="false" />

            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="FullNameTextBox" Text="Full Name" />
                <asp:TextBox ID="FullNameTextBox" runat="server" placeholder="Enter your full name" />
                <asp:RequiredFieldValidator ID="FullNameRequired" runat="server" ControlToValidate="FullNameTextBox" ErrorMessage="Full name is required." Display="Dynamic" CssClass="validation-error" />
            </div>

            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="EmailTextBox" Text="Email Address" />
                <asp:TextBox ID="EmailTextBox" runat="server" TextMode="Email" placeholder="name@kuet.ac.bd" />
                <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="EmailTextBox" ErrorMessage="Email is required." Display="Dynamic" CssClass="validation-error" />
                <asp:RegularExpressionValidator ID="EmailFormatValidator" runat="server" ControlToValidate="EmailTextBox" ErrorMessage="Use a KUET email address (name@kuet.ac.bd)." Display="Dynamic" CssClass="validation-error" ValidationExpression="^[^\s@]+@kuet\.ac\.bd$" />
            </div>

            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="PasswordTextBox" Text="Password" />
                <asp:TextBox ID="PasswordTextBox" runat="server" TextMode="Password" placeholder="Create a password" />
                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="PasswordTextBox" ErrorMessage="Password is required." Display="Dynamic" CssClass="validation-error" />
                <asp:RegularExpressionValidator ID="PasswordLengthValidator" runat="server" ControlToValidate="PasswordTextBox" ErrorMessage="Password must be at least 8 characters long." Display="Dynamic" CssClass="validation-error" ValidationExpression="^.{8,}$" />
            </div>

            <div class="form-group">
                <asp:Label runat="server" Text="Year of Study" />
                <asp:RadioButtonList ID="YearList" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" CssClass="inline-options">
                    <asp:ListItem Value="first">First Year</asp:ListItem>
                    <asp:ListItem Value="second">Second Year</asp:ListItem>
                    <asp:ListItem Value="third">Third Year</asp:ListItem>
                    <asp:ListItem Value="fourth">Fourth Year</asp:ListItem>
                </asp:RadioButtonList>
                <asp:RequiredFieldValidator ID="YearRequired" runat="server" ControlToValidate="YearList" InitialValue="" ErrorMessage="Select your year of study." Display="Dynamic" CssClass="validation-error" />
            </div>

            <div class="form-group">
                <asp:Label runat="server" Text="Areas of Interest" />
                <asp:CheckBoxList ID="InterestList" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" CssClass="inline-options">
                    <asp:ListItem Value="research">Research</asp:ListItem>
                    <asp:ListItem Value="development">Development</asp:ListItem>
                    <asp:ListItem Value="uiux">UI/UX</asp:ListItem>
                    <asp:ListItem Value="competitive-programming">Competitive Programming</asp:ListItem>
                </asp:CheckBoxList>
                <asp:CustomValidator ID="InterestValidator" runat="server" ErrorMessage="Select at least one area of interest." Display="Dynamic" CssClass="validation-error" OnServerValidate="InterestValidator_ServerValidate" />
            </div>

            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="RoleDropDown" Text="Preferred Role" />
                <asp:DropDownList ID="RoleDropDown" runat="server">
                    <asp:ListItem Value="">Choose one</asp:ListItem>
                    <asp:ListItem Value="member">General Member</asp:ListItem>
                    <asp:ListItem Value="research">Research Contributor</asp:ListItem>
                    <asp:ListItem Value="developer">Developer</asp:ListItem>
                    <asp:ListItem Value="designer">Designer</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RoleRequired" runat="server" ControlToValidate="RoleDropDown" InitialValue="" ErrorMessage="Select a preferred role." Display="Dynamic" CssClass="validation-error" />
            </div>

            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="MessageTextBox" Text="Why do you want to join?" />
                <asp:TextBox ID="MessageTextBox" runat="server" TextMode="MultiLine" Rows="5" placeholder="Tell us a little about your interests in software research and development" />
            </div>

            <div class="form-group">
                <asp:Button ID="SubmitRegistrationButton" runat="server" Text="Submit Registration" CssClass="submit-button" OnClick="SubmitRegistrationButton_Click" />
            </div>
        </div>
    </main>
</asp:Content>