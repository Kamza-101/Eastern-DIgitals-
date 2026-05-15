using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace Group_9
{
    public partial class Reports : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["EasternDigitalDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null || Session["UserRole"] == null || Session["UserRole"].ToString() != "Admin")
            {
                Response.Redirect("Login.aspx");
            }

            if (!IsPostBack)
            {
                LoadReportData();
            }
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            LoadReportData();
        }

        private void LoadReportData()
        {
            string selectedStatus = ddlStatus.SelectedValue;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    string sql = @"
                        SELECT 
                            b.BookingID, 
                            ISNULL(b.OrderReference, 'N/A') AS OrderReference, 
                            FORMAT(b.BookingDate, 'MMM dd, yyyy') AS FormattedDate, 
                            ISNULL(s.ServiceName, 'Unknown Service') AS ServiceName, 
                            ISNULL(seeker.Email, 'Guest User') AS SeekerEmail, 
                            ISNULL(provUser.Email, 'Unknown Provider') AS ProviderEmail, 
                            ISNULL(b.TotalCost, 0) AS TotalCost,
                            ISNULL(b.Status, 'Pending') AS Status
                        FROM Bookings b
                        INNER JOIN Services s ON b.ServiceID = s.ServiceID
                        INNER JOIN ServiceProviders p ON s.ProviderID = p.ProviderID
                        LEFT JOIN Users seeker ON b.UserID = seeker.UserID
                        LEFT JOIN Users provUser ON p.UserID = provUser.UserID
                        WHERE (@Status = 'All' OR b.Status = @Status)
                        ORDER BY b.BookingDate DESC";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Status", selectedStatus);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();

                    conn.Open();
                    da.Fill(dt);

                    lblRecordCount.Text = dt.Rows.Count.ToString();

                    decimal totalValue = 0;
                    if (dt.Rows.Count > 0)
                    {
                        totalValue = Convert.ToDecimal(dt.Compute("SUM(TotalCost)", string.Empty));
                    }
                    lblTotalValue.Text = "R " + totalValue.ToString("0.00");

                    int completed = 0, pending = 0, approved = 0, rejected = 0;

                    foreach (DataRow row in dt.Rows)
                    {
                        string status = row["Status"].ToString();
                        if (status == "Completed") completed++;
                        else if (status == "Pending Confirmation") pending++;
                        else if (status == "Approved") approved++;
                        else if (status == "Rejected") rejected++;
                    }

                    string labelsStr = "Completed,Pending,Approved,Rejected";
                    string valuesStr = $"{completed},{pending},{approved},{rejected}";

                    imgChart.ImageUrl = "~/ChartGenerator.aspx?"
                                      + "labels=" + Server.UrlEncode(labelsStr)
                                      + "&values=" + Server.UrlEncode(valuesStr)
                                      + "&t=" + DateTime.Now.Ticks; 

                    if (dt.Rows.Count > 0)
                    {
                        rptData.DataSource = dt;
                        rptData.DataBind();

                        rptData.Visible = true;
                        lblNoData.Visible = false;
                        lblError.Visible = false;
                    }
                    else
                    {
                        rptData.Visible = false;
                        lblNoData.Visible = true;
                    }
                }
                catch (SqlException ex)
                {
                    lblError.Text = "Report Generation Error: " + ex.Message;
                    lblError.Visible = true;
                }
            }
        }
    }
}