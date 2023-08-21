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

namespace WindowsFormsApp2
{
    public partial class Colleges : Form
    {
        public Colleges()
        {
            InitializeComponent();
            ShowCollege();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\OneDrive\Documents\DatabaseProject.mdf;Integrated Security=True;Connect Timeout=30");
        private void ShowCollege()
        {
            con.Open();
            String query = "Select * from CollegeTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            CollegeDGV.DataSource = ds.Tables[0];
            con.Close();

        }
        private void Reset()
        {
            ConameTb.Text = "";
            CoCityCb.SelectedItem = -1;
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (ConameTb.Text == "" || CoCityCb.SelectedIndex == -1  )
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into CollegeTbl(CoName,CoCity,CoDate)values(@Con,@Coc,@Cod)", con);
                    cmd.Parameters.AddWithValue("@Con", ConameTb.Text);
                    cmd.Parameters.AddWithValue("@Coc", CoCityCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Cod", CoDate.Value.Date);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("College Added");
                    con.Close();
                    ShowCollege();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (ConameTb.Text == "" || CoCityCb.SelectedIndex == -1 )
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Update CollegeTbl set CoName=@Con,CoCity=@Coc,CoDate=@Cod Where CoNum = @CoKey", con);
                    cmd.Parameters.AddWithValue("@Con", ConameTb.Text);
                    cmd.Parameters.AddWithValue("@Coc", CoCityCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Cod", CoDate.Value.Date);
                    cmd.Parameters.AddWithValue("@CoKey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("College Updated");
                    con.Close();
                    ShowCollege();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Select the College ");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from CollegeTbl Where CoNum=@CoKey", con);
                    cmd.Parameters.AddWithValue("@CoKey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("College Deleted");
                    con.Close();
                    ShowCollege();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Home obj = new Home();
            obj.Show();
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Student obj = new Student();
            obj.Show();
            this.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Departement obj = new Departement();
            obj.Show();
            this.Close();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Professor obj = new Professor();
            obj.Show();
            this.Close();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Courses obj = new Courses();
            obj.Show();
            this.Close();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Fees obj = new Fees();
            obj.Show();
            this.Close();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Salary obj = new Salary();
            obj.Show();
            this.Close();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            Colleges obj = new Colleges();
            obj.Show();
            this.Close();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        int key = 0;
        private void CollegeDGV_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            ConameTb.Text = CollegeDGV.SelectedRows[0].Cells[1].Value.ToString();
            CoCityCb.SelectedItem = CollegeDGV.SelectedRows[0].Cells[2].Value.ToString();
            CoDate.Text = CollegeDGV.SelectedRows[0].Cells[3].Value.ToString();
            if (ConameTb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(CollegeDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }
    }
}
