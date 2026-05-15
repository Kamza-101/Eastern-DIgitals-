<%@ Page Title="Platform Reports" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="Group_9.Reports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        :root {
            --ios-bg: #f2f2f7;
            --ios-card: #ffffff;
            --ios-blue: #007aff;
            --ios-gray: #8e8e93;
        }
        body { background-color: var(--ios-bg); font-family: -apple-system, BlinkMacSystemFont, "Segoe UI", sans-serif; }
        .ios-container { padding: 30px; max-width: 1200px; margin: auto; }
        .page-title { font-size: 28px; font-weight: 800; margin-bottom: 25px; }
        
        .filter-card { background: var(--ios-card); padding: 20px 25px; border-radius: 15px; box-shadow: 0 4px 12px rgba(0,0,0,0.03); margin-bottom: 25px; }
        .report-card { background: var(--ios-card); padding: 0; border-radius: 15px; box-shadow: 0 4px 12px rgba(0,0,0,0.03); overflow: hidden; }
        
        .btn-ios { background-color: var(--ios-blue); color: white; padding: 10px 20px; border-radius: 10px; font-weight: 600; border: none; transition: 0.2s; width: 100%; }
        .btn-ios:hover { opacity: 0.8; }
        
        .table-report { margin-bottom: 0; width: 100%; border-collapse: collapse; }
        .table-report th { background-color: #f8f9fa; color: var(--ios-gray); font-size: 0.85rem; text-transform: uppercase; letter-spacing: 0.5px; padding: 15px 20px; border-bottom: 2px solid #eaeaea; font-weight: 700; }
        .table-report td { padding: 15px 20px; vertical-align: middle; border-bottom: 1px solid #f0f0f0; font-size: 0.95rem; }
        .table-report tr:last-child td { border-bottom: none; }
        .table-report tr:hover { background-color: #fafafa; }
        
        .status-badge { padding: 5px 12px; border-radius: 12px; font-size: 0.8rem; font-weight: bold; }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="ios-container">
        <h1 class="page-title">Platform Reports</h1>

        <asp:Label ID="lblError" runat="server" CssClass="alert alert-danger d-block fw-bold mb-4" Visible="false"></asp:Label>

        <div class="filter-card">
            <div class="row align-items-end g-3">
                <div class="col-md-4">
                    <label class="form-label fw-bold text-muted small text-uppercase">Report Type</label>
                    <asp:DropDownList ID="ddlReportType" runat="server" CssClass="form-select shadow-sm">
                        <asp:ListItem Value="Bookings">All Bookings & Transactions</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-4">
                    <label class="form-label fw-bold text-muted small text-uppercase">Filter by Status</label>
                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-select shadow-sm">
                        <asp:ListItem Value="All">All Statuses</asp:ListItem>
                        <asp:ListItem Value="Completed">Completed (Revenue Generating)</asp:ListItem>
                        <asp:ListItem Value="Pending Confirmation">Pending Confirmation</asp:ListItem>
                        <asp:ListItem Value="Approved">Approved (In Progress)</asp:ListItem>
                        <asp:ListItem Value="Rejected">Rejected</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-4">
                    <asp:Button ID="btnGenerate" runat="server" Text="Generate Report" CssClass="btn-ios" OnClick="btnGenerate_Click" />
                </div>
            </div>
        </div>

        <div class="d-flex justify-content-between align-items-center mb-3 px-2">
            <h5 class="fw-bold mb-0">Report Results</h5>
            <div class="text-end">
                <span class="text-muted small me-3">Total Records: <asp:Label ID="lblRecordCount" runat="server" CssClass="fw-bold text-dark">0</asp:Label></span>
                <span class="text-muted small">Total Value: <asp:Label ID="lblTotalValue" runat="server" CssClass="fw-bold text-success">R 0.00</asp:Label></span>
            </div>
        </div>

        <div class="filter-card mb-4 text-center">
            <h5 class="fw-bold mb-3 text-start">Status Distribution Overview</h5>
            <asp:Image runat="server" ID="imgChart" CssClass="img-fluid rounded shadow-sm" AlternateText="Status Bar Chart" />
        </div>

        <div class="report-card">
            <div class="table-responsive">
                <table class="table-report">
                    <thead>
                        <tr>
                            <th>Date</th>
                            <th>Order Ref</th>
                            <th>Service</th>
                            <th>Provider (Email)</th>
                            <th>Seeker (Email)</th>
                            <th>Cost</th>
                            <th>Status</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="rptData" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td class="text-nowrap"><%# Eval("FormattedDate") %></td>
                                    <td><strong><%# Eval("OrderReference") %></strong></td>
                                    <td><%# Eval("ServiceName") %></td>
                                    <td class="text-muted"><%# Eval("ProviderEmail") %></td>
                                    <td class="text-muted"><%# Eval("SeekerEmail") %></td>
                                    <td class="fw-bold text-success">R <%# Convert.ToDecimal(Eval("TotalCost")).ToString("0.00") %></td>
                                    <td>
                                        <span class='<%# "status-badge " + 
                                            (Eval("Status").ToString() == "Completed" ? "bg-success text-white" : 
                                            (Eval("Status").ToString() == "Rejected" ? "bg-danger text-white" : 
                                            (Eval("Status").ToString() == "Approved" ? "bg-primary text-white" : "bg-warning text-dark"))) %>'>
                                            <%# Eval("Status") %>
                                        </span>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
                
                <asp:Label ID="lblNoData" runat="server" Visible="false" CssClass="d-block text-center text-muted py-5">
                    No records found for the selected filters.
                </asp:Label>
            </div>
        </div>
        
    </div>
</asp:Content>