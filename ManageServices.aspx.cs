using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace Group_9
{
    public partial class ManageServices : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["EasternDigitalDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null || Session["UserRole"] == null || Session["UserRole"].ToString() != "Provider")
            {
                Response.Redirect("Login.aspx");
            }

            if (!IsPostBack)
            {
                LoadProviderServices();
            }
        }

        private void LoadProviderServices()
        {
            int userId = Convert.ToInt32(Session["UserID"]);

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    string sql = @"SELECT s.* FROM Services s
                                   INNER JOIN ServiceProviders p ON s.ProviderID = p.ProviderID
                                   WHERE p.UserID = @UID";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@UID", userId);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();

                    conn.Open();
                    da.Fill(dt);

                    rptMyServices.DataSource = dt;
                    rptMyServices.DataBind();
                }
                catch (SqlException ex)
                {
                    System.Diagnostics.Debug.WriteLine("Load Services SQL Error: " + ex.Message);
                }
            }
        }

        // Toggles the form open
        protected void btnAddService_Click(object sender, EventArgs e)
        {
            pnlAddService.Visible = true;
            btnAddService.Visible = false;
        }

        // Toggles the form closed
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            pnlAddService.Visible = false;
            btnAddService.Visible = true;
            txtServiceName.Text = "";
            txtPrice.Text = "";
        }

        // Saves the data to the database
        protected void btnSaveService_Click(object sender, EventArgs e)
        {
            int userId = Convert.ToInt32(Session["UserID"]);

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();

                    // 1. Get the actual ProviderID for this user
                    SqlCommand getProviderCmd = new SqlCommand("SELECT ProviderID FROM ServiceProviders WHERE UserID = @UID", conn);
                    getProviderCmd.Parameters.AddWithValue("@UID", userId);
                    object providerResult = getProviderCmd.ExecuteScalar();

                    if (providerResult != null)
                    {
                        int providerId = Convert.ToInt32(providerResult);

                        // 2. Insert the new service
                        string sql = "INSERT INTO Services (ProviderID, ServiceName, Price, Category, Icon) VALUES (@PID, @Name, @Price, 'General', N'✨')";
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@PID", providerId);
                        cmd.Parameters.AddWithValue("@Name", txtServiceName.Text.Trim());
                        cmd.Parameters.AddWithValue("@Price", Convert.ToDecimal(txtPrice.Text.Trim()));

                        cmd.ExecuteNonQuery();

                        // Close form and refresh list
                        btnCancel_Click(sender, e);
                        LoadProviderServices();
                    }
                    else
                    {
                        lblAddError.Text = "Error: Your account is not registered as a Service Provider in the database.";
                        lblAddError.Visible = true;
                    }
                }
                catch (Exception ex)
                {
                    lblAddError.Text = "Error saving service. Check your price format.";
                    lblAddError.Visible = true;
                }
            }
        }

        // Deletes a service
        protected void rptMyServices_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string sql = "DELETE FROM Services WHERE ServiceID = @SID";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@SID", e.CommandArgument);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                LoadProviderServices(); // Refresh screen
            }
        }
    }
}