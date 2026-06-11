<%@ Page Title="Manage Users" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="Bit2Byte.Admin.Users" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main class="page-section container">
        <h2 class="section-title">Admin &mdash; Users</h2>
        <div class="admin-toolbar"><asp:HyperLink runat="server" CssClass="btn btn-outline-secondary" NavigateUrl="~/Admin/Events.aspx">Manage Events</asp:HyperLink></div>
        <asp:Literal ID="StatusLiteral" runat="server" />
        <asp:GridView ID="UsersGrid" runat="server" AutoGenerateColumns="false" CssClass="panel admin-grid" GridLines="Both" DataKeyNames="Id"
            OnRowDeleting="UsersGrid_RowDeleting" OnRowEditing="UsersGrid_RowEditing" OnRowCancelingEdit="UsersGrid_RowCancelingEdit" OnRowUpdating="UsersGrid_RowUpdating">
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="#" />
                <asp:BoundField DataField="Username" HeaderText="Name" />
                <asp:BoundField DataField="Email" HeaderText="Email" />
                <asp:BoundField DataField="Role" HeaderText="Role" />
                <asp:CheckBoxField DataField="IsActive" HeaderText="Active" />
                <asp:TemplateField HeaderText="Actions">
                    <ItemTemplate>
                        <asp:LinkButton runat="server" CommandName="Edit" Text="Edit" CssClass="cmd-link" />
                        &nbsp;
                        <asp:LinkButton runat="server" CommandName="Delete" Text="Delete" CssClass="cmd-link" OnClientClick="return confirm('Delete this user?');" />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:LinkButton runat="server" CommandName="Update" Text="Save" CssClass="btn btn-primary" />
                        &nbsp;
                        <asp:LinkButton runat="server" CommandName="Cancel" Text="Cancel" CssClass="cmd-link" />
                    </EditItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </main>
</asp:Content>
