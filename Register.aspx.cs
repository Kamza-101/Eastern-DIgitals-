using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Group_9
{
    public partial class Register : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["EasternDigitalDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Only runs the first time the page loads
        }

        // Toggles the form fields based on whether they are a Seeker or Provider
        protected void rblUserType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblUserType.SelectedValue == "Seeker")
            {
                pnlSeeker.Visible = true;
                pnlProvider.Visible = false;
            }
            else
            {
                pnlSeeker.Visible = false;
                pnlProvider.Visible = true;
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            // 1. Basic Validation
            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                lblMessage.Text = "Passwords do not match.";
                lblMessage.CssClass = "text-danger fw-bold";
                return;
            }

            // Determine which email box they filled out based on their role selection
            string role = rblUserType.SelectedValue;
            string email = (role == "Seeker") ? txtEmail.Text.Trim() : txtProvEmail.Text.Trim();
            string password = txtPassword.Text.Trim();

            // CRITICAL LOGIC: If they are a provider, their status is forced to 'Pending'
            string status = (role == "Provider") ? "Pending" : "Active";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();

                    // 2. Check if the email already exists to prevent crashes
                    string checkSql = "SELECT COUNT(*) FROM Users WHERE Email = @Email";
                    SqlCommand checkCmd = new SqlCommand(checkSql, conn);
                    checkCmd.Parameters.AddWithValue("@Email", email);

                    if (Convert.ToInt32(checkCmd.ExecuteScalar()) > 0)
                    {
                        lblMessage.Text = "This email is already registered.";
                        lblMessage.CssClass = "text-danger fw-bold";
                        return;
                    }

                    // 3. Insert into Users table
                    // Using OUTPUT INSERTED.UserID lets us grab the new ID immediately so we can link it
                    string insertUserSql = "INSERT INTO Users (Email, Password, UserRole, Status) OUTPUT INSERTED.UserID VALUES (@Email, @Password, @Role, @Status)";
                    SqlCommand cmdUser = new SqlCommand(insertUserSql, conn);
                    cmdUser.Parameters.AddWithValue("@Email", email);
                    cmdUser.Parameters.AddWithValue("@Password", password);
                    cmdUser.Parameters.AddWithValue("@Role", role);
                    cmdUser.Parameters.AddWithValue("@Status", status);

                    int newUserId = Convert.ToInt32(cmdUser.ExecuteScalar());

                    // 4. Create the Service Provider profile link 
                    // (This ensures the ProviderDashboard doesn't crash when looking for their ProviderID later)
                    if (role == "Provider")
                    {
                        string insertProvSql = "INSERT INTO ServiceProviders (UserID) VALUES (@UID)";
                        SqlCommand cmdProv = new SqlCommand(insertProvSql, conn);
                        cmdProv.Parameters.AddWithValue("@UID", newUserId);
                        cmdProv.ExecuteNonQuery();
                    }

                    // 5. Display the correct success message based on their role
                    if (role == "Provider")
                    {
                        lblMessage.Text = "Registration awaiting approval. An Admin will review your account shortly.";
                        lblMessage.CssClass = "text-warning fw-bold fs-5"; // Yellow/Orange warning text
                    }
                    else
                    {
                        lblMessage.Text = "Registration successful! You can now log in.";
                        lblMessage.CssClass = "text-success fw-bold fs-5"; // Green success text
                    }

                    // Clear the form so they don't accidentally submit twice
                    btnClear_Click(null, null);
                }
                catch (SqlException ex)
                {
                    lblMessage.Text = "Database Error: " + ex.Message;
                    lblMessage.CssClass = "text-danger fw-bold";
                }
            }
        }

        // Clears all textboxes and dropdowns
        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtFullName.Text = "";
            txtEmail.Text = "";
            txtContact.Text = "";
            ddlUniversity.SelectedIndex = 0;
            ddlCity.SelectedIndex = 0;

            txtProvName.Text = "";
            txtProvSurname.Text = "";
            txtID.Text = "";
            ddlLocation.SelectedIndex = 0;
            ddlServiceType.SelectedIndex = 0;
            txtProvEmail.Text = "";
            txtProvContact.Text = "";

            txtPassword.Text = "";
            txtConfirmPassword.Text = "";
        }
    }
}