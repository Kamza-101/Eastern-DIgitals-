using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Group_9
{
    public partial class BrowseServices : System.Web.UI.Page
    {
        // 1. Connection string defined at the class level
        string connStr = ConfigurationManager.ConnectionStrings["EasternDigitalDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            // SECURITY BOUNCER: If an Admin or Provider tries to access this page, bounce them back!
            if (Session["UserRole"] != null)
            {
                string role = Session["UserRole"].ToString();

                if (role == "Admin")
                {
                    Response.Redirect("AdminDashboard.aspx", false);
                    return; // Stop running the rest of the page code
                }
                else if (role == "Provider")
                {
                    Response.Redirect("ProviderDashboard.aspx", false);
                    return; // Stop running the rest of the page code
                }
            }

            // THE FIX: Automatically load the catalogue on the first visit
            if (!IsPostBack)
            {
                BindServices();
            }
        }

        // 2. Event: Triggers when the user types text and clicks the blue "Filter" button
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindServices();
        }

        // 3. Event: Triggers instantly when the user changes the dropdown option
        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindServices();
        }

        // 4. The Main ADO.NET Logic Method
        private void BindServices()
        {
            string category = ddlCategory.SelectedValue;
            string searchTerm = txtSearch.Text.Trim();

            // The 'using' block automatically manages the database connection 
            // and ensures it closes properly, even if an error occurs.
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    // WHERE 1=1 is a trick that makes appending "AND" statements easier
                    string query = "SELECT ServiceID, ServiceName, Description, Price, Icon, Tag FROM Services WHERE 1=1";

                    // Append category filter if it's not "All"
                    if (category != "All")
                    {
                        query += " AND Category = @Cat";
                    }

                    // Append text search filter if the user typed something
                    if (!string.IsNullOrEmpty(searchTerm))
                    {
                        query += " AND (ServiceName LIKE @Search OR Description LIKE @Search)";
                    }

                    SqlCommand cmd = new SqlCommand(query, conn);

                    // Safely pass the parameters to prevent SQL Injection
                    if (category != "All")
                    {
                        cmd.Parameters.AddWithValue("@Cat", category);
                    }

                    if (!string.IsNullOrEmpty(searchTerm))
                    {
                        cmd.Parameters.AddWithValue("@Search", "%" + searchTerm + "%");
                    }

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();

                    conn.Open();
                    da.Fill(dt); // Executes the query and fills the data table

                    // Bind the data to your front-end Repeater
                    rptServices.DataSource = dt;
                    rptServices.DataBind();
                }
                catch (SqlException)
                {
                    // LECTURE 8 COMPLIANCE: Do not expose raw database errors to the UI.
                    // We throw this exception up to Global.asax so it can be logged in the AuditLogs securely!
                    throw;
                }
                catch (Exception)
                {
                    // LECTURE 8 COMPLIANCE: Catch general logic crashes and pass them to Global.asax
                    throw;
                }
            }
        }

        // 5. Handles the click of the "View Providers" button inside the Repeater
        protected void rptServices_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "View")
            {
                string serviceId = e.CommandArgument.ToString();

                // Security Check
                if (Session["UserID"] == null)
                {
                    Response.Redirect("Login.aspx", false);
                }
                else
                {
                    // Redirects to ServiceDetails and passes the ID in the URL
                    Response.Redirect("ServiceDetails.aspx?ServiceID=" + serviceId, false);
                }
            }
        }
    }
}