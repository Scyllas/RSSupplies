using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RSSupplies
{
    public partial class Tracker : Form
    {
        internal static List<ImageWithLabel> items = new List<ImageWithLabel>();
        public static bool isGroupsPopulated = false;

        Pages.MainMenu.MainMenu parent;

        public Tracker(Pages.MainMenu.MainMenu mainMenu)
        {
            parent = mainMenu;

            InitializeComponent();

            timerUpdate.Enabled = false;
            TopMost = true;

        }

        private void Tracker_Load(object sender, EventArgs e)
        {

            Height = 40 + (Data.Groups.Count * 70);
            while (isGroupsPopulated == false) ;

            timerUpdate.Enabled = true;
            timerUpdate.Interval = 5000;
            timerUpdate.Start();

            for (int i = 0; i < Data.Groups.Count; i++)
            {
                ImageWithLabel imgLab;
                imgLab.label = new Label
                {
                    Name = Data.Groups[i].name,
                    Text = Data.Groups[i].value.ToString(),
                    Location = new Point(70, 20 + i * 70),
                    Font = new Font("Courier New", 22, FontStyle.Bold),
                    Size = new Size(150, 50),
                    Visible = true,
                    Enabled = true
                };

                imgLab.picturebox = new PictureBox
                {
                    Image = Data.Groups[i].image,
                    Size = new Size(50, 50),
                    Location = new Point(20, 20 + i * 70),
                    Visible = true,
                    Enabled = true
                };


                items.Add(imgLab);

                Controls.Add(imgLab.label);
                Controls.Add(imgLab.picturebox);

            }

        }

        internal static void UpdateValues()
        {
            foreach (Group g in Data.Groups)
            {
                foreach (ImageWithLabel imglab in items)
                {
                    try
                    {
                        if (g.name == imglab.label.Name)
                        {
                            imglab.label.Text = FormatNumber(g.value);
                        }
                    }
                    catch (Exception ex)
                    {
                        Common.Log(ex.Message);
                    }
                }
            }

        }

        private static void ResetValues()
        {
            foreach (Group g in Data.Groups)
            {
                g.value = 0;
            }
        }


        private static string FormatNumber(int num)
        {
            try
            {
                if (num >= 100000)
                    return FormatNumber(num / 1000) + "K";
                if (num >= 10000)
                {
                    return (num / 1000D).ToString("0.#") + "K";
                }
            }
            catch (Exception ex)
            {
                Common.Log(ex.Message);
            }
            return num.ToString("#,0");

        }

        private void TimerUpdate_Tick(object sender, EventArgs e)
        {
            try
            {
                ResetValues();
                Thread mainLoop = new Thread(ThreadedMainLoop);
                mainLoop.Start();

                while (mainLoop.IsAlive) ;

                UpdateValues();


            }
            catch (Exception ex)
            {
                Common.Log(ex.Message);
            }
        }

        private void ThreadedMainLoop()
        {
            try
            {
                parent.InventoryOne.IdentifyInventory();
                if (parent.cbBeastOfBurden.Checked)
                {
                    parent.InventoryTwo.IdentifyInventory();
                }
            }
            catch (Exception ex)
            {
                Common.Log(ex.Message);
            }
        }
    }


}
