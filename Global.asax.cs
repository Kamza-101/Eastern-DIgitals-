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
            // The lecture requires Application_Start to be used meaningfully. 
            // We can set up a global counter or state here.
            Application["PlatformVisits"] = 0;
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            // 1. Get the unhandled exception
            Exception ex = Server.GetLastError();

            if (ex != null)
            {
                // Get the inner exception if one exists for more accurate details
                Exception innerEx = ex.InnerException ?? ex;

                // 2. Build the log entry as required by the lecture (URL, Type, and Message)
                string errorDetails = $"CRITICAL ERROR | URL: {Request.Url} | Type: {innerEx.GetType().Name} | Message: {innerEx.Message}";

                // Determine who caused the error (System if not logged in)
                string user = Session != null && Session["UserID"] != null
                              ? Session["UserID"].ToString()
                              : "SYSTEM";

                // 3. Log it to the AuditLogs database table
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
                    // If logging itself fails, we swallow the exception so it doesn't cause an infinite loop
                }

                // 4. MUST call ClearError before redirecting, otherwise ASP.NET shows the yellow crash page anyway
                Server.ClearError();

                // 5. Redirect the user to a friendly custom error page
                Response.Redirect("~/Error.aspx", false);
            }
        }
    }
}
