<%@ Page Title="Manage Events" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Events.aspx.cs" Inherits="Bit2Byte.Admin.Events" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main class="page-section container">
        <h2 class="section-title">Admin &mdash; Events</h2>
        <div class="admin-toolbar"><asp:HyperLink runat="server" CssClass="btn btn-outline-secondary" NavigateUrl="~/Admin/Users.aspx">Manage Users</asp:HyperLink></div>

        <asp:Literal ID="StatusLiteral" runat="server" />

        <section class="panel">
            <h3>Add New Event</h3>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="NewTitle" Text="Title" />
                <asp:TextBox ID="NewTitle" runat="server" CssClass="form-control" />
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="NewDate" Text="Event Date" />
                <asp:TextBox ID="NewDate" runat="server" TextMode="DateTime" CssClass="form-control" placeholder="yyyy-MM-dd HH:mm" />
                <p class="form-hint muted">Format: yyyy-MM-dd HH:mm (example: 2026-06-12 14:30). Date only, such as 2026-06-12, also works.</p>
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="NewDescription" Text="Description" />
                <asp:TextBox ID="NewDescription" runat="server" TextMode="MultiLine" Rows="4" CssClass="form-control" />
            </div>
            <div class="form-group">
                <asp:Button ID="AddEventButton" runat="server" Text="Add Event" CssClass="submit-button" OnClick="AddEventButton_Click" />
            </div>
        </section>

        <section class="panel admin-table-panel">
            <h3>Existing Events</h3>
            <asp:GridView ID="EventsGrid" runat="server" AutoGenerateColumns="false" CssClass="admin-grid" DataKeyNames="Id"
                OnRowEditing="EventsGrid_RowEditing" OnRowCancelingEdit="EventsGrid_RowCancelingEdit" OnRowUpdating="EventsGrid_RowUpdating" OnRowDeleting="EventsGrid_RowDeleting">
                <HeaderStyle CssClass="admin-grid-header" />
                <RowStyle CssClass="admin-grid-row" />
                <AlternatingRowStyle CssClass="admin-grid-row-alt" />
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="#" ReadOnly="true" />
                    <asp:BoundField DataField="Title" HeaderText="Title" />
                    <asp:BoundField DataField="EventDate" HeaderText="Date" DataFormatString="{0:yyyy-MM-dd}" />
                    <asp:BoundField DataField="CreatedAt" HeaderText="Created" DataFormatString="{0:yyyy-MM-dd}" />
                    <asp:TemplateField HeaderText="Actions">
                        <ItemTemplate>
                            <asp:LinkButton runat="server" CommandName="Edit" Text="Edit" CssClass="cmd-link" />
                            &nbsp;
                            <asp:LinkButton runat="server" CommandName="Delete" Text="Delete" CssClass="cmd-link" OnClientClick="return confirm('Delete this event?');" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:LinkButton runat="server" CommandName="Update" Text="Save" CssClass="btn btn-primary" />
                            &nbsp;
                            <asp:LinkButton runat="server" CommandName="Cancel" Text="Cancel" CssClass="cmd-link" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </section>
    </main>
</asp:Content>
