using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace Group_9
{
    public partial class ViewCart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCartData();
            }
        }

        private void LoadCartData()
        {
            // TODO: Replace this with your actual database call (e.g., SELECT * FROM Cart WHERE UserID = ...)
            DataTable dtCart = GetDummyCartData();

            if (dtCart.Rows.Count > 0)
            {
                // Show items, hide empty message
                pnlCartItems.Visible = true;
                pnlEmptyCart.Visible = false;
                btnProceedCheckout.Enabled = true;

                // Bind data to the repeater
                rptCartItems.DataSource = dtCart;
                rptCartItems.DataBind();

                // Calculate Totals
                lblTotalItems.Text = dtCart.Rows.Count.ToString();

                // Example calculation (Assuming price is a string like "R 150.00" for display, 
                // in reality, you should calculate from decimal column types in your DB)
                lblTotalPrice.Text = "R 400.00"; // Hardcoded for this dummy example
            }
            else
            {
                // Hide items, show empty message
                pnlCartItems.Visible = false;
                pnlEmptyCart.Visible = true;
                btnProceedCheckout.Enabled = false;
                lblTotalItems.Text = "0";
                lblTotalPrice.Text = "R 0.00";
            }
        }

        // Handles the "Remove Item" button click inside the Repeater
        protected void rptCartItems_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Remove")
            {
                string cartIdToRemove = e.CommandArgument.ToString();

                // TODO: Add SQL DELETE logic here using the cartIdToRemove

                lblCartMessage.Text = "Service removed from your cart.";

                // Reload the cart to reflect changes
                LoadCartData();
            }
        }

        // Handles the Proceed to Checkout button
        protected void btnProceedCheckout_Click(object sender, EventArgs e)
        {
            // Redirect to the checkout/booking confirmation page
            Response.Redirect("Checkout.aspx");
        }

        // --- Helper Method for UI Testing ---
        private DataTable GetDummyCartData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("CartID");
            dt.Columns.Add("Icon");
            dt.Columns.Add("ServiceName");
            dt.Columns.Add("ProviderName");
            dt.Columns.Add("ServiceCategory");
            dt.Columns.Add("Price");

            // Adding a few fake items so you can see how the design looks
            dt.Rows.Add("1", "📚", "Java Programming Tutor", "James Motsamai", "Tutoring", "R 150/hr");
            dt.Rows.Add("2", "🎨", "Custom Logo Design", "Sarah Jenkins", "Graphic Design", "R 250 flat");

            return dt;
        }
    }
}