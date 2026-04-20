<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProviderDashboard.aspx.cs" Inherits="Group_9.ProviderDashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .dashboard-widget {
            background-color: white;
            border-radius: 20px;
            padding: 30px;
            border: 1px solid rgba(0,0,0,0.05);
            box-shadow: 0 5px 15px rgba(0,0,0,0.03);
            text-align: center;
        }
        .widget-value {
            font-size: 2rem;
            font-weight: 800;
            color: darkslategray;
        }
        .widget-label {
            font-size: 0.8rem;
            text-transform: uppercase;
            letter-spacing: 1px;
            color: gray;
        }
        .booking-row {
            background: white;
            border-radius: 15px;
            padding: 15px 25px;
            margin-bottom: 15px;
            border: 1px solid whitesmoke;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container py-5">
        
        <h2 class="fw-bold mb-4" style="letter-spacing: -1px;">Dashboard</h2>

        <div class="row g-4 mb-5">
            <div class="col-md-4">
                <div class="dashboard-widget">
                    <div class="widget-label">Total Revenue</div>
                    <div class="widget-value text-success"><asp:Label ID="lblTotalRevenue" runat="server" Text="R 0.00"></asp:Label></div>
                </div>
            </div>
            <div class="col-md-4">
                <div class="dashboard-widget">
                    <div class="widget-label">Active Bookings</div>
                    <div class="widget-value"><asp:Label ID="lblActiveBookings" runat="server" Text="0"></asp:Label></div>
                </div>
            </div>
            <div class="col-md-4">
                <div class="dashboard-widget">
                    <div class="widget-label">Total Completed</div>
                    <div class="widget-value"><asp:Label ID="lblCompletedJobs" runat="server" Text="0"></asp:Label></div>
                </div>
            </div>
        </div>

        <h4 class="fw-bold mb-3">Recent Bookings</h4>
        <div class="ios-card p-0 overflow-hidden">
            <asp:Repeater ID="rptProviderBookings" runat="server">
                <HeaderTemplate>
                    <div class="row p-3 bg-light fw-bold text-muted border-bottom">
                        <div class="col-md-3">Service</div>
                        <div class="col-md-3">Student</div>
                        <div class="col-md-3">Status</div>
                        <div class="col-md-3 text-end">Amount</div>
                    </div>
                </HeaderTemplate>
                <ItemTemplate>
                    <div class="row p-3 border-bottom align-items-center">
                        <div class="col-md-3 fw-bold"><%# Eval("ServiceName") %></div>
                        <div class="col-md-3"><%# Eval("StudentName") %></div>
                        <div class="col-md-3">
                            <span class="badge rounded-pill bg-<%# Eval("StatusColor") %>"><%# Eval("Status") %></span>
                        </div>
                        <div class="col-md-3 text-end fw-bold">R <%# Eval("Price") %></div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
