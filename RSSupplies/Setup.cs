using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace RSSupplies
{
    static class Setup
    {
        internal static void TakeScreenshot(int width, int height, PictureBox pb)
        {
            
            Bitmap printscreen = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Graphics graphics = Graphics.FromImage(printscreen as Image);
            graphics.CopyFromScreen(0, 0, 0, 0, printscreen.Size);

            using (MemoryStream s = new MemoryStream())
            {

                printscreen.Save(s, ImageFormat.Bmp);
                pb.Size = new Size(width, height);

                pb.Image = Image.FromStream(s);
            }
        }

        internal static void ReadInfoCSV()
        {
            string infoFile = "Info.csv";
            string groupFile = "Groups.csv";

            List<string> infocsv = ReadFile(infoFile);
            List<string> groupcsv = ReadFile(groupFile);
           

            Data.Supplies = GetSupplyLines(infocsv);
            Data.Groups = GetGroupLines(groupcsv);

            Tracker.isGroupsPopulated = true;
        }


        private static List<string> ReadFile(string fileName)
        {

            List<string> csv = new List<string>();

            try
            {
                using (var reader = new StreamReader(fileName))
                {
                    while (!reader.EndOfStream)
                    {
                        csv.Add(reader.ReadLine());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Issue relating to " + fileName + "\n" + ex.Message);
                csv = ReadFile(fileName);
            }
            return csv;
        }


       

        private static List<Supply> GetSupplyLines(List<string> csv)
        {
            List<Supply> supplies = new List<Supply>();

            try
            {
                for (int i = 0; i < csv.Count; i++)
                {

                    //Name, FileName, Group, Image Match Value, Doses, Stat Drain, Adrenaline drain, Healing
                    string[] splitter = csv[i].Split(',');
                    if (splitter[0] != "")
                    {
                        supplies.Add(new Supply(splitter[0], splitter[1], splitter[2], splitter[3], splitter[4], splitter[5], splitter[6], splitter[7]));
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Issue relating to Supplies CSV\n" + ex.Message);
            }
            return supplies;
        }

        private static List<Group> GetGroupLines(List<string> csv)
        {
            List<Group> groups = new List<Group>();

            try
            {
                for (int i = 0; i < csv.Count; i++)
                {
                    //Group,Image
                    string[] splitter = csv[i].Split(',');
                    if (splitter[0] != "")
                    {
                        groups.Add(new Group(splitter[0], splitter[1]));
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Issue relating to Group CSV\n" + ex.Message);
            }
            return groups;
        }
    }
}
