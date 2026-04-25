using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace Group_9
{

    public partial class ServiceDetails : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["EasternDigitalDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Retrieve the ServiceID from the URL query string
                string serviceId = Request.QueryString["ServiceID"];

                if (!string.IsNullOrEmpty(serviceId))
                {
                    BindProviders(serviceId);
                }
                else
                {
                    lblNoProviders.Text = "Invalid service selection.";
                    lblNoProviders.Visible = true;
                }
            }
        }

        private void BindProviders(string serviceId)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                // SQL to join Providers and Services to get details for the specific service selected
                string query = @"SELECT P.ProviderID, 
                                        P.FirstName + ' ' + P.Surname AS ProviderName, 
                                        S.ServiceName AS ServiceDesc, 
                                        P.Location, 
                                        4.5 AS Rating, -- Placeholder until you add a Reviews table
                                        S.Price 
                                 FROM ServiceProviders P
                                 JOIN Services S ON P.ProviderID = S.ProviderID 
                                 WHERE S.ServiceID = @SID";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@SID", serviceId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                // Add calculated columns for your UI
                dt.Columns.Add("Initials");
                foreach (DataRow row in dt.Rows)
                {
                    string name = row["ProviderName"].ToString();
                    row["Initials"] = name.Substring(0, 1).ToUpper(); // First letter for the avatar
                }

                rptProviders.DataSource = dt;
                rptProviders.DataBind();

                if (dt.Rows.Count == 0)
                {
                    lblNoProviders.Visible = true;
                }
            }
        }

        protected void rptProviders_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Book")
            {
                string providerId = e.CommandArgument.ToString();
                // Redirect to a booking page, passing the provider ID
                Response.Redirect("BookService.aspx?ProviderID=" + providerId);
            }
        }
    }
}