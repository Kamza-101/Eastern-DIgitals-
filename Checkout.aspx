<%@ Page Title="Checkout" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Checkout.aspx.cs" Inherits="Group_9.Checkout" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .checkout-container {
            max-width: 800px;
            margin: 0 auto;
            background-color: white;
            padding: 30px;
            border-radius: 12px;
            box-shadow: 0 4px 6px whitesmoke;
            border: 1px solid lightgray;
        }
        .section-title {
            background-color: darkslategray;
            color: white;
            padding: 10px 15px;
            border-radius: 8px;
            margin-bottom: 20px;
        }
        .form-control, .form-select {
            border-radius: 8px;
            margin-bottom: 15px;
        }
        .btn-confirm {
            background-color: dodgerblue;
            color: white;
            font-weight: bold;
            width: 100%;
            padding: 12px;
            border-radius: 25px;
            border: none;
        }
        .btn-confirm:hover {
            background-color: royalblue;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container py-5">
        <div class="checkout-container">
            <h2 class="fw-bold mb-4 text-center">Checkout Details</h2>
            
            <asp:Label ID="lblMessage" runat="server" CssClass="d-block text-center mb-3 fw-bold"></asp:Label>

            <h5 class="section-title">Payment Method</h5>
            <asp:DropDownList ID="ddlPaymentMethod" runat="server" CssClass="form-select">
                <asp:ListItem Text="Select Payment Method..." Value="" />
                <asp:ListItem Text="Credit/Debit Card" Value="Card" />
                <asp:ListItem Text="EFT" Value="EFT" />
                <asp:ListItem Text="Cash on Campus" Value="Cash" />
            </asp:DropDownList>

            <h5 class="section-title">Booking Notes & References</h5>
            <asp:TextBox ID="txtNotes" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" placeholder="Add any specific instructions for the provider..."></asp:TextBox>

            <div class="mt-4">
                <asp:Button ID="btnConfirmBooking" runat="server" Text="Confirm Booking " CssClass="btn-confirm" OnClick="btnConfirmBooking_Click" />
            </div>
            
            <div class="text-center mt-3">
                <a href="ViewCart.aspx" class="text-muted">Return to Cart</a>
            </div>
        </div>
    </div>
</asp:Content>

