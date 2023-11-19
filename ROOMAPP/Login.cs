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

namespace ROOMAPP
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        private Form1 mainForm = null;
        public Login (Form callingForm)
        {
            mainForm = callingForm as Form1;
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button_login_Click(object sender, EventArgs e)
        {
            
            if (txt_username.Text == "")
            {
                MessageBox.Show("Enter Username");
            }
            else if(txt_password.Text=="")
            {
                MessageBox.Show("Enter Password");
            }
            else
            {
                try
                {
                    SqlConnection conn = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ROOMAPP;Integrated Security=True;");
                    SqlCommand cmd = new SqlCommand("Select * from tbl_login where username = @username and password = @password", conn);
                    cmd.Parameters.AddWithValue("@username", txt_username.Text);
                    cmd.Parameters.AddWithValue ("@password", txt_password.Text);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();

                    da.Fill(dt);
                    if(dt.Rows.Count > 0)
                    {
                        MessageBox.Show("Login Successful");
                        Form1 mainForm = new Form1();
                        mainForm.LabelText = txt_username.Text;
                        mainForm.Show();                        
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Login Unsuccessful");
                    }
                }
                catch(Exception ex) 
                {
                    MessageBox.Show(ex.Message);
                }
                
            }

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txt_username_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_password_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
