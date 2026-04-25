<%@ Page Title="View Cart" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewCart.aspx.cs" Inherits="Group_9.ViewCart" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .cart-header {
            background-color: darkslategray;
            color: white;
            padding: 20px;
            border-radius: 12px;
            margin-bottom: 30px;
        }
        .cart-item-card {
            background-color: white;
            border: 1px solid lightgray;
            border-radius: 12px;
            padding: 20px;
            margin-bottom: 15px;
            box-shadow: 0 4px 6px whitesmoke;
        }
        .summary-card {
            background-color: whitesmoke;
            border: 1px solid lightgray;
            border-radius: 12px;
            padding: 25px;
            position: sticky;
            top: 20px;
        }
        .btn-checkout {
            background-color: dodgerblue;
            color: white;
            font-weight: bold;
            width: 100%;
            padding: 12px;
            border-radius: 25px;
        }
        .btn-remove {
            color: crimson;
            background: none;
            border: none;
            text-decoration: underline;
            padding: 0;
        }
        .btn-remove:hover {
            color: darkred;
        }
        .empty-cart-message {
            text-align: center;
            padding: 50px 20px;
            background-color: whitesmoke;
            border-radius: 12px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container py-5">
        <h2 class="fw-bold mb-4">Your Cart</h2>
        
        <div class="row">
            <div class="col-lg-8">
                <asp:Repeater ID="rptCart" runat="server" OnItemCommand="rptCart_ItemCommand">
                    <ItemTemplate>
                        <div class="ios-card mb-3 d-flex justify-content-between align-items-center">
                            <div>
                                <h5 class="fw-bold"><%# Eval("ServiceName") %></h5>
                                <p class="text-muted mb-0">Price: R <%# Eval("Price") %></p>
                            </div>
                            <asp:LinkButton ID="btnRemove" runat="server" 
                                CommandName="Remove" 
                                CommandArgument='<%# Eval("CartID") %>' 
                                CssClass="btn btn-outline-danger btn-sm rounded-pill">Remove</asp:LinkButton>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                
                <asp:Panel ID="pnlEmpty" runat="server" Visible="false" CssClass="text-center py-5">
                    <h4>Your cart is empty.</h4>
                    <a href="BrowseServices.aspx" class="btn btn-primary rounded-pill">Continue Browsing</a>
                </asp:Panel>
            </div>

            <div class="col-lg-4">
                <div class="ios-card">
                    <h5 class="fw-bold mb-3">Order Summary</h5>
                    <div class="d-flex justify-content-between mb-3">
                        <span>Total:</span>
                        <span class="fw-bold"><asp:Label ID="lblTotal" runat="server" Text="R 0.00"></asp:Label></span>
                    </div>
                    <asp:Button ID="btnCheckout" runat="server" Text="Checkout" CssClass="btn btn-primary w-100 rounded-pill" OnClick="btnCheckout_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>