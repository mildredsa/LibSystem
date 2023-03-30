using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace LibSystem.Admin
{
    public partial class Graphs : Form
    {
        private SqlConnection con = Database.GetConnection();
        public Graphs()
        {
            InitializeComponent();
        }

        private void DisplayBarChart()
        {
            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT Gender, COUNT(*) AS Borrowers FROM Borrowers WHERE Gender IS NOT NULL GROUP BY Gender", con);
            SqlDataReader reader = cmd.ExecuteReader();

            int maleCount = 0;
            int femaleCount = 0;

            while (reader.Read())
            {
                string gender = reader["Gender"].ToString();
                int count = Convert.ToInt32(reader["Borrowers"]);

                if (gender == "Male")
                {
                    maleCount = count;
                }
                else if (gender == "Female")
                {
                    femaleCount = count;
                }
            }

            reader.Close();
            con.Close();

            // Check if the series already exists in the chart
            Series series = chart1.Series.FirstOrDefault(s => s.Name == "Borrowers by Gender");

            if (series == null)
            {
                series = new Series("Borrowers by Gender");
                series.ChartType = SeriesChartType.Column;
                chart1.Series.Add(series);
            }

            // Update the data for the series
            series.Points.Clear();
            series.Points.AddXY("Male", maleCount);
            series.Points.AddXY("Female", femaleCount);

            // Remove any existing titles and legends
            chart1.Titles.Clear();
            chart1.Legends.Clear();

            // Add the new title
            chart1.Titles.Add("Borrowers by Gender");
            chart1.ChartAreas[0].AxisX.Title = "Gender";
            chart1.ChartAreas[0].AxisY.Title = "Count";
        }


        private void DisplayPieChart()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT Gender, COUNT(*) AS Borrowers FROM Borrowers WHERE Gender IS NOT NULL GROUP BY Gender", con);
            SqlDataReader reader = cmd.ExecuteReader();

            // Initialize variables to hold the counts for male and female borrowers
            int maleCount = 0;
            int femaleCount = 0;

            // Loop through the results of the query and sum the counts for male and female borrowers
            while (reader.Read())
            {
                string gender = reader["Gender"].ToString();
                int count = Convert.ToInt32(reader["Borrowers"]);

                if (gender == "Male")
                {
                    maleCount = count;
                }
                else if (gender == "Female")
                {
                    femaleCount = count;
                }
            }

            // Close the reader and the database connection
            reader.Close();
            con.Close();

            // Create a new series for the chart and set its type to pie
            Series series = new Series();
            series.ChartType = SeriesChartType.Pie;

            // Add the data points for male and female borrowers to the series
            series.Points.AddXY("Male", maleCount);
            series.Points[0].LegendText = "Male Borrowers";
            series.Points[0].Label = "#VALY (#PERCENT{P0})";
            series.Points.AddXY("Female", femaleCount);
            series.Points[1].LegendText = "Female Borrowers";
            series.Points[1].Label = "#VALY (#PERCENT{P0})";

            // Add the series to the chart and set its title and axis labels
            chart2.Series.Clear();
            chart2.Series.Add(series);
            chart2.Titles.Clear();
            chart2.Titles.Add("Borrowers by Gender");
            chart2.ChartAreas[0].AxisX.Title = "Gender";
            chart2.ChartAreas[0].AxisY.Title = "Count";

            // Add a legend to the chart and customize its appearance and behavior
            Legend legend = new Legend();
            legend.Docking = Docking.Bottom;
            legend.Alignment = StringAlignment.Center;
            legend.Title = "Gender";
            legend.TitleAlignment = StringAlignment.Center;
            legend.TitleFont = new Font("Arial", 12, FontStyle.Bold);
            legend.BackColor = Color.AliceBlue;
            legend.BorderWidth = 1;
            legend.BorderColor = Color.Black;
            chart2.Legends.Clear();
            chart2.Legends.Add(legend);

            chart2.ChartAreas[0].Area3DStyle.Enable3D = true;
            chart2.ChartAreas[0].Area3DStyle.Inclination = 30;
            chart2.ChartAreas[0].Area3DStyle.Rotation = 45;
            chart2.ChartAreas[0].Area3DStyle.LightStyle = LightStyle.Realistic;
        }

        private void DisplayLineChart()
        {
            // Retrieve the data from the database
            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT [Date Borrowed], COUNT(DISTINCT Username) AS Borrowed FROM Borrowed GROUP BY [Date Borrowed] ORDER BY [Date Borrowed]", con);
            SqlDataReader reader = cmd.ExecuteReader();

            List<string> dates = new List<string>();
            List<int> borrowers = new List<int>();

            while (reader.Read())
            {
                dates.Add(reader["Date Borrowed"].ToString());
                borrowers.Add(Convert.ToInt32(reader["Borrowed"]));
            }

            reader.Close();
            con.Close();

            // Create the chart
            chart3.Series.Clear();
            chart3.Titles.Clear();
            chart3.Titles.Add("Number of Borrowers by Date");

            Series series = new Series("Borrowed");
            series.ChartType = SeriesChartType.Line;

            for (int i = 0; i < dates.Count; i++)
            {
                series.Points.AddXY(dates[i], borrowers[i]);
            }

            chart3.Series.Add(series);
            chart3.ChartAreas[0].AxisX.Title = "Date Borrowed";
            chart3.ChartAreas[0].AxisY.Title = "Number of Borrowers";
        }


        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    // Display bar chart
                    DisplayBarChart();
                    break;
                case 1:
                    // Display pie chart
                    DisplayPieChart();
                    break;
                case 2:
                    DisplayLineChart();
                    break;
            }
        }
    }
}
