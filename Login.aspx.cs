using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Group_9
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Clears any previous messages when 
            if (!IsPostBack)
            {
                lblLoginMessage.Text = "";
            }
        }

        // This runs when the user clicks the "Log In" button
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            string password = txtPassword.Text;
            string userType = rblLoginType.SelectedValue; // Captures "Seeker" or "Provider"

            // TODO: Add your database connection and verification logic here.

            // Example feedback structure:
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                lblLoginMessage.Text = "Please enter both email and password.";
                lblLoginMessage.CssClass = "fw-bold text-danger";
            }
            else
            {
                lblLoginMessage.Text = $"Attempting to log in as {userType}...";
                lblLoginMessage.CssClass = "fw-bold text-success";
            }
        }
    }
}