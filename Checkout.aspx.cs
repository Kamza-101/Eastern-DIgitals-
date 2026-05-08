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
        // Connection string defined at the class level
        string connStr = ConfigurationManager.ConnectionStrings["EasternDigitalDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Security Check: Ensure the user is logged in before checking out
            if (Session["UserID"] == null)
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void btnConfirmBooking_Click(object sender, EventArgs e)
        {
            // 1. Validation: Ensure a payment method is selected
            if (string.IsNullOrEmpty(ddlPaymentMethod.SelectedValue))
            {
                lblMessage.Text = "Please select a payment method.";
                lblMessage.CssClass = "d-block text-center mb-3 fw-bold text-danger";
                return;
            }

            int userId = Convert.ToInt32(Session["UserID"]);

            // Generate a random 6-digit Order Reference (e.g., ED-482910)
            Random rnd = new Random();
            string orderRef = "ED-" + rnd.Next(100000, 999999).ToString();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();

                    // 2. MOVE ITEMS FROM CART TO BOOKINGS
                    // This query copies the user's cart items, grabs the current prices from the 
                    // Services table, and saves everything permanently into the Bookings table.
                    string insertBookingsSql = @"
                        INSERT INTO Bookings (OrderReference, UserID, ServiceID, PaymentMethod, Notes, TotalCost)
                        SELECT @OrderRef, c.UserID, c.ServiceID, @PayMethod, @Notes, s.Price
                        FROM Cart c
                        INNER JOIN Services s ON c.ServiceID = s.ServiceID
                        WHERE c.UserID = @UID";

                    SqlCommand cmdInsert = new SqlCommand(insertBookingsSql, conn);
                    cmdInsert.Parameters.AddWithValue("@OrderRef", orderRef);
                    cmdInsert.Parameters.AddWithValue("@PayMethod", ddlPaymentMethod.SelectedValue);
                    cmdInsert.Parameters.AddWithValue("@Notes", txtNotes.Text.Trim());
                    cmdInsert.Parameters.AddWithValue("@UID", userId);

                    cmdInsert.ExecuteNonQuery();

                    // 3. EMPTY THE CART
                    // Now that the items are safely saved as a Booking, we clear the Cart table
                    string deleteCartSql = "DELETE FROM Cart WHERE UserID = @UID";
                    SqlCommand cmdDelete = new SqlCommand(deleteCartSql, conn);
                    cmdDelete.Parameters.AddWithValue("@UID", userId);

                    cmdDelete.ExecuteNonQuery();

                    // 4. REDIRECT TO TRACK RECORD
                    // Send the user to their bookings page to see the newly created order
                    Response.Redirect("Bookings.aspx");
                }
                catch (SqlException ex)
                {
                    // ADO.NET Error Handling: Catch database constraints or connection issues
                    lblMessage.Text = "Database Error: " + ex.Message;
                    lblMessage.CssClass = "d-block text-center mb-3 fw-bold text-danger";
                }
                catch (Exception ex)
                {
                    // ADO.NET Error Handling: Catch general C# execution errors
                    lblMessage.Text = "System Error: " + ex.Message;
                    lblMessage.CssClass = "d-block text-center mb-3 fw-bold text-danger";
                }
            }
        }
    }
}

