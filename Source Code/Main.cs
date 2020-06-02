using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Threading;
using System.Windows.Forms;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Drawing;

namespace Password
{
    public partial class Main : Form
    {
        public static string Website = "";
        public static string Username = "";
        public static string Password = "";

        public void AddInfo(string website, string username, string password)
        {
            bunifuDataGridView1.ColumnCount = 3;
            bunifuDataGridView1.Columns[0].Name = Website;
            bunifuDataGridView1.Columns[1].Name = Username;
            bunifuDataGridView1.Columns[2].Name = Password;
            bunifuDataGridView1.Rows.Add(website, username, password);
            bunifuDataGridView1.Update();
        }
        public Main()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
        }
        public void Main_Load(object sender, EventArgs e)
        {
            bunifuDataGridView1.DataSource = LoadData();
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            DataObject data = bunifuDataGridView1.GetClipboardContent();
            Clipboard.SetDataObject(data);
            Thread.Sleep(200);
            Application.Exit();
        }
        private void bunifuDataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu m = new ContextMenu();
                m.MenuItems.Add(new MenuItem("Cut"));
                m.MenuItems.Add(new MenuItem("Copy"));
                m.MenuItems.Add(new MenuItem("Paste"));

                int currentMouseOverRow = bunifuDataGridView1.HitTest(e.X, e.Y).RowIndex;

                if (currentMouseOverRow >= 0)
                {
                    m.MenuItems.Add(new MenuItem(string.Format("Do something to row {0}", currentMouseOverRow.ToString())));
                }

                m.Show(bunifuDataGridView1, new Point(e.X, e.Y));

            }
        }
        private void bunifuDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            AddAccount add = new AddAccount(this);
            add.Show();
            this.Hide();

        }
        public DataTable LoadData()
        {
            DataTable dt = new DataTable();
            MySqlConnection conn = new MySqlConnection("datasource=localhost;port=3306;username=root;password=");
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM newcreds.noob", conn);
            conn.Open();
            MySqlDataReader reader = cmd.ExecuteReader();
            dt.Load(reader);
            return dt;
        }


        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            int index = bunifuDataGridView1.CurrentCell.RowIndex;
            bunifuDataGridView1.Rows.RemoveAt(index);

            string id = bunifuDataGridView1.CurrentRow.Cells["id"].Value.ToString();
            string website = bunifuDataGridView1.CurrentRow.Cells["website"].Value.ToString();
            string username = bunifuDataGridView1.CurrentRow.Cells["username"].Value.ToString();
            string password = bunifuDataGridView1.CurrentRow.Cells["password"].Value.ToString();

            String query = string.Format("DELETE FROM newcreds.noob WHERE ID={0}", id);

            MySqlConnection conn = new MySqlConnection("datasource=localhost;port=3306;username=root;password=");
            conn.Open();
            MySqlCommand command = new MySqlCommand(query, conn);
            command.ExecuteNonQuery();
            conn.Close();
        }

        private void bunifuButton4_Click(object sender, EventArgs e)
        {

        }

        private void bunifuButton4_Click_1(object sender, EventArgs e)
        {
        }

        private void bunifuDataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }
        private void bunifuDataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {

        }
    }
}
