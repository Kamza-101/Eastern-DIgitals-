<%@ Page Title="User Management" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserManagement.aspx.cs" Inherits="Group_9.UserManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        :root { --ios-bg: #f2f2f7; --ios-card: #ffffff; --ios-blue: #007aff; }
        body { background-color: var(--ios-bg); font-family: -apple-system, BlinkMacSystemFont, "Segoe UI", sans-serif; }
        .ios-container { padding: 30px; max-width: 900px; margin: auto; }
        
        .ios-card { background: var(--ios-card); border-radius: 20px; padding: 25px; box-shadow: 0 4px 12px rgba(0,0,0,0.05); }
        
        /* GridView Styling */
        .ios-grid { width: 100%; border-collapse: separate; border-spacing: 0; }
        .ios-grid th { padding: 15px; color: #8e8e93; font-weight: 500; text-align: left; }
        .ios-grid td { padding: 15px; border-bottom: 1px solid #f2f2f7; }
        
        .ios-select { padding: 8px; border-radius: 8px; border: 1px solid #e5e5ea; background: #fff; }
        .btn-save { background: var(--ios-blue); color: white; border: none; padding: 10px 25px; border-radius: 12px; font-weight: 600; margin-top: 20px; }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="ios-container">
        <h1 style="margin-bottom: 25px; font-weight:800;">User Management</h1>

        <div class="ios-card">
            <asp:GridView ID="gvUsers" runat="server" AutoGenerateColumns="False" 
                CssClass="ios-grid" 
                DataKeyNames="UserID" 
                OnRowDataBound="gvUsers_RowDataBound">
                </asp:GridView>

            <asp:Button ID="btnSave" runat="server" Text="Save Changes" CssClass="btn-save" OnClick="btnSave_Click" />
        </div>
    </div>
</asp:Content>
