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

namespace RSSupplies.Pages
{
    public partial class Supplies : Form
    {
        public Supplies()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Supplies_Load(object sender, EventArgs e)
        {

        }

        private List<string[]> ReadFile(string filePath)
        {

            using (StreamReader reader = new StreamReader(filePath))
            {
                List<string[]> file = new List<string[]>();

                string line = "";
                while ((line = reader.ReadLine()) != null)
                {
                    file.Add(line.Split(','));
                }

                return file;
            }
        }
    }
}
