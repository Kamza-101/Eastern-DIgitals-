<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Group_9.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .login-card {
            max-width: 500px;
            margin: 60px auto;
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
            width: 100%;
            font-weight: bold;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="card login-card">
            <div class="form-header">
                <h3>Welcome Back</h3>
                <p class="mb-0">Log in to your EasternDigital Account</p>
            </div>
            
            <div class="card-body p-4">
                <div class="mb-4 text-center">
                    <label class="form-label fw-bold d-block">I am logging in as a:</label>
                    <div class="d-flex justify-content-center">
                        <asp:RadioButtonList ID="rblLoginType" runat="server" RepeatDirection="Horizontal" CssClass="mx-auto">
                            <asp:ListItem Value="Seeker" Selected="True">&nbsp;Service Seeker&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="Provider">&nbsp;Service Provider</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>

                <hr />

                <div class="row g-3">
                    <div class="col-md-12">
                        <label class="form-label">Email Address</label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" placeholder="Enter your email"></asp:TextBox>
                    </div>
                    
                    <div class="col-md-12">
                        <label class="form-label">Password</label>
                        <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Enter your password"></asp:TextBox>
                    </div>
                </div>

                <div class="mt-4">
                    <asp:Button ID="btnLogin" runat="server" Text="Log In" CssClass="btn btn-custom-primary" OnClick="btnLogin_Click" />
                </div>

                <div class="mt-3 text-center">
                    <asp:Label ID="lblLoginMessage" runat="server" CssClass="fw-bold text-danger"></asp:Label>
                </div>
                
                <div class="mt-3 text-center">
                    <p class="text-muted small">Don't have an account? <a href="Register.aspx" class="text-decoration-none">Register here</a></p>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
