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
            string searchQuery = txtSearch.Text;
            string selectedCategory = ddlFilterCategory.SelectedValue;

        }
    }
}