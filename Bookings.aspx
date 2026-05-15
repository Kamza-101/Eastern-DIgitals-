<%@ Page Title="My Bookings | EasternDigital" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Bookings.aspx.cs" Inherits="Group_9.Bookings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .booking-card {
            background-color: white;
            border: 1px solid lightgray;
            border-radius: 12px;
            padding: 20px;
            margin-bottom: 15px;
            box-shadow: 0 4px 6px whitesmoke;
            display: flex;
            flex-direction: column; 
            transition: transform 0.2s;
        }
        .booking-card:hover {
            transform: translateY(-2px);
            box-shadow: 0 8px 12px lightgray;
        }
        .provider-avatar {
            background-color: whitesmoke;
            font-size: 30px;
            width: 60px;
            height: 60px;
            border-radius: 50%;
            display: flex;
            align-items: center;
            justify-content: center;
            flex-shrink: 0;
        }
        .status-badge {
            padding: 6px 15px;
            border-radius: 20px;
            font-size: 0.85rem;
            font-weight: bold;
            text-align: center;
        }
        .status-pending { background-color: #ffe8cc; color: #fd7e14; }
        .status-approved { background-color: #e8f8ec; color: #34c759; }
        .status-completed { background-color: #e6f2ff; color: #007aff; }
        .status-rejected { background-color: #ffebe9; color: #ff3b30; }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container py-5">
        <h2 class="fw-bold mb-4 text-center">My Bookings</h2>
        
        <asp:Label ID="lblMessage" runat="server" CssClass="d-block text-center fw-bold mt-3 mb-4 text-danger" Visible="false"></asp:Label>

        <div class="row justify-content-center">
            <div class="col-lg-9">
                
                <asp:Repeater ID="rptBookings" runat="server">
                    <ItemTemplate>
                        <div class="booking-card">
                            
                            <div class="d-flex flex-column flex-md-row align-items-md-center justify-content-between w-100 gap-3">
                                <div class="d-flex align-items-center gap-4">
                                    <div class="provider-avatar shadow-sm border">
                                        <%# Eval("Icon") %>
                                    </div>
                                    
                                    <div>
                                        <h5 class="fw-bold mb-1 text-dark"><%# Eval("FirstName") %> <%# Eval("Surname") %></h5>
                                        <p class="text-muted mb-1 fw-semibold"><%# Eval("ServiceName") %></p>
                                        <p class="text-muted mb-0 small">
                                            📅 <%# Convert.ToDateTime(Eval("BookingDate")).ToString("MMM dd, yyyy - hh:mm tt") %> <br />
                                            💰 R <%# Convert.ToDecimal(Eval("TotalCost")).ToString("0.00") %> &nbsp;|&nbsp; Ref: <%# Eval("OrderReference") %>
                                        </p>
                                    </div>
                                </div>
                                
                                <div class="text-md-end mt-3 mt-md-0" style="min-width: 150px;">
                                    <div class='status-badge <%# GetStatusCssClass(Eval("Status").ToString()) %>'>
                                        <%# Eval("Status") %>
                                    </div>
                                </div>
                            </div>

                            <asp:Panel ID="pnlRejection" runat="server" Visible='<%# Eval("Status").ToString() == "Rejected" %>' 
                                CssClass="alert alert-danger mt-3 mb-0 py-2 px-3 small border-0" 
                                style="background-color: #fff0f0; border-radius: 8px;">
                                <strong class="text-danger">Reason for rejection:</strong> 
                                <span class="text-dark"><%# Eval("RejectionReason") %></span>
                            </asp:Panel>

                        </div>
                    </ItemTemplate>
                </asp:Repeater>

            </div>
        </div>
    </div>
</asp:Content>