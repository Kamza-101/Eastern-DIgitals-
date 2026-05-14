<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Group_9.Login" %>

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
            border: none;
        }
        .btn-custom-primary:hover {
            background-color: #0b5ed7;
            color: white;
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

                <asp:ValidationSummary 
                    ID="ValidationSummary1"
                    runat="server"
                    HeaderText="Please fix the following errors:"
                    CssClass="text-danger fw-bold mb-3" />

                 <div class="mb-4 text-center">
                    <label class="form-label fw-bold d-block">I am logging in as a:</label>
                    <div class="d-flex justify-content-center">
                        <asp:RadioButtonList ID="rblLoginType" runat="server" RepeatDirection="Horizontal" CssClass="mx-auto">
                            <asp:ListItem Value="Seeker" Selected="True">&nbsp;Service Seeker&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="Provider">&nbsp;Service Provider&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="Admin">&nbsp;Admin</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>

                <hr />

                <div class="row g-3">

                     <div class="col-md-12">
                        <label class="form-label">Email Address</label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" placeholder="Enter your email"></asp:TextBox>

                          <asp:RequiredFieldValidator
                            ID="rfvEmail"
                            runat="server"
                            ControlToValidate="txtEmail"
                            ErrorMessage="Email Address is required"
                            ForeColor="Red" />

                         <asp:RegularExpressionValidator
                            ID="revEmail"
                            runat="server"
                            ControlToValidate="txtEmail"
                            ValidationExpression="\w+([-\+.’]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                            ErrorMessage="Enter a valid email address"
                            ForeColor="Red" />
                    </div>
                    
                    <div class="col-md-12">
                        <label class="form-label">Password</label>
                        <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Enter your password"></asp:TextBox>

                        <asp:RequiredFieldValidator
                            ID="rfvPassword"
                            runat="server"
                            ControlToValidate="txtPassword"
                            ErrorMessage="Password is required"
                            ForeColor="Red" />

                    </div>
                </div>

                <div class="mt-4">
                    <asp:Button ID="btnLogin" runat="server" Text="Log In" CssClass="btn btn-custom-primary" OnClick="btnLogin_Click" />
                </div>

                <div class="mt-3 text-center">
                    <asp:Label ID="lblLoginMessage" runat="server" CssClass="fw-bold text-danger"></asp:Label>
                </div>
                
                <div class="mt-2 text-center">
                    <asp:LinkButton ID="btnShowForgot" runat="server" CssClass="text-primary text-decoration-underline small fw-bold" OnClick="btnShowForgot_Click" CausesValidation="false">Forgot your password?</asp:LinkButton>
                </div>

                <asp:Panel ID="pnlForgot" runat="server" Visible="false" CssClass="mt-3 p-3 border rounded bg-light text-start">
                    <h6 class="fw-bold text-dark">Password Recovery</h6>
                    <p class="small text-muted mb-2">Enter your registered email and we will send your password to you.</p>
                    <asp:TextBox ID="txtForgotEmail" runat="server" CssClass="form-control form-control-sm mb-2" TextMode="Email" placeholder="Email address"></asp:TextBox>
                    <asp:Button ID="btnSendPassword" runat="server" Text="Send Password" CssClass="btn btn-sm btn-dark w-100" OnClick="btnSendPassword_Click" CausesValidation="false" />
                    <asp:Label ID="lblForgotMessage" runat="server" CssClass="d-block mt-2 small fw-bold text-center"></asp:Label>
                </asp:Panel>

                <div class="mt-4 text-center">
                    <p class="text-muted small">Don't have an account? <a href="Register.aspx" class="text-decoration-none fw-bold">Register here</a></p>
                </div>
            </div>
        </div>
    </div>
</asp:Content>