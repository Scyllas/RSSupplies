using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RSSupplies.Pages.Supplies
{
    public partial class Supplies : Form
    {
        public Supplies()
        {
            InitializeComponent();
        }

        private void Supplies_Load(object sender, EventArgs e)
        {
            SuppliesFunctions.LoadGrid(dgvCSV);
        }
        

        private void Button4_Click(object sender, EventArgs e)
        {
            SuppliesFunctions.LoadGrid(dgvCSV);
            DisableModification();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Common.DeleteExistingFile("info.csv");
            SuppliesFunctions.SaveData("info.csv", dgvCSV);
            SuppliesFunctions.LoadGrid(dgvCSV);
            DisableModification();
        }



        

        private void Button3_Click(object sender, EventArgs e)
        {
            EnableModification();
        }

        private void EnableModification()
        {
            dgvCSV.ReadOnly = false;
            button2.Enabled = true;
            button4.Enabled = true;
        }

        private void DisableModification()
        {
            dgvCSV.ReadOnly = true;
            button2.Enabled = false;
            button4.Enabled = false;
        }
    }
}
