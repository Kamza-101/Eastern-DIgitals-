using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Group_9
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                // User is not logged in (Guest)
                phGuestNav.Visible = true;
                phLogoutNav.Visible = false;

                // Hide all role-specific navigation
                phMemberNav.Visible = false;
                phProviderNav.Visible = false;
                phAdminNav.Visible = false;
            }
            else
            {
                // User is logged in
                phGuestNav.Visible = false;
                phLogoutNav.Visible = true;

                string role = Session["UserRole"].ToString();

                if (role == "Provider")
                {
                    phMemberNav.Visible = false;
                    phProviderNav.Visible = true;
                    phAdminNav.Visible = false;
                }
                else if (role == "Seeker")
                {
                    phProviderNav.Visible = false;
                    phMemberNav.Visible = true;
                    phAdminNav.Visible = false;
                }
                else if (role == "Admin")
                {
                    phMemberNav.Visible = false;
                    phProviderNav.Visible = false;
                    phAdminNav.Visible = true; // This makes the Reports & Admin Dashboard visible!
                }
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            // Clear all data stored in the server memory 
            Session.Clear();

            // Completely destroy the current session to ensure security 
            Session.Abandon();

            // Redirect the user back to the public Home page
            Response.Redirect("Default.aspx");
        }
    }
}