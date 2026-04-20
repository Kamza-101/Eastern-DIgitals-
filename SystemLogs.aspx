<%@ Page Title="System Audit Logs" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SystemLogs.aspx.cs" Inherits="Group_9.SystemLogs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        :root { --ios-bg: #f2f2f7; --ios-card: #ffffff; --ios-blue: #007aff; }
        body { background-color: var(--ios-bg); font-family: -apple-system, BlinkMacSystemFont, "Segoe UI", sans-serif; }
        .ios-container { padding: 30px; max-width: 1000px; margin: auto; }
        .ios-card { background: var(--ios-card); border-radius: 20px; padding: 25px; box-shadow: 0 4px 12px rgba(0,0,0,0.05); }
        
        .log-grid { width: 100%; border-collapse: separate; border-spacing: 0; margin-top: 20px; }
        .log-grid th { padding: 15px; color: #8e8e93; font-weight: 500; text-align: left; border-bottom: 2px solid #f2f2f7; }
        .log-grid td { padding: 15px; border-bottom: 1px solid #f2f2f7; font-size: 14px; }
        
        .search-box { padding: 10px 15px; border-radius: 12px; border: 1px solid #e5e5ea; width: 300px; }
        .btn-filter { background: var(--ios-blue); color: white; border: none; padding: 10px 20px; border-radius: 12px; font-weight: 600; }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="ios-container">
        <h1 style="margin-bottom: 25px; font-weight:800;">System Audit Logs</h1>

        <div class="ios-card">
            <div style="display:flex; gap: 10px; align-items: center; margin-bottom: 20px;">
                <asp:TextBox ID="txtSearch" runat="server" CssClass="search-box" placeholder="Filter by action or user..."></asp:TextBox>
                <asp:Button ID="btnSearch" runat="server" Text="Filter" CssClass="btn-filter" OnClick="btnSearch_Click" />
            </div>

            <asp:GridView ID="gvLogs" runat="server" AutoGenerateColumns="True" CssClass="log-grid">
                <HeaderStyle BackColor="White" />
            </asp:GridView>
        </div>
    </div>
</asp:Content>
