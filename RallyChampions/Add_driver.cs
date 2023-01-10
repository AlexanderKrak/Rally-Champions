using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace RallyChampions
{
    public partial class Add_driver : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-1VDMEEA\sqlexpress;Initial Catalog=wrc_rally_champions;Integrated Security=True;Pooling=False");
        public Add_driver()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "INSERT INTO driver_info(season, country, driver, car) VALUES('" + txtSeason.Text + "', '" + txtCountry.Text + "', '" + txtDriver.Text + "', '" + txtCar.Text + "')";
            cmd.ExecuteNonQuery();
            conn.Close();

            txtSeason.Text = "";
            txtCountry.Text = "";
            txtDriver.Text = "";
            txtCar.Text = "";

            MessageBox.Show("Driver added successfully");
        }
    }
}
