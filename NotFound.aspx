<%@ Page Title="Page Not Found" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NotFound.aspx.cs" Inherits="Group_9.NotFound" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container text-center" style="padding-top: 100px; padding-bottom: 100px;">
        <h1 class="text-warning fw-bold" style="font-size: 6rem; letter-spacing: -2px;">404</h1>
        <h3 class="fw-bold mb-3">Page Not Found</h3>
        <p class="text-muted fs-5 mb-4">The page you are looking for might have been removed, <br />had its name changed, or is temporarily unavailable.</p>
        
        <a href="Default.aspx" class="btn btn-primary fw-bold rounded-pill px-5 py-2 shadow-sm">Return to Home</a>
    </div>
</asp:Content>
