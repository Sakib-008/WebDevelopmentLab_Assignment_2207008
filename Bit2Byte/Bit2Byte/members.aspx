<%@ Page Title="Members" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Members.aspx.cs" Inherits="Bit2Byte.Members" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main class="page-section container">
        <h2 class="section-title">Meet Our Members</h2>
        <asp:Literal ID="GreetingLiteral" runat="server" />
        <section class="content-grid">
            <article class="panel">
                <img src="https://placehold.co/600x360/dbe9f5/1a202c?text=Bit2Byte+Team" alt="Bit2Byte team members collaborating">
                <h3>Executive Team</h3>
                <ul>
                    <li>Research Lead: Guides technical exploration and paper review</li>
                    <li>Development Lead: Coordinates software projects and prototypes</li>
                    <li>Secretary: Records notes and manages announcements</li>
                    <li>Design Lead: Supports UI, UX, and presentation quality</li>
                </ul>
            </article>
            <article class="panel">
                <img src="https://placehold.co/600x360/e9f1fb/1a202c?text=Research+%26+Development" alt="Group of students representing the community">
                <h3>Member Highlights</h3>
                <p>Our members come from different departments and contribute to a range of software ideas.</p>
                <ol>
                    <li>Regular attendees join weekly technical discussions.</li>
                    <li>Project members help build tools, apps, and research demos.</li>
                    <li>New members are welcomed and guided by mentors.</li>
                </ol>
                <p>
                    Want to become part of the team? Head to the <a href="<%: ResolveUrl("~/register.aspx") %>">registration form</a>.
                </p>
            </article>
        </section>
    </main>
</asp:Content>