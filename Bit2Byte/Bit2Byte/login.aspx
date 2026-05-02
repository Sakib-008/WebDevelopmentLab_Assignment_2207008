<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Bit2Byte.Login" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main class="page-section container">
        <h2 class="section-title">Member Login</h2>
        <div class="form-wrapper">
            <asp:ValidationSummary ID="LoginValidationSummary" runat="server" CssClass="validation-summary" HeaderText="Please fix the following:" />
            <asp:Label ID="StatusLabel" runat="server" EnableViewState="false" />

            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="EmailTextBox" Text="Email Address" />
                <asp:TextBox ID="EmailTextBox" runat="server" TextMode="Email" placeholder="name@kuet.ac.bd" />
                <asp:RequiredFieldValidator ID="LoginEmailRequired" runat="server" ControlToValidate="EmailTextBox" ErrorMessage="Email is required." Display="Dynamic" CssClass="validation-error" />
                <asp:RegularExpressionValidator ID="LoginEmailValidator" runat="server" ControlToValidate="EmailTextBox" ErrorMessage="Use a KUET email address (name@kuet.ac.bd)." Display="Dynamic" CssClass="validation-error" ValidationExpression="^[^\s@]+@kuet\.ac\.bd$" />
            </div>

            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="PasswordTextBox" Text="Password" />
                <asp:TextBox ID="PasswordTextBox" runat="server" TextMode="Password" placeholder="Enter your password" />
                <asp:RequiredFieldValidator ID="LoginPasswordRequired" runat="server" ControlToValidate="PasswordTextBox" ErrorMessage="Password is required." Display="Dynamic" CssClass="validation-error" />
            </div>

            <div class="form-group">
                <asp:Button ID="LoginButton" runat="server" Text="Login" CssClass="submit-button" OnClick="LoginButton_Click" />
            </div>
        </div>
    </main>
</asp:Content>