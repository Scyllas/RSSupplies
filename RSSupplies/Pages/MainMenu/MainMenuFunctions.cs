using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RSSupplies.Pages.MainMenu
{
    class MainMenuFunctions
    {
        internal static void EnableStart(Pages.MainMenu.MainMenu mainMenu)
        {
            mainMenu.cbBeastOfBurden.Enabled = true;

            if (mainMenu.cbBeastOfBurden.Checked == true)
            {
                if (mainMenu.textBox1.Text == "" || mainMenu.textBox2.Text == "" || mainMenu.textBox3.Text == "" || mainMenu.textBox4.Text == "" ||
                   mainMenu.textBox5.Text == "" || mainMenu.textBox6.Text == "" || mainMenu.textBox7.Text == "" || mainMenu.textBox8.Text == "")

                    mainMenu.startButton.Enabled = false;
                else
                    mainMenu.startButton.Enabled = true;
            }
            else
            {
                if (mainMenu.textBox1.Text == "" || mainMenu.textBox2.Text == "" || mainMenu.textBox3.Text == "" || mainMenu.textBox4.Text == "")
                    mainMenu.startButton.Enabled = false;
                else
                    mainMenu.startButton.Enabled = true;

            }
        }

        internal static void LoadPresetFile(string filePath, Pages.MainMenu.MainMenu mainMenu)
        {
            List<string[]> file = ReadFile(filePath);
            UpdateBoxes(file, mainMenu);

        }


        private static void UpdateBoxes(List<string[]> file, Pages.MainMenu.MainMenu mainMenu)
        {
            mainMenu.textBox1.Text = file[0][0];
            mainMenu.textBox2.Text = file[0][1];
            mainMenu.textBox3.Text = file[0][2];
            mainMenu.textBox4.Text = file[0][3];

            mainMenu.textBox5.Text = file[1][0];
            mainMenu.textBox6.Text = file[1][1];
            mainMenu.textBox7.Text = file[1][2];
            mainMenu.textBox8.Text = file[1][3];
        }

        private static List<string[]> ReadFile(string filePath)
        {

            using (StreamReader reader = new StreamReader(filePath))
            {
                List<string[]> file = new List<string[]>();
                try
                {
                    file.Add(reader.ReadLine().Split(','));
                    file.Add(reader.ReadLine().Split(','));
                }
                catch { }//only one line in file
                return file;
            }
        }

        internal static void SavePresetFile(string fileName, Pages.MainMenu.MainMenu mainMenu)
        {
            Common.DeleteExistingFile(fileName);
            WritePresetFile(fileName, mainMenu);
        }

        private static void WritePresetFile(string fileName, Pages.MainMenu.MainMenu mainMenu)
        {
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                List<string> inventory = new List<string>() { mainMenu.textBox1.Text, mainMenu.textBox2.Text, mainMenu.textBox3.Text, mainMenu.textBox4.Text};
                writer.WriteLine(BuildInventory(inventory));

                if (mainMenu.cbBeastOfBurden.Checked == true)
                {
                    List<string> bob = new List<string>() { mainMenu.textBox1.Text, mainMenu.textBox2.Text, mainMenu.textBox3.Text, mainMenu.textBox4.Text };
                    writer.WriteLine(BuildInventory(bob));
                }
            }

        }

        private static string BuildInventory(List<string> inventory)
        {
            string output = "";

            foreach (string text in inventory)
            {
                output += text ?? "";
                output += ",";
            }

            output = output.Substring(0, output.Length-1);

            return output;
        }

        internal static bool IsFormOpen(string formName)
        {
            FormCollection fc = Application.OpenForms;

            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == formName)
                {
                    frm.Show();
                    frm.BringToFront();
                    return true;
                }
            }
            return false;
        }
    }
}
