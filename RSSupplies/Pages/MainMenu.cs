using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RSSupplies
{
    public partial class MainMenu : Form
    {

        Tracker tracker;
        internal ImageCycle InventoryOne;
        internal ImageCycle InventoryTwo;

        public MainMenu()
        {
            InitializeComponent();
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            Data.Boxes.Add(new Box());
            Data.Boxes.Add(new Box());

            Setup.ReadInfoCSV();

            startButton.Enabled = false;
            stopButton.Enabled = false;

        }




        private void Button1_Click(object sender, EventArgs e)
        {

            TakeScreenshotAsync(0);


        }

        private void Button2_Click(object sender, EventArgs e)
        {
            TakeScreenshotAsync(1);

        }


        private void TakeScreenshotAsync(int inventory)
        {

            Screenshot s = new Screenshot(inventory, this);

        }


        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            EnableStart();
        }

        internal void EnableStart()
        {
            cbBeastOfBurden.Enabled = true;

            if (cbBeastOfBurden.Checked == true)
            {
                if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" ||
                   textBox5.Text == "" || textBox6.Text == "" || textBox7.Text == "" || textBox8.Text == "")

                    startButton.Enabled = false;
                else
                    startButton.Enabled = true;
            }
            else
            {
                if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "")
                    startButton.Enabled = false;
                else
                    startButton.Enabled = true;

            }
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            startButton.Enabled = false;
            stopButton.Enabled = true;
            cbBeastOfBurden.Enabled = false;

            tracker = new Tracker(this);
            tracker.Show();

            InventoryOne = new ImageCycle(0);

            if (cbBeastOfBurden.Checked)
            {
                InventoryTwo = new ImageCycle(1);
            }

        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            EnableStart();
            stopButton.Enabled = false;
            InventoryOne.isRunning = false;

            if (cbBeastOfBurden.Checked)
            {
                InventoryTwo.isRunning = false;
            }
            tracker.Dispose();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            LoadPresetFile("Preset1.csv");
        }


        private void Button6_Click(object sender, EventArgs e)
        {
            LoadPresetFile("Preset2.csv");
        }


        private void LoadPresetFile(string filePath)
        {
            List<string[]> file = ReadFile(filePath);
            UpdateBoxes(file);

        }

        private void UpdateBoxes(List<string[]> file)
        {
           // Data.Boxes[0];
        }

        private List<string[]> ReadFile(string filePath)
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

        private void Button3_Click(object sender, EventArgs e)
        {

        }

        private void Button4_Click(object sender, EventArgs e)
        {
            if (!IsFormOpen("Supplies"))
            {
                Supplies sup = new Supplies();
                sup.Show();
            }
        }

        private bool IsFormOpen(string formName)
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
