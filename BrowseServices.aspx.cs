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
                
            }

        }

        protected void btnSearch_Click(object sender, EventArgs e)
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
            // Send the user to a page specifically for tutors
            Response.Redirect("Providers.aspx?category=Tutoring");
        }

        protected void btnViewPrinting_Click(object sender, EventArgs e)
        {
            Response.Redirect("Providers.aspx?category=Printing");
        }


    }
}