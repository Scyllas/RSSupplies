﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RSSupplies
{
    public partial class Screenshot : Form
    {
        internal bool start = false;

        int currentBoxNo = 0;

        MainMenu main;

        public Screenshot(int boxNo, MainMenu mainMenu)
        {
            InitializeComponent();

            currentBoxNo = boxNo;

            main = mainMenu;

            Show();
        }

        private void Screenshot_Load(object sender, EventArgs e)
        {
            Hide();

            Cursor = Cursors.Cross;

            Setup.TakeScreenshot(Width, Height, pictureBox1);

            Show();
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

            main.EnableStart();

            Close();
        }



        private void DrawRectangle(Box box)
        {
            pictureBox1.CreateGraphics().DrawRectangle(box.Pen, box.X, box.Y, box.Width, box.Height);
        }


    }
}
