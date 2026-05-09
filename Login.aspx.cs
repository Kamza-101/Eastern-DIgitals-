using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Group_9
{
    public partial class Login : System.Web.UI.Page
    {
        // Connection string defined at the class level
        string connStr = ConfigurationManager.ConnectionStrings["EasternDigitalDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            // STATE MANAGEMENT: Auto-redirect if the user is already logged in
            if (Session["UserID"] != null && Session["UserRole"] != null)
            {
                string existingRole = Session["UserRole"].ToString();

                // Redirect based on the existing session role
                if (existingRole == "Admin") Response.Redirect("AdminDashboard.aspx");
                else if (existingRole == "Provider") Response.Redirect("ProviderDashboard.aspx");
                else Response.Redirect("BrowseServices.aspx");
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            // Stop if validation fails
            if (!Page.IsValid)
            {
                return;
            }

            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();
            string role = rblLoginType.SelectedValue; // Seeker, Provider, or Admin

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    // We verify the credentials AND the role match
                    string sql = "SELECT UserID FROM Users WHERE Email = @Email AND Password = @Password AND UserRole = @Role AND Status = 'Active'";
                    SqlCommand cmd = new SqlCommand(sql, conn);

                    // Parameters prevent SQL Injection
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password", password);
                    cmd.Parameters.AddWithValue("@Role", role);

                    conn.Open();

                    // ExecuteScalar returns the UserID if found, or null if the login fails
                    object userId = cmd.ExecuteScalar();

                    if (userId != null)
                    {
                        // 1. Authentication Successful: Save ID and Role into server memory
                        Session["UserID"] = userId.ToString();
                        Session["UserRole"] = role;

                        // 2. Redirect based on role selection
                        if (role == "Admin") Response.Redirect("AdminDashboard.aspx");
                        else if (role == "Provider") Response.Redirect("ProviderDashboard.aspx");
                        else Response.Redirect("BrowseServices.aspx");
                    }
                    else
                    {
                        // Failure: Show an error message on the screen
                        lblLoginMessage.Text = "Invalid email, password, or role selection.";
                        lblLoginMessage.CssClass = "text-danger fw-bold";
                    }
                }
                catch (SqlException ex)
                {
                    // ADO.NET Error Handling for Database issues
                    lblLoginMessage.Text = "Database Error: " + ex.Message;
                    lblLoginMessage.CssClass = "text-danger fw-bold";
                }
                catch (Exception ex)
                {
                    // ADO.NET Error Handling for System crashes
                    lblLoginMessage.Text = "System Error: " + ex.Message;
                    lblLoginMessage.CssClass = "text-danger fw-bold";
                }
            }
        }
    }
}
