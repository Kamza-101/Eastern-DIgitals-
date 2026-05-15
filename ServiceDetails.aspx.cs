using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace Group_9
{

    public partial class ServiceDetails : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["EasternDigitalDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
 
            if (Session["UserID"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            if (!IsPostBack)
            {
                LoadServiceAndProviderDetails();
            }
        }

        private void LoadServiceAndProviderDetails()
        {
            string serviceId = Request.QueryString["ServiceID"];

            if (string.IsNullOrEmpty(serviceId))
            {
                Response.Redirect("BrowseServices.aspx");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    string query = @"
                        SELECT 
                            s.ServiceName, s.Description, s.Price, s.Icon,
                            p.FirstName, p.Surname, p.ContactNumber, p.Location
                        FROM Services s
                        INNER JOIN ServiceProviders p ON s.ProviderID = p.ProviderID
                        WHERE s.ServiceID = @ServiceID";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ServiceID", serviceId);

                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            lblServiceName.Text = reader["ServiceName"].ToString();
                            lblServiceDesc.Text = reader["Description"].ToString();
                            lblPrice.Text = "R " + Convert.ToDecimal(reader["Price"]).ToString("0.00");
                           

                            lblProviderName.Text = reader["FirstName"].ToString() + " " + reader["Surname"].ToString();
                            lblLocation.Text = reader["Location"].ToString();
                            lblContact.Text = reader["ContactNumber"].ToString();
                        }
                        else
                        {
                            Response.Redirect("BrowseServices.aspx");
                        }
                    }
                }
                catch (SqlException ex)
                {
                    Response.Write("<script>alert('Database Error loading details: " + ex.Message + "');</script>");
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('System Error loading details: " + ex.Message + "');</script>");
                }
            }
        }

        protected void btnAddToCart_Click(object sender, EventArgs e)
        {
            int userId = Convert.ToInt32(Session["UserID"]);

            int serviceId;
            if (!int.TryParse(Request.QueryString["ServiceID"], out serviceId))
            {
                Response.Write("<script>alert('Invalid Service Selected.');</script>");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    string checkSql = "SELECT COUNT(*) FROM Cart WHERE UserID = @UID AND ServiceID = @SID";
                    SqlCommand cmdCheck = new SqlCommand(checkSql, conn);
                    cmdCheck.Parameters.AddWithValue("@UID", userId);
                    cmdCheck.Parameters.AddWithValue("@SID", serviceId);

                    conn.Open();
                    int exists = (int)cmdCheck.ExecuteScalar();

                    if (exists > 0)
                    {
                        Response.Write("<script>alert('This service is already in your cart!');</script>");
                    }
                    else
                    {
                        string insertSql = "INSERT INTO Cart (UserID, ServiceID) VALUES (@UID, @SID)";
                        SqlCommand cmdInsert = new SqlCommand(insertSql, conn);
                        cmdInsert.Parameters.AddWithValue("@UID", userId);
                        cmdInsert.Parameters.AddWithValue("@SID", serviceId);

                        cmdInsert.ExecuteNonQuery();

                        Response.Redirect("ViewCart.aspx");
                    }
                }
                catch (SqlException ex)
                {
                    Response.Write("<script>alert('Database Error adding to cart: " + ex.Message + "');</script>");
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('System Error adding to cart: " + ex.Message + "');</script>");
                }
            }
        }
    }
}