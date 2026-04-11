using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Group_9
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // This runs when the page first loads. 
            // You can leave it empty for now.
        }

        // Paste the logic right here, below Page_Load but before the closing braces
        protected void rblUserType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblUserType.SelectedValue == "Seeker")
            {
                pnlSeeker.Visible = true;
                pnlProvider.Visible = false;
            }
            else
            {
                pnlSeeker.Visible = false;
                pnlProvider.Visible = true;
            }
        }
    }
}