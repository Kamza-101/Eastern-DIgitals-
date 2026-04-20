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
                // Logic to load services from the database can go here in the future
            }
        }

        protected void btnViewTutoring_Click(object sender, EventArgs e)
        {
            // 1. Capture what the user is searching for
            string searchQuery = txtSearch.Text.Trim();
            string selectedCategory = ddlFilterCategory.SelectedValue;

            // 2. (Future Step) Here is where you will write a SQL query like:
            // "SELECT * FROM Services WHERE Category = '" + selectedCategory + "'"

            // 3. For now, let's just test that the button works by changing the search bar text
            if (!string.IsNullOrEmpty(selectedCategory))
            {
                txtSearch.Text = "Searching for: " + selectedCategory;
            }
        }

        protected void btnViewTutoring_Click(object sender, EventArgs e)
        {
            // Send the user to the details page specifically for tutors
            Response.Redirect("ServiceDetails.aspx?category=Tutoring");
        }

        protected void btnViewPrinting_Click(object sender, EventArgs e)
        {
            // Send the user to the details page specifically for printing
            Response.Redirect("ServiceDetails.aspx?category=Printing");
        }

        protected void btnViewDesign_Click(object sender, EventArgs e)
        {
            // Send the user to the details page specifically for graphic design
            Response.Redirect("ServiceDetails.aspx?category=Graphic Design");
        }

        protected void btnViewRepair_Click(object sender, EventArgs e)
        {
            // Send the user to the details page specifically for device repair
            Response.Redirect("ServiceDetails.aspx?category=Device Repair");
        }
    }
}