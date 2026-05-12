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
            if (!IsPostBack)
            {
                // Security check
                if (Session["UserRole"]?.ToString() != "Admin") Response.Redirect("Login.aspx");

                BindLogs(""); // Load all logs initially
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindLogs(txtSearch.Text);
        }

        private void BindLogs(string searchTerm)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                // Start with base query
                string query = "SELECT UserName, ActionDescription, LogTime FROM AuditLogs";

                // Add filter condition if a search term exists
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    query += " WHERE UserName LIKE @Search OR ActionDescription LIKE @Search";
                }

                query += " ORDER BY LogTime DESC";

                SqlCommand cmd = new SqlCommand(query, conn);

                // Add parameter if searching
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    // The % wildcard allows for partial matches (e.g., 'John' will find 'John Maputla')
                    cmd.Parameters.AddWithValue("@Search", "%" + searchTerm + "%");
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                gvLogs.DataSource = dt;
                gvLogs.DataBind();
            }
        }
    }
}