using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video;
using Microsoft.VisualBasic;

namespace ColorDetectionIPCamera
{
    public partial class Form1 : Form
    {

        MJPEGStream stream;
        bounds bound = new bounds
        {
            x = 0,
            y = 0,
            rad = 25
        };

        public Form1()
        {
            InitializeComponent();
            stream = new MJPEGStream();
            stream.NewFrame += stream_NewFrame;
        }

        private void stream_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap bmp = (Bitmap)eventArgs.Frame.Clone();
            pictureBox1.Image = bmp;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Image img = pictureBox1.Image;
            if (img != null)
            {
                Bitmap bmp = new Bitmap(img);
                Rectangle rect = new Rectangle(bound.x, bound.y, bound.rad, bound.rad);

                int r = 0, g = 0, b = 0, pxCount = rect.Width * rect.Height;

                for (int i = rect.X; i < rect.Width + rect.X; i++)
                {
                    for (int j = rect.Y; j < rect.Height + rect.Y; j++)
                    {
                        r += bmp.GetPixel(i, j).R;
                        g += bmp.GetPixel(i, j).G;
                        b += bmp.GetPixel(i, j).B;
                        //bmp.SetPixel(i, j, Color.Green);
                    }
                }

                r = r / pxCount;
                g = g / pxCount;
                b = b / pxCount;

                this.Text = "R:" + r + " G: " + g + " B: " + b;
                pictureBox3.BackColor = Color.FromArgb(r, g, b);
            }
            else
            {
                MessageBox.Show("Your Steam Source Is Null");
            }

        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            //e.Graphics.DrawEllipse(Pens.Purple, new Rectangle((pictureBox1.Width / 2) - 25, (pictureBox1.Height / 2) - 25, 50, 50));

            bound = new bounds
            {
                x = trackBar1.Value,
                y = trackBar2.Value,
                rad = 25,
            };

            e.Graphics.DrawRectangle(new Pen(Brushes.Purple, 2), new Rectangle(bound.x, bound.y, bound.rad, bound.rad));
        }


        private void pictureBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            trackBar1.Value = e.X - bound.rad / 2;
            trackBar2.Value = e.Y - bound.rad / 2;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            stream.Source = Interaction.InputBox("Please Enter Stream Source URL", "Stream URL", "http://192.168.1.128:8080/video");
            stream.Stop();
            pictureBox1.Image = null;
            stream.Start();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            trackBar1.Value = (pictureBox1.Width / 2) - bound.rad / 2;
            trackBar2.Value = (pictureBox1.Height / 2) - bound.rad / 2;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            stream.SignalToStop();
            stream.Stop();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            trackBar1.Minimum = 0;
            trackBar2.Minimum = 0;

            trackBar1.Maximum = pictureBox1.Width - bound.rad;
            trackBar2.Maximum = pictureBox1.Height - bound.rad;

            trackBar1.Value = (pictureBox1.Width / 2) - bound.rad / 2;
            trackBar2.Value = (pictureBox1.Height / 2) - bound.rad / 2;
        }
    }
}
