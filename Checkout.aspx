<%@ Page Title="Checkout" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Checkout.aspx.cs" Inherits="Group_9.Checkout" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
            .checkout-container {
            max-width: 800px;
            margin: 3rem auto;
            background: white;
            border-radius: 20px;
            padding: 3rem;
            box-shadow: 0 10px 30px gainsboro;
        }
        .summary-box {
            background-color: whitesmoke;
            border-radius: 15px;
            padding: 20px;
            margin-bottom: 2rem;
        }
        .ios-input {
            border-radius: 12px;
            border: 1px solid lightgray;
            padding: 12px;
            background-color: whitesmoke;
        }
        .btn-pay {
            background-color: royalblue;
            color: white;
            border-radius: 30px;
            padding: 15px;
            font-weight: bold;
            width: 100%;
            transition: transform 0.2s;
        }
        .btn-pay:hover {
            transform: scale(1.02);
            color: white;
        }
        .success-icon {
            font-size: 5rem;
            color: mediumseagreen;
        }
    </style>
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   <div class="container">
        
        <asp:Panel ID="pnlCheckoutForm" runat="server" CssClass="checkout-container">
            <h2 class="fw-bold mb-4 text-dark">Secure Checkout</h2>
            
            <div class="summary-box">
                <h5 class="fw-bold mb-3">Order Summary</h5>
                <div class="d-flex justify-content-between mb-2">
                    <span class="text-muted">Total Services:</span>
                    <asp:Label ID="lblTotalItems" runat="server" CssClass="fw-bold">0</asp:Label>
                </div>
                <div class="d-flex justify-content-between border-top pt-2">
                    <span class="fw-bold fs-5">Amount Due:</span>
                    <asp:Label ID="lblTotalPrice" runat="server" CssClass="fw-bold fs-5 text-primary">R 0.00</asp:Label>
                </div>
            </div>

            <h5 class="fw-bold mb-3">Billing Details</h5>
            <div class="row g-3 mb-4">
                <div class="col-md-6">
                    <label class="form-label text-muted small fw-bold">Full Name</label>
                    <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control ios-input" placeholder="e.g. James Motsamai"></asp:TextBox>
                </div>
                <div class="col-md-6">
                    <label class="form-label text-muted small fw-bold">Campus / Location</label>
                    <asp:TextBox ID="txtLocation" runat="server" CssClass="form-control ios-input" placeholder="e.g. WSU Alice Campus"></asp:TextBox>
                </div>
            </div>

            <h5 class="fw-bold mb-3">Payment Method</h5>
            <div class="mb-4">
                <asp:RadioButtonList ID="rblPaymentMethod" runat="server" CssClass="form-check">
                    <asp:ListItem Value="EFT" Selected="True">&nbsp; Electronic Funds Transfer (EFT)</asp:ListItem>
                    <asp:ListItem Value="Cash">&nbsp; Cash on Completion</asp:ListItem>
                    <asp:ListItem Value="Voucher">&nbsp; EasternDigital Voucher</asp:ListItem>
                </asp:RadioButtonList>
            </div>

            <asp:Label ID="lblCheckoutError" runat="server" CssClass="text-danger fw-bold d-block mb-3"></asp:Label>

            <asp:Button ID="btnCompleteOrder" runat="server" Text="Confirm & Place Order" CssClass="btn btn-pay" OnClick="btnCompleteOrder_Click" />
        </asp:Panel>

        <asp:Panel ID="pnlSuccess" runat="server" CssClass="checkout-container text-center" Visible="false">
            <div class="success-icon mb-3">✓</div>
            <h2 class="fw-bold text-dark mb-3">Booking Confirmed!</h2>
            <p class="text-muted fs-5">Thank you for using EasternDigital. Your service provider will contact you shortly.</p>
            
            <div class="summary-box d-inline-block text-start mt-3 mb-4 px-5">
                <p class="mb-1 text-muted">Reference Number:</p>
                <asp:Label ID="lblReferenceNumber" runat="server" CssClass="fs-4 fw-bold text-dark">#ED-0000</asp:Label>
            </div>
            
            <br />
            <a href="Default.aspx" class="btn btn-outline-dark rounded-pill px-5 py-2 fw-bold">Return to Home</a>
        </asp:Panel>

    </div>

</asp:Content>

