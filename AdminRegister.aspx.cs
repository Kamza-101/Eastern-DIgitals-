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
            if (Session["UserID"] == null || Session["UserRole"] == null || Session["UserRole"].ToString() != "Admin")
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void btnRegisterAdmin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();


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

                    string checkSql = "SELECT COUNT(*) FROM Users WHERE Email = @Email";
                    SqlCommand checkCmd = new SqlCommand(checkSql, conn);
                    checkCmd.Parameters.AddWithValue("@Email", email);

                    int userExists = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (userExists > 0)
                    {
                        lblMessage.Text = "Error: An account with this email already exists.";
                        return;
                    }

                    string insertSql = "INSERT INTO Users (Email, Password, UserRole, Status) VALUES (@Email, @Password, 'Admin', 'Active')";
                    SqlCommand insertCmd = new SqlCommand(insertSql, conn);
                    insertCmd.Parameters.AddWithValue("@Email", email);
                    insertCmd.Parameters.AddWithValue("@Password", password); 

                    insertCmd.ExecuteNonQuery();

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