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
                phGuestNav.Visible = true;
                phMemberNav.Visible = false;
                phProviderNav.Visible = false;
                phLogoutNav.Visible = false;
            }
            else
            {
                phGuestNav.Visible = false;
                phLogoutNav.Visible = true;

                string role = Session["UserRole"].ToString();

                if (role == "Provider")
                {
                    phMemberNav.Visible = false;
                    phProviderNav.Visible = true;
                }
                else if (role == "Seeker")
                {
                    phProviderNav.Visible = false;
                    phMemberNav.Visible = true;
                }
                else if (role == "Admin")
                {
                    phMemberNav.Visible = false;
                    phProviderNav.Visible = false;
                }
            }
        } // <-- Make sure Page_Load closes here!

        // ADD THIS METHOD BACK IN:
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