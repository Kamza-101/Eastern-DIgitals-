<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Group_9.Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .register-card { max-width: 800px; margin: 40px auto; border-radius: 15px; box-shadow: 0 10px 30px rgba(0,0,0,0.1); background: #ffffff; }
        .form-header { background: #212529; color: white; padding: 20px; border-radius: 15px 15px 0 0; text-align: center; }
        .btn-custom-primary { background-color: #0d6efd; color: white; border-radius: 25px; padding: 10px 30px; }
        .btn-custom-clear { background-color: #f8f9fa; border: 1px solid #ced4da; border-radius: 25px; padding: 10px 30px; }
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
                  <!-- Validation Summary -->
                <asp:ValidationSummary 
                    ID="ValidationSummary1" 
                    runat="server"
                    HeaderText="Please fix the following errors:"
                    CssClass="text-danger fw-bold mb-3" />

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

                 <!-- SEEKER PANEL -->
                <asp:Panel ID="pnlSeeker" runat="server" Visible="true">
                    <div class="row g-3">

                         <!-- Full Name -->
                        <div class="col-md-12">
                            <label class="form-label">Full Name</label>
                            <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control" placeholder="Enter your full name"></asp:TextBox>
                            
                            <asp:RequiredFieldValidator
                                ID="rfvFullName"
                                runat="server"
                                ControlToValidate="txtFullName"
                                ErrorMessage="Full Name is required"
                                ForeColor="Red" />
                        </div>

                        <!-- Email -->
                        <div class="col-md-6">
                            <label class="form-label">Email Address</label>
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email"></asp:TextBox>

                             <asp:RequiredFieldValidator
                                ID="rfvEmail"
                                runat="server"
                                ControlToValidate="txtEmail"
                                ErrorMessage="Email is required"
                                ForeColor="Red" />

                            <asp:RegularExpressionValidator
                                ID="revEmail"
                                runat="server"
                                ControlToValidate="txtEmail"
                                ErrorMessage="Invalid email format"
                                ValidationExpression="\w+([-\+.’]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                ForeColor="Red" />
                        </div>

                         <!-- Contact -->
                        <div class="col-md-6">
                            <label class="form-label">Contact Number</label>
                            <asp:TextBox ID="txtContact" runat="server" CssClass="form-control" MaxLength="10" placeholder="10 digits"></asp:TextBox>

                             <asp:RequiredFieldValidator
                                ID="rfvContact"
                                runat="server"
                                ControlToValidate="txtContact"
                                ErrorMessage="Contact Number is required"
                                ForeColor="Red" />

                            <asp:RegularExpressionValidator
                                ID="revContact"
                                runat="server"
                                ControlToValidate="txtContact"
                                ValidationExpression="^\d{10}$"
                                ErrorMessage="Enter a valid 10-digit number"
                                ForeColor="Red" />

                        </div>

                          <!-- University -->
                        <div class="col-md-6">
                            <label class="form-label">University</label>
                            <asp:DropDownList ID="ddlUniversity" runat="server" CssClass="form-select">
                                <asp:ListItem Text="-- Select University --" Value="" />
                                <asp:ListItem>Walter Sisulu University</asp:ListItem>
                                <asp:ListItem>University Of Fort Hare</asp:ListItem>
                                <asp:ListItem>Rhodes University</asp:ListItem>
                                <asp:ListItem>Nelson Mandela University</asp:ListItem>
                            </asp:DropDownList>

                             <asp:RequiredFieldValidator
                                ID="rfvUniversity"
                                runat="server"
                                ControlToValidate="ddlUniversity"
                                InitialValue=""
                                ErrorMessage="Please select a university"
                                ForeColor="Red" />

                        </div>

                         <!-- City -->
                        <div class="col-md-6">
                            <label class="form-label">City</label>
                            <asp:DropDownList ID="ddlCity" runat="server" CssClass="form-select">
                                <asp:ListItem Text="-- Select City --" Value="" />
                                <asp:ListItem>Gqeberha</asp:ListItem>
                                <asp:ListItem>Mthatha</asp:ListItem>
                                <asp:ListItem>Alice</asp:ListItem>
                                <asp:ListItem>Grahamstown</asp:ListItem>
                                <asp:ListItem>Mthatha</asp:ListItem>
                            </asp:DropDownList>

                             <asp:RequiredFieldValidator
                                ID="rfvCity"
                                runat="server"
                                ControlToValidate="ddlCity"
                                InitialValue=""
                                ErrorMessage="Please select a city"
                                ForeColor="Red" />

                        </div>
                    </div>
                </asp:Panel>

                 <!-- PROVIDER PANEL -->
                <asp:Panel ID="pnlProvider" runat="server" Visible="false">
                    <div class="row g-3">


                        <!-- Name -->
                        <div class="col-md-6">
                            <label class="form-label">Name</label>
                            <asp:TextBox ID="txtProvName" runat="server" CssClass="form-control"></asp:TextBox>

                           <asp:RequiredFieldValidator
                                ID="rfvProvName"
                                runat="server"
                                ControlToValidate="txtProvName"
                                ErrorMessage="Name is required"
                                ForeColor="Red" />

                        </div>

                         <!-- Surname -->
                        <div class="col-md-6">
                            <label class="form-label">Surname</label>
                            <asp:TextBox ID="txtProvSurname" runat="server" CssClass="form-control"></asp:TextBox>

                             <asp:RequiredFieldValidator
                                ID="rfvProvSurname"
                                runat="server"
                                ControlToValidate="txtProvSurname"
                                ErrorMessage="Surname is required"
                                ForeColor="Red" />

                        </div>

                        <!-- ID Number -->
                        <div class="col-md-6">
                            <label class="form-label">ID Number</label>
                            <asp:TextBox ID="txtID" runat="server" CssClass="form-control"></asp:TextBox>

                             <asp:RequiredFieldValidator
                                ID="rfvID"
                                runat="server"
                                ControlToValidate="txtID"
                                ErrorMessage="ID Number is required"
                                ForeColor="Red" />

                             <asp:RegularExpressionValidator
                                ID="revID"
                                runat="server"
                                ControlToValidate="txtID"
                                ValidationExpression="^\d{13}$"
                                ErrorMessage="ID Number must be 13 digits"
                                ForeColor="Red" />

                        </div>

                         <!-- Location -->
                        <div class="col-md-6">
                            <label class="form-label">Location</label>
                            <asp:DropDownList ID="ddlLocation" runat="server" CssClass="form-select">
                                <asp:ListItem Text="-- Select Location --" Value="" />
                                <asp:ListItem>Port Elizabeth</asp:ListItem>
                                <asp:ListItem>Mthatha</asp:ListItem>
                                <asp:ListItem>Grahamstown</asp:ListItem>
                                <asp:ListItem>Bhisho</asp:ListItem>
                               <asp:ListItem>East London</asp:ListItem>
                            </asp:DropDownList>

                               <asp:RequiredFieldValidator
                                ID="rfvLocation"
                                runat="server"
                                ControlToValidate="ddlLocation"
                                InitialValue=""
                                ErrorMessage="Please select a location"
                                ForeColor="Red" />

                        </div>

                         <!-- Service Type -->
                        <div class="col-md-12">
                            <label class="form-label">Service Type</label>
                            <asp:DropDownList ID="ddlServiceType" runat="server" CssClass="form-select">
                                <asp:ListItem Text="-- What service do you provide? --" Value="" />
                                <asp:ListItem>Tutoring Services</asp:ListItem>
                                <asp:ListItem>Printing Services</asp:ListItem>
                                <asp:ListItem>Graphic Design</asp:ListItem>
                                <asp:ListItem>Device Repair Services</asp:ListItem>
                            </asp:DropDownList>

                            <asp:RequiredFieldValidator
                                ID="rfvServiceType"
                                runat="server"
                                ControlToValidate="ddlServiceType"
                                InitialValue=""
                                ErrorMessage="Please select a service type"
                                ForeColor="Red" />

                        </div>

                        <!-- Provider Email -->
                        <div class="col-md-6">
                            <label class="form-label">Email</label>
                            <asp:TextBox ID="txtProvEmail" runat="server" CssClass="form-control" TextMode="Email"></asp:TextBox>

                            <asp:RequiredFieldValidator
                                ID="rfvProvEmail"
                                runat="server"
                                ControlToValidate="txtProvEmail"
                                ErrorMessage="Email is required"
                                ForeColor="Red" />

                            <asp:RegularExpressionValidator
                                ID="revProvEmail"
                                runat="server"
                                ControlToValidate="txtProvEmail"
                                ValidationExpression="\w+([-\+.’]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                ErrorMessage="Invalid email format"
                                ForeColor="Red" />
                        </div>

                        <!-- Provider Contact -->
                        <div class="col-md-6">
                            <label class="form-label">Contact Number</label>
                            <asp:TextBox ID="txtProvContact" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>

                            <asp:RequiredFieldValidator
                                ID="rfvProvContact"
                                runat="server"
                                ControlToValidate="txtProvContact"
                                ErrorMessage="Contact Number is required"
                                ForeColor="Red" />

                            <asp:RegularExpressionValidator
                                ID="revProvContact"
                                runat="server"
                                ControlToValidate="txtProvContact"
                                ValidationExpression="^\d{10}$"
                                ErrorMessage="Enter a valid 10-digit number"
                                ForeColor="Red" />

                        </div>
                    </div>
                </asp:Panel>

                <!-- PASSWORD SECTION -->
                <div class="row g-3 mt-2">

                    <!-- Password -->
                    <div class="col-md-6">
                        <label class="form-label">Password</label>
                        <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>

                        <asp:RequiredFieldValidator
                            ID="rfvPassword"
                            runat="server"
                            ControlToValidate="txtPassword"
                            ErrorMessage="Password is required"
                            ForeColor="Red" />

                    </div>

                    <!-- Confirm Password -->
                    <div class="col-md-6">
                        <label class="form-label">Confirm Password</label>
                        <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>


                       <asp:RequiredFieldValidator
                            ID="rfvConfirmPassword"
                            runat="server"
                            ControlToValidate="txtConfirmPassword"
                            ErrorMessage="Please confirm password"
                            ForeColor="Red" />

                         <asp:CompareValidator
                            ID="cvPassword"
                            runat="server"
                            ControlToValidate="txtConfirmPassword"
                            ControlToCompare="txtPassword"
                            ErrorMessage="Passwords do not match"
                            ForeColor="Red" />

                    </div>
                </div>

                <!-- BUTTONS -->
                <div class="mt-4 d-flex gap-2">
                    <asp:Button ID="btnRegister" runat="server" Text="Create Account" CssClass="btn btn-custom-primary" OnClick="btnRegister_Click" />
                    <asp:Button ID="btnClear" runat="server" Text="Clear Form" CssClass="btn btn-custom-clear" OnClick="btnClear_Click" CausesValidation="false" />

                </div>

                <!-- MESSAGE -->
                <div class="mt-3">
                    <asp:Label ID="lblMessage" runat="server" CssClass="fw-bold"></asp:Label>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
