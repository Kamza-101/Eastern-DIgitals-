using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Group_9
{
    public partial class Checkout : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["EasternDigitalDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserID"] == null) Response.Redirect("Login.aspx");
                LoadOrderSummary();
            }
        }

        private void LoadOrderSummary()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                // Calculate total from cart
                string query = "SELECT SUM(S.Price) AS TotalPrice, COUNT(C.CartID) AS ItemCount " +
                               "FROM Cart C JOIN Services S ON C.ServiceID = S.ServiceID " +
                               "WHERE C.UserID = @UID";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UID", Session["UserID"]);

                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    lblTotalItems.Text = rdr["ItemCount"].ToString();
                    lblTotalPrice.Text = "R " + Convert.ToDecimal(rdr["TotalPrice"]).ToString("F2");
                }
            }
        }

        protected void btnCompleteOrder_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction(); // Start Transaction

                try
                {
                    // 1. Create the Booking
                    string insertBooking = @"INSERT INTO Bookings (UserID, TotalAmount, PaymentMethod, Status, BookingDate) 
                                             OUTPUT INSERTED.BookingID 
                                             VALUES (@UID, @Total, @Method, 'Pending', GETDATE())";

                    SqlCommand cmdBook = new SqlCommand(insertBooking, conn, trans);
                    cmdBook.Parameters.AddWithValue("@UID", Session["UserID"]);
                    cmdBook.Parameters.AddWithValue("@Total", lblTotalPrice.Text.Replace("R ", ""));
                    cmdBook.Parameters.AddWithValue("@Method", rblPaymentMethod.SelectedValue);

                    int newBookingId = (int)cmdBook.ExecuteScalar();

                    // 2. Clear the user's cart
                    string clearCart = "DELETE FROM Cart WHERE UserID = @UID";
                    SqlCommand cmdClear = new SqlCommand(clearCart, conn, trans);
                    cmdClear.Parameters.AddWithValue("@UID", Session["UserID"]);
                    cmdClear.ExecuteNonQuery();

                    trans.Commit(); // Save everything

                    // 3. Show Success
                    pnlCheckoutForm.Visible = false;
                    pnlSuccess.Visible = true;
                    lblReferenceNumber.Text = "#ED-" + newBookingId.ToString("D5");
                }
                catch (Exception ex)
                {
                    trans.Rollback(); // Undo if anything fails
                    lblCheckoutError.Text = "Transaction failed: " + ex.Message;
                }
            }
        }
    }
}
        
    