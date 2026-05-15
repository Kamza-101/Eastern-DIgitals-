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
                                return; 
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
                                Session["UserID"] = dbUserId;
                                Session["UserRole"] = role;

                                isLoginSuccessful = true; 

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
                    } 

                    if (isLoginSuccessful)
                    {
                        string logSql = "INSERT INTO AuditLogs (UserName, ActionDescription, LogTime) VALUES (@User, @Action, GETDATE())";
                        using (SqlCommand logCmd = new SqlCommand(logSql, conn))
                        {
                            logCmd.Parameters.AddWithValue("@User", email);
                            logCmd.Parameters.AddWithValue("@Action", "User successfully logged in as " + role);
                            logCmd.ExecuteNonQuery(); 
                        }

                        Response.Redirect(redirectPage, false);
                    }
                }
                catch (SqlException)
                {
                    throw; 
                }
                catch (Exception)
                {
                    throw; 
                }
            }
        }

        protected void btnShowForgot_Click(object sender, EventArgs e)
        {
            pnlForgot.Visible = !pnlForgot.Visible;
            lblForgotMessage.Text = "";
        }

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
                    string sql = "SELECT Password FROM Users WHERE Email = @Email";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Email", email);

                    conn.Open();
                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        string password = result.ToString();

                        SendPasswordEmail(email, password);

                        lblForgotMessage.Text = "Your password has been successfully sent to your email!";
                        lblForgotMessage.CssClass = "text-success";

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
                    throw;
                }
            }
        }

        private void SendPasswordEmail(string toEmail, string password)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("dstixx809@gmail.com", "EasternDigital Support"); 
            mail.To.Add(toEmail);
            mail.Subject = "EasternDigital - Password Recovery";

            mail.Body = $"Hello,\n\nAs requested, here is your password for your EasternDigital account:\n\n" +
                        $"Password: {password}\n\n" +
                        $"Please keep your credentials safe.\n\nRegards,\nThe EasternDigital Team";

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;

            smtp.Credentials = new NetworkCredential("dstixx809@gmail.com", "arqmogbqqwirshyx");

            smtp.Send(mail);
        }
    }
}
