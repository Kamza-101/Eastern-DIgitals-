using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace Group_9
{
    public partial class ProviderDashboard : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["EasternDigitalDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null || Session["UserRole"] == null || Session["UserRole"].ToString() != "Provider")
            {
                Response.Redirect("Login.aspx", false);
                return;
            }

            if (!IsPostBack)
            {
                if (Request.QueryString["upgrade"] == "success")
                {
                    lblUpgradeSuccess.Text = "Payment successful! Welcome to EasternDigital Premium.";
                    lblUpgradeSuccess.Visible = true;
                }

                CheckPremiumStatus();
                LoadDashboardMetrics();
                BindProviderBookings();
                BindEarningsHistory();
            }
        }

        private void CheckPremiumStatus()
        {
            int userId = Convert.ToInt32(Session["UserID"]);

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    string sql = "SELECT IsPremium FROM ServiceProviders WHERE UserID = @UID";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@UID", userId);

                    conn.Open();
                    object result = cmd.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        bool isPremium = Convert.ToBoolean(result);

                        if (isPremium)
                        {
                            pnlBasicStatus.Visible = false;
                            pnlPremiumStatus.Visible = true;
                        }
                        else
                        {
                            pnlBasicStatus.Visible = true;
                            pnlPremiumStatus.Visible = false;
                        }
                    }
                }
                catch (SqlException ex)
                {
                    System.Diagnostics.Debug.WriteLine("Premium Check Error: " + ex.Message);
                }
            }
        }

        private void LoadDashboardMetrics()
        {
            int userId = Convert.ToInt32(Session["UserID"]);

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();

                    // FIXED: Added nested ISNULL to protect against NULL math crashes
                    string qRevenue = @"SELECT ISNULL(SUM(ISNULL(b.TotalCost, 0)), 0) 
                                        FROM Bookings b 
                                        INNER JOIN Services s ON b.ServiceID = s.ServiceID 
                                        INNER JOIN ServiceProviders p ON s.ProviderID = p.ProviderID
                                        WHERE p.UserID = @UID AND b.Status = 'Completed'";

                    SqlCommand cmdRev = new SqlCommand(qRevenue, conn);
                    cmdRev.Parameters.AddWithValue("@UID", userId);

                    decimal totalRev = Convert.ToDecimal(cmdRev.ExecuteScalar());
                    lblTotalRevenue.Text = "R " + totalRev.ToString("F2");

                    // Calculate Goal Progress
                    decimal goalTarget = 5000m;
                    decimal percentage = totalRev > 0 ? (totalRev / goalTarget) * 100 : 0m;
                    decimal visualPercentage = percentage > 100 ? 100 : percentage;

                    lblCurrentRevTracker.Text = totalRev.ToString("F2");
                    lblGoalPercentage.Text = Math.Round(percentage, 0).ToString();
                    pnlProgressBar.Style.Add("width", visualPercentage.ToString("0") + "%");

                    // 2. Active Bookings
                    string qActive = @"SELECT COUNT(*) 
                                       FROM Bookings b 
                                       INNER JOIN Services s ON b.ServiceID = s.ServiceID 
                                       INNER JOIN ServiceProviders p ON s.ProviderID = p.ProviderID
                                       WHERE p.UserID = @UID AND b.Status IN ('Pending Confirmation', 'Approved')";

                    SqlCommand cmdAct = new SqlCommand(qActive, conn);
                    cmdAct.Parameters.AddWithValue("@UID", userId);
                    lblActiveBookings.Text = cmdAct.ExecuteScalar().ToString();

                    // 3. Completed Jobs
                    string qComp = @"SELECT COUNT(*) 
                                     FROM Bookings b 
                                     INNER JOIN Services s ON b.ServiceID = s.ServiceID 
                                     INNER JOIN ServiceProviders p ON s.ProviderID = p.ProviderID
                                     WHERE p.UserID = @UID AND b.Status = 'Completed'";

                    SqlCommand cmdComp = new SqlCommand(qComp, conn);
                    cmdComp.Parameters.AddWithValue("@UID", userId);
                    lblCompletedJobs.Text = cmdComp.ExecuteScalar().ToString();
                }
                catch (SqlException ex)
                {
                    System.Diagnostics.Debug.WriteLine("Dashboard Metrics Error: " + ex.Message);
                }
            }
        }

        private void BindProviderBookings()
        {
            int userId = Convert.ToInt32(Session["UserID"]);

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    string query = @"
                        SELECT 
                            b.BookingID, 
                            ISNULL(b.OrderReference, 'N/A') AS OrderReference, 
                            ISNULL(b.Status, 'Pending') AS Status, 
                            b.TotalCost,
                            ISNULL(s.ServiceName, 'Unknown Service') AS ServiceName, 
                            ISNULL(u.Email, 'Guest User') AS StudentName, 
                            FORMAT(b.BookingDate, 'MMM dd, yyyy') AS FormattedDate
                        FROM Bookings b
                        INNER JOIN Services s ON b.ServiceID = s.ServiceID
                        INNER JOIN ServiceProviders p ON s.ProviderID = p.ProviderID
                        LEFT JOIN Users u ON b.UserID = u.UserID
                        WHERE p.UserID = @UID
                        ORDER BY b.BookingDate DESC";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@UID", userId);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();

                    conn.Open();
                    da.Fill(dt);

                    lblError.Visible = false;
                    rptProviderBookings.DataSource = dt;
                    rptProviderBookings.DataBind();
                }
                catch (SqlException ex)
                {
                    lblError.Text = "SQL Crash: " + ex.Message;
                    lblError.Visible = true;
                }
            }
        }

        private void BindEarningsHistory()
        {
            int userId = Convert.ToInt32(Session["UserID"]);

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    // FIXED: Added ISNULL(b.TotalCost, 0) to prevent crash on empty price
                    string sql = @"
                        SELECT 
                            ISNULL(b.TotalCost, 0) AS TotalCost, 
                            ISNULL(s.ServiceName, 'Unknown Service') AS ServiceName, 
                            FORMAT(b.BookingDate, 'MMM dd, yyyy') AS FormattedDate
                        FROM Bookings b
                        INNER JOIN Services s ON b.ServiceID = s.ServiceID
                        INNER JOIN ServiceProviders p ON s.ProviderID = p.ProviderID
                        WHERE p.UserID = @UID AND b.Status = 'Completed'
                        ORDER BY b.BookingDate DESC";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@UID", userId);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();

                    conn.Open();
                    da.Fill(dt);

                    rptEarnings.DataSource = dt;
                    rptEarnings.DataBind();
                }
                catch (SqlException ex)
                {
                    System.Diagnostics.Debug.WriteLine("Earnings SQL Error: " + ex.Message);
                }
            }
        }

        protected void rptProviderBookings_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string bookingId = e.CommandArgument.ToString();
            string newStatus = "";

            if (e.CommandName == "Approve")
            {
                newStatus = "Approved";
            }
            else if (e.CommandName == "Reject")
            {
                newStatus = "Rejected";
            }
            else if (e.CommandName == "Complete")
            {
                newStatus = "Completed";
            }

            if (!string.IsNullOrEmpty(newStatus))
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    try
                    {
                        string sql = "UPDATE Bookings SET Status = @Status WHERE BookingID = @BID";
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@Status", newStatus);
                        cmd.Parameters.AddWithValue("@BID", bookingId);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        lblError.Text = "Status Update Error: " + ex.Message;
                        lblError.Visible = true;
                    }
                }

                // Refresh the whole UI
                LoadDashboardMetrics();
                BindProviderBookings();
                BindEarningsHistory();
            }
        }
    }
}





