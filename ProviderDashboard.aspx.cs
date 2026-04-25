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
    
        public partial class ProviderDashboard : System.Web.UI.Page
        {
            string connStr = ConfigurationManager.ConnectionStrings["EasternDigitalDB"].ConnectionString;

            protected void Page_Load(object sender, EventArgs e)
            {
                if (!IsPostBack)
                {
                    // Ensure provider is logged in
                    if (Session["UserID"] == null) Response.Redirect("Login.aspx");

                    LoadDashboardMetrics();
                    LoadRecentBookings();
                }
            }

            private void LoadDashboardMetrics()
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();

                    // 1. Get Total Revenue
                    string qRevenue = "SELECT ISNULL(SUM(Price), 0) FROM Bookings WHERE ProviderID = @PID AND Status = 'Completed'";
                    SqlCommand cmdRev = new SqlCommand(qRevenue, conn);
                    cmdRev.Parameters.AddWithValue("@PID", Session["UserID"]);
                    lblTotalRevenue.Text = "R " + Convert.ToDecimal(cmdRev.ExecuteScalar()).ToString("F2");

                    // 2. Get Active Bookings
                    string qActive = "SELECT COUNT(*) FROM Bookings WHERE ProviderID = @PID AND Status = 'Confirmed'";
                    SqlCommand cmdAct = new SqlCommand(qActive, conn);
                    cmdAct.Parameters.AddWithValue("@PID", Session["UserID"]);
                    lblActiveBookings.Text = cmdAct.ExecuteScalar().ToString();

                    // 3. Get Completed Jobs
                    string qComp = "SELECT COUNT(*) FROM Bookings WHERE ProviderID = @PID AND Status = 'Completed'";
                    SqlCommand cmdComp = new SqlCommand(qComp, conn);
                    cmdComp.Parameters.AddWithValue("@PID", Session["UserID"]);
                    lblCompletedJobs.Text = cmdComp.ExecuteScalar().ToString();
                }
            }

            private void LoadRecentBookings()
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    // Join Bookings with Students to get names
                    string query = @"SELECT S.ServiceName, ST.FullName AS StudentName, B.Status, B.Price 
                                 FROM Bookings B 
                                 JOIN Services S ON B.ServiceID = S.ServiceID 
                                 JOIN Students ST ON B.StudentID = ST.StudentID 
                                 WHERE B.ProviderID = @PID 
                                 ORDER BY B.BookingDate DESC";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@PID", Session["UserID"]);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Add a column for the status color class
                    dt.Columns.Add("StatusColor");
                    foreach (DataRow row in dt.Rows)
                    {
                        string status = row["Status"].ToString();
                        if (status == "Confirmed") row["StatusColor"] = "success";
                        else if (status == "Pending") row["StatusColor"] = "warning";
                        else row["StatusColor"] = "secondary";
                    }

                    rptProviderBookings.DataSource = dt;
                    rptProviderBookings.DataBind();
                }
            }
        }
    
}




