using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Group_9
{
    public partial class UserManagement : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["EasternDigitalDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserRole"]?.ToString() != "Admin") Response.Redirect("Login.aspx");
                BindGrid();
            }
        }

        private void BindGrid()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT UserID, Email AS Username, UserRole, Status FROM Users";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvUsers.DataSource = dt;
                gvUsers.DataBind();
            }
        }

        protected void gvUsers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = (DataRowView)e.Row.DataItem;

                DropDownList ddlRole = (DropDownList)e.Row.FindControl("ddlRole");
                DropDownList ddlStatus = (DropDownList)e.Row.FindControl("ddlStatus");

                ddlRole.SelectedValue = drv["UserRole"].ToString();
                ddlStatus.SelectedValue = drv["Status"].ToString();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                foreach (GridViewRow row in gvUsers.Rows)
                {
                    int userId = Convert.ToInt32(gvUsers.DataKeys[row.RowIndex].Value);

                    DropDownList ddlRole = (DropDownList)row.FindControl("ddlRole");
                    DropDownList ddlStatus = (DropDownList)row.FindControl("ddlStatus");

                    string sql = "UPDATE Users SET UserRole = @Role, Status = @Status WHERE UserID = @UID";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Role", ddlRole.SelectedValue);
                    cmd.Parameters.AddWithValue("@Status", ddlStatus.SelectedValue);
                    cmd.Parameters.AddWithValue("@UID", userId);

                    cmd.ExecuteNonQuery();
                }
            }

            BindGrid();
        }
    }
}