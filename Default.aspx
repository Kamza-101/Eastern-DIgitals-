<%@ Page Title="Home" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Group_9.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        /* iOS 26 Inspired Design System */
        body {
            /* Forces the browser to use the native Apple system font if available */
            font-family: -apple-system, BlinkMacSystemFont, "Segoe UI", Roboto, Helvetica, Arial, sans-serif;
            background-color: whitesmoke;
        }
        
        .ios-container {
            margin-top: 4rem;
            margin-bottom: 4rem;
        }

        /* The massive, smooth Apple-style card */
        .ios-hero-card {
            background: white;
            border-radius: 40px; 
            padding: 6rem 2rem;
            box-shadow: 0 20px 50px gainsboro;
            border: 1px solid white;
        }

        /* Sleek top text */
        .top-banner {
            font-weight: 700;
            color: darkgray;
            letter-spacing: 1px;
            text-transform: uppercase;
            font-size: 0.9rem;
        }

        /* The modern pill badge */
        .ios-badge {
            display: inline-block;
            background-color: whitesmoke;
            color: dimgray;
            padding: 8px 20px;
            border-radius: 30px;
            font-size: 0.9rem;
            font-weight: bold;
            text-transform: uppercase;
            letter-spacing: 2px;
            margin-bottom: 1.5rem;
        }

        /* Massive, tight headline */
        .ios-title {
            font-size: 4.5rem;
            font-weight: 900;
            letter-spacing: -2px;
            margin-bottom: 1.5rem;
        }

        /* The classic Apple color-shifting text effect */
        .ios-gradient-text {
            background: linear-gradient(90deg, royalblue, mediumorchid);
            -webkit-background-clip: text;
            -webkit-text-fill-color: transparent;
        }

        /* Refined, highly readable paragraph text */
        .ios-subtitle {
            font-size: 1.3rem;
            font-weight: 500;
            color: dimgray;
            line-height: 1.6;
            max-width: 800px;
            margin: 0 auto 3rem auto;
        }

        /* Pill-shaped primary button */
        .ios-btn-primary {
            background-color: royalblue;
            color: white;
            border-radius: 40px;
            padding: 15px 40px;
            font-size: 1.1rem;
            font-weight: bold;
            text-decoration: none;
            transition: transform 0.2s ease, box-shadow 0.2s ease;
        }
        .ios-btn-primary:hover {
            transform: scale(1.02);
            box-shadow: 0 10px 20px lightsteelblue;
            color: white;
        }

        /* Pill-shaped secondary button */
        .ios-btn-secondary {
            background-color: whitesmoke;
            color: black;
            border-radius: 40px;
            padding: 15px 40px;
            font-size: 1.1rem;
            font-weight: bold;
            text-decoration: none;
            transition: background-color 0.2s ease;
        }
        .ios-btn-secondary:hover {
            background-color: gainsboro;
            color: black;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container ios-container">
        <div class="ios-hero-card text-center">
            
            <p class="top-banner mb-4">Bridging success for everyone</p>
            
            <div class="ios-badge">Group 9</div>
            
            <h1 class="ios-title text-dark">
                Welcome to <br />
                <span class="ios-gradient-text">EasternDigital.</span>
            </h1>
            
            <p class="ios-subtitle">
                EasternDigital provides a specialized marketplace for students in the Eastern Cape to easily access and book affordable services like tutoring, printing, and graphic design. 
                Our platform empowers local providers by turning informal work into a professional digital portfolio, bridging the gap between talent and opportunity while driving economic growth in our community.
            </p>

                    <div class="d-grid gap-3 d-md-flex justify-content-md-center">
                        <a href="Register.aspx" class="btn btn-primary btn-lg px-5">Join as a Member</a>
                        <a href="Services.aspx" class="btn btn-outline-primary btn-lg px-5">Explore services</a>
                    </div>
                </div>
            </div>
            
        </div>
    </div>
</asp:Content>