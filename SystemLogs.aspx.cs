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
    public partial class SystemLogs : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadLogs("");
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadLogs(txtSearch.Text.Trim());
        }

        private void LoadLogs(string filter)
        {
            string connString = ConfigurationManager.ConnectionStrings["MyDbConn"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                // Querying the AuditLogs table
                string query = "SELECT * FROM AuditLogs WHERE Action LIKE @Filter OR Username LIKE @Filter ORDER BY Timestamp DESC";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Filter", "%" + filter + "%");

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    gvLogs.DataSource = dt;
                    gvLogs.DataBind();
                }
            }
        }
    }
}