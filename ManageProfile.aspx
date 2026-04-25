<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageProfile.aspx.cs" Inherits="Group_9.ManageProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<style>
        .profile-container { max-width: 700px; margin: 40px auto; }
        .ios-card { background: #ffffff; border-radius: 20px; padding: 30px; box-shadow: 0 4px 12px rgba(0,0,0,0.05); }
        .form-label { font-weight: 600; color: #8e8e93; text-transform: uppercase; font-size: 0.7rem; letter-spacing: 1px; }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="profile-container">
        <h2 class="fw-bold mb-4">Edit Profile</h2>
        
        <div class="ios-card">
            <div class="mb-3">
                <label class="form-label">Professional Bio</label>
                <asp:TextBox ID="txtBio" runat="server" CssClass="form-control ios-input" TextMode="MultiLine" Rows="4" placeholder="Describe your expertise..."></asp:TextBox>
            </div>
            
            <div class="mb-4">
                <label class="form-label">Skills (comma separated)</label>
                <asp:TextBox ID="txtSkills" runat="server" CssClass="form-control ios-input" placeholder="e.g., Java, Web Design, Tutoring"></asp:TextBox>
            </div>

            <asp:Button ID="btnSave" runat="server" Text="Save Changes" CssClass="btn btn-primary w-100 rounded-pill py-2" OnClick="btnSave_Click" />
            <asp:Label ID="lblStatus" runat="server" CssClass="mt-3 d-block text-center fw-bold"></asp:Label>
        </div>
    </div>
</asp:Content>
