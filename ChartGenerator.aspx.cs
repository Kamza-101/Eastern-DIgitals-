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
            string labelsParam = Request.QueryString["labels"] ?? "Completed,Pending,Approved,Rejected";
            string valuesParam = Request.QueryString["values"] ?? "0,0,0,0";

            string[] labels = labelsParam.Split(',');
            string[] valStrings = valuesParam.Split(',');
            int[] values = new int[valStrings.Length];

            int maxValue = 1;
            for (int i = 0; i < valStrings.Length; i++)
            {
                int.TryParse(valStrings[i], out values[i]);
                if (values[i] > maxValue) maxValue = values[i];
            }

            int barHeight = 35, barGap = 15;

            using (Bitmap bmp = new Bitmap(600, 250))
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White);

                using (SolidBrush barBrush = new SolidBrush(Color.FromArgb(0, 122, 255))) 
                using (SolidBrush textBrush = new SolidBrush(Color.FromArgb(28, 28, 30))) 
                using (Font lblFont = new Font("Arial", 11, FontStyle.Bold))
                using (Font valFont = new Font("Arial", 10, FontStyle.Regular))
                {
                    for (int i = 0; i < values.Length; i++)
                    {
                        int y = 20 + i * (barHeight + barGap);

                        int barWidth = (int)(((double)values[i] / maxValue) * 400);
                        if (barWidth < 5) barWidth = 5; 

                        g.DrawString(labels[i], lblFont, textBrush, 10, y + 8);
                        g.FillRectangle(barBrush, 120, y, barWidth, barHeight);
                        g.DrawString(values[i].ToString(), valFont, textBrush, 125 + barWidth, y + 8);
                    }
                }

                Response.ContentType = "image/png";
                bmp.Save(Response.OutputStream, ImageFormat.Png);
            }

            Response.End();
        }
    }
}