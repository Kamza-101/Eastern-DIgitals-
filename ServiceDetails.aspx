<%@ Page Title="Service Providers" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ServiceDetails.aspx.cs" Inherits="Group_9.ServiceDetails" %>

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
        .provider-card {
            background-color: white;
            border: 1px solid lightgray;
            border-radius: 12px;
            padding: 20px;
            margin-bottom: 20px;
            box-shadow: 0 4px 6px whitesmoke;
            transition: transform 0.2s ease;
        }
        .provider-card:hover {
            transform: translateY(-3px);
            box-shadow: 0 8px 15px lightgray;
        }
        .provider-avatar {
            background-color: dodgerblue;
            color: white;
            width: 60px;
            height: 60px;
            border-radius: 50%;
            display: flex;
            align-items: center;
            justify-content: center;
            font-size: 24px;
            font-weight: bold;
            flex-shrink: 0;
        }
        .btn-book {
            background-color: dodgerblue;
            color: white;
            font-weight: bold;
            border-radius: 20px;
            padding: 10px 25px;
            border: none;
            transition: background-color 0.3s;
        }
        .btn-book:hover {
            background-color: royalblue;
            color: white;
        }
        .price-tag {
            color: seagreen;
            font-weight: bold;
            font-size: 1.5rem;
        }
        .service-banner {
            width: 100%;
            height: 250px;
            object-fit: cover;
            border-radius: 12px;
            margin-bottom: 20px;
            background-color: #f8f9fa;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container py-4">
        
        <div class="page-header shadow-sm">
            <h2 class="m-0 fw-bold">
                <asp:Label ID="lblServiceName" runat="server"></asp:Label>
            </h2>
            <p class="text-light m-0 mt-2">Review the details below and add this service to your cart.</p>
        </div>

        <div class="row justify-content-center">
            <div class="col-lg-10">
                
                <asp:Image ID="imgService" runat="server" CssClass="service-banner shadow-sm" />

                <div class="provider-card d-flex flex-column flex-md-row justify-content-between align-items-md-center gap-4">
                    
                    <div class="d-flex align-items-start gap-3 w-100">
                        <div class="provider-avatar shadow-sm">
                            👤
                        </div>
                        
                        <div>
                            <h5 class="fw-bold text-dark mb-1">
                                Provided by: <asp:Label ID="lblProviderName" runat="server"></asp:Label>
                            </h5>
                            <p class="text-muted mb-2">
                                <strong>About this service:</strong> <asp:Label ID="lblServiceDesc" runat="server"></asp:Label>
                            </p>
                            <p class="text-muted small mb-0">
                                📍 <asp:Label ID="lblLocation" runat="server"></asp:Label> &nbsp;|&nbsp; 
                                📞 <asp:Label ID="lblContact" runat="server"></asp:Label>
                            </p>
                        </div>
                    </div>
                    
                    <div class="text-md-end mt-3 mt-md-0 border-start ps-md-4 min-vw-25">
                        <div class="price-tag mb-3">
                            <asp:Label ID="lblPrice" runat="server"></asp:Label>
                        </div>
                        
                        <asp:Button ID="btnAddToCart" runat="server" Text="Add to Cart" CssClass="btn-book w-100 shadow-sm" OnClick="btnAddToCart_Click" />
                    </div>

                </div>

                <div class="text-center mt-5">
                    <a href="BrowseServices.aspx" class="btn btn-outline-dark rounded-pill px-4">← Back to Marketplace</a>
                </div>

            </div>
        </div>

    </div>
</asp:Content>