using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RSSupplies.Pages.Supplies
{
    class SuppliesFunctions
    {

        internal static void LoadGrid(DataGridView dgvCSV)
        {
            List<string[]> info = ReadFile("info.csv");
            DisplayInfo(info, dgvCSV);
        }


        private static List<string[]> ReadFile(string filePath)
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

        private static void DisplayInfo(List<string[]> info, DataGridView dgvCSV)
        {
            foreach (string[] line in info)
            {
                dgvCSV.Rows.Add(line);
            }
        }

        internal static void SaveData(string filePath, DataGridView dgvCSV)
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
    }
}
