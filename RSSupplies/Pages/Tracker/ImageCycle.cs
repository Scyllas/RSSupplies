using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Imaging;

namespace RSSupplies
{
    class ImageCycle
    {
        static readonly bool debugMode = false;
        readonly int boxNo;

        internal bool isRunning = false;

        internal ImageCycle(int boxNumber)
        {
            isRunning = true;
            boxNo = boxNumber;
        }

        internal void IdentifyInventory()
        {

            CreateScreenshot();

            CheckEverySupplyForMatches();

        }

        private void CheckEverySupplyForMatches()
        {
            foreach (Supply supply in Data.Supplies)
            {
                IsSupplyAMatch(supply);
            }
        }

        private void IsSupplyAMatch(Supply supply)
        {
            lock (supply)
            {
                int matches = CompareInventoryToSupply(supply);

                if (matches > 0)
                {
                    WhatGroupDoesSupplyBelongTo(supply, matches);

                }
            }
        }

        private void WhatGroupDoesSupplyBelongTo(Supply supply, int matches)
        {
            foreach (Group g in Data.Groups)
            {
                if (supply.group == g.name)
                {
                    g.value += (supply.healing * matches * supply.doses);
                }
            }
        }

        private int CompareInventoryToSupply(Supply supply)
        {
            int numberOfMatches = 0;
            try
            {

                    Bitmap image = new Bitmap("supplies/" + supply.fileName);
                    Bitmap coverted = image.Clone(new Rectangle(0, 0, image.Width, image.Height), PixelFormat.Format24bppRgb);

                    ExhaustiveTemplateMatching tm = new ExhaustiveTemplateMatching(0);

                    TemplateMatch[] matchings = tm.ProcessImage(Data.Boxes[boxNo].Image, coverted);

                    CleanupDebugFolders();

                    foreach (TemplateMatch m in matchings)
                    {

                        DebugMatches(m, Data.Boxes[boxNo]);
                        if (m.Similarity > supply.matchValue)
                        {
                            numberOfMatches++;
                        }
                    }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return numberOfMatches;
        }

        private static void CleanupDebugFolders()
        {

            if (debugMode)
            {
                try
                {
                    try
                    {

                        Directory.CreateDirectory("Samples");
                        DirectoryInfo di = new DirectoryInfo("Samples");

                        foreach (FileInfo file in di.GetFiles())
                        {
                            file.Delete();
                        }
                    }
                    catch (Exception ex)
                    {
                        Common.Log(ex.Message);
                    }
                    try
                    {
                        Directory.CreateDirectory("Matches");
                        DirectoryInfo di = new DirectoryInfo("Matches");

                        foreach (FileInfo file in di.GetFiles())
                        {
                            file.Delete();
                        }
                    }
                    catch (Exception ex)
                    {
                        Common.Log(ex.Message);
                    }

                }
                catch (Exception ex)
                {
                    Common.Log(ex.Message);
                }
            }
        }

        private void DebugMatches(TemplateMatch m, Box box)
        {
            if (debugMode)
            {
                try
                {
                    Rectangle cropRect = m.Rectangle;
                    Bitmap target = new Bitmap(cropRect.Width, cropRect.Height);

                    using (Graphics g = Graphics.FromImage(target))
                    {
                        g.DrawImage(box.Image, new Rectangle(0, 0, target.Width, target.Height),
                                         cropRect,
                                         GraphicsUnit.Pixel);
                    }
                    if (m.Similarity >= 0.88f)
                    {
                        target.Save(@"Matches\testscreen" + DateTime.Now.Ticks + ".jpg");
                    }
                    else
                    {
                        target.Save(@"Samples\testscreen" + DateTime.Now.Ticks + ".jpg");
                    }
                }
                catch (Exception ex)
                {
                    Common.Log(ex.Message);
                }
            }
        }

        private void CreateScreenshot()
        {

            try
            {

                if (Data.Boxes[boxNo].IsIntialised())
                {
                    CaptureScreens(Data.Boxes[boxNo]);
                }
                else
                {
                    MessageBox.Show("Inventory " + boxNo + " is not set up");
                }

            }
            catch (Exception ex)
            {
                Common.Log(ex.Message);
            }
        }

        private void CaptureScreens(Box box)
        {
            Bitmap bmp = new Bitmap(box.Width, box.Height, PixelFormat.Format24bppRgb);
            Graphics g = Graphics.FromImage(bmp);
            g.CopyFromScreen(box.X, box.Y, 0, 0, new Size(box.Width, box.Height));
            box.Image = bmp;
        }
    }
}
