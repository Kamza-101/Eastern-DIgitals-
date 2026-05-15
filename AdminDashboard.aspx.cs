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
    public partial class AdminDashboard : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["EasternDigitalDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserRole"] == null || Session["UserRole"].ToString() != "Admin")
                {
                    Response.Redirect("Login.aspx");
                }

                RefreshAllData();
            }
        }

        private void RefreshAllData()
        {
            LoadMetrics();
            LoadPendingProviders();
            LoadAllUsers();
            LoadActivityLogs();
        }

        private void LoadMetrics()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();

                    SqlCommand cmdUsers = new SqlCommand("SELECT COUNT(*) FROM Users", conn);
                    lblTotalUsers.Text = cmdUsers.ExecuteScalar().ToString();

                    SqlCommand cmdServices = new SqlCommand("SELECT COUNT(*) FROM Services", conn);
                    lblActiveServices.Text = cmdServices.ExecuteScalar().ToString();

                    SqlCommand cmdBookings = new SqlCommand("SELECT COUNT(*) FROM Bookings WHERE Status = 'Pending Confirmation'", conn);
                    lblPendingBookings.Text = cmdBookings.ExecuteScalar().ToString();

                    SqlCommand cmdPremium = new SqlCommand("SELECT COUNT(*) FROM ServiceProviders WHERE IsPremium = 1", conn);
                    int premiumCount = Convert.ToInt32(cmdPremium.ExecuteScalar());
                    lblPremiumProviders.Text = premiumCount.ToString();

                    decimal mrr = premiumCount * 50m;
                    lblMRR.Text = mrr.ToString("0.00");

                    SqlCommand cmdCommission = new SqlCommand("SELECT ISNULL(SUM(TotalCost * 0.05), 0) FROM Bookings WHERE Status = 'Completed'", conn);
                    decimal commission = Convert.ToDecimal(cmdCommission.ExecuteScalar());

                    decimal totalProfit = mrr + commission;
                    lblTotalProfit.Text = totalProfit.ToString("0.00");
                }
                catch (Exception ex)
                {
                    lblAdminMessage.Text = "Metrics Load Error: " + ex.Message;
                    lblAdminMessage.Visible = true;
                }
            }
        }

        private void LoadPendingProviders()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    // JOIN RELATION: Pulls contextual name metrics and classifications safely from the profile schema
                    string sql = @"
                        SELECT 
                            u.UserID, 
                            u.Email, 
                            ISNULL(p.FirstName, 'N/A') AS FirstName, 
                            ISNULL(p.Surname, 'N/A') AS Surname, 
                            ISNULL(p.ServiceType, 'General Profile Setup') AS ProposedService
                        FROM Users u
                        INNER JOIN ServiceProviders p ON u.UserID = p.UserID
                        WHERE u.UserRole = 'Provider' AND u.Status = 'Pending'";

                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    rptPendingProviders.DataSource = dt;
                    rptPendingProviders.DataBind();
                }
                catch (Exception ex)
                {
                    lblAdminMessage.Text = "Database Loading Crash: " + ex.Message;
                    lblAdminMessage.Visible = true;
                }
            }
        }

        private void LoadAllUsers()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    string sql = @"SELECT UserID, Email, ISNULL(UserRole, 'Seeker') AS UserRole, ISNULL(Status, 'Active') AS Status 
                                   FROM Users 
                                   WHERE UserRole != 'Admin' AND Status != 'Pending'
                                   ORDER BY UserRole ASC, Email ASC";
                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    rptAllUsers.DataSource = dt;
                    rptAllUsers.DataBind();
                }
                catch (Exception ex) { }
            }
        }

        private void LoadActivityLogs()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    string query = "SELECT TOP 5 UserName, ActionDescription, LogTime FROM AuditLogs ORDER BY LogTime DESC";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    rptActivity.DataSource = dt;
                    rptActivity.DataBind();
                }
                catch (Exception ex) { }
            }
        }

        protected void rptPendingProviders_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string userId = e.CommandArgument.ToString();
            string newStatus = e.CommandName == "Approve" ? "Active" : "Rejected";

            UpdateUserStatus(userId, newStatus, null);
            lblAdminMessage.Text = $"Provider registration successfully {newStatus.ToLower()}!";
            lblAdminMessage.Visible = true;

            RefreshAllData();
        }

        protected void rptAllUsers_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "ToggleStatus")
            {
                string[] args = e.CommandArgument.ToString().Split('|');
                string userId = args[0];
                string currentStatus = args[1];

                string newStatus = (currentStatus == "Active") ? "Suspended" : "Active";
                string reason = null;

                if (newStatus == "Suspended")
                {
                    TextBox txtReason = (TextBox)e.Item.FindControl("txtSuspendReason");
                    if (txtReason != null && !string.IsNullOrWhiteSpace(txtReason.Text))
                    {
                        reason = txtReason.Text.Trim();
                    }
                    else
                    {
                        reason = "Violation of platform policies.";
                    }
                }

                UpdateUserStatus(userId, newStatus, reason);
                lblAdminMessage.Text = $"User account is now {newStatus}.";
                lblAdminMessage.Visible = true;

                RefreshAllData();
            }
        }

        private void UpdateUserStatus(string userId, string newStatus, string reason = null)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    string sql = "UPDATE Users SET Status = @Status, SuspensionReason = @Reason WHERE UserID = @UID";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Status", newStatus);
                    cmd.Parameters.AddWithValue("@UID", userId);

                    if (reason == null)
                        cmd.Parameters.AddWithValue("@Reason", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@Reason", reason);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    lblAdminMessage.Text = "Database Update Error: " + ex.Message;
                    lblAdminMessage.Visible = true;
                }
            }
        }
    }
}