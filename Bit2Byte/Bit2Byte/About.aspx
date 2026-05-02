<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="Bit2Byte.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main class="page-section container">
        <h2 class="section-title">About Our Club</h2>
        <section class="content-grid">
            <article class="panel">
                <img src="https://placehold.co/600x360/e7eef7/1a202c?text=About+the+Club" alt="Students discussing club plans">
                <p>
                    Bit2Byte was formed to help KUET students connect through software research,
                    development practice, and collaborative learning.
                </p>
                <p>
                    We welcome learners from every year group and encourage a friendly, inclusive,
                    and technically focused environment.
                </p>
            </article>
            <article class="panel">
                <h3>Our Mission</h3>
                <p>
                    To provide a supportive space where members can learn, build, research, and collaborate on software ideas.
                </p>
                <h3>Club Values</h3>
                <ul>
                    <li>Curiosity and experimentation</li>
                    <li>Teamwork and peer learning</li>
                    <li>Research mindset and innovation</li>
                    <li>Practical software development</li>
                </ul>
                <p>
                    If you want to take part, visit the <a href="<%: ResolveUrl("~/register.aspx") %>">registration page</a>.
                </p>
            </article>
        </section>
    </main>
</asp:Content>
