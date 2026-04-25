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
    public partial class Bookings : System.Web.UI.Page
    {
        // Connection string read from Web.config
        string connStr = ConfigurationManager.ConnectionStrings["EasternDigitalDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Ensure only logged-in users access this page
                if (Session["UserID"] == null) Response.Redirect("Login.aspx");
                LoadBookings();
            }
        }

        private void LoadBookings()
        {
            try
            {
                // Using SqlConnection to manage the communication channel
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    // Query to get bookings for the specific UserID
                    string query = @"SELECT BookingID, ServiceName, ProviderName, BookingDate, Status, Price, Icon 
                                     FROM Bookings 
                                     WHERE UserID = @UID 
                                     ORDER BY BookingDate DESC";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@UID", Session["UserID"]); // Prevent SQL Injection

                    conn.Open(); // Open connection

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        rptBookings.DataSource = dt;
                        rptBookings.DataBind();
                        rptBookings.Visible = true;
                        pnlNoBookings.Visible = false;
                    }
                    else
                    {
                        rptBookings.Visible = false;
                        pnlNoBookings.Visible = true;
                    }
                }
            }
            catch (SqlException ex)
            {
                lblMessage.Text = "Unable to load bookings. Please try again.";
                // In a production app, log ex.Message to an AuditLogs table
            }
        }

        // Handles button styling dynamically when the Repeater binds data
        protected void rptBookings_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Button btnAction = (Button)e.Item.FindControl("btnAction");
                DataRowView drv = (DataRowView)e.Item.DataItem;

                if (btnAction != null && drv != null)
                {
                    string status = drv["Status"].ToString();

                    switch (status)
                    {
                        case "Pending":
                            btnAction.Text = "Cancel Request";
                            btnAction.CssClass = "btn btn-outline-danger btn-sm btn-action w-100";
                            break;
                        case "Confirmed":
                            btnAction.Text = "Message Provider";
                            btnAction.CssClass = "btn btn-outline-primary btn-sm btn-action w-100";
                            break;
                        case "Completed":
                            btnAction.Text = "Leave Review";
                            btnAction.CssClass = "btn btn-outline-secondary btn-sm btn-action w-100";
                            break;
                    }
                }
            }
        }

        // Handles button clicks for cancellation or actions
        protected void rptBookings_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "ProcessAction")
            {
                int bookingId = Convert.ToInt32(e.CommandArgument);
                Button btn = (Button)e.CommandSource;

                if (btn.Text == "Cancel Request")
                {
                    UpdateBookingStatus(bookingId, "Cancelled");
                }

                LoadBookings(); // Refresh list after action
            }
        }

        private void UpdateBookingStatus(int bookingId, string newStatus)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string sql = "UPDATE Bookings SET Status = @Status WHERE BookingID = @BID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Status", newStatus);
                cmd.Parameters.AddWithValue("@BID", bookingId);

                conn.Open();
                cmd.ExecuteNonQuery(); // Execute non-query for updates
            }
        }
    }
}