<%@ Page Title="Find a Provider | EasternDigital" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BrowseServices.aspx.cs" Inherits="Group_9.BrowseServices" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
    .service-card {
        transition: transform 0.3s ease, box-shadow 0.3s ease;
        border: none;
        border-radius: 12px;
        box-shadow: 0 4px 6px whitesmoke;
        height: 100%;
    }
    .service-card:hover {
        transform: translateY(-5px);
        box-shadow: 0 10px 20px lightgray;
    }
    .card-icon-wrapper {
        background-color: whitesmoke;
        border-radius: 12px 12px 0 0;
        padding: 30px;
        text-align: center;
        border-bottom: 2px solid lightgray;
    }
    .category-badge {
        position: absolute;
        top: 15px;
        right: 15px;
    }
    .search-bar-container {
        background-color: darkslategray;
        border-radius: 12px;
        padding: 20px;
        margin-bottom: 30px;
    }
</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="container py-4">
        <div class="text-center mb-5">
            <h2 class="display-6 fw-bold text-dark">EasternDigital Marketplace</h2>
            <p class="text-muted fs-5">Browse affordable services provided by local students.</p>
        </div>

        <div class="ios-card p-3 mb-4 d-flex gap-2">
            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control ios-input" placeholder="Search services..."></asp:TextBox>
            <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-select ios-input">
                <asp:ListItem Text="All Categories" Value="All" />
                <asp:ListItem Text="Tutoring Services" Value="Tutoring" />
                <asp:ListItem Text="Printing Services" Value="Printing" />
                <asp:ListItem Text="Graphic Design" Value="Graphic Design" />
            </asp:DropDownList>
            <asp:Button ID="btnSearch" runat="server" Text="Filter" CssClass="btn btn-primary ios-btn" OnClick="btnSearch_Click" />
        </div>

        <div class="row row-cols-1 row-cols-md-2 row-cols-lg-4 g-4">
            <asp:Repeater ID="rptServices" runat="server" OnItemCommand="rptServices_ItemCommand">
                <ItemTemplate>
                    <div class="col">
                        <div class="card service-card h-100">
                            <span class="badge bg-success category-badge"><%# Eval("Tag") %></span>
                            <div class="card-icon-wrapper">
                                <h1 class="display-4 mb-0"><%# Eval("Icon") %></h1>
                            </div>
                            <div class="card-body">
                                <h5 class="card-title fw-bold"><%# Eval("ServiceName") %></h5>
                                <p class="card-text text-muted small"><%# Eval("Description") %></p>
                                <hr />
                                <div class="d-flex justify-content-between align-items-center">
                                    <span class="fw-bold text-dark">R <%# Eval("Price") %></span>
                                    <asp:Button ID="btnView" runat="server" 
                                        Text="View Providers" 
                                        CommandName="View" 
                                        CommandArgument='<%# Eval("ServiceID") %>' 
                                        CssClass="btn btn-sm btn-outline-primary" />
                                </div>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>