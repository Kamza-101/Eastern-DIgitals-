using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Group_9
{
    public partial class UserManagement : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindUserData();
            }
        }

        private void BindUserData()
        {
            // Dummy data for demonstration. 
            // In your real project, replace this with your database call (e.g., db.Users.ToList())
            var users = new List<object>
            {
                new { Username = "JohnMap" },
                new { Username = "SarahDev" }
            };

            gvUsers.DataSource = users;
            gvUsers.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            // Logic to loop through the GridView and update database
            foreach (GridViewRow row in gvUsers.Rows)
            {
                // Get the dropdowns
                DropDownList ddlRole = (DropDownList)row.FindControl("ddlRole");
                DropDownList ddlStatus = (DropDownList)row.FindControl("ddlStatus");
                string username = row.Cells[0].Text;

                // Example: Perform your UPDATE SQL query here
                // UpdateUserInDb(username, ddlRole.SelectedValue, ddlStatus.SelectedValue);
            }
        }
    }
}