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
            if (Session["UserID"] == null)
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void btnConfirmBooking_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ddlPaymentMethod.SelectedValue))
            {
                lblMessage.Text = "Please select a payment method.";
                lblMessage.CssClass = "d-block text-center mb-3 fw-bold text-danger";
                return;
            }

            int userId = Convert.ToInt32(Session["UserID"]);

            Random rnd = new Random();
            string orderRef = "ED-" + rnd.Next(100000, 999999).ToString();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();

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

                    string deleteCartSql = "DELETE FROM Cart WHERE UserID = @UID";
                    SqlCommand cmdDelete = new SqlCommand(deleteCartSql, conn);
                    cmdDelete.Parameters.AddWithValue("@UID", userId);

                    cmdDelete.ExecuteNonQuery();

                    Response.Redirect("BookingSuccess.aspx?ref=" + orderRef, false);
                }
                catch (SqlException ex)
                {
                    lblMessage.Text = "Database Error: " + ex.Message;
                    lblMessage.CssClass = "d-block text-center mb-3 fw-bold text-danger";
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "System Error: " + ex.Message;
                    lblMessage.CssClass = "d-block text-center mb-3 fw-bold text-danger";
                }
            }
        }
    }
}