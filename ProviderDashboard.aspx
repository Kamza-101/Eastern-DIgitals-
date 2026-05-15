<%@ Page Title="Provider Dashboard" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProviderDashboard.aspx.cs" Inherits="Group_9.ProviderDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        :root {
            --ios-bg: #f2f2f7;
            --ios-card: #ffffff;
            --ios-green: #34c759;
            --ios-blue: #007aff;
        }
        body { 
            background-color: var(--ios-bg); 
            font-family: -apple-system, BlinkMacSystemFont, "Segoe UI", Roboto, sans-serif; 
        }

        .metric-card {
            background: var(--ios-card);
            border-radius: 20px;
            padding: 30px 20px;
            text-align: center;
            box-shadow: 0 4px 12px rgba(0,0,0,0.03);
            border: 1px solid #f0f0f0;
            height: 100%;
        }
        .metric-title {
            font-size: 0.85rem;
            font-weight: 700;
            color: #8e8e93;
            text-transform: uppercase;
            letter-spacing: 1px;
            margin-bottom: 10px;
        }
        .metric-value {
            font-size: 2.5rem;
            font-weight: 800;
            color: #1c1c1e;
        }
        .metric-revenue {
            color: var(--ios-green);
        }

        .booking-card {
            background: var(--ios-card);
            border-radius: 20px;
            padding: 20px;
            margin-bottom: 15px;
            box-shadow: 0 4px 12px rgba(0,0,0,0.03);
            border: 1px solid #f0f0f0;
        }
        .avatar-circle {
            font-size: 1.8rem;
            background: #f8f9fa;
            border-radius: 50%;
            width: 60px;
            height: 60px;
            display: flex;
            align-items: center;
            justify-content: center;
            border: 1px solid #e9ecef;
        }

        .premium-banner {
            background: linear-gradient(135deg, #FFD700 0%, #FDB931 100%);
            color: #000;
            border: none;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container py-5" style="max-width: 1000px;">
        <h2 class="fw-bold mb-4">Dashboard</h2>

        <asp:Label ID="lblUpgradeSuccess" runat="server" CssClass="alert alert-success d-block fw-bold mb-4 rounded-4 shadow-sm" Visible="false"></asp:Label>

        <asp:Panel ID="pnlBasicStatus" runat="server" CssClass="alert alert-warning d-flex flex-column flex-md-row justify-content-between align-items-center mb-4 shadow-sm rounded-4 border-0">
            <div class="mb-3 mb-md-0">
                <h5 class="fw-bold mb-1">Boost Your Profile!</h5>
                <p class="mb-0 text-dark">Upgrade to Premium for R50/month to appear at the top of student searches and get a verified badge.</p>
            </div>
            <a href="UpgradePremium.aspx" class="btn btn-dark fw-bold rounded-pill px-4 py-2 shadow-sm text-nowrap">Upgrade Now</a>
        </asp:Panel>

        <asp:Panel ID="pnlPremiumStatus" runat="server" Visible="false" CssClass="alert premium-banner d-flex justify-content-between align-items-center mb-4 shadow-sm rounded-4">
            <div>
                <h5 class="fw-bold mb-1">⭐ Premium Member</h5>
                <p class="mb-0">Your profile is currently boosted in EasternDigital student search results.</p>
            </div>
        </asp:Panel>
        
        <div class="row g-4 mb-4">
            <div class="col-md-4">
                <div class="metric-card">
                    <div class="metric-title">Total Revenue</div>
                    <asp:Label ID="lblTotalRevenue" runat="server" CssClass="metric-value metric-revenue">R 0.00</asp:Label>
                </div>
            </div>
            <div class="col-md-4">
                <div class="metric-card">
                    <div class="metric-title">Active Bookings</div>
                    <asp:Label ID="lblActiveBookings" runat="server" CssClass="metric-value">0</asp:Label>
                </div>
            </div>
            <div class="col-md-4">
                <div class="metric-card">
                    <div class="metric-title">Total Completed</div>
                    <asp:Label ID="lblCompletedJobs" runat="server" CssClass="metric-value">0</asp:Label>
                </div>
            </div>
        </div>

        <div class="metric-card mb-5 text-start" style="padding: 25px;">
            <div class="d-flex justify-content-between align-items-end mb-3">
                <div>
                    <div class="metric-title">Monthly Revenue Target</div>
                    <h4 class="fw-bold mb-0 text-dark">
                        R <asp:Label ID="lblCurrentRevTracker" runat="server">0.00</asp:Label> 
                        <span class="text-muted" style="font-size: 1rem; font-weight: 500;">/ R 5,000</span>
                    </h4>
                </div>
                <div class="text-success fw-bold" style="font-size: 1.2rem;">
                    <asp:Label ID="lblGoalPercentage" runat="server">0</asp:Label>%
                </div>
            </div>
            
            <div class="progress shadow-sm" style="height: 14px; border-radius: 20px; background-color: #e5e5ea;">
                <asp:Panel ID="pnlProgressBar" runat="server" CssClass="progress-bar" style="width: 0%; border-radius: 20px; background-color: #34c759; transition: width 1s ease-in-out;"></asp:Panel>
            </div>
        </div>

        <h3 class="fw-bold mb-4">Recent Bookings</h3>
        
        <asp:Label ID="lblError" runat="server" CssClass="alert alert-danger d-block fw-bold mb-4" Visible="false"></asp:Label>
        
        <asp:Repeater ID="rptProviderBookings" runat="server" OnItemCommand="rptProviderBookings_ItemCommand">
            <ItemTemplate>
                <div class="booking-card">
                    <div class="d-flex flex-column flex-md-row justify-content-between align-items-center">
                        
                        <div class="d-flex align-items-center gap-3 w-100">
                            <div class="avatar-circle">
                                👤
                            </div>
                            <div>
                                <h5 class="fw-bold mb-1"><%# Eval("StudentName") %></h5>
                                <p class="text-muted mb-0 fw-semibold"><%# Eval("ServiceName") %> &nbsp;|&nbsp; <%# Eval("FormattedDate") %></p>
                                <span class="badge bg-secondary mt-2"><%# Eval("Status") %></span>
                            </div>
                        </div>
                        
                        <div class="mt-3 mt-md-0 d-flex flex-column align-items-md-end gap-2">
                            <div class="d-flex gap-2">
                                <asp:Button ID="btnApprove" runat="server" Text="Approve" 
                                    CssClass="btn btn-success fw-bold rounded-pill px-4 shadow-sm" 
                                    CommandName="Approve" CommandArgument='<%# Eval("BookingID") %>' 
                                    Visible='<%# Convert.ToString(Eval("Status")) == "Pending Confirmation" %>' 
                                    CausesValidation="false" />
                                    
                                <asp:Button ID="btnReject" runat="server" Text="Reject" 
                                    CssClass="btn btn-outline-danger fw-bold rounded-pill px-4" 
                                    CommandName="Reject" CommandArgument='<%# Eval("BookingID") %>' 
                                    Visible='<%# Convert.ToString(Eval("Status")) == "Pending Confirmation" %>' 
                                    CausesValidation="false" />

                                <asp:Button ID="btnComplete" runat="server" Text="Mark as Completed ✓" 
                                    CssClass="btn btn-primary fw-bold rounded-pill px-4 shadow-sm" 
                                    CommandName="Complete" CommandArgument='<%# Eval("BookingID") %>' 
                                    Visible='<%# Convert.ToString(Eval("Status")) == "Approved" %>' 
                                    CausesValidation="false" />
                            </div>
                            
                            <asp:TextBox ID="txtRejectReason" runat="server" 
                                CssClass="form-control form-control-sm mt-1" 
                                Placeholder="Reason if rejecting..." 
                                Visible='<%# Convert.ToString(Eval("Status")) == "Pending Confirmation" %>' 
                                style="width: 250px; font-size: 0.85rem; border-radius: 8px;">
                            </asp:TextBox>
                        </div>
                        
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>

        <h3 class="fw-bold mb-4 mt-5">Earnings History</h3>
        
        <asp:Repeater ID="rptEarnings" runat="server">
            <ItemTemplate>
                <div class="card border-0 shadow-sm mb-2" style="border-radius: 12px; background-color: #fafafa;">
                    <div class="card-body d-flex justify-content-between align-items-center px-4 py-3">
                        <div>
                            <h6 class="fw-bold mb-1"><%# Eval("ServiceName") %></h6>
                            <small class="text-muted"><%# Eval("FormattedDate") %></small>
                        </div>
                        <div class="text-success fw-bold fs-5">
                            + R <%# Convert.ToDecimal(Eval("TotalCost")).ToString("0.00") %>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
            <FooterTemplate>
                <asp:Label ID="lblNoEarnings" runat="server" 
                    Visible='<%# rptEarnings.Items.Count == 0 %>' 
                    CssClass="d-block text-center text-muted mt-4">
                    Complete your first booking to see your earnings here!
                </asp:Label>
            </FooterTemplate>
        </asp:Repeater>
        
    </div>
</asp:Content>