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
        string connStr = ConfigurationManager.ConnectionStrings["EasternDigitalDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Security check: Must be logged in to view bookings
            if (Session["UserID"] == null)
            {
                Response.Redirect("Login.aspx", false);
                return;
            }

            if (!IsPostBack)
            {
                BindBookings();
            }
        }

        private void BindBookings()
        {
            int userId = Convert.ToInt32(Session["UserID"]);

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    // 1. The massive INNER JOIN to get everything your wireframe needs
                    string query = @"
                        SELECT 
                            b.BookingID, 
                            b.OrderReference, 
                            b.BookingDate, 
                            b.Status, 
                            b.TotalCost,
                            s.ServiceName, 
                            s.Icon,
                            p.FirstName, 
                            p.Surname
                        FROM Bookings b
                        INNER JOIN Services s ON b.ServiceID = s.ServiceID
                        INNER JOIN ServiceProviders p ON s.ProviderID = p.ProviderID
                        WHERE b.UserID = @UID
                        ORDER BY b.BookingDate DESC";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@UID", userId);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();

                    conn.Open();
                    da.Fill(dt);

                    // 2. Bind it to your front-end Repeater
                    rptBookings.DataSource = dt;
                    rptBookings.DataBind();

                    // Optional: If there are no bookings, show a message
                    if (dt.Rows.Count == 0)
                    {
                        lblMessage.Text = "You have not made any bookings yet.";
                        lblMessage.Visible = true;
                    }
                }
                catch (SqlException)
                {
                    // LECTURE 8 COMPLIANCE: Pass database crash securely to Global.asax
                    throw;
                }
                catch (Exception)
                {
                    // LECTURE 8 COMPLIANCE: Pass system crash securely to Global.asax
                    throw;
                }
            }
        }
    }
}

