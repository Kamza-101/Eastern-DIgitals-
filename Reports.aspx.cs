using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace Group_9
{
   
        public partial class Reports : System.Web.UI.Page
        {
            string connStr = ConfigurationManager.ConnectionStrings["EasternDigitalDB"].ConnectionString;

            protected void Page_Load(object sender, EventArgs e)
            {
                // Security: Only Admins should access reports
                if (!IsPostBack)
                {
                    if (Session["UserRole"]?.ToString() != "Admin")
                    {
                        Response.Redirect("Login.aspx");
                    }
                }
            }

            protected void btnGenerate_Click(object sender, EventArgs e)
            {
                string reportType = ddlReportType.SelectedValue;
                string query = "";

                // Determine query based on dropdown selection
                if (reportType == "Sales")
                {
                    // Aggregates revenue and counts per service
                    query = @"SELECT ServiceName, 
                                 SUM(Price) as TotalRevenue, 
                                 COUNT(BookingID) as BookingCount 
                          FROM Bookings 
                          WHERE Status = 'Completed' 
                          GROUP BY ServiceName";
                }
                else if (reportType == "Users")
                {
                    // Groups users by role
                    query = "SELECT UserRole, COUNT(*) as TotalCount FROM Users GROUP BY UserRole";
                }

                GenerateReport(query);
            }

            private void GenerateReport(string query)
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    try
                    {
                        SqlCommand cmd = new SqlCommand(query, conn);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();

                        conn.Open();
                        da.Fill(dt); // Populate DataTable

                        // Bind to GridView
                        gvReport.DataSource = dt;
                        gvReport.DataBind();
                    }
                    catch (SqlException ex)
                    {
                        // Basic error handling for report generation
                        Response.Write("<script>alert('Error generating report: " + ex.Message + "');</script>");
                    }
                }
            }
        }
   
}