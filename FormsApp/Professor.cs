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
    public partial class Professor : Form
    {
        public Professor()
        {
            InitializeComponent();
            Showprofs();
            GetDepID();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\OneDrive\Documents\DatabaseProject.mdf;Integrated Security=True;Connect Timeout=30");
        private void GetDepID()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select DepNum From DepartementTB1", con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("DepNum", typeof(int));
            dt.Load(Rdr);
            PrDepIdCb.ValueMember = "DepNum";
            PrDepIdCb.DataSource = dt;
            con.Close();
        }
        private void GetDepName()
        {
            con.Open();
            String Query = "Select * from DepartementTB1 where DepNum=" + PrDepIdCb.SelectedValue.ToString();
            SqlCommand cmd = new SqlCommand(Query, con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                PrDepNameTb.Text = dr["DepName"].ToString();
            }
            con.Close();
        }



        private void Showprofs()
        {
            con.Open();
            String query = "Select * From ProfessorTBl";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            PrDGV.DataSource = ds.Tables[0];
            con.Close();
        }
        private void reset()
        {
            PrNameTb.Text = "";
            PrGenCb.SelectedIndex = -1;
            PrDepNameTb.Text = "";
            PrSalTb.Text = "";
            PrAddTb.Text = "";
            PrExTb.Text = "";
            PrQualCb.SelectedIndex = -1;
        }
        private void PrDepIdCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetDepName();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Departement obj = new Departement();
            obj.Show();
            this.Hide(); 
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Student obj = new Student();
            obj.Show();
            this.Hide(); 
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (PrNameTb.Text == "" || PrAddTb.Text == "" || PrDepNameTb.Text == "" || PrGenCb.SelectedIndex == -1 || PrSalTb.Text == "" || PrQualCb.SelectedIndex == -1 || PrDepIdCb.SelectedIndex == -1 || PrExTb.Text=="" )
            {
                MessageBox.Show("Missing information");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into ProfessorTBl (PrName,PrDOB,PrGen,PrAdd,PrDepId,PrDepName,PrExp,PrSalary,PrQual)values(@Pn,@Pdo,@Pg,@Pa,@Pdi,@Pdn,@Pe,@Ps,@Pq)", con);
                    cmd.Parameters.AddWithValue("@Pn", PrNameTb.Text);
                    cmd.Parameters.AddWithValue("@Pdo", PrDOB.Value.Date);
                    cmd.Parameters.AddWithValue("@Pg", PrGenCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Pa", PrAddTb.Text);
                    cmd.Parameters.AddWithValue("@Pdi", PrDepIdCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@Pdn", PrDepNameTb.Text);
                    cmd.Parameters.AddWithValue("@Pe", PrExTb.Text);
                    cmd.Parameters.AddWithValue("@Ps", PrSalTb.Text);
                    cmd.Parameters.AddWithValue("@Pq", PrQualCb.SelectedItem.ToString());
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Professor Added");
                    con.Close();
                    Showprofs();
                    reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (PrNameTb.Text == "" || PrAddTb.Text == "" || PrDepNameTb.Text == "" || PrGenCb.SelectedIndex == -1 || PrSalTb.Text == "" || PrQualCb.SelectedIndex == -1 || PrDepIdCb.SelectedIndex == -1 || PrExTb.Text == "")
            {
                MessageBox.Show("Missing information");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand ("Update ProfessorTBl Set PrName=@Pn,PrDOB=@Pdo,PrGen=@Pg,PrAdd=@Pa,PrDepId=@Pdi,PrDepName=@Pdn,PrExp=@Pe,PrSalary=@Ps,PrQual=@Pq where PrNum=@Pkey", con);
                    cmd.Parameters.AddWithValue("@Pn", PrNameTb.Text);
                    cmd.Parameters.AddWithValue("@Pdo", PrDOB.Value.Date);
                    cmd.Parameters.AddWithValue("@Pg", PrGenCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Pa", PrAddTb.Text);
                    cmd.Parameters.AddWithValue("@Pdi", PrDepIdCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@Pdn", PrDepNameTb.Text);
                    cmd.Parameters.AddWithValue("@Pe", PrExTb.Text);
                    cmd.Parameters.AddWithValue("@Ps", PrSalTb.Text);
                    cmd.Parameters.AddWithValue("@Pq", PrQualCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@PKey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Information Updated");
                    con.Close();
                    Showprofs();
                    reset();
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
                MessageBox.Show("Select the Professor ");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from ProfessorTBl Where PrNum=@Pkey", con);
                    cmd.Parameters.AddWithValue("@Pkey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Student Deleted");
                    con.Close();
                    Showprofs();
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
        int key = 0;

        private void SalDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
                PrNameTb.Text = PrDGV.SelectedRows[0].Cells[1].Value.ToString();
                PrDOB.Text = PrDGV.SelectedRows[0].Cells[2].Value.ToString();
                PrGenCb.SelectedItem = PrDGV.SelectedRows[0].Cells[3].Value.ToString();
                PrAddTb.Text = PrDGV.SelectedRows[0].Cells[4].Value.ToString();
                PrQualCb.SelectedItem = PrDGV.SelectedRows[0].Cells[5].Value.ToString();
                PrExTb.Text = PrDGV.SelectedRows[0].Cells[6].Value.ToString();
                PrDepIdCb.SelectedValue = PrDGV.SelectedRows[0].Cells[7].Value.ToString();
                PrDepNameTb.Text = PrDGV.SelectedRows[0].Cells[8].Value.ToString();
                PrSalTb.Text = PrDGV.SelectedRows[0].Cells[9].Value.ToString();
                if (PrNameTb.Text == "")
                {
                    key = 0;
                }
                else
                {
                    key = Convert.ToInt32(PrDGV.SelectedRows[0].Cells[0].Value.ToString());
                }
            }
        }
    }
