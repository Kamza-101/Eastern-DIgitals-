<%@ Page Title="Booking Successful" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BookingSuccess.aspx.cs" Inherits="Group_9.BookingSuccess" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .success-container {
            max-width: 600px;
            margin: 50px auto;
            background-color: white;
            padding: 40px 30px;
            border-radius: 12px;
            box-shadow: 0 4px 15px rgba(0,0,0,0.05);
            border: 1px solid #e8f8ec;
            text-align: center;
        }
        .success-icon {
            font-size: 80px;
            color: #34c759; 
            margin-bottom: 20px;
        }
        .btn-my-bookings {
            background-color: #007aff; 
            color: white;
            font-weight: bold;
            padding: 12px 30px;
            border-radius: 25px;
            border: none;
            text-decoration: none;
            display: inline-block;
            margin-top: 20px;
            transition: opacity 0.2s;
        }
        .btn-my-bookings:hover {
            opacity: 0.8;
            color: white;
        }
        .reference-box {
            background-color: #f2f2f7;
            padding: 10px;
            border-radius: 8px;
            font-weight: bold;
            font-family: monospace;
            font-size: 1.1rem;
            display: inline-block;
            margin: 15px 0;
            color: #1c1c1e;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="success-container">
            
            <div class="success-icon">✓</div>
            <h2 class="fw-bold mb-3">Booking Request Sent!</h2>
            
            <p class="text-muted fs-5 mb-2">
                Your service request has been successfully submitted to the provider. 
            </p>
            
            <asp:Panel ID="pnlReference" runat="server" Visible="false">
                <div class="reference-box">
                    Ref: <asp:Label ID="lblOrderRef" runat="server"></asp:Label>
                </div>
            </asp:Panel>

            <div class="alert alert-warning mt-4 mb-4 text-start" style="border-radius: 10px; border-left: 5px solid #ff9500;">
                <h6 class="fw-bold text-dark mb-1">⏳ What happens next?</h6>
                <p class="mb-0 small text-dark">
                    Your booking is currently <strong>Pending Confirmation</strong>. The service provider will review your request and you will see the status update in your dashboard once they approve or decline it.
                </p>
            </div>

            <a href="Bookings.aspx" class="btn-my-bookings">View My Bookings</a>

        </div>
    </div>
</asp:Content>
