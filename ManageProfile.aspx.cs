using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;


namespace Group_9
{
    public partial class ManageProfile : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["EasternDigitalDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserID"] == null) Response.Redirect("Login.aspx");
                LoadProfileData();
            }
        }

        private void LoadProfileData()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                // SELECT current bio and skills
                string sql = "SELECT Bio, Skills FROM ServiceProviders WHERE UserID = @UID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@UID", Session["UserID"]);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    txtBio.Text = reader["Bio"].ToString();
                    txtSkills.Text = reader["Skills"].ToString();
                }
                reader.Close();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                // UPDATE query to save changes
                string sql = "UPDATE ServiceProviders SET Bio = @Bio, Skills = @Skills WHERE UserID = @UID";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@Bio", txtBio.Text);
                cmd.Parameters.AddWithValue("@Skills", txtSkills.Text);
                cmd.Parameters.AddWithValue("@UID", Session["UserID"]);

                conn.Open();
                cmd.ExecuteNonQuery(); // Execute non-query for updates

                lblStatus.Text = "Profile updated successfully!";
                lblStatus.CssClass = "mt-3 d-block text-center fw-bold text-success";
            }
        }
    }
}