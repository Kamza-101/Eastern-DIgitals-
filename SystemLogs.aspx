<%@ Page Title="System Logs" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SystemLogs.aspx.cs" Inherits="Group_9.SystemLogs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        :root {
            --ios-bg: #f2f2f7;
            --ios-card: #ffffff;
            --ios-gray: #8e8e93;
        }
        body { background-color: var(--ios-bg); font-family: -apple-system, BlinkMacSystemFont, "Segoe UI", sans-serif; }
        .ios-container { padding: 30px; max-width: 1000px; margin: auto; }
        
        .page-title { font-size: 28px; font-weight: 800; margin-bottom: 5px; }
        .page-subtitle { color: var(--ios-gray); font-size: 0.95rem; margin-bottom: 25px; }
        
        .report-card { background: var(--ios-card); padding: 0; border-radius: 15px; box-shadow: 0 4px 12px rgba(0,0,0,0.03); overflow: hidden; }
        
        /* Custom Table Styling */
        .table-report { width: 100%; border-collapse: collapse; margin-bottom: 0; }
        .table-report th { background-color: #f8f9fa; color: var(--ios-gray); font-size: 0.85rem; text-transform: uppercase; letter-spacing: 0.5px; padding: 15px 20px; border-bottom: 2px solid #eaeaea; text-align: left; font-weight: 700; }
        .table-report td { padding: 15px 20px; border-bottom: 1px solid #f0f0f0; font-size: 0.95rem; vertical-align: middle; }
        .table-report tr:last-child td { border-bottom: none; }
        .table-report tr:hover { background-color: #fafafa; }
        
        /* Monospace font for exact time alignment */
        .log-time { font-family: 'SF Mono', Consolas, monospace; font-size: 0.85rem; color: #555; }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="ios-container">
        <h1 class="page-title">System Audit Logs</h1>
        <p class="page-subtitle">Complete chronological trail of platform activity and administrative actions.</p>

        <asp:Label ID="lblError" runat="server" CssClass="alert alert-danger d-block fw-bold mb-4" Visible="false"></asp:Label>

        <div class="report-card">
            <div class="table-responsive">
                <table class="table-report">
                    <thead>
                        <tr>
                            <th>Date & Time</th>
                            <th>User (Email)</th>
                            <th>Action Description</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="rptLogs" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td class="log-time text-nowrap"><%# Convert.ToDateTime(Eval("LogTime")).ToString("yyyy-MM-dd HH:mm:ss") %></td>
                                    <td><strong><%# Eval("UserName") %></strong></td>
                                    <td class="text-muted"><%# Eval("ActionDescription") %></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
                
                <asp:Label ID="lblNoData" runat="server" Visible="false" CssClass="d-block text-center text-muted py-5">
                    No system logs recorded yet.
                </asp:Label>
            </div>
        </div>
    </div>
</asp:Content>