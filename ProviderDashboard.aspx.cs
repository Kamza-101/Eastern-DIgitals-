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

                // Call the safe method to load the top-right profile widget
                LoadProviderProfile();

                CheckPremiumStatus();
                LoadDashboardMetrics();
                BindProviderBookings();
                BindEarningsHistory();
            }
        }

        // BULLETPROOF PROFILE LOAD: Pulls data safely without crashing on missing columns
        private void LoadProviderProfile()
        {
            int userId = Convert.ToInt32(Session["UserID"]);

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    // We select everything (*) from Users and ServiceProviders to ensure we grab the right columns
                    string query = @"
                        SELECT 
                            u.*, 
                            sp.*,
                            (SELECT TOP 1 ServiceName FROM Services WHERE ProviderID = sp.ProviderID) AS ServiceType
                        FROM Users u
                        INNER JOIN ServiceProviders sp ON u.UserID = sp.UserID
                        WHERE u.UserID = @UID";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@UID", userId);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Safely extract values (Checks for both FullName AND FirstName/LastName just in case)
                            string fullName = SafeGetString(reader, "FullName");
                            if (string.IsNullOrWhiteSpace(fullName))
                            {
                                fullName = SafeGetString(reader, "FirstName") + " " + SafeGetString(reader, "LastName");
                            }

                            // Safely check for Phone or PhoneNumber
                            string contact = SafeGetString(reader, "PhoneNumber");
                            if (string.IsNullOrWhiteSpace(contact)) contact = SafeGetString(reader, "Phone");

                            string serviceType = SafeGetString(reader, "ServiceType");
                            string location = SafeGetString(reader, "City");

                            // 1. Bind Name
                            lblProviderName.Text = string.IsNullOrWhiteSpace(fullName.Trim()) ? "Service Provider" : fullName.Trim();

                            // 2. Set the initial circle (first letter of name)
                            if (!string.IsNullOrWhiteSpace(fullName.Trim()))
                            {
                                lblProviderInitial.Text = fullName.Trim().Substring(0, 1).ToUpper();
                            }
                            else
                            {
                                lblProviderInitial.Text = "P";
                            }

                            // 3. Fallbacks for missing info
                            lblServiceType.Text = string.IsNullOrWhiteSpace(serviceType) ? "Registered Provider" : serviceType;
                            
                            
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Profile Load Error: " + ex.Message);
                    lblProviderName.Text = "Profile Loading...";
                    lblProviderInitial.Text = "!";
                }
            }
        }

        // HELPER METHOD: This prevents "Column does not exist" crashes from breaking the card
        private string SafeGetString(SqlDataReader reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i).Equals(columnName, StringComparison.OrdinalIgnoreCase))
                {
                    return reader[i] != DBNull.Value ? reader[i].ToString() : "";
                }
            }
            return "";
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

                    string qRevenue = @"SELECT ISNULL(SUM(ISNULL(b.TotalCost, 0)), 0) 
                                        FROM Bookings b 
                                        INNER JOIN Services s ON b.ServiceID = s.ServiceID 
                                        INNER JOIN ServiceProviders p ON s.ProviderID = p.ProviderID
                                        WHERE p.UserID = @UID AND b.Status = 'Completed'";

                    SqlCommand cmdRev = new SqlCommand(qRevenue, conn);
                    cmdRev.Parameters.AddWithValue("@UID", userId);

                    decimal totalRev = Convert.ToDecimal(cmdRev.ExecuteScalar());
                    lblTotalRevenue.Text = "R " + totalRev.ToString("F2");

                    decimal goalTarget = 5000m;
                    decimal percentage = totalRev > 0 ? (totalRev / goalTarget) * 100 : 0m;
                    decimal visualPercentage = percentage > 100 ? 100 : percentage;

                    lblCurrentRevTracker.Text = totalRev.ToString("F2");
                    lblGoalPercentage.Text = Math.Round(percentage, 0).ToString();
                    pnlProgressBar.Style.Add("width", visualPercentage.ToString("0") + "%");

                    string qActive = @"SELECT COUNT(*) 
                                       FROM Bookings b 
                                       INNER JOIN Services s ON b.ServiceID = s.ServiceID 
                                       INNER JOIN ServiceProviders p ON s.ProviderID = p.ProviderID
                                       WHERE p.UserID = @UID AND b.Status IN ('Pending Confirmation', 'Approved')";

                    SqlCommand cmdAct = new SqlCommand(qActive, conn);
                    cmdAct.Parameters.AddWithValue("@UID", userId);
                    lblActiveBookings.Text = cmdAct.ExecuteScalar().ToString();

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
            string reason = null; 

            if (e.CommandName == "Approve")
            {
                newStatus = "Approved";
            }
            else if (e.CommandName == "Reject")
            {
                newStatus = "Rejected";

                TextBox txtReason = (TextBox)e.Item.FindControl("txtRejectReason");
                if (txtReason != null && !string.IsNullOrWhiteSpace(txtReason.Text))
                {
                    reason = txtReason.Text.Trim();
                }
                else
                {
                    reason = "Provider is currently unavailable to fulfill this request."; 
                }
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
                        string sql = "UPDATE Bookings SET Status = @Status, RejectionReason = @Reason WHERE BookingID = @BID";
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@Status", newStatus);
                        cmd.Parameters.AddWithValue("@BID", bookingId);

                        if (reason == null)
                        {
                            cmd.Parameters.AddWithValue("@Reason", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Reason", reason);
                        }

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        lblError.Text = "Status Update Error: " + ex.Message;
                        lblError.Visible = true;
                    }
                }

                LoadDashboardMetrics();
                BindProviderBookings();
                BindEarningsHistory();
            }
        }
    }
}