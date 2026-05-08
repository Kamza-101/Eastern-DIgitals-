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
                // Ensure only Admins see this
                if (Session["UserRole"] == null || Session["UserRole"].ToString() != "Admin")
                {
                    Response.Redirect("Login.aspx");
                }

                RefreshAllData();
            }
        }

        // Helper method to reload the whole page after making a change
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

                    // 1. Total Users
                    SqlCommand cmdUsers = new SqlCommand("SELECT COUNT(*) FROM Users", conn);
                    lblTotalUsers.Text = cmdUsers.ExecuteScalar().ToString();

                    // 2. Active Services
                    SqlCommand cmdServices = new SqlCommand("SELECT COUNT(*) FROM Services", conn);
                    lblActiveServices.Text = cmdServices.ExecuteScalar().ToString();

                    // 3. Pending Bookings
                    SqlCommand cmdBookings = new SqlCommand("SELECT COUNT(*) FROM Bookings WHERE Status = 'Pending Confirmation'", conn);
                    lblPendingBookings.Text = cmdBookings.ExecuteScalar().ToString();
                }
                catch (Exception ex)
                {
                    lblAdminMessage.Text = "Metrics Load Error: " + ex.Message;
                    lblAdminMessage.Visible = true;
                }
            }
        }

        // NEW: Loads Providers who are waiting to be approved
        private void LoadPendingProviders()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    string sql = "SELECT UserID, Email FROM Users WHERE UserRole = 'Provider' AND Status = 'Pending'";
                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    rptPendingProviders.DataSource = dt;
                    rptPendingProviders.DataBind();
                }
                catch (Exception ex) { /* Handle silently or log */ }
            }
        }

        // NEW: Loads all active and suspended users (excluding Admins and pending providers)
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
                catch (Exception ex) { /* Handle silently or log */ }
            }
        }

        private void LoadActivityLogs()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    // Fetch the 5 most recent activities
                    string query = "SELECT TOP 5 UserName, ActionDescription, LogTime FROM AuditLogs ORDER BY LogTime DESC";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    rptActivity.DataSource = dt;
                    rptActivity.DataBind();
                }
                catch (Exception ex) { /* If AuditLogs table doesn't exist yet, it won't crash the page */ }
            }
        }

        // NEW: Handles Accept/Decline for new Providers
        protected void rptPendingProviders_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string userId = e.CommandArgument.ToString();
            string newStatus = e.CommandName == "Approve" ? "Active" : "Rejected";

            UpdateUserStatus(userId, newStatus);
            lblAdminMessage.Text = $"Provider registration successfully {newStatus.ToLower()}!";
            lblAdminMessage.Visible = true;

            RefreshAllData();
        }

        // NEW: Handles Suspend/Activate for existing users
        protected void rptAllUsers_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "ToggleStatus")
            {
                string[] args = e.CommandArgument.ToString().Split('|');
                string userId = args[0];
                string currentStatus = args[1];

                string newStatus = (currentStatus == "Active") ? "Suspended" : "Active";

                UpdateUserStatus(userId, newStatus);
                lblAdminMessage.Text = $"User account is now {newStatus}.";
                lblAdminMessage.Visible = true;

                RefreshAllData();
            }
        }

        // Helper Method to execute the SQL Update
        private void UpdateUserStatus(string userId, string newStatus)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    string sql = "UPDATE Users SET Status = @Status WHERE UserID = @UID";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Status", newStatus);
                    cmd.Parameters.AddWithValue("@UID", userId);

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