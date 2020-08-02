using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RSSupplies
{
    public partial class Screenshot : Form
    {
        internal bool start = false;

        private bool isScreenshotbeingTaken = false;

        int currentBoxNo = 0;

        Pages.MainMenu.MainMenu main;

        public Screenshot(int boxNo, Pages.MainMenu.MainMenu mainMenu)
        {
            InitializeComponent();

            currentBoxNo = boxNo;

            main = mainMenu;

            Show();
        }

        private void Screenshot_Load(object sender, EventArgs e)
        {
            if (!isScreenshotbeingTaken) {

                isScreenshotbeingTaken = true;

                try
                {

                    Hide();

                    Cursor = Cursors.Cross;

                    TakeScreenshot(pictureBox1);

                    Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Issue when selecting inventory");
                    Common.Log(ex.Message);
                }
                
            }
        }

        private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
        {

            if (pictureBox1.Image == null)
                return;

            if (start)
            {

                pictureBox1.Refresh();

                Data.Boxes[currentBoxNo].SetSize(e);

                DrawRectangle(Data.Boxes[currentBoxNo]);

            }

        }

        private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            //validate when user right-click
            if (!start)
            {

                if (e.Button == MouseButtons.Left)
                {
                    Data.Boxes[currentBoxNo].SetCoord(e);
                }

                Data.Boxes[currentBoxNo].SetPen(Color.Red);

                //refresh picture box
                pictureBox1.Refresh();
                //start control variable for draw rectangle
                start = true;
            }
        }

        private void PictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            //validate if there is image
            if (pictureBox1.Image == null)
                return;
            //same functionality when mouse is over
            if (e.Button == MouseButtons.Left)
            {
                pictureBox1.Refresh();
                Data.Boxes[currentBoxNo].SetSize(e);

                DrawRectangle(Data.Boxes[currentBoxNo]);
                DialogResult isHappy = MessageBox.Show("Happy with the selected area?", "Confirm", MessageBoxButtons.YesNo);

                if (isHappy == DialogResult.Yes)
                {
                    Data.Boxes[currentBoxNo].SetTextBox(main, currentBoxNo);
                }
                else
                {
                    Data.Boxes[currentBoxNo].Reset();
                }

            }

            currentBoxNo = -1;
            main.Enabled = true;
            start = false;

            Pages.MainMenu.MainMenuFunctions.EnableStart(main);


            isScreenshotbeingTaken = false;

            Close();
        }



        private void DrawRectangle(Box box)
        {
            pictureBox1.CreateGraphics().DrawRectangle(box.Pen, box.X, box.Y, box.Width, box.Height);
        }

        internal static void TakeScreenshot(PictureBox pb)
        {

            Bitmap printscreen = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            
            Graphics graphics = Graphics.FromImage(printscreen as Image);
            graphics.CopyFromScreen(0, 0, 0, 0, printscreen.Size);

            using (MemoryStream s = new MemoryStream())
            {

                printscreen.Save(s, ImageFormat.Bmp);
                pb.Size = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);

                pb.Image = Image.FromStream(s);
            }
        }
    }
}
