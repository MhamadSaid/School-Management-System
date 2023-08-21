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
    public partial class Departement : Form
    {
        public Departement()
        {
            InitializeComponent();
            ShowDep();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\OneDrive\Documents\DatabaseProject.mdf;Integrated Security=True;Connect Timeout=30");
        private void ShowDep()
        {
            con.Open();
            String query = "Select * from DepartementTB1";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            DepDGV.DataSource = ds.Tables[0];
            con.Close();

        }
        private void Reset()
        {
            DepNamePTB.Text = "";
            DepIntake.Text = "";
            DepFeesTB.Text = "";
        }
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (DepNamePTB.Text == "" || DepIntake.Text == "" || DepFeesTB.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into DepartementTB1(DepName,DepIntake,DepFees)values(@DN,@DI,@Df)", con);
                    cmd.Parameters.AddWithValue("@DN", DepNamePTB.Text);
                    cmd.Parameters.AddWithValue("@DI", DepIntake.Text);
                    cmd.Parameters.AddWithValue("@Df", DepFeesTB.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Departement Added");
                    con.Close();
                    ShowDep();
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
            if (DepNamePTB.Text == "" || DepIntake.Text == "" || DepFeesTB.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Update DepartementTB1 set DepName=@DN,DepIntake=@DI,DepFees=@Df Where DepNum = @Dkey", con);
                    cmd.Parameters.AddWithValue("@DN", DepNamePTB.Text);
                    cmd.Parameters.AddWithValue("@DI", DepIntake.Text);
                    cmd.Parameters.AddWithValue("@Df", DepFeesTB.Text);
                    cmd.Parameters.AddWithValue("@Dkey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Departement Updated");
                    con.Close();
                    ShowDep();
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
                MessageBox.Show("Select the Departement");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from DepartementTB1 Where DepNum=@Dkey", con);
                    cmd.Parameters.AddWithValue("@Dkey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Departement Deleted");
                    con.Close();
                    ShowDep();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Student obj = new Student();
            obj.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Professor obj = new Professor();
            obj.Show();
            this.Hide();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Home obj = new Home();
            obj.Show();
            this.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Departement obj = new Departement();
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
        int key = 0;
        private void DepDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

                DepNamePTB.Text = DepDGV.SelectedRows[0].Cells[1].Value.ToString();
                DepIntake.Text = DepDGV.SelectedRows[0].Cells[2].Value.ToString();
                DepFeesTB.Text = DepDGV.SelectedRows[0].Cells[3].Value.ToString();
                if (DepNamePTB.Text == "")
                {
                    key = 0;
                }
                else
                {
                    key = Convert.ToInt32(DepDGV.SelectedRows[0].Cells[0].Value.ToString());
                }
            }
        }
    }
