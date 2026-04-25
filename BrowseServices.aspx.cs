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
    public partial class BrowseServices : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["EasternDigitalDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindServices("All");
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindServices(ddlCategory.SelectedValue);
        }

        private void BindServices(string category)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                // Select query to fetch all services
                string query = "SELECT ServiceID, ServiceName, Description, Price, Icon, Tag FROM Services";

                if (category != "All")
                {
                    query += " WHERE Category = @Cat";
                }

                SqlCommand cmd = new SqlCommand(query, conn);
                if (category != "All")
                {
                    cmd.Parameters.AddWithValue("@Cat", category);
                }

                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                rptServices.DataSource = dt;
                rptServices.DataBind();
            }
        }

        // Handles the click of the "View Providers" button
        protected void rptServices_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "View")
            {
                string serviceId = e.CommandArgument.ToString();
                // Redirect user to a list of providers for this specific service
                Response.Redirect("ProvidersList.aspx?ServiceID=" + serviceId);
            }
        }
    }
}