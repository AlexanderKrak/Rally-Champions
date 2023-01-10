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
    
    public partial class View_drivers : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-1VDMEEA\sqlexpress;Initial Catalog=wrc_rally_champions;Integrated Security=True;Pooling=False");
        int comboVal;
        public void LoadPage()
        {
            try
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM driver_info";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        public View_drivers()
        {
            InitializeComponent();
        }

        private void View_drivers_Load(object sender, EventArgs e)
        {
            LoadPage();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM driver_info WHERE driver LIKE '%" + txtDriverSearch.Text + "%'";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtDriverSearch_KeyUp(object sender, KeyEventArgs e)
        {
            int i = 0;
            try
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM driver_info WHERE driver LIKE '%" + txtDriverSearch.Text + "%'";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                i = Convert.ToInt32(dt.Rows.Count.ToString());
                dataGridView1.DataSource = dt;
                conn.Close();

                if(i == 0)
                {
                    MessageBox.Show("No record found");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboVal = Convert.ToInt32(comboBox1.SelectedIndex.ToString());
            
        }

        private void txtComboSearch_KeyUp(object sender, KeyEventArgs e)
        {
            conn.Open();
            string sqlQuery = "";
            switch (comboVal)
            {
                case 0:
                    sqlQuery = "SELECT * FROM driver_info WHERE season LIKE '%" + txtComboSearch.Text + "%'";
                    break;
                case 1:
                    sqlQuery = "SELECT * FROM driver_info WHERE country LIKE '%" + txtComboSearch.Text + "%'";
                    break;
                case 2:
                    sqlQuery = "SELECT * FROM driver_info WHERE driver LIKE '%" + txtComboSearch.Text + "%'";
                    break;
                case 3:
                    sqlQuery = "SELECT * FROM driver_info WHERE car LIKE '%" + txtComboSearch.Text + "%'";
                    break;
                default:
                    break;
            }
            
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sqlQuery;
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            panelEdit.Visible = true;
            int i;
            i = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());
            //MessageBox.Show(i.ToString());

            try
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM driver_info WHERE id = " + i + "";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    txtSeasonEdit.Text = dr["season"].ToString();
                    txtCountryEdit.Text = dr["country"].ToString();
                    txtDriverEdit.Text = dr["driver"].ToString();
                    txtCarEdit.Text = dr["car"].ToString();
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int i;
            i = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());
            try
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE driver_info SET season = '" + txtSeasonEdit.Text + "', country = '" + txtCountryEdit.Text + "', driver = '" + txtDriverEdit.Text + "', car = '" + txtCarEdit.Text + "' WHERE id = " + i + "";
                cmd.ExecuteNonQuery();
                conn.Close();
                LoadPage();
                MessageBox.Show("record updated successfully");
                panelEdit.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
