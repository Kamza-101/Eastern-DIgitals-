<%@ Page Title="Service Providers" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ServiceDetails.aspx.cs" Inherits="Group_9.ServiceDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .page-header {
            background-color: darkslategray;
            color: white;
            padding: 30px;
            border-radius: 12px;
            margin-bottom: 30px;
            text-align: center;
        }
        .provider-card {
            background-color: white;
            border: 1px solid lightgray;
            border-radius: 12px;
            padding: 20px;
            margin-bottom: 20px;
            box-shadow: 0 4px 6px whitesmoke;
            transition: transform 0.2s ease;
        }
        .provider-card:hover {
            transform: translateY(-3px);
            box-shadow: 0 8px 15px lightgray;
        }
        .provider-avatar {
            background-color: dodgerblue;
            color: white;
            width: 60px;
            height: 60px;
            border-radius: 50%;
            display: flex;
            align-items: center;
            justify-content: center;
            font-size: 24px;
            font-weight: bold;
        }
        .btn-book {
            background-color: dodgerblue;
            color: white;
            font-weight: bold;
            border-radius: 20px;
            padding: 8px 20px;
        }
        .btn-book:hover {
            background-color: royalblue;
            color: white;
        }
        .price-tag {
            color: seagreen;
            font-weight: bold;
            font-size: 1.2rem;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container py-4">
        
        <div class="page-header">
            <h2 class="m-0">
                <asp:Label ID="lblCategoryTitle" runat="server" Text="Available Providers"></asp:Label>
            </h2>
            <p class="text-light m-0 mt-2">Select a provider below to view their portfolio or book their service.</p>
        </div>

        <div class="row">
            <asp:Repeater ID="rptProviders" runat="server" OnItemCommand="rptProviders_ItemCommand">
                <ItemTemplate>
                    <div class="col-md-12">
                        <div class="provider-card d-flex flex-column flex-md-row justify-content-between align-items-md-center gap-3">
                            
                            <div class="d-flex align-items-center gap-3">
                                <div class="provider-avatar">
                                    <%# Eval("Initials") %>
                                </div>
                                
                                <div>
                                    <h5 class="fw-bold text-dark mb-1"><%# Eval("ProviderName") %></h5>
                                    <p class="text-muted small mb-1"><strong>Service:</strong> <%# Eval("ServiceDesc") %></p>
                                    <p class="text-muted small mb-0">
                                        📍 <%# Eval("Location") %> | ⭐ <%# Eval("Rating") %>/5.0
                                    </p>
                                </div>
                            </div>
                            
                            <div class="text-md-end mt-3 mt-md-0">
                                <div class="price-tag mb-2"><%# Eval("Price") %></div>
                                <asp:LinkButton ID="btnBook" runat="server" CssClass="btn btn-book" CommandName="Book" CommandArgument='<%# Eval("ProviderID") %>'>
                                    Book Service
                                </asp:LinkButton>
                            </div>

                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>

            <asp:Label ID="lblNoProviders" runat="server" CssClass="text-center text-muted fw-bold d-block mt-4" Visible="false">
                Currently, there are no providers listed in this category.
            </asp:Label>
        </div>

        <div class="text-center mt-4">
            <a href="BrowseServices.aspx" class="btn btn-outline-dark rounded-pill px-4">Back to Categories</a>
        </div>

    </div>
</asp:Content>