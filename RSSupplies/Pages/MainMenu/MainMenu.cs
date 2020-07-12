using System;
using System.Windows.Forms;

namespace RSSupplies.Pages.MainMenu
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
            MainMenuFunctions.EnableStart(this);
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
            MainMenuFunctions.EnableStart(this);
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
            MainMenuFunctions.LoadPresetFile("Preset1.csv", this);
        }


        private void Button6_Click(object sender, EventArgs e)
        {
            MainMenuFunctions.LoadPresetFile("Preset2.csv", this);
        }


        private void Button3_Click(object sender, EventArgs e)
        {
            //groups button
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            if (!MainMenuFunctions.IsFormOpen("Supplies"))
            {
                RSSupplies.Pages.Supplies.Supplies sup = new RSSupplies.Pages.Supplies.Supplies();
                sup.Show();
            }
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            MainMenuFunctions.SavePresetFile("Preset1.csv", this);
        }

        private void Button8_Click(object sender, EventArgs e)
        {

            MainMenuFunctions.SavePresetFile("Preset2.csv", this);
        }


    }
}
