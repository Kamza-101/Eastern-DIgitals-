using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;

namespace Group_9
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            Application["PlatformVisits"] = 0;
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();

            if (ex != null)
            {
                Exception innerEx = ex.InnerException ?? ex;

                string errorDetails = $"CRITICAL ERROR | URL: {Request.Url} | Type: {innerEx.GetType().Name} | Message: {innerEx.Message}";

                string user = "SYSTEM";
                if (HttpContext.Current != null && HttpContext.Current.Session != null && HttpContext.Current.Session["UserID"] != null)
                {
                    user = HttpContext.Current.Session["UserID"].ToString();
                }

                string connStr = ConfigurationManager.ConnectionStrings["EasternDigitalDB"].ConnectionString;
                try
                {
                    using (SqlConnection conn = new SqlConnection(connStr))
                    {
                        string logSql = "INSERT INTO AuditLogs (UserName, ActionDescription, LogTime) VALUES (@User, @Action, GETDATE())";
                        using (SqlCommand cmd = new SqlCommand(logSql, conn))
                        {
                            cmd.Parameters.AddWithValue("@User", user);
                            cmd.Parameters.AddWithValue("@Action", errorDetails);

                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                catch
                {

                }

                Server.ClearError();

                Response.Redirect("~/Errors.aspx", false);
            }
        }
    }
}
