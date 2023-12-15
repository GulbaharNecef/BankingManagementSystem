using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankingManagementSystem
{
    public partial class Splash : Form
    {
        public Splash()
        {
            InitializeComponent();
            timer1.Start();  // Start the timer when the form loads
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (progressBar1.Value < 100)
                {
                    progressBar1.Value += 10;
                }
                else
                {
                    timer1.Stop();
                    MessageBox.Show("Progress Completed");
                    //close splash screen
                    //Close();
                    Hide();
                    Login login = new Login();
                    login.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }
    }
}
