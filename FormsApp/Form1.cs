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
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
            CountStudents();
            CountDepartement();
            CountColleges();
            CountProfessor();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\OneDrive\Documents\DatabaseProject.mdf;Integrated Security=True;Connect Timeout=30");
        
        private void CountStudents() {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select count (*) From StudentTB1", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            StdLbl.Text = dt.Rows[0][0].ToString();
            con.Close();
        }

        private void CountDepartement()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select count (*) From DepartementTB1", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            DepLbl.Text = dt.Rows[0][0].ToString();
            con.Close();
        }
        private void CountColleges()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select count (*) From CollegeTbl", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            CollegeLbl.Text = dt.Rows[0][0].ToString();
            con.Close();
        }
        private void CountProfessor()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select count (*) From ProfessorTBl", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            ProfLbl.Text = dt.Rows[0][0].ToString();
            con.Close();
        }
        private void label2_Click(object sender, EventArgs e)
        {
            Student obj = new Student();
            obj.Show();
            this.Hide();
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

        private void label1_Click(object sender, EventArgs e)
        {
            Home obj = new Home();
            obj.Show();
            this.Hide();
        }

        private void label13_Click(object sender, EventArgs e)
        {
            Courses obj = new Courses();
            obj.Show();
            this.Hide();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            Colleges obj = new Colleges();
            obj.Show();
            this.Hide();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Fees obj = new Fees();
            obj.Show();
            this.Hide();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Salary obj = new Salary();
            obj.Show();
            this.Hide();
        }


    }
}
