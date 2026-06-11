<%@ Page Title="Member Home" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Bit2Byte.MemberProfiles.Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main class="page-section container">
        <section class="panel member-hero">
            <p class="eyebrow">Member Area</p>
            <h2 class="section-title">Welcome, <asp:Literal ID="WelcomeLiteral" runat="server" /></h2>
            <p class="muted">This is your home inside Bit2Byte. Use the quick actions below to update your profile or explore the community.</p>
            <div class="button-group">
                <asp:HyperLink ID="ProfileLink" runat="server" CssClass="button button-primary" NavigateUrl="~/Members/Profile.aspx">My Profile</asp:HyperLink>
                <asp:HyperLink ID="EventsLink" runat="server" CssClass="button button-secondary" NavigateUrl="~/Events.aspx">View Events</asp:HyperLink>
            </div>
        </section>

        <section class="card-grid member-cards">
            <article class="card">
                <h3>Profile</h3>
                <p>Update your avatar, bio, interests, password, and email from one place.</p>
            </article>
            <article class="card">
                <h3>Events</h3>
                <p>See the latest upcoming events and admin-posted announcements.</p>
            </article>
            <article class="card">
                <h3>Community</h3>
                <p>Stay connected with other members and contribute to club activities.</p>
            </article>
        </section>
    </main>
</asp:Content>
