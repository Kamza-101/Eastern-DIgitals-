<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Bookings.aspx.cs" Inherits="Group_9.Bookings" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .page-header {
            background-color: darkslategray;
            color: white;
            padding: 30px;
            border-radius: 12px;
            margin-bottom: 30px;
            text-align: center;
        }
        .booking-card {
            background-color: white;
            border: 1px solid lightgray;
            border-radius: 12px;
            padding: 20px;
            margin-bottom: 20px;
            box-shadow: 0 4px 6px whitesmoke;
            transition: transform 0.2s ease, box-shadow 0.2s ease;
        }
        .booking-card:hover {
            transform: translateY(-3px);
            box-shadow: 0 8px 15px lightgray;
        }
        .icon-box {
            background-color: whitesmoke;
            border-radius: 10px;
            width: 70px;
            height: 70px;
            display: flex;
            align-items: center;
            justify-content: center;
            font-size: 2rem;
        }
        .status-badge {
            padding: 8px 15px;
            border-radius: 20px;
            font-weight: bold;
            font-size: 0.85rem;
            display: inline-block;
        }
        .status-Pending { background-color: lightgoldenrodyellow; color: darkgoldenrod; }
        .status-Confirmed { background-color: honeydew; color: mediumseagreen; }
        .status-Completed { background-color: whitesmoke; color: dimgray; }
        .btn-action { border-radius: 20px; font-weight: bold; padding: 8px 20px; }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="container py-4">
        <div class="page-header">
            <h2 class="m-0 fw-bold">My Bookings</h2>
            <p class="text-light m-0 mt-2">Track and manage your requested services on EasternDigital.</p>
        </div>

        <div class="row">
            <div class="col-lg-10 mx-auto">
                <asp:Label ID="lblMessage" runat="server" CssClass="fw-bold d-block mb-3 text-center"></asp:Label>

                <asp:Repeater ID="rptBookings" runat="server" OnItemDataBound="rptBookings_ItemDataBound" OnItemCommand="rptBookings_ItemCommand">
                    <ItemTemplate>
                        <div class="booking-card">
                            <div class="row align-items-center">
                                <div class="col-md-5 d-flex align-items-center gap-3">
                                    <div class="icon-box">
                                        <%# Eval("Icon") %>
                                    </div>
                                    <div>
                                        <h5 class="fw-bold text-dark mb-1"><%# Eval("ServiceName") %></h5>
                                        <p class="text-muted small mb-0">Provider: <strong><%# Eval("ProviderName") %></strong></p>
                                        <p class="text-muted small mb-0">Booking ID: #<%# Eval("BookingID") %></p>
                                    </div>
                                </div>
                                <div class="col-md-4 border-start ps-md-4">
                                    <p class="text-dark mb-1"><small class="text-muted">Scheduled for:</small><br />
                                        <strong><%# Eval("BookingDate") %></strong>
                                    </p>
                                    <span class='status-badge status-<%# Eval("Status") %>'>
                                        <%# Eval("Status") %>
                                    </span>
                                </div>
                                <div class="col-md-3 text-md-end">
                                    <h5 class="fw-bold text-dark mb-3"><%# Eval("Price") %></h5>
                                    <asp:Button ID="btnAction" runat="server" 
                                        CommandName="ProcessAction" 
                                        CommandArgument='<%# Eval("BookingID") %>' 
                                        CssClass="btn btn-outline-primary btn-sm btn-action w-100" />
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>

                <asp:Panel ID="pnlNoBookings" runat="server" Visible="false" CssClass="text-center py-5 bg-light rounded-3 border">
                    <h3 class="text-muted mb-3">No bookings found</h3>
                    <a href="Services.aspx" class="btn btn-primary btn-action px-4">Browse Marketplace</a>
                </asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>
