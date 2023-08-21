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
    public partial class Student : Form
    {
        public Student()
        {
            InitializeComponent();
            ShowStudents();
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
            StdDepIdCb.ValueMember = "DepNum";
            StdDepIdCb.DataSource = dt;
            con.Close();
        }
        private void GetDepName()
        {
            con.Open();
            String Query = "Select * from DepartementTB1 where DepNum=" + StdDepIdCb.SelectedValue.ToString();
            SqlCommand cmd = new SqlCommand(Query, con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach(DataRow dr in dt.Rows)
            {
                DepNameTb.Text = dr["DepName"].ToString();
            }
            con.Close();
        }



        private void ShowStudents()
        {
            con.Open();
            String query = "Select * From StudentTB1";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            StdDGV.DataSource = ds.Tables[0];
            con.Close();
        }
        private void reset()
        {
            StdTb.Text = "";
            StdGenCb.SelectedIndex = - 1;
            DepNameTb.Text = "";
            StdDepIdCb.SelectedIndex = -1;                        
            PhoneTb.Text = "";
            StdAddTb.Text="";
        }
        
        

        private void StdDepIdCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetDepName();
        }

        private void StdSave_Click(object sender, EventArgs e)
        {
                if (StdTb.Text == "" || StdAddTb.Text == "" || DepNameTb.Text == "" || StdAddTb.Text == "" || PhoneTb.Text == "" || StdGenCb.SelectedIndex == -1 || StdDepIdCb.SelectedIndex == -1 || SemCb.SelectedIndex == -1)
                {
                    MessageBox.Show("Missing information");
                }
                else
                {
                    try
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("insert into StudentTB1 (StName,StDOB,StGen,StAddress,StDepId,StDepName,StPhone,StSem)values(@sn,@so,@sg,@sa,@sdi,@sdn,@sp,@ss)", con);
                        cmd.Parameters.AddWithValue("@sn", StdTb.Text);
                        cmd.Parameters.AddWithValue("@so", StdDob.Value.Date);
                        cmd.Parameters.AddWithValue("@sg", StdGenCb.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@sa", StdAddTb.Text);
                        cmd.Parameters.AddWithValue("@sdi", StdDepIdCb.SelectedValue.ToString());
                        cmd.Parameters.AddWithValue("@sdn", DepNameTb.Text);
                        cmd.Parameters.AddWithValue("@sp", PhoneTb.Text);
                        cmd.Parameters.AddWithValue("@ss", SemCb.SelectedItem.ToString());
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Information Added");
                        con.Close();
                        ShowStudents();
                        reset();
                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show(Ex.Message);
                    }
                }
            }

        private void StdDelete_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Select the Student");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from StudentTB1 Where StNum=@Skey", con);
                    cmd.Parameters.AddWithValue("@Skey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Student Deleted");
                    con.Close();
                    ShowStudents();
                    reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        
        private void StdEdit_Click(object sender, EventArgs e)
        {
            if (StdTb.Text == "" || StdAddTb.Text == "" || DepNameTb.Text == "" || StdAddTb.Text == "" || PhoneTb.Text == "" || StdGenCb.SelectedIndex == -1 || StdDepIdCb.SelectedIndex == -1 || SemCb.SelectedIndex == -1)
            {
                MessageBox.Show("Missing information");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Update StudentTB1 Set StName=@sn,StDOB=@so,StGen=@sg,StAddress=@sa,StDepId=@sdi,StDepName=@sdn,StPhone=@sp,StSem=@ss where StNum=@Skey", con);
                    cmd.Parameters.AddWithValue("@sn", StdTb.Text);
                    cmd.Parameters.AddWithValue("@so", StdDob.Value.Date);
                    cmd.Parameters.AddWithValue("@sg", StdGenCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@sa", StdAddTb.Text);
                    cmd.Parameters.AddWithValue("@sdi", StdDepIdCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@sdn", DepNameTb.Text);
                    cmd.Parameters.AddWithValue("@sp", PhoneTb.Text);
                    cmd.Parameters.AddWithValue("@ss", SemCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Skey",key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Student Updated");
                    con.Close();
                    ShowStudents();
                    reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Departement obj = new Departement();
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

        private void label2_Click(object sender, EventArgs e)
        {
            Student obj = new Student();
            obj.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Courses obj = new Courses();
            obj.Show();
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Home obj = new Home();
            obj.Show();
            this.Hide();
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

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
        int key = 0;
        private void SalDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
                StdTb.Text = StdDGV.SelectedRows[0].Cells[1].Value.ToString();
                StdDob.Text = StdDGV.SelectedRows[0].Cells[2].Value.ToString();
                StdGenCb.SelectedItem = StdDGV.SelectedRows[0].Cells[3].Value.ToString();
                StdAddTb.Text = StdDGV.SelectedRows[0].Cells[4].Value.ToString();
                StdDepIdCb.SelectedValue = StdDGV.SelectedRows[0].Cells[5].Value.ToString();
                DepNameTb.Text = StdDGV.SelectedRows[0].Cells[6].Value.ToString();
                PhoneTb.Text = StdDGV.SelectedRows[0].Cells[7].Value.ToString();
                SemCb.SelectedItem = StdDGV.SelectedRows[0].Cells[8].Value.ToString();
                if (StdTb.Text == "")
                {
                    key = 0;
                }
                else
                {
                    key = Convert.ToInt32(StdDGV.SelectedRows[0].Cells[0].Value.ToString());
                }
            }

        }
    }
    

