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
        string connStr = ConfigurationManager.ConnectionStrings["EasternDigitalDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Visible = false;
        }

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

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            if (!chkPOPIA.Checked)
            {
                lblMessage.Text = "You must read and agree to the POPIA Privacy Policy to create an account.";
                lblMessage.CssClass = "text-danger fw-bold";
                lblMessage.Visible = true;
                return;
            }

            string role = rblUserType.SelectedValue;
            string password = txtPassword.Text.Trim();
            string email = "";

            if (string.IsNullOrEmpty(password) || password != txtConfirmPassword.Text.Trim())
            {
                lblMessage.Text = "Passwords do not match or are empty.";
                lblMessage.CssClass = "text-danger fw-bold";
                lblMessage.Visible = true;
                return;
            }

            if (role == "Seeker")
            {
                if (string.IsNullOrWhiteSpace(txtFullName.Text) ||
                    string.IsNullOrWhiteSpace(txtEmail.Text) ||
                    string.IsNullOrWhiteSpace(txtContact.Text) ||
                    string.IsNullOrWhiteSpace(ddlUniversity.SelectedValue) ||
                    string.IsNullOrWhiteSpace(ddlCity.SelectedValue))
                {
                    lblMessage.Text = "Please fill in all required Seeker fields.";
                    lblMessage.CssClass = "text-danger fw-bold";
                    lblMessage.Visible = true;
                    return;
                }
                email = txtEmail.Text.Trim();
            }
            else 
            {
                if (string.IsNullOrWhiteSpace(txtProvName.Text) ||
                    string.IsNullOrWhiteSpace(txtProvSurname.Text) ||
                    string.IsNullOrWhiteSpace(txtID.Text) ||
                    string.IsNullOrWhiteSpace(ddlLocation.SelectedValue) ||
                    string.IsNullOrWhiteSpace(ddlServiceType.SelectedValue) ||
                    string.IsNullOrWhiteSpace(txtProvEmail.Text) ||
                    string.IsNullOrWhiteSpace(txtProvContact.Text))
                {
                    lblMessage.Text = "Please fill in all required Provider fields.";
                    lblMessage.CssClass = "text-danger fw-bold";
                    lblMessage.Visible = true;
                    return;
                }
                email = txtProvEmail.Text.Trim();
            }

            string status = (role == "Provider") ? "Pending" : "Active";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();

                    string checkSql = "SELECT COUNT(*) FROM Users WHERE Email = @Email";
                    SqlCommand checkCmd = new SqlCommand(checkSql, conn);
                    checkCmd.Parameters.AddWithValue("@Email", email);

                    if (Convert.ToInt32(checkCmd.ExecuteScalar()) > 0)
                    {
                        lblMessage.Text = "This email is already registered.";
                        lblMessage.CssClass = "text-danger fw-bold";
                        lblMessage.Visible = true;
                        return;
                    }

                    string insertUserSql = "INSERT INTO Users (Email, Password, UserRole, Status) OUTPUT INSERTED.UserID VALUES (@Email, @Password, @Role, @Status)";
                    SqlCommand cmdUser = new SqlCommand(insertUserSql, conn);
                    cmdUser.Parameters.AddWithValue("@Email", email);
                    cmdUser.Parameters.AddWithValue("@Password", password);
                    cmdUser.Parameters.AddWithValue("@Role", role);
                    cmdUser.Parameters.AddWithValue("@Status", status);

                    int newUserId = Convert.ToInt32(cmdUser.ExecuteScalar());

                    if (role == "Provider")
                    {
                        string insertProvSql = "INSERT INTO ServiceProviders (UserID, FirstName, Surname) VALUES (@UID, @FName, @SName)";
                        SqlCommand cmdProv = new SqlCommand(insertProvSql, conn);
                        cmdProv.Parameters.AddWithValue("@UID", newUserId);
                        cmdProv.Parameters.AddWithValue("@FName", txtProvName.Text.Trim());
                        cmdProv.Parameters.AddWithValue("@SName", txtProvSurname.Text.Trim());
                        cmdProv.ExecuteNonQuery();
                    }

                    Response.Redirect("Login.aspx", false);
                }
                catch (SqlException)
                {
                    throw;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtFullName.Text = "";
            txtEmail.Text = "";
            txtContact.Text = "";
            ddlUniversity.SelectedIndex = 0;
            ddlCity.SelectedIndex = 0;

            txtProvName.Text = "";
            txtProvSurname.Text = "";
            txtID.Text = "";
            ddlLocation.SelectedIndex = 0;
            ddlServiceType.SelectedIndex = 0;
            txtProvEmail.Text = "";
            txtProvContact.Text = "";

            txtPassword.Text = "";
            txtConfirmPassword.Text = "";

            chkPOPIA.Checked = false;

            lblMessage.Visible = false;
        }
    }
}