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
    public partial class Salary : Form
    {
        public Salary()
        {
            InitializeComponent();
            Showsalary();
            GetProfId();
        }

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\OneDrive\Documents\DatabaseProject.mdf;Integrated Security=True;Connect Timeout=30");

        private void Showsalary()
        {
            con.Open();
            String query = "Select * From SalaryTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            SalDGV.DataSource = ds.Tables[0];
            con.Close();
        }
        private void reset()
        {
            ProfIdCb.SelectedIndex = -1;
            SalProfNameTb.Text = "";
            SalAmTb.Text = "";
        }

        private void GetProfId()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select PrNum From ProfessorTBl", con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("PrNum", typeof(int));
            dt.Load(Rdr);
            ProfIdCb.ValueMember = "PrNum";
            ProfIdCb.DataSource = dt;
            con.Close();
        }

        private void GetProfName()
        {
            con.Open();
            String Query = "Select * from ProfessorTBl where PrNum=" + ProfIdCb.SelectedValue.ToString();
            SqlCommand cmd = new SqlCommand(Query, con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                SalProfNameTb.Text = dr["PrName"].ToString();
                SalAmTb.Text = dr["PrSalary"].ToString();
            }
            con.Close();
        }

        private void SalPayBtn_Click(object sender, EventArgs e)
        {
            if (SalAmTb.Text == "" || ProfIdCb.SelectedIndex ==-1)
            {
                MessageBox.Show("Missing information");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into SalaryTbl (PrNum,PrName,PrSalary,SPeriod,SPDate)values(@Spnu,@Spna,@Ps,@Sp,@Sd)", con);
                    cmd.Parameters.AddWithValue("@Spnu", ProfIdCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@Spna", SalProfNameTb.Text);
                    cmd.Parameters.AddWithValue("@Ps", SalAmTb.Text);
                    cmd.Parameters.AddWithValue("@Sp", SalPeriod.Value.Date);
                    cmd.Parameters.AddWithValue("@Sd", DateTime.Today.Date);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Salary Added");
                    con.Close();
                    Showsalary();
                    reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void ProfIdCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetProfName();
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (SalAmTb.Text == "" || ProfIdCb.SelectedIndex == -1)
            {
                MessageBox.Show("Missing information");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Update SalaryTbl set PrNum=@Spnu,PrName=@Spna,PrSalary=@Ps,SPeriod=@Sp,SPDate=@Sd where SNum=@Skey", con);
                    cmd.Parameters.AddWithValue("@Spnu", ProfIdCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@Spna", SalProfNameTb.Text);
                    cmd.Parameters.AddWithValue("@Ps", SalAmTb.Text);
                    cmd.Parameters.AddWithValue("@Sp", SalPeriod.Value.Date);
                    cmd.Parameters.AddWithValue("@Sd", DateTime.Today.Date);
                    cmd.Parameters.AddWithValue("@Skey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Salary Updated");
                    con.Close();
                    Showsalary();
                    reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        

        private void SalResetBtn_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Select the Salary");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from SalaryTbl Where SNum=@Skey", con);
                    cmd.Parameters.AddWithValue("@Skey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Salary Deleted");
                    con.Close();
                    Showsalary();
                    reset();
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
        private void SalDGV_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

                ProfIdCb.SelectedValue = SalDGV.SelectedRows[0].Cells[1].Value.ToString();
                SalProfNameTb.Text = SalDGV.SelectedRows[0].Cells[2].Value.ToString();
                SalAmTb.Text = SalDGV.SelectedRows[0].Cells[3].Value.ToString();
                if (SalProfNameTb.Text == "")
                {
                    key = 0;
                }
                else
                {
                    key = Convert.ToInt32(SalDGV.SelectedRows[0].Cells[0].Value.ToString());
                }
            
        }
    }
}
