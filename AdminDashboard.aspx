<%@ Page Title="Admin Dashboard" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminDashboard.aspx.cs" Inherits="Group_9.AdminDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        :root {
            --ios-bg: #f2f2f7;
            --ios-card: #ffffff;
            --ios-blue: #007aff;
            --ios-green: #34c759;
            --ios-orange: #ff9500;
        }
        body { background-color: var(--ios-bg); font-family: -apple-system, BlinkMacSystemFont, "Segoe UI", sans-serif; }
        .ios-container { padding: 30px; max-width: 1000px; margin: auto; }
        .page-title { font-size: 28px; font-weight: 800; margin-bottom: 25px; }
        
        /* Metric Cards */
        .metric-card { background: var(--ios-card); padding: 25px; border-radius: 20px; box-shadow: 0 4px 12px rgba(0,0,0,0.05); text-align: center; }
        .metric-value { font-size: 32px; font-weight: 700; color: #000; }
        .metric-label { font-size: 14px; color: #8e8e93; text-transform: uppercase; letter-spacing: 0.5px; margin-top: 5px; }
        
        /* Table / Activity Section */
        .activity-card { background: var(--ios-card); border-radius: 20px; padding: 25px; box-shadow: 0 4px 12px rgba(0,0,0,0.05); margin-top: 20px; }
        
        /* Action Button */
        .btn-ios { background-color: var(--ios-blue); color: white; padding: 12px 25px; border-radius: 12px; text-decoration: none; font-weight: 600; display: inline-block; transition: opacity 0.2s; }
        .btn-ios:hover { opacity: 0.8; color: white; }
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

        <div class="activity-card">
            <h4>System Actions</h4>
            <p class="text-muted small">Manage administrative access and user permissions.</p>
            <asp:HyperLink ID="lnkRegisterAdmin" runat="server" NavigateUrl="~/AdminRegister.aspx" CssClass="btn-ios">
                + Register New Admin
            </asp:HyperLink>
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
                                <td><%# Eval("UserName") %></td>
                                <td><%# Eval("ActionDescription") %></td>
                                <td><%# Eval("LogTime") %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>