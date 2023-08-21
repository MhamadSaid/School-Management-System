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
    public partial class Courses : Form
    {
        public Courses()
        {
            InitializeComponent();
            GetDepID();
            ShowCourses();
            GetProfId();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\OneDrive\Documents\DatabaseProject.mdf;Integrated Security=True;Connect Timeout=30");

        private void GetProfId()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select PrNum From ProfessorTBl", con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("PrNum", typeof(int));
            dt.Load(Rdr);
            PrIdCb.ValueMember = "PrNum";
            PrIdCb.DataSource = dt;
            con.Close();
        }

        private void GetProfName()
        {
            con.Open();
            String Query = "Select * from ProfessorTBl where PrNum=" + PrIdCb.SelectedValue.ToString();
            SqlCommand cmd = new SqlCommand(Query, con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows) {
                ProfTb.Text = dr["PrName"].ToString();
            }
            con.Close();
        }
        private void GetDepID()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select DepNum From DepartementTB1", con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("DepNum", typeof(int));
            dt.Load(Rdr);
            DepIdCb.ValueMember = "DepNum";
            DepIdCb.DataSource = dt;
            con.Close();
        }
        private void GetDepName()
        {
            con.Open();
            String Query = "Select * from DepartementTB1 where DepNum=" + DepIdCb.SelectedValue.ToString();
            SqlCommand cmd = new SqlCommand(Query, con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                DepNameTb.Text = dr["DepName"].ToString();
            }
            con.Close();
        }

        private void ShowCourses()
        {
            con.Open();
            String query = "Select * From CourseTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            CourseDGV.DataSource = ds.Tables[0];
            con.Close();
        }
        private void reset()
        {
            CNameTb.Text = "";
            PrIdCb.SelectedIndex = -1;
            DurationTb.Text = "";
            DepIdCb.SelectedIndex = -1;
            ProfTb.Text = "";
            DepNameTb.Text = "";
        }
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (CNameTb.Text == "" || DurationTb.Text == "" || DepIdCb.SelectedIndex == -1 || DepNameTb.Text == "" || PrIdCb.SelectedIndex == -1 || ProfTb.Text == "" )
            {
                MessageBox.Show("Missing information");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into CourseTbl (CName,CDuration,CDepId,CDepName,CProfID,CPrName)values(@cn,@cd,@cdi,@cdn,@cPid,@cPN)", con);
                    cmd.Parameters.AddWithValue("@cn", CNameTb.Text);
                    cmd.Parameters.AddWithValue("@cd", DurationTb.Text);
                    cmd.Parameters.AddWithValue("@cdi", DepIdCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@cdn", DepNameTb.Text);
                    cmd.Parameters.AddWithValue("@cPid", PrIdCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@cPN", ProfTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Courses Added");
                    con.Close();
                    ShowCourses();
                    reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void DepIdCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetDepName();
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

        private void label3_Click(object sender, EventArgs e)
        {
            Departement obj = new Departement();
            obj.Show();
            this.Hide();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        

        private void PrIdCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetProfName();
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (CNameTb.Text == "" || DurationTb.Text == "" || DepIdCb.SelectedIndex == -1 || DepNameTb.Text == "" || PrIdCb.SelectedIndex == -1 || ProfTb.Text == "")
            {
                MessageBox.Show("Missing information");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Update CourseTbl set CName=@cn,CDuration=@cd,CDepId=@cdi,CDepName=@cdn,CProfID=@cPid,CPrName=@cPN where CNum=@Ckey", con);
                    cmd.Parameters.AddWithValue("@cn", CNameTb.Text);
                    cmd.Parameters.AddWithValue("@cd", DurationTb.Text);
                    cmd.Parameters.AddWithValue("@cdi", DepIdCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@cdn", DepNameTb.Text);
                    cmd.Parameters.AddWithValue("@cPid", PrIdCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@cPN", ProfTb.Text);
                    cmd.Parameters.AddWithValue("@Ckey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Courses Edited");
                    con.Close();
                    ShowCourses();
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
                MessageBox.Show("Select the Courses");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from CourseTbl Where CNum=@Ckey", con);
                    cmd.Parameters.AddWithValue("@Ckey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Courses Deleted");
                    con.Close();
                    ShowCourses();
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
        private void CourseDGV_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
                CNameTb.Text = CourseDGV.SelectedRows[0].Cells[1].Value.ToString();
                DurationTb.Text = CourseDGV.SelectedRows[0].Cells[2].Value.ToString();
                DepIdCb.SelectedItem = CourseDGV.SelectedRows[0].Cells[3].Value.ToString();
                DepNameTb.Text = CourseDGV.SelectedRows[0].Cells[4].Value.ToString();
                PrIdCb.SelectedValue = CourseDGV.SelectedRows[0].Cells[5].Value.ToString();
                ProfTb.Text = CourseDGV.SelectedRows[0].Cells[6].Value.ToString();
                if (CNameTb.Text == "")
                {
                    key = 0;
                }
                else
                {
                    key = Convert.ToInt32(CourseDGV.SelectedRows[0].Cells[0].Value.ToString());
                }
            }
    }
}
