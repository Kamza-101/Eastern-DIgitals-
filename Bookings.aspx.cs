using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Group_9
{
    public partial class Bookings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadBookings();
            }

        }

        private void LoadBookings()
        {
            DataTable dtBookings = GetDummyBookingsData();

            if (dtBookings != null && dtBookings.Rows.Count > 0)
            {
                rptBookings.DataSource = dtBookings;
                rptBookings.DataBind();
                rptBookings.Visible = true;
                pnlNoBookings.Visible = false;
            }
            else
            {
                rptBookings.Visible = false;
                pnlNoBookings.Visible = true;
            }
        }

        // Sets the button text and color when the list is loading
        protected void rptBookings_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Button btnAction = (Button)e.Item.FindControl("btnAction");
                DataRowView drv = (DataRowView)e.Item.DataItem;

                if (btnAction != null && drv != null)
                {
                    string status = drv["Status"].ToString();

                    if (status == "Pending")
                    {
                        btnAction.Text = "Cancel Request";
                        btnAction.CssClass = "btn btn-outline-danger btn-sm btn-action w-100";
                    }
                    else if (status == "Confirmed")
                    {
                        btnAction.Text = "Message Provider";
                        btnAction.CssClass = "btn btn-outline-primary btn-sm btn-action w-100";
                    }
                    else if (status == "Completed")
                    {
                        btnAction.Text = "Leave Review";
                        btnAction.CssClass = "btn btn-outline-secondary btn-sm btn-action w-100";
                    }
                }
            }
        }

        // Handles the click event for the buttons
        protected void rptBookings_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "ProcessAction")
            {
                string bookingId = e.CommandArgument.ToString();
                Button btn = (Button)e.CommandSource;

                if (btn.Text == "Cancel Request")
                {
                    lblMessage.Text = "Success: Booking #" + bookingId + " has been cancelled.";
                    lblMessage.CssClass = "fw-bold text-danger d-block mb-3 text-center";
                }
                else
                {
                    lblMessage.Text = "Action: " + btn.Text + " for Booking #" + bookingId;
                    lblMessage.CssClass = "fw-bold text-success d-block mb-3 text-center";
                }

                LoadBookings(); // Refresh UI
            }
        }

        private DataTable GetDummyBookingsData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("BookingID");
            dt.Columns.Add("Icon");
            dt.Columns.Add("ServiceName");
            dt.Columns.Add("ProviderName");
            dt.Columns.Add("BookingDate");
            dt.Columns.Add("Status");
            dt.Columns.Add("Price");

            dt.Rows.Add("1044", "📚", "Java Tutor", "Sipho Ndlovu", "15 April 2026", "Confirmed", "R 150.00");
            dt.Rows.Add("1045", "🖨️", "Printing", "Alice Copy Shop", "16 April 2026", "Pending", "R 350.00");
            dt.Rows.Add("1012", "🔧", "Repair", "TechFix Mthatha", "10 April 2026", "Completed", "R 850.00");

            return dt;
        }
    }
}