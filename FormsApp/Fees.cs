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
    public partial class Fees : Form
    {
        public Fees()
        {
            InitializeComponent();
            ShowFees();
            GetStudentId();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\OneDrive\Documents\DatabaseProject.mdf;Integrated Security=True;Connect Timeout=30");

        private void ShowFees()
        {
            con.Open();
            String query = "Select * from FeesTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            FeesDGV.DataSource = ds.Tables[0];
            con.Close();

        }
        private void Reset()
        {
            StNameTb.Text = "";
            StdIdCb.SelectedItem = -1;
            DepNameTb.Text = "";
            FeAmTb.Text = "";
        }
        private void GetStudentId()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select StNum From StudentTB1", con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("StNum", typeof(int));
            dt.Load(Rdr);
            StdIdCb.ValueMember = "StNum";
            StdIdCb.DataSource = dt;
            con.Close();
        }
        private void GetStudentName()
        {
            con.Open();
            String Query = "Select * from StudentTB1 where StNum=" + StdIdCb.SelectedValue.ToString();
            SqlCommand cmd = new SqlCommand(Query, con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                StNameTb.Text = dr["StName"].ToString();
                DepNameTb.Text = dr["StDepName"].ToString();
            }
            con.Close();
        }

        private void StdIdCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetStudentName();
        }

        private void FeePayBtn_Click(object sender, EventArgs e)
        {
            if (StNameTb.Text == "" || FeAmTb.Text == "" || DepNameTb.Text == "")

            {
                MessageBox.Show("Missing information");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into FeesTbl (StNum,StName,StDep,FPeriod,FAmount,PayDate)values(@Snu,@Sna,@Sdn,@fp,@fa,@Fpd)", con);
                    cmd.Parameters.AddWithValue("@Snu", StdIdCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@Sna", StNameTb.Text);
                    cmd.Parameters.AddWithValue("@Sdn", DepNameTb.Text);
                    cmd.Parameters.AddWithValue("@fp", FePrCb.SelectedItem);
                    cmd.Parameters.AddWithValue("@fa", FeAmTb.Text);
                    cmd.Parameters.AddWithValue("@Fpd", DateTime.Today.Date);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Fees Added");
                    con.Close();
                    ShowFees();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void FeeEdit_Click(object sender, EventArgs e)
        {
            if (StNameTb.Text == "" || FeAmTb.Text == "" || DepNameTb.Text == "")
            {
                MessageBox.Show("Missing information");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Update FeesTbl Set StNum=@Snu,StName=@Sna,StDep=@Sdn,FPeriod=@fp,FAmount=@fa,PayDate=@Fpd where FNum=@Fkey", con);
                    cmd.Parameters.AddWithValue("@Snu", StdIdCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@Sna", StNameTb.Text);
                    cmd.Parameters.AddWithValue("@Sdn", DepNameTb.Text);
                    cmd.Parameters.AddWithValue("@fp", FePrCb.SelectedItem);
                    cmd.Parameters.AddWithValue("@fa", FeAmTb.Text);
                    cmd.Parameters.AddWithValue("@Fpd", DateTime.Today.Date);
                    cmd.Parameters.AddWithValue("@Fkey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Fees Updated");
                    con.Close();
                    ShowFees();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }


        private void FeeResetBtn_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Select the Fees");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from FeesTbl Where FNum=@Fkey", con);
                    cmd.Parameters.AddWithValue("@Fkey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Fees Deleted");
                    con.Close();
                    ShowFees();
                    Reset();
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
        private void FeesDGV_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
                StdIdCb.SelectedValue = FeesDGV.SelectedRows[0].Cells[1].Value.ToString();
                StNameTb.Text = FeesDGV.SelectedRows[0].Cells[2].Value.ToString();
                DepNameTb.Text = FeesDGV.SelectedRows[0].Cells[3].Value.ToString();
                FePrCb.SelectedItem = FeesDGV.SelectedRows[0].Cells[4].Value.ToString();
                FeAmTb.Text = FeesDGV.SelectedRows[0].Cells[5].Value.ToString();
                if (StNameTb.Text == "")
                {
                    key = 0;
                }
                else
                {
                    key = Convert.ToInt32(FeesDGV.SelectedRows[0].Cells[0].Value.ToString());
                }
            
        }
    }
}
