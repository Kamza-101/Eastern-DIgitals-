using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

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
            if (Session["UserID"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    string query = @"SELECT C.CartID, S.ServiceName, S.Price 
                                     FROM Cart C 
                                     JOIN Services S ON C.ServiceID = S.ServiceID 
                                     WHERE C.UserID = @UID";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@UID", Session["UserID"]);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();

                    conn.Open();
                    da.Fill(dt);

                    rptCart.DataSource = dt;
                    rptCart.DataBind();

                    pnlEmpty.Visible = (dt.Rows.Count == 0);

                    CalculateTotal(dt);
                }
                catch (SqlException ex)
                {
                    Response.Write("<script>alert('Database Error loading cart: " + ex.Message + "');</script>");
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('System Error loading cart: " + ex.Message + "');</script>");
                }
            }
        }

        private void CalculateTotal(DataTable dt)
        {
            decimal total = 0;
            foreach (DataRow row in dt.Rows)
            {
                total += Convert.ToDecimal(row["Price"]);
            }
            lblTotal.Text = "R " + total.ToString("0.00");
        }


        protected void rptCart_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Remove")
            {
                int cartId = int.Parse(e.CommandArgument.ToString());

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    try
                    {
                        string sql = "DELETE FROM Cart WHERE CartID = @CID";
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@CID", cartId);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Response.Write("<script>alert('Error removing item: " + ex.Message + "');</script>");
                    }
                }


                BindCart();
            }
        }

        protected void btnCheckout_Click(object sender, EventArgs e)
        {

            Response.Redirect("Checkout.aspx");
        }
    }
}

