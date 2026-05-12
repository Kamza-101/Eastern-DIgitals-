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
            align-items: center;
            justify-content: space-between;
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
        /* Dynamic Status Colors based on your wireframe */
        .status-pending {
            background-color: #ffe8cc;
            color: #fd7e14; /* Orange for Pending */
        }
        .status-approved {
            background-color: #d1e7dd;
            color: #198754; /* Green for Approved */
        }
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
                        <div class="booking-card flex-column flex-md-row gap-3">
                            
                            <div class="d-flex align-items-center gap-4 w-100">
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
                                <div class='status-badge <%# Eval("Status").ToString() == "Pending Confirmation" ? "status-pending" : "status-approved" %>'>
                                    <%# Eval("Status") %>
                                </div>
                            </div>

                        </div>
                    </ItemTemplate>
                </asp:Repeater>

            </div>
        </div>
    </div>
</asp:Content>
