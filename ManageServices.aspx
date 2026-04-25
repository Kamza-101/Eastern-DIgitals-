<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageServices.aspx.cs" Inherits="Group_9.ManageServices" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        :root {
            --ios-bg: #f2f2f7;
            --ios-card: #ffffff;
            --ios-blue: #007aff;
            --ios-green: #34c759;
            --ios-red: #ff3b30;
            --ios-gray: #e5e5ea;
        }
        body { background-color: var(--ios-bg); font-family: -apple-system, BlinkMacSystemFont, "Segoe UI", sans-serif; }
        .ios-container { padding: 30px; max-width: 900px; margin: auto; }
        .section-title { font-size: 22px; font-weight: 700; margin: 30px 0 15px 0; }
        
        .ios-card { background: var(--ios-card); border-radius: 20px; padding: 20px; margin-bottom: 15px; box-shadow: 0 4px 12px rgba(0,0,0,0.05); display: flex; align-items: center; justify-content: space-between; }
        
        .btn-ios { border: none; padding: 10px 20px; border-radius: 12px; font-weight: 600; cursor: pointer; transition: 0.2s; }
        .btn-add { background: var(--ios-blue); color: white; }
        .btn-approve { background: var(--ios-green); color: white; }
        .btn-reject { background: var(--ios-red); color: white; margin-left: 10px; }
        .btn-delete { background: var(--ios-gray); color: #000; }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   <div class="ios-container">
        
        <h2 class="section-title">My Services</h2>
        <asp:Button ID="btnAddService" runat="server" Text="+ Add New Service" CssClass="btn-ios btn-add" OnClick="btnAddService_Click" />
        
        <div style="margin-top:20px;">
            <asp:Repeater ID="rptMyServices" runat="server" OnItemCommand="rptMyServices_ItemCommand">
                <ItemTemplate>
                    <div class="ios-card">
                        <div><strong><%# Eval("ServiceName") %></strong><br /><small>R <%# Eval("Price") %></small></div>
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn-ios btn-delete" 
                            CommandName="Delete" CommandArgument='<%# Eval("ServiceID") %>' />
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>

        <h2 class="section-title">Booking Requests</h2>
        <asp:Repeater ID="rptRequests" runat="server" OnItemCommand="rptRequests_ItemCommand">
            <ItemTemplate>
                <div class="ios-card">
                    <div>
                        <strong><%# Eval("StudentName") %></strong><br />
                        <small><%# Eval("ServiceName") %> | <%# Eval("BookingDate", "{0:MMM dd, yyyy}") %></small>
                    </div>
                    <div>
                        <asp:Button ID="btnApprove" runat="server" Text="Approve" CssClass="btn-ios btn-approve" 
                            CommandName="Approve" CommandArgument='<%# Eval("BookingID") %>' />
                        <asp:Button ID="btnReject" runat="server" Text="Reject" CssClass="btn-ios btn-reject" 
                            CommandName="Reject" CommandArgument='<%# Eval("BookingID") %>' />
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>