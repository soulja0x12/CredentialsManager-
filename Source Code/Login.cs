using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Data;
using System.Linq;
using CredsManager.Properties;
using System.Windows.Forms;
using System.IO;
using System.Text;
using System.Threading;

namespace Password
{
    public partial class Login : Form
    {


        public static string enckeypath = @"C:\random.txt";
        public static string deckeypath = @"C:\random.txt.bytes";
        public static string pass = "dylanmadeit";
        public Login()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
        }

        public static void WriteKey()
        {
            FileStream fs1 = new FileStream(enckeypath, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter writer = new StreamWriter(fs1);
            writer.Write(RandomString(32));
            writer.Close();
        }


        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789qwertyuiopasdfghjklzxcvbnm!@#$%^&*";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        private void Form1_Load(object sender, EventArgs e)
        {

            if (File.Exists(deckeypath))
            {
                File.Delete(deckeypath);
            }
            if (Settings.Default.FirstUse == true)
            {
                WriteKey();
                string firstkey = ReadKey(enckeypath);
                Clipboard.SetText(firstkey);
                MessageBox.Show("Your key has been copied to your clipboard. Here is your key: " + firstkey, "AnonCreds", MessageBoxButtons.OK);
            }
            if (!File.Exists(enckeypath))
            {
                string firstkey = ReadKey(enckeypath);
                Clipboard.SetText(firstkey);
                MessageBox.Show("Your key has been copied to your clipboard. Here is your key: " + firstkey, "AnonCreds", MessageBoxButtons.OK);
            }
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(bunifuTextBox1.Text))
            {
                MessageBox.Show("Please the encryption key!");
                return;
            }

            if (bunifuTextBox1.Text == ReadKey(enckeypath))
            {
                Encryption.EncryptFile(enckeypath, pass);
                MessageBox.Show("Successfully Logged In!!");
                File.Delete(enckeypath);
                Main man = new Main();
                man.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Encryption key is incorrect, please try again!");
                bunifuTextBox1.Clear();
                return;
            }
        }

        public static string ReadKey(string path)
        {
            string text;
            var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                text = streamReader.ReadToEnd();
            }
            return text;
        }

        private void bunifuTextBox1_TextChanged(object sender, EventArgs e )
        {

        }

        private void bunifuTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                bunifuButton1_Click(sender, e);
            }
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            File.WriteAllText(enckeypath, string.Empty);
            FileStream fs1 = new FileStream(enckeypath, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter writer = new StreamWriter(fs1);
            writer.Write(RandomString(32));
            writer.Close();
            string newkey = ReadKey(enckeypath);
            MessageBox.Show("Your new key has been copied to your clipboard. Here is your key: " + newkey);
            Clipboard.SetText(newkey);
        }
    }
}
