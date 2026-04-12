<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Group_9.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
   <style>
        .hero-box {
            background: #f8f9fa;
            border-radius: 15px;
            padding: 4rem 2rem;
            margin-top: 2rem;
            box-shadow: 0 4px 12px rgba(0,0,0,0.08);
        }
        .group-tag {
            text-transform: uppercase;
            letter-spacing: 2px;
            color: #0d6efd;
            font-weight: bold;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="hero-box text-center">
        <div class="container">
            <p class="group-tag mb-2">Group 9</p>
            <h1 class="display-4 fw-bold text-dark">What we're all about</h1>
            
            <div class="row justify-content-center">
                <div class="col-lg-9">
                    <p class="lead text-dark mb-5">
                        EasternDigital provides a specialized marketplace for students in the Eastern Cape to easily access and book affordable services like tutoring, printing, and graphic design. 
                        Our platform empowers local providers by turning informal work into a professional digital portfolio, bridging the gap between talent and opportunity while driving economic growth in our community.
                    </p>

                    <div class="d-grid gap-3 d-md-flex justify-content-md-center">
                        <a href="Register.aspx" class="btn btn-primary btn-lg px-5">Join as a Member</a>
                        <a href="BrowseServices.aspx" class="btn btn-outline-primary btn-lg px-5">Explore services</a>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
