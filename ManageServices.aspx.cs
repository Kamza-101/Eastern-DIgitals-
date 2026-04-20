using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Group_9
{
    public partial class ManageServices : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Load your services and bookings from the database here
        }

        protected void btnAddService_Click(object sender, EventArgs e)
        {
            // Logic to open modal or redirect to AddService.aspx
            Response.Redirect("AddService.aspx");
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            // Logic to delete service from DB
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            // Logic to update Booking Status in DB to 'Approved'
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            // Logic to update Booking Status in DB to 'Rejected'
        }
    }
}