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

namespace RSSupplies
{
    public partial class Supplies : Form
    {
        public Supplies()
        {
            InitializeComponent();
        }

        private void Supplies_Load(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void LoadGrid()
        {
            List<string[]> info = ReadFile("info.csv");
            DisplayInfo(info);
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

        private void DisplayInfo(List<string[]> info)
        {
            foreach (string[] line in info)
            {
                dgvCSV.Rows.Add(line);
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            LoadGrid();
            DisableModification();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DeleteExistingFile("Supplies.csv");
            SaveData("Supplies.csv");
            LoadGrid();
            DisableModification();
        }

        private void DeleteExistingFile(string filePath)
        {
            File.Delete(filePath);
        }

        private void SaveData(string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                DataTable dt = new DataTable();
                foreach (DataGridViewColumn col in dgvCSV.Columns)
                {
                    dt.Columns.Add(col.Name);
                }

                foreach (DataGridViewRow row in dgvCSV.Rows)
                {
                    DataRow dRow = dt.NewRow();
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        dRow[cell.ColumnIndex] = cell.Value;
                    }
                    dt.Rows.Add(dRow);
                }

                foreach (DataRow dr in dt.Rows)
                {
                    string name = dr["itemName"].ToString() ?? "";
                    string fileName = dr["fileName"].ToString() ?? "";
                    string group = dr["group"].ToString() ?? "";
                    string imageMatchValue = dr["image"].ToString() ?? "";

                    string doses = dr["doses"].ToString() ?? "";
                    string statDrain = dr["statDrain"].ToString() ?? "";
                    string adrenalineDrain = dr["adrenDrain"].ToString() ?? "";
                    string healing = dr["Healing"].ToString() ?? "";


                    string output = name + ',' +
                                    fileName + ',' +
                                    group + ',' +
                                    imageMatchValue + ',' +

                                    doses + ',' +
                                    statDrain + ',' +
                                    adrenalineDrain + ',' +
                                    healing;

                    writer.WriteLine(output);
                }
            }
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
