using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Group_9
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            {
                // SECURITY BOUNCER: If an Admin or Provider tries to access this page, bounce them back!
                if (Session["UserRole"] != null)
                {
                    string role = Session["UserRole"].ToString();

                    if (role == "Admin")
                    {
                        Response.Redirect("AdminDashboard.aspx", false);
                        return; // Stop running the rest of the page code
                    }
                    else if (role == "Provider")
                    {
                        Response.Redirect("ProviderDashboard.aspx", false);
                        return; // Stop running the rest of the page code
                    }
                }

                if (!IsPostBack)
                {
                    // Your existing code to load the services goes here...
                }
            }
        }
    }
}