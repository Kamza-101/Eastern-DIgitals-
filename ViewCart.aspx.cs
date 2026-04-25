using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
 using System.Configuration;

namespace Group_9
{
    public partial class ViewCart : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["EasternDigitalDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCart();
            }
        }

        private void BindCart()
        {
            // Only show items for the logged-in User
            if (Session["UserID"] == null) { Response.Redirect("Login.aspx"); }

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"SELECT C.CartID, S.ServiceName, S.Price 
                                 FROM Cart C 
                                 JOIN Services S ON C.ServiceID = S.ServiceID 
                                 WHERE C.UserID = @UID";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UID", Session["UserID"]);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                rptCart.DataSource = dt;
                rptCart.DataBind();

                pnlEmpty.Visible = (dt.Rows.Count == 0);
                CalculateTotal(dt);
            }
        }

        private void CalculateTotal(DataTable dt)
        {
            decimal total = 0;
            foreach (DataRow row in dt.Rows)
            {
                total += Convert.ToDecimal(row["Price"]);
            }
            lblTotal.Text = "R " + total.ToString("F2");
        }

        protected void rptCart_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Remove")
            {
                int cartId = int.Parse(e.CommandArgument.ToString());

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string sql = "DELETE FROM Cart WHERE CartID = @CID";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@CID", cartId);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                BindCart(); // Refresh the list
            }
        }

        protected void btnCheckout_Click(object sender, EventArgs e)
        {
            // You will create a BookingConfirmation.aspx page next
            Response.Redirect("BookingConfirmation.aspx");
        }
    }
}