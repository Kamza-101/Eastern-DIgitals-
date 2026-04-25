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
                if (Session["UserRole"]?.ToString() != "Admin") Response.Redirect("Login.aspx");

                LoadMetrics();
                LoadActivityLogs();
            }
        }

        private void LoadMetrics()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                // 1. Total Users
                SqlCommand cmdUsers = new SqlCommand("SELECT COUNT(*) FROM Users", conn);
                lblTotalUsers.Text = cmdUsers.ExecuteScalar().ToString(); // Efficiently fetch single value

                // 2. Active Services
                SqlCommand cmdServices = new SqlCommand("SELECT COUNT(*) FROM Services", conn);
                lblActiveServices.Text = cmdServices.ExecuteScalar().ToString();

                // 3. Pending Bookings
                SqlCommand cmdBookings = new SqlCommand("SELECT COUNT(*) FROM Bookings WHERE Status = 'Pending'", conn);
                lblPendingBookings.Text = cmdBookings.ExecuteScalar().ToString();
            }
        }

        private void LoadActivityLogs()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                // Fetch the 5 most recent activities
                string query = "SELECT TOP 5 UserName, ActionDescription, LogTime FROM AuditLogs ORDER BY LogTime DESC";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                rptActivity.DataSource = dt;
                rptActivity.DataBind();
            }
        }
    }
}