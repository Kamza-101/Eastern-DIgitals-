<%@ Page Title="Upgrade to Premium" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UpgradePremium.aspx.cs" Inherits="Group_9.UpgradePremium" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .checkout-card { max-width: 500px; margin: 40px auto; border-radius: 15px; box-shadow: 0 10px 30px rgba(0,0,0,0.1); }
        .premium-header { background: linear-gradient(135deg, #FFD700 0%, #FDB931 100%); color: #000; padding: 20px; border-radius: 15px 15px 0 0; text-align: center; }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="card checkout-card">
            <div class="premium-header">
                <h3 class="fw-bold mb-0">⭐ Premium Provider Upgrade</h3>
            </div>
            
            <div class="card-body p-4">
                <div class="alert alert-info text-center">
                    <strong>Monthly Subscription:</strong> R50.00 ZAR<br />
                    <small>Billed monthly. Cancel anytime.</small>
                </div>

                <h5 class="mb-3 border-bottom pb-2">Mock Payment Details</h5>
                
                <div class="mb-3">
                    <label class="form-label">Name on Card</label>
                    <asp:TextBox ID="txtCardName" runat="server" CssClass="form-control" placeholder="e.g. John Doe"></asp:TextBox>
                </div>
                
                <div class="mb-3">
                    <label class="form-label">Card Number (Mock)</label>
                    <asp:TextBox ID="txtCardNumber" runat="server" CssClass="form-control" placeholder="1234 5678 9101 1121" MaxLength="16"></asp:TextBox>
                </div>

                <div class="row g-3 mb-4">
                    <div class="col-6">
                        <label class="form-label">Expiry Date</label>
                        <asp:TextBox ID="txtExpiry" runat="server" CssClass="form-control" placeholder="MM/YY"></asp:TextBox>
                    </div>
                    <div class="col-6">
                        <label class="form-label">CVV</label>
                        <asp:TextBox ID="txtCVV" runat="server" CssClass="form-control" placeholder="123" MaxLength="3"></asp:TextBox>
                    </div>
                </div>

                <asp:Button ID="btnPayUpgrade" runat="server" Text="Pay R50.00 & Upgrade" CssClass="btn btn-dark w-100 fw-bold py-2" OnClick="btnPayUpgrade_Click" />
                
                <div class="mt-3 text-center">
                    <asp:Label ID="lblMessage" runat="server" Visible="false"></asp:Label>
                </div>
                
                <div class="mt-3 text-center">
                    <a href="ProviderDashboard.aspx" class="text-decoration-none text-muted">Cancel and return to Dashboard</a>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
