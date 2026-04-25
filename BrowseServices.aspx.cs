using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Group_9
{
    public partial class BrowseServices : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Logic to load services
            }
        }

        // --- ADDED THIS METHOD ---
       
        protected void btnViewTutoring_Click(object sender, EventArgs e)
        {
            // ... existing code ...
        }

        protected void btnViewPrinting_Click(object sender, EventArgs e)
        {
            Response.Redirect("ServiceDetails.aspx?category=Printing");
        }

        protected void btnViewDesign_Click(object sender, EventArgs e)
        {
            Response.Redirect("ServiceDetails.aspx?category=Graphic Design");
        }

        protected void btnViewRepair_Click(object sender, EventArgs e)
        {
            Response.Redirect("ServiceDetails.aspx?category=Device Repair");
        }
    }
}