<%@ Page Title="Events" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Events.aspx.cs" Inherits="Bit2Byte.Events" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main class="page-section container">
        <h2 class="section-title">Upcoming Events</h2>
            <p>
            Below is our sample events schedule for the semester. Each event is designed to help members code,
            research, collaborate, and improve practical software skills.
        </p>

        <asp:Literal ID="EventsStatus" runat="server" />

        <asp:Repeater ID="EventsRepeater" runat="server">
            <HeaderTemplate>
                <div class="card-grid">
            </HeaderTemplate>
            <ItemTemplate>
                <article class="card">
                    <h3><%# Eval("Title") %></h3>
                    <p class="muted">Date: <%# ((DateTime)Eval("EventDate")).ToString("yyyy-MM-dd HH:mm") %></p>
                    <p><%# Eval("Description") ?? "" %></p>
                </article>
            </ItemTemplate>
            <FooterTemplate>
                </div>
            </FooterTemplate>
        </asp:Repeater>
    </main>
</asp:Content>