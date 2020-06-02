using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Password
{
    public partial class AddAccount : Form
    {
        Main man;
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789qwertyuiopasdfghjklzxcvbnm!@#$%^&*";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public AddAccount(Main ma)
        {
            InitializeComponent();
            this.man = ma;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
        }


        private void bunifuButton1_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(bunifuTextBox1.Text))
            {
                MessageBox.Show("Please enter in the designated website!");
                return;
            }
            if (string.IsNullOrEmpty(bunifuTextBox2.Text))
            {
                MessageBox.Show("Please enter in a username!");
                return;
            }
            if (string.IsNullOrEmpty(bunifuTextBox3.Text))
            {
                MessageBox.Show("Please enter in a password!");
                return;
            }
            else
            {
                MySqlConnection conn = new MySqlConnection("datasource=localhost;port=3306;username=root;password=");
                string query = "INSERT into newcreds.noob(website, username, password) VALUES('" + bunifuTextBox1.Text + "', '" + bunifuTextBox2.Text + "', '" + bunifuTextBox3.Text + "')";
                conn.Open();

                try
                {
                    MySqlCommand command = new MySqlCommand(query, conn);

                    if (command.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("Success");
                        this.Close();

                        Main man = new Main();
                        this.Close();
                        man.Show();
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }
        

        public void Main_Load(object sender, EventArgs e)
        {

        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            Main man = new Main();
            this.Close();
            man.Show();
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            bunifuTextBox3.Text = RandomString(20);
        }
    }
}
