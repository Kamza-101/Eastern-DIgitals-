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
    public partial class UpgradePremium : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["EasternDigitalDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null || Session["UserRole"] == null || Session["UserRole"].ToString() != "Provider")
            {
                Response.Redirect("Login.aspx", false);
            }
            lblMessage.Visible = false;
        }

        protected void btnPayUpgrade_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCardName.Text) || string.IsNullOrWhiteSpace(txtCardNumber.Text))
            {
                lblMessage.Text = "Please fill in the mock card details to process the upgrade.";
                lblMessage.CssClass = "text-danger fw-bold";
                lblMessage.Visible = true;
                return;
            }

            string userId = Session["UserID"].ToString();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();

                    string updateSql = "UPDATE ServiceProviders SET IsPremium = 1 WHERE UserID = @UID";
                    using (SqlCommand cmdUpdate = new SqlCommand(updateSql, conn))
                    {
                        cmdUpdate.Parameters.AddWithValue("@UID", userId);
                        cmdUpdate.ExecuteNonQuery();
                    }

                    string logSql = "INSERT INTO AuditLogs (UserName, ActionDescription, LogTime) VALUES (@User, @Action, GETDATE())";
                    using (SqlCommand logCmd = new SqlCommand(logSql, conn))
                    {
                        logCmd.Parameters.AddWithValue("@User", "Provider ID: " + userId);
                        logCmd.Parameters.AddWithValue("@Action", "Processed R50 payment for Premium Subscription.");
                        logCmd.ExecuteNonQuery();
                    }

                    Response.Redirect("ProviderDashboard.aspx?upgrade=success", false);
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
    }
}