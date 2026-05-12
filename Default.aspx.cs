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
                // STATE MANAGEMENT CHECK: Are they logged in?
                if (Session["UserID"] != null)
                {
                    // The user is logged in. Their "Home" is now the Service Catalogue.
                    Response.Redirect("BrowseServices.aspx");
                }

                // If Session["UserID"] is null, the code ignores the IF statement 
                // and loads the normal Default.aspx page so guests can see the landing page and log in.
        }
    }
}