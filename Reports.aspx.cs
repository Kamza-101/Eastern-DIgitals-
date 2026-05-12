using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace Group_9
{
    public partial class Reports : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["EasternDigitalDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Security Check: Only Admins can view reports
            if (Session["UserID"] == null || Session["UserRole"] == null || Session["UserRole"].ToString() != "Admin")
            {
                Response.Redirect("Login.aspx");
            }

            if (!IsPostBack)
            {
                // Load the default report (All Bookings) when the page first opens
                LoadReportData();
            }
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            // Reload the report based on the new dropdown filters
            LoadReportData();
        }

        private void LoadReportData()
        {
            string selectedStatus = ddlStatus.SelectedValue;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    // Advanced SQL Query: Joining Bookings, Services, and Users twice to get both emails
                    string sql = @"
                        SELECT 
                            b.BookingID, 
                            ISNULL(b.OrderReference, 'N/A') AS OrderReference, 
                            FORMAT(b.BookingDate, 'MMM dd, yyyy') AS FormattedDate, 
                            ISNULL(s.ServiceName, 'Unknown Service') AS ServiceName, 
                            ISNULL(seeker.Email, 'Guest User') AS SeekerEmail, 
                            ISNULL(provUser.Email, 'Unknown Provider') AS ProviderEmail, 
                            ISNULL(b.TotalCost, 0) AS TotalCost,
                            ISNULL(b.Status, 'Pending') AS Status
                        FROM Bookings b
                        INNER JOIN Services s ON b.ServiceID = s.ServiceID
                        INNER JOIN ServiceProviders p ON s.ProviderID = p.ProviderID
                        LEFT JOIN Users seeker ON b.UserID = seeker.UserID
                        LEFT JOIN Users provUser ON p.UserID = provUser.UserID
                        WHERE (@Status = 'All' OR b.Status = @Status)
                        ORDER BY b.BookingDate DESC";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Status", selectedStatus);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();

                    conn.Open();
                    da.Fill(dt);

                    // 1. Update the Summary Labels
                    lblRecordCount.Text = dt.Rows.Count.ToString();

                    decimal totalValue = 0;
                    if (dt.Rows.Count > 0)
                    {
                        // Calculate the sum of the TotalCost column directly from the DataTable
                        totalValue = Convert.ToDecimal(dt.Compute("SUM(TotalCost)", string.Empty));
                    }
                    lblTotalValue.Text = "R " + totalValue.ToString("0.00");

                    // ---------------------------------------------------------
                    // NEW: Calculate data for the GDI+ Bar Chart
                    // ---------------------------------------------------------
                    int completed = 0, pending = 0, approved = 0, rejected = 0;

                    foreach (DataRow row in dt.Rows)
                    {
                        string status = row["Status"].ToString();
                        if (status == "Completed") completed++;
                        else if (status == "Pending Confirmation") pending++;
                        else if (status == "Approved") approved++;
                        else if (status == "Rejected") rejected++;
                    }

                    // Build the query strings
                    string labelsStr = "Completed,Pending,Approved,Rejected";
                    string valuesStr = $"{completed},{pending},{approved},{rejected}";

                    // Point the Image control to the GDI+ generator page and UrlEncode the values
                    imgChart.ImageUrl = "~/ChartGenerator.aspx?"
                                      + "labels=" + Server.UrlEncode(labelsStr)
                                      + "&values=" + Server.UrlEncode(valuesStr)
                                      + "&t=" + DateTime.Now.Ticks; // Force cache refresh so it updates instantly
                    // ---------------------------------------------------------

                    // 2. Bind the Data to the UI Table
                    if (dt.Rows.Count > 0)
                    {
                        rptData.DataSource = dt;
                        rptData.DataBind();

                        rptData.Visible = true;
                        lblNoData.Visible = false;
                        lblError.Visible = false;
                    }
                    else
                    {
                        rptData.Visible = false;
                        lblNoData.Visible = true;
                    }
                }
                catch (SqlException ex)
                {
                    lblError.Text = "Report Generation Error: " + ex.Message;
                    lblError.Visible = true;
                }
            }
        }
    }
}