using System;

namespace YourProject
{
    public partial class ServiceDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string serviceId = Request.QueryString["id"];

                if (!string.IsNullOrEmpty(serviceId))
                {
                    LoadServiceDetails(serviceId);
                }
                else
                {
                    // Update this line as well
                    Response.Redirect("BrowseServices.aspx");
                }
            }
        }

        private void LoadServiceDetails(string id)
        {
            // For now, using logic to simulate fetching from a database
            if (id == "101")
            {
                lblServiceName.Text = "Programming Tutoring";
                lblDescription.Text = "One-on-one sessions for C#, Java, and SQL. Ideal for IT students preparing for exams.";
                lblPrice.Text = "R 150.00 / hour";
                lblProviderName.Text = "James Motsamai";
            }
            else if (id == "102")
            {
                lblServiceName.Text = "Purified Water Delivery";
                lblDescription.Text = "Fresh, purified water delivered directly to your doorstep. Subscriptions available.";
                lblPrice.Text = "R 20.00 / 5 Liters";
                lblProviderName.Text = "AquaFlow EC";
            }
            // In a real project, you would run a SQL query: 
            // "SELECT * FROM Services WHERE ServiceID = @id"
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            // Change this from "ServicesList.aspx" to "BrowseServices.aspx"
            Response.Redirect("BrowseServices.aspx");
        }
    }
}