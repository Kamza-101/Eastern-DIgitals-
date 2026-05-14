using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Group_9
{
    public partial class ChartGenerator : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // 1. Read the data passed from the Reports page via Query String
            string labelsParam = Request.QueryString["labels"] ?? "Completed,Pending,Approved,Rejected";
            string valuesParam = Request.QueryString["values"] ?? "0,0,0,0";

            string[] labels = labelsParam.Split(',');
            string[] valStrings = valuesParam.Split(',');
            int[] values = new int[valStrings.Length];

            int maxValue = 1; // Prevent division by zero
            for (int i = 0; i < valStrings.Length; i++)
            {
                int.TryParse(valStrings[i], out values[i]);
                if (values[i] > maxValue) maxValue = values[i];
            }

            int barHeight = 35, barGap = 15;

            // 2. Create the Bitmap and Graphics objects (Always dispose them!)
            using (Bitmap bmp = new Bitmap(600, 250))
            using (Graphics g = Graphics.FromImage(bmp))
            {
                // Step 1: Fill background
                g.Clear(Color.White);

                using (SolidBrush barBrush = new SolidBrush(Color.FromArgb(0, 122, 255))) // iOS Blue
                using (SolidBrush textBrush = new SolidBrush(Color.FromArgb(28, 28, 30))) // iOS Dark Text
                using (Font lblFont = new Font("Arial", 11, FontStyle.Bold))
                using (Font valFont = new Font("Arial", 10, FontStyle.Regular))
                {
                    for (int i = 0; i < values.Length; i++)
                    {
                        int y = 20 + i * (barHeight + barGap);

                        // Calculate width proportional to the max value (max width is 400px)
                        int barWidth = (int)(((double)values[i] / maxValue) * 400);
                        if (barWidth < 5) barWidth = 5; // Minimum visual width

                        // Draw Label, Bar, and Value
                        g.DrawString(labels[i], lblFont, textBrush, 10, y + 8);
                        g.FillRectangle(barBrush, 120, y, barWidth, barHeight);
                        g.DrawString(values[i].ToString(), valFont, textBrush, 125 + barWidth, y + 8);
                    }
                }

                // 3. Serve the image directly to the browser
                Response.ContentType = "image/png";
                bmp.Save(Response.OutputStream, ImageFormat.Png);
            }

            // End the response to prevent HTML corruption
            Response.End();
        }
    }
}