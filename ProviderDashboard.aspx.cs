using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Group_9
{
    public partial class ProviderDashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDashboard();
            }

        }

        private void LoadDashboard()
        {
            // TODO: In your real project, replace this with:
            // 1. SELECT SUM(Price) FROM Bookings WHERE ProviderID = ...
            // 2. SELECT COUNT(*) FROM Bookings WHERE ProviderID = ...

            lblTotalRevenue.Text = "R 4,250.00";
            lblActiveBookings.Text = "3";
            lblCompletedJobs.Text = "12";

            // Bind the list
            rptProviderBookings.DataSource = GetDashboardData();
            rptProviderBookings.DataBind();
        }

        private DataTable GetDashboardData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ServiceName");
            dt.Columns.Add("StudentName");
            dt.Columns.Add("Status");
            dt.Columns.Add("StatusColor"); // Used for bootstrap badge color
            dt.Columns.Add("Price");

            dt.Rows.Add("Java Tutoring", "James Motsamai", "Confirmed", "success", "150.00");
            dt.Rows.Add("Thesis Printing", "Lindiwe Dlamini", "Pending", "warning", "350.00");
            dt.Rows.Add("Graphic Design", "Sipho Khumalo", "Completed", "secondary", "500.00");

            return dt;
        }
    }
}

    
