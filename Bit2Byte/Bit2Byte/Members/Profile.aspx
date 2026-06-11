<%@ Page Title="My Profile" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="Bit2Byte.MemberProfiles.Profile" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main class="page-section container profile-layout">
        <section class="profile-hero panel">
            <div class="profile-avatar-wrap">
                <asp:Image ID="AvatarImage" runat="server" CssClass="profile-avatar" ImageUrl="~/Content/Images/default-avatar.svg" AlternateText="Profile avatar" />
            </div>
            <div>
                <p class="eyebrow">Member Profile</p>
                <h2 class="section-title">Your dashboard</h2>
                <p class="muted">Update your personal details, avatar, bio, interests, and password from one place.</p>
                <asp:Literal ID="StatusLiteral" runat="server" />
            </div>
        </section>

        <section class="profile-grid">
            <div class="panel">
                <h3>Profile Details</h3>
                <div class="form-group">
                    <label>Username</label>
                    <asp:TextBox ID="UsernameTextBox" runat="server" CssClass="form-control" />
                </div>
                <div class="form-group">
                    <label>Email</label>
                    <asp:TextBox ID="EmailTextBox" runat="server" CssClass="form-control" ReadOnly="true" />
                </div>
                <div class="form-group">
                    <label>Role</label>
                    <asp:TextBox ID="RoleTextBox" runat="server" CssClass="form-control" ReadOnly="true" />
                </div>
                <div class="form-group">
                    <label>Avatar</label>
                    <asp:FileUpload ID="AvatarUpload" runat="server" CssClass="form-control" />
                </div>
                <div class="form-group">
                    <label>Bio</label>
                    <asp:TextBox ID="BioTextBox" runat="server" TextMode="MultiLine" Rows="4" CssClass="form-control" placeholder="Tell others about yourself" />
                </div>
                <div class="form-group">
                    <label>Interests</label>
                    <asp:CheckBoxList ID="InterestsCheckBoxList" runat="server" RepeatLayout="Flow" CssClass="interest-list">
                        <asp:ListItem Value="research">Research</asp:ListItem>
                        <asp:ListItem Value="development">Development</asp:ListItem>
                        <asp:ListItem Value="uiux">UI/UX</asp:ListItem>
                        <asp:ListItem Value="competitive-programming">Competitive Programming</asp:ListItem>
                        <asp:ListItem Value="web">Web Development</asp:ListItem>
                    </asp:CheckBoxList>
                </div>
                <div class="form-group">
                    <asp:Button ID="SaveProfileButton" runat="server" Text="Save Profile" CssClass="submit-button" OnClick="SaveProfileButton_Click" />
                </div>
            </div>

            <div class="panel">
                <h3>Change Password</h3>
                <div class="form-group">
                    <label>Current Password</label>
                    <asp:TextBox ID="CurrentPassword" runat="server" TextMode="Password" CssClass="form-control" />
                </div>
                <div class="form-group">
                    <label>New Password</label>
                    <asp:TextBox ID="NewPassword" runat="server" TextMode="Password" CssClass="form-control" />
                </div>
                <div class="form-group">
                    <label>Confirm New Password</label>
                    <asp:TextBox ID="ConfirmPassword" runat="server" TextMode="Password" CssClass="form-control" />
                </div>
                <hr />
                <h3>Change Email</h3>
                <p class="muted">You will need to confirm the change with a token link after requesting it.</p>
                <div class="form-group">
                    <label>New Email</label>
                    <asp:TextBox ID="NewEmailTextBox" runat="server" CssClass="form-control" TextMode="Email" />
                </div>
                <div class="form-group">
                    <label>Current Password for Email Change</label>
                    <asp:TextBox ID="EmailCurrentPasswordTextBox" runat="server" TextMode="Password" CssClass="form-control" />
                </div>
                <div class="form-group">
                    <asp:Button ID="RequestEmailChangeButton" runat="server" Text="Request Email Change" CssClass="btn btn-outline-secondary" OnClick="RequestEmailChangeButton_Click" />
                </div>
                <asp:Literal ID="EmailChangeLinkLiteral" runat="server" />
            </div>
        </section>
    </main>
</asp:Content>
