<%@ Page Title="System Error" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="Group_9.Error" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container text-center" style="padding-top: 100px; padding-bottom: 100px;">
        <div style="font-size: 4rem; margin-bottom: 20px;">⚠️</div>
        <h2 class="text-danger fw-bold mb-3">Oops! Something went wrong.</h2>
        <p class="text-muted fs-5 mb-4">We've encountered an unexpected system error. <br />Our administrators have been notified and are looking into the issue.</p>
        
        <a href="Default.aspx" class="btn btn-primary fw-bold rounded-pill px-5 py-2 shadow-sm">Return to Home</a>
    </div>
</asp:Content>
