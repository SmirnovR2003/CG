using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace _2
{
    public partial class Form1 : Form
    {
        struct SDrawWithMouse
        {
            public bool start;
            public Color color;
        }
        private const int DEFUALT_PICTURE_WIDTH = 1000;
        private const int DEFUALT_PICTURE_HEIGHT = 600;
        private const int DRAW_CIRCLE_RADIUS = 5;

        private SDrawWithMouse drawWithMouse;

        public Form1()
        {
            InitializeComponent();
            drawWithMouse = new SDrawWithMouse();
            drawWithMouse.color = Color.Red;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap image; 

            OpenFileDialog open_dialog = new OpenFileDialog(); 
            open_dialog.Filter = "Drawer Files(*.JPG;)|*.JPG;"; 
            if (open_dialog.ShowDialog() == DialogResult.OK) 
            {
                try
                {
                    image = new Bitmap(open_dialog.FileName);
                    pictureBox1.Image = image;
                    pictureBox1.Invalidate();

                }
                catch
                {
                    DialogResult rezult = MessageBox.Show("Невозможно открыть выбранный файл",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap image = new Bitmap(DEFUALT_PICTURE_WIDTH, DEFUALT_PICTURE_HEIGHT);
            pictureBox1.Image = image;
            pictureBox1.Invalidate();

        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(pictureBox1.Image == null)
            {
                return;
            }
            Bitmap bmpSave = (Bitmap)pictureBox1.Image;
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Drawer Files(*.JPG;)|*.JPG;";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                bmpSave.Save(sfd.FileName);

            }

        }

        private void changeColorToolStripMenuItem_Click(object sender, EventArgs e)
        {

            ColorDialog colorDialog = new ColorDialog();

            colorDialog.Color = drawWithMouse.color;

            if (colorDialog.ShowDialog() == DialogResult.Cancel)
                return;

            drawWithMouse.color = colorDialog.Color;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            drawWithMouse.start = true;

        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            //поддержать png
            //сделать рисование непрерывным
            if(
                !drawWithMouse.start
                || e.X >= pictureBox1.Image.Width
                || e.X < 0
                || e.Y >= pictureBox1.Image.Height
                || e.Y < 0)
            {
                return;
            }
            Drawer image = new Drawer();

            image.FillCircle(
                ((Bitmap)pictureBox1.Image),
                new Point(
                    e.X,
                    e.Y 
                    ),
                DRAW_CIRCLE_RADIUS,
                drawWithMouse.color
                );
            pictureBox1.Refresh();

        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            drawWithMouse.start = false;

        }
    }
}

