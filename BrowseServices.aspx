<%@ Page Title="Find a Provider | EasternDigital" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BrowseServices.aspx.cs" Inherits="Group_9.BrowseServices" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
    .service-card {
        transition: transform 0.3s ease, box-shadow 0.3s ease;
        border: none;
        border-radius: 12px;
        box-shadow: 0 4px 6px whitesmoke;
        height: 100%;
    }
    .service-card:hover {
        transform: translateY(-5px);
        box-shadow: 0 10px 20px lightgray;
    }
    .card-icon-wrapper {
        background-color: whitesmoke;
        border-radius: 12px 12px 0 0;
        padding: 30px;
        text-align: center;
        border-bottom: 2px solid lightgray;
    }
    .category-badge {
        position: absolute;
        top: 15px;
        right: 15px;
    }
    .search-bar-container {
        background-color: darkslategray;
        border-radius: 12px;
        padding: 20px;
        margin-bottom: 30px;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container py-4">
        
        <div class="text-center mb-5">
            <h2 class="display-6 fw-bold text-dark">EasternDigital Marketplace</h2>
            <p class="text-muted fs-5">Browse affordable services provided by local students and professionals.</p>
        </div>

       

        <div class="row row-cols-1 row-cols-md-2 row-cols-lg-4 g-4">
            
            <div class="col">
                <div class="card service-card">
                    <span class="badge bg-success category-badge">High Demand</span>
                    <div class="card-icon-wrapper">
                        <h1 class="display-4 text-primary mb-0">📚</h1>
                    </div>
                    <div class="card-body">
                        <h5 class="card-title fw-bold">Tutoring Services</h5>
                        <p class="card-text text-muted small">Get help with your modules. Affordable peer-to-peer tutoring in programming, math, and more.</p>
                        <hr />
                        <div class="d-flex justify-content-between align-items-center">
                            <span class="fw-bold text-dark">From R80/hr</span>
                            <asp:Button ID="btnViewTutoring" runat="server" Text="View Providers" CssClass="btn btn-sm btn-outline-primary" OnClick="btnViewTutoring_Click" />
                        </div>
                    </div>
                </div>
            </div>

            <div class="col">
                <div class="card service-card">
                    <div class="card-icon-wrapper">
                        <h1 class="display-4 text-secondary mb-0">🖨️</h1>
                    </div>
                    <div class="card-body">
                        <h5 class="card-title fw-bold">Printing Services</h5>
                        <p class="card-text text-muted small">Fast and affordable local printing for assignments, posters, and documents.</p>
                        <hr />
                        <div class="d-flex justify-content-between align-items-center">
                            <span class="fw-bold text-dark">From R1/page</span>
                            <asp:Button ID="btnViewPrinting" runat="server" Text="View Providers" CssClass="btn btn-sm btn-outline-primary" onClick="btnViewPrinting_Click" />
                        </div>
                    </div>
                </div>
            </div>

            <div class="col">
                <div class="card service-card">
                    <div class="card-icon-wrapper">
                        <h1 class="display-4 text-info mb-0">🎨</h1>
                    </div>
                    <div class="card-body">
                        <h5 class="card-title fw-bold">Graphic Design</h5>
                        <p class="card-text text-muted small">Logos, flyers, and digital portfolios created by talented local student designers.</p>
                        <hr />
                        <div class="d-flex justify-content-between align-items-center">
                            <span class="fw-bold text-dark">Varies</span>
                            <asp:Button ID="btnViewDesign" runat="server" Text="View Providers" CssClass="btn btn-sm btn-outline-primary" onClick="btnViewDesign_Click"/>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col">
                <div class="card service-card">
                    <span class="badge bg-warning text-dark category-badge">Essential</span>
                    <div class="card-icon-wrapper">
                        <h1 class="display-4 text-danger mb-0">🔧</h1>
                    </div>
                    <div class="card-body">
                        <h5 class="card-title fw-bold">Device Repair</h5>
                        <p class="card-text text-muted small">Screen replacements, software troubleshooting, and hardware fixes for laptops and phones.</p>
                        <hr />
                        <div class="d-flex justify-content-between align-items-center">
                            <span class="fw-bold text-dark">Quote Based</span>
                            <asp:Button ID="btnViewRepair" runat="server" Text="View Providers" CssClass="btn btn-sm btn-outline-primary" onClick="btnViewRepair_Click"/>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>
</asp:Content>
