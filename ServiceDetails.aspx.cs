using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace Group_9
{

    public partial class ServiceDetails : System.Web.UI.Page
    {
        // Define the connection string
        string connStr = ConfigurationManager.ConnectionStrings["EasternDigitalDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            // 1. SECURITY CHECK: Ensure the user is logged in
            if (Session["UserID"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            // 2. LOAD DATA: Only fetch from the database when the page first loads
            if (!IsPostBack)
            {
                LoadServiceAndProviderDetails();
            }
        }

        // --- METHOD 1: Fetch and display the details ---
        private void LoadServiceAndProviderDetails()
        {
            // Get the ServiceID from the URL
            string serviceId = Request.QueryString["ServiceID"];

            // If there is no ID in the URL, send them back to the browse page
            if (string.IsNullOrEmpty(serviceId))
            {
                Response.Redirect("BrowseServices.aspx");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    // INNER JOIN query to get both Service AND Provider data
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

                    // ExecuteReader is used because we are reading specific columns from one row
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Assign data to front-end labels
                            lblServiceName.Text = reader["ServiceName"].ToString();
                            lblServiceDesc.Text = reader["Description"].ToString();
                            lblPrice.Text = "R " + Convert.ToDecimal(reader["Price"]).ToString("0.00");
                            imgService.ImageUrl = reader["Icon"].ToString();

                            lblProviderName.Text = reader["FirstName"].ToString() + " " + reader["Surname"].ToString();
                            lblLocation.Text = reader["Location"].ToString();
                            lblContact.Text = reader["ContactNumber"].ToString();
                        }
                        else
                        {
                            // If the ID in the URL is fake or deleted
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

        // --- METHOD 2: Add the item to the cart ---
        protected void btnAddToCart_Click(object sender, EventArgs e)
        {
            // Get IDs
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
                    // 1. Check for duplicates in the cart
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
                        // 2. Insert into the Cart table
                        string insertSql = "INSERT INTO Cart (UserID, ServiceID) VALUES (@UID, @SID)";
                        SqlCommand cmdInsert = new SqlCommand(insertSql, conn);
                        cmdInsert.Parameters.AddWithValue("@UID", userId);
                        cmdInsert.Parameters.AddWithValue("@SID", serviceId);

                        cmdInsert.ExecuteNonQuery();

                        // 3. Success! Redirect to the Cart page
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