using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Group_9
{
    public partial class SystemLogs : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["EasternDigitalDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            // SECURITY BOUNCER: Only Admins can view the raw system logs
            if (Session["UserID"] == null || Session["UserRole"] == null || Session["UserRole"].ToString() != "Admin")
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                LoadAllLogs();
            }
        }

        private void LoadAllLogs()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    // Fetch EVERY log in the system, newest at the top
                    string sql = "SELECT UserName, ActionDescription, LogTime FROM AuditLogs ORDER BY LogTime DESC";

                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();

                    conn.Open();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        rptLogs.DataSource = dt;
                        rptLogs.DataBind();

                        rptLogs.Visible = true;
                        lblNoData.Visible = false;
                    }
                    else
                    {
                        rptLogs.Visible = false;
                        lblNoData.Visible = true;
                    }
                }
                catch (SqlException ex)
                {
                    lblError.Text = "Database Connection Error: " + ex.Message;
                    lblError.Visible = true;
                }
                catch (Exception ex)
                {
                    // Catch-all for missing tables or syntax errors
                    lblError.Text = "System Error: " + ex.Message;
                    lblError.Visible = true;
                }
            }
        }
    }
}