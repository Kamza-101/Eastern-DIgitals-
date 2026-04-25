using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Group_9
{
    public partial class Register : System.Web.UI.Page
    {
        // Define your connection string here as a class-level variable
        string connStr = ConfigurationManager.ConnectionStrings["EasternDigitalDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            // This runs when the page first loads. 
            // You can leave it empty for now.
        }

        // Paste the logic right here, below Page_Load but before the closing braces
        protected void rblUserType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblUserType.SelectedValue == "Seeker")
            {
                pnlSeeker.Visible = true;
                pnlProvider.Visible = false;
            }
            else
            {
                pnlSeeker.Visible = false;
                pnlProvider.Visible = true;
            }
        }

            // 2. YOUR NEW DATABASE LOGIC (Added below the one above)
        protected void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();

                    // Step 1: Insert into Users (Common to both)
                    string userSql = "INSERT INTO Users (Email, Password, UserType) OUTPUT INSERTED.UserID VALUES (@Email, @Password, @Type)";
                    SqlCommand cmdUser = new SqlCommand(userSql, conn);
                    cmdUser.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmdUser.Parameters.AddWithValue("@Password", txtPassword.Text);
                    cmdUser.Parameters.AddWithValue("@Type", rblUserType.SelectedValue);

                    int newUserId = (int)cmdUser.ExecuteScalar();

                    // Step 2: Insert into the specific table based on the Panel visibility
                    if (rblUserType.SelectedValue == "Seeker")
                    {
                        string seekerSql = "INSERT INTO ServiceSeekers (UserID, FullName, ContactNumber, University, City) VALUES (@UID, @Name, @Contact, @Uni, @City)";
                        SqlCommand cmdSeeker = new SqlCommand(seekerSql, conn);
                        cmdSeeker.Parameters.AddWithValue("@UID", newUserId);
                        cmdSeeker.Parameters.AddWithValue("@Name", txtFullName.Text);
                        // ... add all other Seeker parameters here ...
                        cmdSeeker.ExecuteNonQuery();
                    }
                    else
                    {
                        string provSql = "INSERT INTO ServiceProviders (UserID, FirstName, Surname, ContactNumber, IDNumber, Location, ServiceType) VALUES (@UID, @Name, @Surname, @Contact, @ID, @Loc, @Service)";
                        SqlCommand cmdProv = new SqlCommand(provSql, conn);
                        cmdProv.Parameters.AddWithValue("@UID", newUserId);
                        // ... add all other Provider parameters here ...
                        cmdProv.ExecuteNonQuery();
                    }

                    lblMessage.Text = "Registration successful!";
                    lblMessage.CssClass = "text-success fw-bold";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error: " + ex.Message;
            }
        }
    }
}