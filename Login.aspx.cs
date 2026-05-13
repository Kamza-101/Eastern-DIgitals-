using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Mail;

namespace Group_9
{
    public partial class Login : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["EasternDigitalDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            // STATE MANAGEMENT: Auto-redirect if the user is already logged in
            if (Session["UserID"] != null && Session["UserRole"] != null)
            {
                string existingRole = Session["UserRole"].ToString();

                if (existingRole == "Admin") Response.Redirect("AdminDashboard.aspx", false);
                else if (existingRole == "Provider") Response.Redirect("ProviderDashboard.aspx", false);
                else Response.Redirect("BrowseServices.aspx", false);
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
            string role = rblLoginType.SelectedValue;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    string sql = "SELECT UserID, Password, Status FROM Users WHERE Email = @Email AND UserRole = @Role";
                    SqlCommand cmd = new SqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Role", role);

                    conn.Open();

                    // Track success so we can log it AFTER the DataReader closes
                    bool isLoginSuccessful = false;
                    string redirectPage = "";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string dbPassword = reader["Password"].ToString();
                            string status = reader["Status"].ToString();
                            string dbUserId = reader["UserID"].ToString();

                            if (dbPassword != password)
                            {
                                lblLoginMessage.Text = "Incorrect password. Please try again.";
                                lblLoginMessage.CssClass = "text-danger fw-bold";
                                return; // Stop here
                            }

                            if (status == "Pending")
                            {
                                lblLoginMessage.Text = "Your Service Provider account is currently pending administrative approval.";
                                lblLoginMessage.CssClass = "text-warning fw-bold";
                            }
                            else if (status == "Suspended")
                            {
                                lblLoginMessage.Text = "Your account has been suspended by an Administrator.";
                                lblLoginMessage.CssClass = "text-danger fw-bold";
                            }
                            else if (status == "Active")
                            {
                                // Passwords match and account is active!
                                Session["UserID"] = dbUserId;
                                Session["UserRole"] = role;

                                isLoginSuccessful = true; // Mark as successful

                                // Determine where they go
                                if (role == "Admin") redirectPage = "AdminDashboard.aspx";
                                else if (role == "Provider") redirectPage = "ProviderDashboard.aspx";
                                else redirectPage = "BrowseServices.aspx";
                            }
                        }
                        else
                        {
                            lblLoginMessage.Text = "No account found with this email address for the selected role.";
                            lblLoginMessage.CssClass = "text-danger fw-bold";
                        }
                    } // The SqlDataReader safely closes here!

                    // IF SUCCESSFUL: Write to AuditLogs, then redirect
                    if (isLoginSuccessful)
                    {
                        string logSql = "INSERT INTO AuditLogs (UserName, ActionDescription, LogTime) VALUES (@User, @Action, GETDATE())";
                        using (SqlCommand logCmd = new SqlCommand(logSql, conn))
                        {
                            logCmd.Parameters.AddWithValue("@User", email);
                            logCmd.Parameters.AddWithValue("@Action", "User successfully logged in as " + role);
                            logCmd.ExecuteNonQuery(); // Physically saves the log
                        }

                        // Now bounce them to their dashboard
                        Response.Redirect(redirectPage, false);
                    }
                }
                catch (SqlException)
                {
                    throw; // Passes database crash to Global.asax
                }
                catch (Exception)
                {
                    throw; // Passes system crash to Global.asax
                }
            }
        }

        // Toggles the visibility of the Forgot Password panel
        protected void btnShowForgot_Click(object sender, EventArgs e)
        {
            pnlForgot.Visible = !pnlForgot.Visible;
            lblForgotMessage.Text = "";
        }

        // Handles the actual password retrieval and email sending
        protected void btnSendPassword_Click(object sender, EventArgs e)
        {
            string email = txtForgotEmail.Text.Trim();

            if (string.IsNullOrEmpty(email))
            {
                lblForgotMessage.Text = "Please enter your email address.";
                lblForgotMessage.CssClass = "text-danger";
                return;
            }

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    // 1. Find the password for this email
                    string sql = "SELECT Password FROM Users WHERE Email = @Email";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Email", email);

                    conn.Open();
                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        string password = result.ToString();

                        // 2. Send the email
                        SendPasswordEmail(email, password);

                        lblForgotMessage.Text = "Your password has been successfully sent to your email!";
                        lblForgotMessage.CssClass = "text-success";

                        // 3. Log this sensitive action to the AuditLogs table!
                        string logSql = "INSERT INTO AuditLogs (UserName, ActionDescription, LogTime) VALUES (@User, @Action, GETDATE())";
                        using (SqlCommand logCmd = new SqlCommand(logSql, conn))
                        {
                            logCmd.Parameters.AddWithValue("@User", email);
                            logCmd.Parameters.AddWithValue("@Action", "User requested a direct password recovery via email.");
                            logCmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        lblForgotMessage.Text = "This email is not registered in our system.";
                        lblForgotMessage.CssClass = "text-danger";
                    }
                }
                catch (Exception)
                {
                    // Securely pass system crashes up to Global.asax as mandated by Lecture 8
                    throw;
                }
            }
        }

        // The SMTP Email Configuration
        private void SendPasswordEmail(string toEmail, string password)
        {
            // Set up the email message
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("dstixx809@gmail.com", "EasternDigital Support"); // Put your email here
            mail.To.Add(toEmail);
            mail.Subject = "EasternDigital - Password Recovery";

            mail.Body = $"Hello,\n\nAs requested, here is your password for your EasternDigital account:\n\n" +
                        $"Password: {password}\n\n" +
                        $"Please keep your credentials safe.\n\nRegards,\nThe EasternDigital Team";

            // Configure the SMTP Server (Using Gmail as an example)
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;

            // Put your email and your Google App Password here
            smtp.Credentials = new NetworkCredential("dstixx809@gmail.com", "arqmogbqqwirshyx");

            smtp.Send(mail);
        }
    }
}
