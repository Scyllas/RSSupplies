using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RSSupplies
{
    class Group
    {
        public string name;
        public Bitmap image;
        public int value;

        internal Group(string Name, string Path)
        {

            name = Name;
            image = new Bitmap(@".\Supplies\" + Path);


        }
    }

    class Supply
    {

        public string name;
        public string fileName;
        public string group;
        public float matchValue = 0.9f;
        public int doses = 1;
        public bool statDrain = false;
        public bool adrenalineDrain = false;
        public int healing = 0;


        internal Supply(string Name, string FileName, string Group, string MatchValue, string Doses, string StatDrain, string AdrenalineDrain, string Healing)
        {
            try
            {
                name = Name;
                fileName = FileName;
                group = Group;
                matchValue = Convert.ToSingle(MatchValue);
                doses = Convert.ToInt32(Doses);
                statDrain = StatDrain == "Y" ? true : false;
                adrenalineDrain = AdrenalineDrain == "Y" ? true : false;
                healing = Convert.ToInt32(Healing);
            }
            catch
            {
                MessageBox.Show(Name + "\n" + FileName + "\n" + Group + "\n" + MatchValue + "\n" + Doses + "\n" + StatDrain + "\n" + AdrenalineDrain + "\n" + Healing);
            }
        }
    }

    static class Data
    {
        public static List<Supply> Supplies = new List<Supply>();
        public static List<Group> Groups = new List<Group>();
        public static List<Box> Boxes = new List<Box>();
    }

    class Box
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Pen Pen { get; set; }
        public Bitmap Image { get; set; }


        public void Reset()
        {
            X = 0;
            Y = 0;
            Width = 0;
            Height = 0;
        }

        public void SetCoord(MouseEventArgs e)
        {
            X = e.X;
            Y = e.Y;
        }

        public void SetSize(MouseEventArgs e)
        {
            Width = e.X - X;
            Height = e.Y - Y;
        }

        public void SetImage(Bitmap b)
        {
            Image = b;
        }

        public void SetPen(Color c)
        {
            Pen = new Pen(c, 1)
            {
                DashStyle = DashStyle.DashDotDot
            };
        }

        internal bool IsIntialised()
        {
            if (X == 0 && Y == 0 && Width == 0 && Height == 0)
            {
                return false;
            }
            return true;
        }

        internal void SetTextBox(Pages.MainMenu.MainMenu main, int currentBoxNo)
        {
            if (currentBoxNo == 0) {
                main.textBox1.Text = X.ToString();
                main.textBox2.Text = Y.ToString();
                main.textBox3.Text = (X + Width).ToString();
                main.textBox4.Text = (Y + Height).ToString();
            }
            else
            {
                main.textBox5.Text = X.ToString();
                main.textBox6.Text = Y.ToString();
                main.textBox7.Text = (X + Width).ToString();
                main.textBox8.Text = (Y + Height).ToString();
            }
        }
    }

    struct ImageWithLabel
    {
        public Label label;
        public PictureBox picturebox;
    }
}
