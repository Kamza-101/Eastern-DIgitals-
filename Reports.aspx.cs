using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;

namespace Group_9
{
    public partial class Reports : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Optional: Auto-generate a default report on load
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            BindReportData();
        }

        private void BindReportData()
        {
            // Replace with your actual Connection String from Web.config
            string connString = ConfigurationManager.ConnectionStrings["MyDbConn"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                // Querying the AdminReporting table
                string query = "SELECT * FROM AdminReporting WHERE ReportCategory = @Category";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Category", ddlReportType.SelectedValue);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    gvReport.DataSource = dt;
                    gvReport.DataBind();
                }
            }
        }
    }
}