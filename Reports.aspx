<%@ Page Title="Reports" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="Group_9.Reports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        :root { --ios-bg: #f2f2f7; --ios-card: #ffffff; --ios-blue: #007aff; }
        body { background-color: var(--ios-bg); font-family: -apple-system, BlinkMacSystemFont, "Segoe UI", sans-serif; }
        .ios-container { padding: 30px; max-width: 1000px; margin: auto; }
        .ios-card { background: var(--ios-card); border-radius: 20px; padding: 25px; box-shadow: 0 4px 12px rgba(0,0,0,0.05); }
        
        /* Table / Report Styling */
        .report-grid { width: 100%; border-collapse: separate; border-spacing: 0; margin-top: 20px; }
        .report-grid th { padding: 15px; color: #8e8e93; font-weight: 500; text-align: left; border-bottom: 2px solid #f2f2f7; }
        .report-grid td { padding: 15px; border-bottom: 1px solid #f2f2f7; }
        
        .btn-generate { background: var(--ios-blue); color: white; border: none; padding: 10px 20px; border-radius: 12px; font-weight: 600; cursor: pointer; }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="ios-container">
        <h1 style="margin-bottom: 25px; font-weight:800;">Business Reports</h1>

        <div class="ios-card">
            <div style="display:flex; gap: 15px; align-items: center;">
                <asp:DropDownList ID="ddlReportType" runat="server" CssClass="ios-select" style="padding: 10px; border-radius: 12px;">
                    <asp:ListItem Value="Sales">Sales Report</asp:ListItem>
                    <asp:ListItem Value="Users">User Growth</asp:ListItem>
                </asp:DropDownList>
                <asp:Button ID="btnGenerate" runat="server" Text="Generate Report" CssClass="btn-generate" OnClick="btnGenerate_Click" />
            </div>

            <asp:GridView ID="gvReport" runat="server" AutoGenerateColumns="True" CssClass="report-grid">
                <HeaderStyle BackColor="White" />
            </asp:GridView>
        </div>
    </div>
</asp:Content>