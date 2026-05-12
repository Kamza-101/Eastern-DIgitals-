<%@ Page Title="Service Details" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ServiceDetails.aspx.cs" Inherits="Group_9.ServiceDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        :root {
            --ios-bg: #f2f2f7;
            --ios-card: #ffffff;
            --ios-blue: #007aff;
            --ios-green: #2fa562;
            --header-bg: #1e3d3d; /* Dark teal matching your design */
        }
        body { background-color: var(--ios-bg); font-family: -apple-system, BlinkMacSystemFont, "Segoe UI", sans-serif; }
        
        .service-header {
            background-color: var(--header-bg);
            color: white;
            padding: 40px 20px;
            border-radius: 12px;
            margin-bottom: 30px;
            box-shadow: 0 4px 12px rgba(0,0,0,0.1);
        }
        
        .provider-card {
            background: var(--ios-card);
            border-radius: 20px;
            padding: 30px;
            box-shadow: 0 4px 15px rgba(0,0,0,0.04);
            border: 1px solid #f0f0f0;
            transition: transform 0.2s ease;
        }
        .provider-card:hover {
            transform: translateY(-3px);
            box-shadow: 0 8px 20px rgba(0,0,0,0.08);
        }

        .provider-avatar {
            background: var(--ios-blue);
            color: white;
            border-radius: 50%;
            width: 70px;
            height: 70px;
            display: flex;
            align-items: center;
            justify-content: center;
            font-size: 2rem;
            box-shadow: 0 4px 10px rgba(0, 122, 255, 0.3);
            flex-shrink: 0;
        }

        .btn-book {
            background-color: var(--ios-blue);
            color: white;
            font-weight: bold;
            border-radius: 30px;
            padding: 12px 25px;
            border: none;
            transition: all 0.2s ease;
            width: 100%;
        }
        .btn-book:hover {
            background-color: #005bb5;
            color: white;
            transform: translateY(-2px);
        }
        
        .price-tag {
            color: var(--ios-green);
            font-weight: 800;
            font-size: 1.8rem;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container py-4" style="max-width: 900px;">
        
        <div class="service-header text-center">
            <h1 class="fw-bold mb-2">
                <asp:Label ID="lblServiceName" runat="server" Text="Service Name"></asp:Label>
            </h1>
            <p class="mb-0 text-light" style="opacity: 0.9;">Review the details below and add this service to your cart.</p>
        </div>

        <div class="provider-card">
            <div class="row align-items-center">
                
                <div class="col-md-8 mb-4 mb-md-0 d-flex gap-4 align-items-start">
                    <div class="provider-avatar">
                        👤
                    </div>
                    
                    <div>
                        <h4 class="fw-bold text-dark mb-2">
                            Provided by: <asp:Label ID="lblProviderName" runat="server"></asp:Label>
                        </h4>
                        
                        <p class="text-muted mb-3" style="line-height: 1.6;">
                            <strong class="text-dark">About this service:</strong> 
                            <asp:Label ID="lblServiceDesc" runat="server"></asp:Label>
                        </p>
                        
                        <div class="d-flex flex-wrap gap-4 text-muted small fw-semibold">
                            <span><span style="font-size: 1.1rem; color: #ff3b30;">📍</span> <asp:Label ID="lblLocation" runat="server"></asp:Label></span>
                            <span><span style="font-size: 1.1rem; color: var(--ios-blue);">📞</span> <asp:Label ID="lblContact" runat="server"></asp:Label></span>
                        </div>
                    </div>
                </div>

                <div class="col-md-4 text-md-end text-center border-start-md ps-md-4" style="border-color: #f0f0f0 !important;">
                    <div class="price-tag mb-3">
                        <asp:Label ID="lblPrice" runat="server"></asp:Label>
                    </div>
                    
                    <asp:Button ID="btnAddToCart" runat="server" Text="Add to Cart" CssClass="btn-book shadow-sm" OnClick="btnAddToCart_Click" />
                </div>
                
            </div>
        </div>

        <div class="text-center mt-5">
            <a href="BrowseServices.aspx" class="btn btn-outline-dark rounded-pill px-4 fw-bold shadow-sm">← Back to Marketplace</a>
        </div>

    </div>
</asp:Content>