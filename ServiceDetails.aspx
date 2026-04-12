<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ServiceDetails.aspx.cs" Inherits="YourProject.ServiceDetails" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Service Details</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        .service-header { background: #007bff; color: white; padding: 60px 0; margin-bottom: 30px; }
        .provider-card { border-left: 5px solid #007bff; }
        .price-tag { font-size: 1.5rem; font-weight: bold; color: #28a745; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="service-header text-center">
            <div class="container">
                <asp:Label ID="lblServiceName" runat="server" Text="Service Name" Font-Size="XX-Large" CssClass="fw-bold"></asp:Label>
                <p class="lead">Professional service in the Eastern Cape</p>
            </div>
        </div>

        <div class="container">
            <div class="row">
                <div class="col-md-8">
                    <div class="card mb-4 shadow-sm p-4">
                        <h4 class="text-secondary">About this Service</h4>
                        <hr />
                        <asp:Label ID="lblDescription" runat="server" Text="Detailed description goes here..." CssClass="text-muted"></asp:Label>
                        
                        <div class="mt-4">
                            <span class="text-dark fw-bold">Price: </span>
                            <asp:Label ID="lblPrice" runat="server" CssClass="price-tag" Text="R 0.00"></asp:Label>
                        </div>
                    </div>
                </div>

                <div class="col-md-4">
                    <div class="card p-3 provider-card shadow-sm">
                        <h5 class="fw-bold">Provider Information</h5>
                        <hr />
                        <div class="mb-2">
                            <strong>Name:</strong> <asp:Label ID="lblProviderName" runat="server"></asp:Label>
                        </div>
                        <div class="mb-3 text-muted">
                            <small>Verified Provider in East London</small>
                        </div>
                        <asp:Button ID="btnBookNow" runat="server" Text="Book This Service" CssClass="btn btn-primary w-100" />
                        <asp:Button ID="btnBack" runat="server" Text="Back to Services" CssClass="btn btn-link w-100 mt-2" OnClick="btnBack_Click" />
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>