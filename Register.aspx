<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Group_9.Register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .register-card {
            max-width: 800px;
            margin: 40px auto;
            border: none;
            border-radius: 15px;
            box-shadow: 0 10px 30px rgba(0,0,0,0.1);
            background: #ffffff;
        }
        .form-header {
            background: #212529;
            color: white;
            padding: 20px;
            border-radius: 15px 15px 0 0;
            text-align: center;
        }
        .btn-custom-primary {
            background-color: #0d6efd;
            color: white;
            border-radius: 25px;
            padding: 10px 30px;
        }
        .btn-custom-clear {
            background-color: #f8f9fa;
            border: 1px solid #ced4da;
            border-radius: 25px;
            padding: 10px 30px;
        }
        .validation-box {
            margin-top: 20px;
            font-size: 0.9rem;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="card register-card">
            <div class="form-header">
                <h3>Create Your Account</h3>
                <p class="mb-0">Join EasternDigital's Student Marketplace</p>
            </div>
            
            <div class="card-body p-4">
                <div class="mb-4 text-center">
                    <label class="form-label fw-bold d-block">I am registering as a:</label>
                    <div class="d-flex justify-content-center">
                        <asp:RadioButtonList ID="rblUserType" runat="server" RepeatDirection="Horizontal" 
                            AutoPostBack="true" OnSelectedIndexChanged="rblUserType_SelectedIndexChanged" CssClass="mx-auto">
                            <asp:ListItem Value="Seeker" Selected="True">&nbsp;Service Seeker&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="Provider">&nbsp;Service Provider</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>

                <hr />

                <asp:Panel ID="pnlSeeker" runat="server" Visible="true">
                    <div class="row g-3">
                        <div class="col-md-12">
                            <label class="form-label">Full Name</label>
                            <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control" placeholder="Enter your full name"></asp:TextBox>
                        </div>
                        <div class="col-md-6">
                            <label class="form-label">Email Address</label>
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email"></asp:TextBox>
                        </div>
                        <div class="col-md-6">
                            <label class="form-label">Contact Number</label>
                            <asp:TextBox ID="txtContact" runat="server" CssClass="form-control" MaxLength="10" placeholder="10 digits"></asp:TextBox>
                        </div>
                        <div class="col-md-6">
                            <label class="form-label">University</label>
                            <asp:DropDownList ID="ddlUniversity" runat="server" CssClass="form-select">
                                <asp:ListItem Text="-- Select University --" Value="" />
                                <asp:ListItem>Walter Sisulu University</asp:ListItem>
                                <asp:ListItem>University Of Fort Hare</asp:ListItem>
                                <asp:ListItem>Rhodes University</asp:ListItem>
                                <asp:ListItem>Nelson Mandela University</asp:ListItem>
                                <asp:ListItem>UNISA</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-6">
                            <label class="form-label">City</label>
                            <asp:DropDownList ID="ddlCity" runat="server" CssClass="form-select">
                                <asp:ListItem Text="-- Select City --" Value="" />
                                <asp:ListItem>Port Elizabeth</asp:ListItem>
                                <asp:ListItem>Mthatha</asp:ListItem>
                                <asp:ListItem>Alice</asp:ListItem>
                                <asp:ListItem>Butterworth</asp:ListItem>
                                <asp:ListItem>East London</asp:ListItem>
                                <asp:ListItem>Bisho</asp:ListItem>
                                <asp:ListItem>Grahamstown</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </asp:Panel>

                <asp:Panel ID="pnlProvider" runat="server" Visible="false">
                    <div class="row g-3">
                        <div class="col-md-6">
                            <label class="form-label">Name</label>
                            <asp:TextBox ID="txtProvName" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-6">
                            <label class="form-label">Surname</label>
                            <asp:TextBox ID="txtProvSurname" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-6">
                            <label class="form-label">ID Number</label>
                            <asp:TextBox ID="txtID" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-6">
                            <label class="form-label">Location</label>
                            <asp:DropDownList ID="ddlLocation" runat="server" CssClass="form-select">
                                <asp:ListItem Text="-- Select Location --" Value="" />
                                <asp:ListItem>Port Elizabeth</asp:ListItem>
                                <asp:ListItem>Mthatha</asp:ListItem>
                                <asp:ListItem>Alice</asp:ListItem>
                                <asp:ListItem>Butterworth</asp:ListItem>
                                <asp:ListItem>East London</asp:ListItem>
                                <asp:ListItem>Bisho</asp:ListItem>
                                <asp:ListItem>Grahamstown</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-12">
                            <label class="form-label">Service Type</label>
                            <asp:DropDownList ID="ddlServiceType" runat="server" CssClass="form-select">
                                <asp:ListItem Text="-- What service do you provide? --" Value="" />
                                <asp:ListItem>Printing Services</asp:ListItem>
                                <asp:ListItem>Tutoring Services</asp:ListItem>
                                <asp:ListItem>Graphic Design Services</asp:ListItem>
                                <asp:ListItem>Device Repair Services</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-6">
                            <label class="form-label">Email</label>
                            <asp:TextBox ID="txtProvEmail" runat="server" CssClass="form-control" TextMode="Email"></asp:TextBox>
                        </div>
                        <div class="col-md-6">
                            <label class="form-label">Contact Number</label>
                            <asp:TextBox ID="txtProvContact" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                        </div>
                    </div>
                </asp:Panel>

                <div class="row g-3 mt-2">
                    <div class="col-md-6">
                        <label class="form-label">Password</label>
                        <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                    </div>
                    <div class="col-md-6">
                        <label class="form-label">Confirm Password</label>
                        <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                    </div>
                </div>

                <div class="mt-4">
                    <asp:CheckBox ID="chkTerms" runat="server" Text="&nbsp;I agree to the Terms and Conditions" CssClass="form-check-input ms-0" />
                </div>

                <div class="mt-4 d-flex gap-2">
                    <asp:Button ID="btnRegister" runat="server" Text="Create Account" CssClass="btn btn-custom-primary" />
                    <asp:Button ID="btnClear" runat="server" Text="Clear Form" CssClass="btn btn-custom-clear" />
                </div>

                <div class="mt-3">
                    <asp:Label ID="lblMessage" runat="server" CssClass="fw-bold"></asp:Label>
                </div>

                <div class="validation-box">
                    <asp:ValidationSummary ID="vsRegister" runat="server" CssClass="alert alert-danger" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
