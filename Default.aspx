<%@ Page Title="Home" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Group_9.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
    /* iOS Inspired Design System */
    body {
        font-family: -apple-system, BlinkMacSystemFont, "Segoe UI", Roboto, Helvetica, Arial, sans-serif;
        background-color: #f5f5f7; /* Classic Apple light gray */
    }
    
    .ios-container {
        margin-top: 4rem;
        margin-bottom: 4rem;
    }

    .ios-hero-card {
        background: #000; /* Dark base for the image to sit on */
        border-radius: 40px; 
        padding: 6rem 2rem;
        box-shadow: 0 20px 50px rgba(0,0,0,0.2);
        position: relative;
        overflow: hidden;
        z-index: 1; 
    }

    /* The Background Image Layer */
    .ios-hero-card::before {
        content: "";
        position: absolute;
        top: 0;
        left: 0;
        width: 100%;
        height: 100%;
        background-image: url('Images/default.png');
        background-size: cover;
        background-position: center;
        /* We use a linear gradient overlay to darken the image so white text stays readable */
        background-image: linear-gradient(rgba(0, 0, 0, 0.6), rgba(0, 0, 0, 0.6)), url('Images/default.png');
        opacity: 0.9; 
        z-index: -1; 
        pointer-events: none; 
    }

    /* Text Protection & Contrast */
    .top-banner {
        font-weight: 700;
        color: rgba(255, 255, 255, 0.9);
        letter-spacing: 1.5px;
        text-transform: uppercase;
        font-size: 0.85rem;
        text-shadow: 0 2px 4px rgba(0,0,0,0.5); /* Added soft shadow */
    }

    .ios-badge {
        display: inline-block;
        background-color: rgba(255, 255, 255, 0.15); 
        backdrop-filter: blur(12px); 
        -webkit-backdrop-filter: blur(12px);
        color: #fff;
        padding: 8px 20px;
        border: 1px solid rgba(255,255,255,0.2);
        border-radius: 30px;
        font-size: 0.8rem;
        font-weight: 600;
        letter-spacing: 2px;
        margin-bottom: 1.5rem;
    }

    .ios-title {
        font-size: 4.5rem;
        font-weight: 800;
        letter-spacing: -2px;
        margin-bottom: 1.5rem;
        color: #ffffff; /* Pure white for maximum contrast */
        text-shadow: 0 4px 15px rgba(0,0,0,0.6); /* Added shadow to make it pop */
    }

    /* Gradient remains vibrant but with better contrast */
    .ios-gradient-text {
        background: linear-gradient(90deg, #60a5fa, #c084fc);
        -webkit-background-clip: text;
        -webkit-text-fill-color: transparent;
        filter: drop-shadow(0 2px 10px rgba(0,0,0,0.4)); /* Glow effect for gradient */
    }

    .ios-subtitle {
        font-size: 1.25rem;
        font-weight: 500; /* Bumped up slightly for readability */
        color: rgba(255, 255, 255, 0.95); /* Brightened slightly */
        line-height: 1.6;
        max-width: 750px;
        margin: 0 auto 3rem auto;
        text-shadow: 0 2px 8px rgba(0,0,0,0.5); /* Added soft shadow */
    }

    /* Buttons */
    .ios-btn-primary {
        background-color: #0071e3; /* Official Apple Blue */
        color: white;
        border-radius: 40px;
        padding: 16px 40px;
        font-size: 1rem;
        font-weight: 600;
        text-decoration: none;
        transition: all 0.3s cubic-bezier(0.4, 0, 0.2, 1);
        border: none;
    }
    
    .ios-btn-primary:hover {
        background-color: #0077ed;
        transform: scale(1.03);
        color: white;
    }

    .ios-btn-secondary {
        background-color: rgba(255, 255, 255, 0.1);
        backdrop-filter: blur(15px);
        -webkit-backdrop-filter: blur(15px);
        color: white;
        border: 1px solid rgba(255, 255, 255, 0.3);
        border-radius: 40px;
        padding: 16px 40px;
        font-size: 1rem;
        font-weight: 600;
        text-decoration: none;
        transition: all 0.3s ease;
    }
    
    .ios-btn-secondary:hover {
        background-color: rgba(255, 255, 255, 0.2);
        transform: scale(1.03);
        color: white;
    }
</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container ios-container">
        <div class="ios-hero-card text-center">
            
            <p class="top-banner mb-4">Bridging success for everyone</p>
            
            <div class="ios-badge">Group 9</div>
            
            <h1 class="ios-title">
                Welcome to <br />
                <span class="ios-gradient-text">EasternDigital.</span>
            </h1>
            
            <p class="ios-subtitle">
                EasternDigital provides a specialized marketplace for students in the Eastern Cape to easily access and book affordable services like tutoring, printing, and graphic design. 
                Our platform empowers local providers by turning informal work into a professional digital portfolio, bridging the gap between talent and opportunity while driving economic growth in our community.
            </p>

            <div class="d-grid gap-3 d-md-flex justify-content-md-center">
                <a href="Register.aspx" class="btn btn-primary btn-lg px-5 ios-btn-primary">Join as a Member</a>
                <a href="BrowseServices.aspx" class="btn btn-outline-primary btn-lg px-5 ios-btn-secondary" style="border: none;">Explore services</a>
            </div>
        </div>
    </div>
</asp:Content>