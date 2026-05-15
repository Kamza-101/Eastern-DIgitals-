<%@ Page Title="Admin Dashboard" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminDashboard.aspx.cs" Inherits="Group_9.AdminDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        :root {
            --ios-bg: #f2f2f7;
            --ios-card: #ffffff;
            --ios-blue: #007aff;
            --ios-green: #34c759;
            --ios-orange: #ff9500;
            --ios-red: #ff3b30;
        }
        body { background-color: var(--ios-bg); font-family: -apple-system, BlinkMacSystemFont, "Segoe UI", sans-serif; }
        .ios-container { padding: 30px; max-width: 1000px; margin: auto; }
        .page-title { font-size: 28px; font-weight: 800; margin-bottom: 25px; }
        
        .metric-card { background: var(--ios-card); padding: 25px; border-radius: 20px; box-shadow: 0 4px 12px rgba(0,0,0,0.05); text-align: center; height: 100%; }
        .metric-value { font-size: 32px; font-weight: 700; color: #000; }
        .metric-label { font-size: 14px; color: #8e8e93; text-transform: uppercase; letter-spacing: 0.5px; margin-top: 5px; }
        
        .activity-card { background: var(--ios-card); border-radius: 20px; padding: 25px; box-shadow: 0 4px 12px rgba(0,0,0,0.05); margin-top: 20px; }
        
        .btn-ios { background-color: var(--ios-blue); color: white; padding: 12px 25px; border-radius: 12px; text-decoration: none; font-weight: 600; display: inline-block; transition: opacity 0.2s; border: none; cursor: pointer; }
        .btn-ios:hover { opacity: 0.8; color: white; }
        
        .user-list-item { display: flex; justify-content: space-between; align-items: center; padding: 15px 0; border-bottom: 1px solid #f0f0f0; }
        .user-list-item:last-child { border-bottom: none; }
        .badge-role { padding: 4px 10px; border-radius: 8px; font-size: 0.75rem; font-weight: bold; background-color: #e6f2ff; color: var(--ios-blue); }
        .badge-status-active { background-color: #e8f8ec; color: var(--ios-green); padding: 4px 10px; border-radius: 8px; font-size: 0.75rem; font-weight: bold; }
        .badge-status-suspended { background-color: #ffebe9; color: var(--ios-red); padding: 4px 10px; border-radius: 8px; font-size: 0.75rem; font-weight: bold; }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="ios-container">
        <h1 class="page-title">Admin Dashboard</h1>

        <div class="row g-4">
            <div class="col-md-3">
                <div class="metric-card">
                    <div class="metric-value"><asp:Label ID="lblTotalUsers" runat="server" Text="0"></asp:Label></div>
                    <div class="metric-label">Total Users</div>
                </div>
            </div>
            <div class="col-md-3">
                <div class="metric-card">
                    <div class="metric-value"><asp:Label ID="lblActiveServices" runat="server" Text="0"></asp:Label></div>
                    <div class="metric-label">Active Services</div>
                </div>
            </div>
            <div class="col-md-3">
                <div class="metric-card">
                    <div class="metric-value"><asp:Label ID="lblPendingBookings" runat="server" Text="0"></asp:Label></div>
                    <div class="metric-label">Pending Bookings</div>
                </div>
            </div>
            <div class="col-md-3">
                <div class="metric-card" style="border-bottom: 4px solid var(--ios-green);">
                    <div class="metric-value"><asp:Label ID="lblSystemHealth" runat="server" Text="100%"></asp:Label></div>
                    <div class="metric-label">System Health</div>
                </div>
            </div>
        </div>

        <h4 class="mt-5 mb-3 fw-bold">Platform Monetization (Live Profit)</h4>
        <div class="row g-4">
            <div class="col-md-4">
                <div class="metric-card" style="background: linear-gradient(135deg, #FFD700 0%, #FDB931 100%);">
                    <div class="metric-value"><asp:Label ID="lblPremiumProviders" runat="server" Text="0"></asp:Label></div>
                    <div class="metric-label" style="color: #000; font-weight: bold;">⭐ Premium Subscriptions</div>
                </div>
            </div>
            <div class="col-md-4">
                <div class="metric-card">
                    <div class="metric-value" style="color: var(--ios-green);">R <asp:Label ID="lblMRR" runat="server" Text="0.00"></asp:Label></div>
                    <div class="metric-label">Monthly Recurring Revenue</div>
                </div>
            </div>
            <div class="col-md-4">
                <div class="metric-card">
                    <div class="metric-value" style="color: var(--ios-blue);">R <asp:Label ID="lblTotalProfit" runat="server" Text="0.00"></asp:Label></div>
                    <div class="metric-label">Total Profit (inc. 5% Fees)</div>
                </div>
            </div>
        </div>

        <asp:Label ID="lblAdminMessage" runat="server" CssClass="alert alert-info d-block mt-4 fw-bold" Visible="false"></asp:Label>

        <div class="activity-card">
            <h4>Pending Provider Registrations</h4>
            <p class="text-muted small mb-3">Review and approve new service providers before they can list services.</p>
            
            <asp:Repeater ID="rptPendingProviders" runat="server" OnItemCommand="rptPendingProviders_ItemCommand">
                <ItemTemplate>
                    <div class="user-list-item">
                        <div>
                            <strong style="font-size: 1.1rem;"><%# Eval("FirstName") %> <%# Eval("Surname") %></strong><br />
                            <span class="text-muted mb-1 d-block" style="font-size: 0.85rem;"><%# Eval("Email") %></span>
                            <span class="text-dark fw-semibold" style="font-size: 0.85rem;">Proposed Service: <%# Eval("ProposedService") %></span><br />
                            <span class="badge bg-warning text-dark mt-2">Pending Approval</span>
                        </div>
                        <div class="d-flex gap-2">
                            <asp:Button ID="btnApprove" runat="server" Text="Approve" CssClass="btn btn-success fw-bold rounded-pill px-3 py-1" CommandName="Approve" CommandArgument='<%# Eval("UserID") %>' />
                            <asp:Button ID="btnReject" runat="server" Text="Decline" CssClass="btn btn-outline-danger fw-bold rounded-pill px-3 py-1" CommandName="Decline" CommandArgument='<%# Eval("UserID") %>' />
                        </div>
                    </div>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="lblNoPending" runat="server" Visible='<%# rptPendingProviders.Items.Count == 0 %>' CssClass="text-muted d-block text-center py-3">No pending provider registrations.</asp:Label>
                </FooterTemplate>
            </asp:Repeater>
        </div>

        <div class="activity-card">
            <div class="d-flex justify-content-between align-items-center mb-3">
                <div>
                    <h4 class="mb-0">User Management</h4>
                    <p class="text-muted small mb-0">Suspend or activate existing accounts.</p>
                </div>
                <asp:HyperLink ID="lnkRegisterAdmin" runat="server" NavigateUrl="~/AdminRegister.aspx" CssClass="btn-ios" style="padding: 8px 15px; font-size: 0.9rem;">
                    + New Admin
                </asp:HyperLink>
            </div>
            
            <asp:Repeater ID="rptAllUsers" runat="server" OnItemCommand="rptAllUsers_ItemCommand">
                <ItemTemplate>
                    <div class="user-list-item">
                        <div>
                            <strong style="font-size: 1.1rem;"><%# Eval("Email") %></strong><br />
                            <div class="mt-1 d-flex gap-2">
                                <span class="badge-role"><%# Eval("UserRole") %></span>
                                <span class='<%# Eval("Status").ToString() == "Active" ? "badge-status-active" : "badge-status-suspended" %>'>
                                    <%# Eval("Status") %>
                                </span>
                            </div>
                        </div>
                        <div class="d-flex align-items-center gap-2">
                            <asp:TextBox ID="txtSuspendReason" runat="server" 
                                CssClass="form-control form-control-sm" 
                                Placeholder="Reason for suspension..." 
                                Visible='<%# Eval("Status").ToString() == "Active" %>' 
                                style="width: 200px; font-size: 0.8rem; border-radius: 8px;">
                            </asp:TextBox>

                            <asp:Button ID="btnToggle" runat="server" 
                                Text='<%# Eval("Status").ToString() == "Active" ? "Suspend" : "Activate" %>' 
                                CssClass='<%# "btn fw-bold rounded-pill px-4 py-1 " + (Eval("Status").ToString() == "Active" ? "btn-outline-danger" : "btn-outline-success") %>' 
                                CommandName="ToggleStatus" 
                                CommandArgument='<%# Eval("UserID") + "|" + Eval("Status") %>' />
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>

        <div class="activity-card">
            <h4 style="margin-bottom: 20px;">Recent System Activity</h4>
            <table class="table table-borderless">
                <thead>
                    <tr style="color: #8e8e93; font-size: 13px;">
                        <th>User</th>
                        <th>Action</th>
                        <th>Time</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptActivity" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><strong><%# Eval("UserName") %></strong></td>
                                <td><%# Eval("ActionDescription") %></td>
                                <td class="text-muted"><%# Convert.ToDateTime(Eval("LogTime")).ToString("MMM dd, HH:mm") %></td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            <tr runat="server" visible='<%# rptActivity.Items.Count == 0 %>'>
                                <td colspan="3" class="text-center text-muted">No recent activity logs found.</td>
                            </tr>
                        </FooterTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>