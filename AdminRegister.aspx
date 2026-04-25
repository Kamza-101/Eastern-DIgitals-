<%@ Page Title="Register Admin" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminRegister.aspx.cs" Inherits="Group_9.AdminRegister" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .register-card { max-width: 500px; margin: 60px auto; border-radius: 15px; box-shadow: 0 10px 30px rgba(0,0,0,0.1); background: #ffffff; }
        .form-header { background: #d9534f; color: white; padding: 20px; border-radius: 15px 15px 0 0; text-align: center; }
        .btn-custom { background-color: #d9534f; color: white; border-radius: 25px; padding: 10px; width: 100%; border: none; font-weight: bold; }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="card register-card">
            <div class="form-header">
                <h3>Create New Admin</h3>
                <p class="mb-0">Secure Access Only</p>
            </div>
            <div class="card-body p-4">
                <div class="row g-3">
                    <div class="col-md-12">
                        <label class="form-label">Full Name</label>
                        <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control" placeholder="Enter full name"></asp:TextBox>
                    </div>
                    <div class="col-md-12">
                        <label class="form-label">Email Address</label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email"></asp:TextBox>
                    </div>
                    <div class="col-md-12">
                        <label class="form-label">Password</label>
                        <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                    </div>
                </div>
                <div class="mt-4">
                    <asp:Button ID="btnRegisterAdmin" runat="server" Text="Create Admin" CssClass="btn btn-custom" OnClick="btnRegisterAdmin_Click" />
                </div>
                <div class="mt-3 text-center">
                    <asp:Label ID="lblMessage" runat="server" CssClass="fw-bold text-danger"></asp:Label>
                </div>
            </div>
        </div>
    </div>
</asp:Content>