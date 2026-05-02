<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Bit2Byte._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>
        <section class="hero container">
            <div class="hero-text">
                <h2>Explore software research, build real projects, and grow with KUET.</h2>
                <p>
                    Bit2Byte is the Software Research &amp; Development Community of KUET.
                    We bring students together through coding sessions, technical discussions,
                    project collaboration, and skill-building activities throughout the year.
                </p>
                <div class="button-group">
                    <a class="button button-primary" href="<%: ResolveUrl("~/register.aspx") %>">Join Bit2Byte</a>
                    <a class="button button-secondary" href="<%: ResolveUrl("~/events.aspx") %>">View Events</a>
                </div>
            </div>
            <div class="hero-image">
                <img src="https://placehold.co/720x480/e7eef7/1a202c?text=Students+collaborating+on+software+projects" alt="Students collaborating on software projects">
            </div>
        </section>

        <section class="container card-grid">
            <article class="card">
                <h3>Welcome Message</h3>
                <p>
                    Bit2Byte is open to KUET students who want to learn software development,
                    explore research ideas, and contribute to a vibrant technical community.
                </p>
            </article>
            <article class="card">
                <h3>What We Do</h3>
                <ul>
                    <li>Weekly coding and problem-solving sessions</li>
                    <li>Research discussions and project showcases</li>
                    <li>Workshops on tools, frameworks, and software practice</li>
                </ul>
            </article>
            <article class="card">
                <h3>Quick Links</h3>
                <p>Explore the club pages below:</p>
                <ul>
                    <li><a href="<%: ResolveUrl("~/about.aspx") %>">Learn more about Bit2Byte</a></li>
                    <li><a href="<%: ResolveUrl("~/events.aspx") %>">See upcoming events</a></li>
                    <li><a href="<%: ResolveUrl("~/members.aspx") %>">Meet our members</a></li>
                </ul>
            </article>
        </section>
    </main>

</asp:Content>
