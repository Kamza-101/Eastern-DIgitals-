using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Group_9
{
    public partial class Checkout : System.Web.UI.Page

    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // In a real app, you would pull these from the Session or Database
                lblTotalItems.Text = "2";
                lblTotalPrice.Text = "R 400.00";
            }
        }

        protected void btnCompleteOrder_Click(object sender, EventArgs e)
        {
            // 1. Validate that the user filled in their details
            if (string.IsNullOrWhiteSpace(txtFullName.Text) || string.IsNullOrWhiteSpace(txtLocation.Text))
            {
                lblCheckoutError.Text = "Please fill in both your Full Name and Location.";
                return;
            }

            // 2. Clear any previous errors
            lblCheckoutError.Text = "";

            // 3. Generate a random reference number
            Random rnd = new Random();
            lblReferenceNumber.Text = "#ED-" + rnd.Next(10000, 99999).ToString();

            // 4. Hide the checkout form and show the success screen!
            pnlCheckoutForm.Visible = false;
            pnlSuccess.Visible = true;
        }
    }
}
        
    