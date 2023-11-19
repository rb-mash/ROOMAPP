using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ROOMAPP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();         
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            
        }

        public string LabelText
        {
            get { return Lbl.Text; }
            set { Lbl.Text = value; }
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ROOMAPP;Integrated Security=True;");

            try
            {
                conn.Open(); // Open the database connection

                // Create an SQL command to retrieve data from the "tbl_key" table
                SqlCommand cmd = new SqlCommand("SELECT * FROM key_tbl WHERE Venue = @venue", conn);
                cmd.Parameters.AddWithValue("@venue", textBox1.Text);

                // Execute the SQL command and retrieve data
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read()) // Check if there is data to read
                {
                    // Assuming you want to display a specific column (change "ColumnName" to the actual column name)
                    string displayVenue = reader["Holder"].ToString();
                    string displayTime = reader["Time"].ToString();
                    string displayStatus = reader["Status"].ToString();

                    // Display the retrieved data in label2
                    label2.Text = displayVenue;
                    lbl_time.Text = displayTime;
                    textBox2.Text = displayTime;                
                    statusComboBox.Text = displayStatus;
                }
                else
                {
                    label2.Text = "Not found!"; // Handle the case where no data is found
                    
                    lbl_time.Text = "Not found!";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                conn.Close(); // Close the database connection
            }
        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void log_out_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            this.Close();
            login.Show();
        }

        private void FIND_Click(object sender, EventArgs e)
        {

        }

        

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void saveButton_Click_1(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ROOMAPP;Integrated Security=True;");

            try
            {
                conn.Open(); 
                string possessor; 

                
                SqlCommand cmd = new SqlCommand("UPDATE key_tbl SET Status = @status, Holder = @holder, Time = @time WHERE Venue = @venue", conn);

                switch (statusComboBox.SelectedItem.ToString())
                {
                    case "In":
                        possessor = "Office";
                        break;
                    case "Out":
                        possessor = Lbl.Text;
                        break;
                   
                    default:
                        possessor = "Unknown";
                        break;
                }

                cmd.Parameters.AddWithValue("@venue", textBox1.Text);
                cmd.Parameters.AddWithValue("@status", statusComboBox.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@holder", possessor); // Use the "possessor" variable
                cmd.Parameters.AddWithValue("@time", DateTime.Now); // Set the Time field to the current timestamp

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Status, Holder, and Time updated successfully.");
                    
                    try
                    {
                        //conn.Open(); // Open the database connection

                        // Create an SQL command to retrieve data from the "tbl_key" table
                        SqlCommand cmd2 = new SqlCommand("SELECT * FROM key_tbl WHERE Venue = @venue", conn);
                        cmd2.Parameters.AddWithValue("@venue", textBox1.Text);

                        // Execute the SQL command and retrieve data
                        SqlDataReader reader = cmd2.ExecuteReader();

                        if (reader.Read()) // Check if there is data to read
                        {
                            // Assuming you want to display a specific column (change "ColumnName" to the actual column name)
                            string displayVenue = reader["Holder"].ToString();
                            string displayTime = reader["Time"].ToString();
                            string displayStatus = reader["Status"].ToString();

                            // Display the retrieved data in label2
                            label2.Text = displayVenue;
                            lbl_time.Text = displayTime;
                            textBox2.Text = displayTime;
                            statusComboBox.Text = displayStatus;
                        }
                        else
                        {
                            label2.Text = "Not found!"; // Handle the case where no data is found

                            lbl_time.Text = "Not found!";
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                    finally
                    {
                        conn.Close(); // Close the database connection
                    }
                }
                else
                {
                    MessageBox.Show("No record found for the provided Venue");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {

                conn.Close(); // Close the database connection
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            string statusFilter = comboBox1.SelectedItem.ToString(); // Get the selected status ("In" or "Out")

            // Clear the existing items in the ListView
            listViewVenues.Items.Clear();

            SqlConnection conn = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ROOMAPP;Integrated Security=True;");

            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT Venue, Holder, Time FROM key_tbl WHERE Status = @status", conn);
                cmd.Parameters.AddWithValue("@status", statusFilter);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string venue = reader["Venue"].ToString();
                    string holder = reader["Holder"].ToString();
                    string time = reader["Time"].ToString();

                    // Create a ListViewItem and add sub-items
                    ListViewItem item = new ListViewItem(venue);
                    
                    item.SubItems.Add(holder);
                    item.SubItems.Add(time);

                    // Add the item to the ListView
                    listViewVenues.Items.Add(item);
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void listViewVenues_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
