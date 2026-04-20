using System;
using System.Data;
using System.Web.UI.WebControls;

namespace Group_9
{

    public partial class ServiceDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Grab the category from the URL (e.g., ?category=Tutoring)
                string category = Request.QueryString["category"];

                if (!string.IsNullOrEmpty(category))
                {
                    lblCategoryTitle.Text = $"Available Providers: {category}";
                    LoadProvidersByCategory(category);
                }
                else
                {
                    lblCategoryTitle.Text = "All Available Providers";
                    // Fallback to load everything or show an error
                    LoadProvidersByCategory("All");
                }
            }
        }

        private void LoadProvidersByCategory(string category)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ProviderID");
            dt.Columns.Add("Initials");
            dt.Columns.Add("ProviderName");
            dt.Columns.Add("ServiceDesc");
            dt.Columns.Add("Location");
            dt.Columns.Add("Rating");
            dt.Columns.Add("Price");

            // Filter dummy data based on the requested category
            switch (category.ToLower())
            {
                case "tutoring":
                    dt.Rows.Add("T1", "JM", "James Motsamai", "Java & C# Programming", "Alice", "4.8", "R 150/hr");
                    dt.Rows.Add("T2", "SJ", "Sarah Jenkins", "Business Mathematics", "Port Elizabeth", "4.9", "R 120/hr");
                    dt.Rows.Add("T3", "SN", "Sipho Ndlovu", "Information Systems Theory", "East London", "4.5", "R 100/hr");
                    break;

                case "printing":
                    dt.Rows.Add("P1", "PH", "PrintHub Campus", "Color & B&W, Binding", "Mthatha", "4.7", "R 1/page");
                    dt.Rows.Add("P2", "ED", "Express Docs", "Bulk Printing & Scanning", "Butterworth", "4.6", "R 0.80/page");
                    dt.Rows.Add("P3", "LP", "Lihle's Printers", "Poster & Assignment Printing", "Alice", "4.9", "R 1.20/page");
                    break;

                case "graphic design":
                    dt.Rows.Add("G1", "CJ", "Creative J", "Custom Logos & Branding", "Grahamstown", "5.0", "R 250 flat");
                    dt.Rows.Add("G2", "VT", "Visuals by Thabo", "Event Flyers & Posters", "Bisho", "4.8", "R 150/design");
                    dt.Rows.Add("G3", "DA", "Digital Arts PE", "Digital Portfolio Design", "Port Elizabeth", "4.9", "R 400 flat");
                    break;

                case "device repair":
                    dt.Rows.Add("R1", "TF", "TechFix East London", "Screen Replacements", "East London", "4.8", "From R 500");
                    dt.Rows.Add("R2", "PM", "PC Medics", "Software Troubleshooting", "Mthatha", "4.7", "From R 200");
                    dt.Rows.Add("R3", "FG", "FixIt Guys", "Hardware & Battery Fixes", "Alice", "4.6", "From R 350");
                    break;

                default:
                    // If no valid category is matched
                    break;
            }

            if (dt.Rows.Count > 0)
            {
                rptProviders.DataSource = dt;
                rptProviders.DataBind();
                lblNoProviders.Visible = false;
            }
            else
            {
                rptProviders.DataSource = null;
                rptProviders.DataBind();
                lblNoProviders.Visible = true;
            }
        }

        // Handles the "Book Service" button click
        protected void rptProviders_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Book")
            {
                string providerId = e.CommandArgument.ToString();

                // TODO: Add Logic to add this specific provider's service to the cart
                // For now, redirect to the cart to simulate the flow
                Response.Redirect("ViewCart.aspx");
            }
        }
    }
}