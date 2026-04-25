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
    public partial class ManageServices : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["EasternDigitalDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) { BindDashboard(); }
        }

        private void BindDashboard()
        {
            // Fetch Services for this Provider
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                // 1. Bind Services
                string queryServices = "SELECT ServiceID, ServiceName, Price FROM Services WHERE ProviderID = @PID";
                SqlCommand cmdS = new SqlCommand(queryServices, conn);
                cmdS.Parameters.AddWithValue("@PID", Session["UserID"]); // Assumes Provider is logged in

                SqlDataAdapter daS = new SqlDataAdapter(cmdS);
                DataTable dtS = new DataTable();
                daS.Fill(dtS);
                rptMyServices.DataSource = dtS;
                rptMyServices.DataBind();

                // 2. Bind Bookings
                string queryReq = "SELECT BookingID, StudentName, ServiceName, BookingDate FROM Bookings WHERE ProviderID = @PID AND Status = 'Pending'";
                SqlCommand cmdR = new SqlCommand(queryReq, conn);
                cmdR.Parameters.AddWithValue("@PID", Session["UserID"]);

                SqlDataAdapter daR = new SqlDataAdapter(cmdR);
                DataTable dtR = new DataTable();
                daR.Fill(dtR);
                rptRequests.DataSource = dtR;
                rptRequests.DataBind();
            }
        }

        // Handle Delete Service
        protected void rptMyServices_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                int sId = int.Parse(e.CommandArgument.ToString());
                ExecuteSql("DELETE FROM Services WHERE ServiceID = @SID", "@SID", sId);
                BindDashboard();
            }
        }

        // Handle Approve/Reject Bookings
        protected void rptRequests_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int bId = int.Parse(e.CommandArgument.ToString());
            string newStatus = (e.CommandName == "Approve") ? "Confirmed" : "Rejected";

            ExecuteSql("UPDATE Bookings SET Status = @Status WHERE BookingID = @BID", "@Status", newStatus, "@BID", bId);
            BindDashboard();
        }

        // Helper method to keep code clean
        private void ExecuteSql(string query, string paramName, object paramValue, string paramName2 = null, object paramValue2 = null)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue(paramName, paramValue);
                if (paramName2 != null) cmd.Parameters.AddWithValue(paramName2, paramValue2);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}