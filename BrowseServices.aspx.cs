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
            if (Session["UserRole"] != null)
            {
                string role = Session["UserRole"].ToString();

                if (role == "Admin")
                {
                    Response.Redirect("AdminDashboard.aspx", false);
                    return; 
                }
                else if (role == "Provider")
                {
                    Response.Redirect("ProviderDashboard.aspx", false);
                    return; 
                }
            }
            if (!IsPostBack)
            {
                BindServices();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindServices();
        }

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindServices();
        }

        private void BindServices()
        {
            string category = ddlCategory.SelectedValue;
            string searchTerm = txtSearch.Text.Trim();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    string query = @"
                        SELECT 
                            s.ServiceID, 
                            s.ServiceName, 
                            s.Description, 
                            s.Price, 
                            s.Icon, 
                            s.Tag, 
                            ISNULL(p.IsPremium, 0) AS IsPremium 
                        FROM Services s
                        INNER JOIN ServiceProviders p ON s.ProviderID = p.ProviderID 
                        WHERE 1=1";

                    if (category != "All")
                    {
                        query += " AND s.Category = @Cat";
                    }

                    if (!string.IsNullOrEmpty(searchTerm))
                    {
                        query += " AND (s.ServiceName LIKE @Search OR s.Description LIKE @Search)";
                    }

                    query += " ORDER BY p.IsPremium DESC, s.ServiceName ASC";

                    SqlCommand cmd = new SqlCommand(query, conn);

                    if (category != "All")
                    {
                        cmd.Parameters.AddWithValue("@Cat", category);
                    }

                    if (!string.IsNullOrEmpty(searchTerm))
                    {
                        cmd.Parameters.AddWithValue("@Search", "%" + searchTerm + "%");
                    }

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();

                    conn.Open();
                    da.Fill(dt); 

                    rptServices.DataSource = dt;
                    rptServices.DataBind();
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

        protected void rptServices_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "View")
            {
                string serviceId = e.CommandArgument.ToString();

                if (Session["UserID"] == null)
                {
                    Response.Redirect("Login.aspx", false);
                }
                else
                {
                    Response.Redirect("ServiceDetails.aspx?ServiceID=" + serviceId, false);
                }
            }
        }
    }
}