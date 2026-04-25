using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Group_9
{
    public partial class AdminRegister : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // SECURITY CHECK: Only allow current Admins to view this page
            if (!User.Identity.IsAuthenticated || !User.IsInRole("Admin"))
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void btnRegisterAdmin_Click(object sender, EventArgs e)
        {
            // 1. Validate inputs (Add your validation logic here)

            // 2. Logic to create user in your database
            // Example:
            // string email = txtEmail.Text;
            // string password = txtPassword.Text;
            // YourDatabaseMethod.CreateUser(email, password, "Admin");

            lblMessage.Text = "Admin account created successfully.";
            lblMessage.CssClass = "fw-bold text-success";
        }
    }
}