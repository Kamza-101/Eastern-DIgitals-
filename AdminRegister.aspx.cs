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
    public partial class AdminRegister : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["EasternDigitalDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Security Check: Only logged-in Admins should be allowed to create other Admins!
            if (Session["UserID"] == null || Session["UserRole"] == null || Session["UserRole"].ToString() != "Admin")
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void btnRegisterAdmin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();

            // Note: We pull txtFullName.Text from the UI, but we don't send it to the DB 
            // since the Users table doesn't have a name column!

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                lblMessage.Text = "Please fill in all required fields.";
                return;
            }

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();

                    // 1. Check if the email is already registered
                    string checkSql = "SELECT COUNT(*) FROM Users WHERE Email = @Email";
                    SqlCommand checkCmd = new SqlCommand(checkSql, conn);
                    checkCmd.Parameters.AddWithValue("@Email", email);

                    int userExists = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (userExists > 0)
                    {
                        lblMessage.Text = "Error: An account with this email already exists.";
                        return;
                    }

                    // 2. Insert the new Admin into the database
                    string insertSql = "INSERT INTO Users (Email, Password, UserRole, Status) VALUES (@Email, @Password, 'Admin', 'Active')";
                    SqlCommand insertCmd = new SqlCommand(insertSql, conn);
                    insertCmd.Parameters.AddWithValue("@Email", email);
                    insertCmd.Parameters.AddWithValue("@Password", password); // Note: In a production app, hash this password!

                    insertCmd.ExecuteNonQuery();

                    // 3. Success! Redirect straight back to the Admin Dashboard
                    Response.Redirect("AdminDashboard.aspx", false);
                }
                catch (SqlException ex)
                {
                    lblMessage.Text = "Database Error: " + ex.Message;
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "System Error: " + ex.Message;
                }
            }
        }
    }
}