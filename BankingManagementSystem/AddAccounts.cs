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

namespace BankingManagementSystem
{
    public partial class AddAccounts : Form
    {
        public AddAccounts()
        {
            InitializeComponent();
            DisplayAccounts();
        }

        //SqlConnection con = new SqlConnection(@"Server=localhost;Database=BankDb;TrustServerCertificate=true;Trusted_Connection=true;");
        //SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=BankDb;Integrated Security=true;");
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Asus\Documents\BankDb.mdf;Integrated Security=True;Connect Timeout=30;");

        private void DisplayAccounts()
        {
            con.Open();
            string query = "select * from AccountTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            AccountDGV.DataSource = ds.Tables[0];//The data source of a DataGridView (AccountDGV) is set to the first table in the DataSet
            con.Close();
        }
        private void Reset()
        {
            AcNameTb.Text = "";
            AcPhoneTb.Text = ""; //string.Empty?;
            AcAddressTb.Text = "";
            GenderCb.SelectedIndex = -1;
            OccupationTb.Text = "";
            IncomeTb.Text = "";
            EducationCb.SelectedIndex = -1;
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Exit Application", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
        private void SubmitBtn_Click(object sender, EventArgs e)
        {
            if(AcNameTb.Text == "" || AcPhoneTb.Text == "" || AcAddressTb.Text == "" || GenderCb.SelectedIndex == -1 || OccupationTb.Text == "" || EducationCb.SelectedIndex == -1 || IncomeTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Insert into AccountTbl(ACName, ACPhone, ACAddress, ACGen, ACOccup, ACEduc, ACInc, ACBalance)values(@AN, @AP, @AA, @AG, @AO,@AE, @AI, @AB)", con);
                    cmd.Parameters.AddWithValue("@AN", AcNameTb.Text);
                    cmd.Parameters.AddWithValue("@AP", AcPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@AA", AcAddressTb.Text);
                    cmd.Parameters.AddWithValue("@AG", GenderCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@AO", OccupationTb.Text);
                    cmd.Parameters.AddWithValue("@AE", EducationCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@AI", IncomeTb.Text);
                    cmd.Parameters.AddWithValue("@AB", 0);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Account Created!");
                    con.Close();
                    Reset();//after account added reset datas in textboxes
                    DisplayAccounts();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {

            if (key == 0)
            {
                MessageBox.Show("Select the Account");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from AccountTbl where ACNum =@AcKey", con);
                    cmd.Parameters.AddWithValue("@AcKey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Account Deleted!");
                    con.Close();
                    Reset();//after account added reset datas in textboxes
                    DisplayAccounts();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        int key = 0;    
        private void AccountDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            AcNameTb.Text = AccountDGV.SelectedRows[0].Cells[1].Value.ToString();
            AcPhoneTb.Text = AccountDGV.SelectedRows[0].Cells[2].Value.ToString();
            AcAddressTb.Text = AccountDGV.SelectedRows[0].Cells[3].Value.ToString();
            GenderCb.SelectedItem = AccountDGV.SelectedRows[0].Cells[4].Value.ToString();
            OccupationTb.Text = AccountDGV.SelectedRows[0].Cells[5].Value.ToString();
            EducationCb.SelectedItem = AccountDGV.SelectedRows[0].Cells[6].Value.ToString();
            IncomeTb.Text = AccountDGV.SelectedRows[0].Cells[7].Value.ToString();
            if(AcNameTb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(AccountDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }
    }
}
