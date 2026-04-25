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
    <div class="container py-4">
        
        <div class="cart-header text-center">
            <h2 class="m-0">Your Service Bookings</h2>
            <p class="text-light m-0 mt-2">Review your selected services before finalizing your request.</p>
        </div>

        <div class="row g-4">
            <div class="col-lg-8">
                
                <asp:Label ID="lblCartMessage" runat="server" CssClass="fw-bold text-success d-block mb-3"></asp:Label>

                <asp:Panel ID="pnlCartItems" runat="server">
                    <asp:Repeater ID="rptCartItems" runat="server" OnItemCommand="rptCartItems_ItemCommand">
                        <ItemTemplate>
                            <div class="cart-item-card d-flex justify-content-between align-items-center flex-wrap gap-3">
                                <div class="d-flex align-items-center gap-3">
                                    <div class="bg-light rounded p-3 text-center" style="min-width: 80px;">
                                        <span class="fs-3"><%# Eval("Icon") %></span>
                                    </div>
                                    <div>
                                        <h5 class="fw-bold text-dark mb-1"><%# Eval("ServiceName") %></h5>
                                        <p class="text-muted small mb-0">Provider: <%# Eval("ProviderName") %></p>
                                        <p class="text-muted small mb-0">Type: <%# Eval("ServiceCategory") %></p>
                                    </div>
                                </div>
                                
                                <div class="text-end">
                                    <h5 class="fw-bold text-dark mb-2"><%# Eval("Price") %></h5>
                                    <asp:LinkButton ID="btnRemove" runat="server" CommandName="Remove" CommandArgument='<%# Eval("CartID") %>' CssClass="btn-remove small">
                                        Remove Item
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </asp:Panel>

                <asp:Panel ID="pnlEmptyCart" runat="server" Visible="false" CssClass="empty-cart-message">
                    <h3 class="text-muted">Your cart is currently empty.</h3>
                    <p class="text-muted mb-4">Looks like you haven't added any services yet.</p>
                    <a href="Services.aspx" class="btn btn-outline-primary btn-lg rounded-pill px-4">Browse Services</a>
                </asp:Panel>
            </div>

            <div class="col-lg-4">
                <div class="summary-card shadow-sm">
                    <h4 class="fw-bold mb-4 text-dark">Booking Summary</h4>
                    
                    <div class="d-flex justify-content-between mb-3 border-bottom pb-2">
                        <span class="text-muted">Total Services:</span>
                        <asp:Label ID="lblTotalItems" runat="server" CssClass="fw-bold text-dark">0</asp:Label>
                    </div>
                    
                    <div class="d-flex justify-content-between mb-4">
                        <span class="fw-bold fs-5 text-dark">Estimated Total:</span>
                        <asp:Label ID="lblTotalPrice" runat="server" CssClass="fw-bold fs-5 text-success">R 0.00</asp:Label>
                    </div>

                    <p class="small text-muted mb-4">
                        <em>* Final price may vary slightly based on specific provider quotes and duration of the task. Payment is handled securely upon completion.</em>
                    </p>

                    <asp:Button ID="btnProceedCheckout" runat="server" Text="Proceed to Checkout" CssClass="btn btn-checkout" OnClick="btnProceedCheckout_Click" />
                    
                    <div class="text-center mt-3">
                        <a href="Services.aspx" class="text-decoration-none small">Continue Browsing</a>
                    </div>
                </div>
            </div>
        </div>

    </div>
</asp:Content>