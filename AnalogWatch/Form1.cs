using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;

namespace AnalogWatch
{

    public partial class Form1 : Form
    {
        Point startingPoint = new Point(100, 100);
        float seconds;
        float minutes;
        float hours;
        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           eraseHands();
           updateTime();
           refreshStuff();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            Application.DoEvents();
            
            updateTime();
            timer1.Start();
            drawClock();
            refreshStuff();
            
        }
        private void updateTime()
        {
            var CurDate = DateTime.Now;
            label1.Text = "" + CurDate.Hour + " :";
            label2.Text = "" + CurDate.Minute + " :";
            label3.Text = "" + CurDate.Second;
            hours = CurDate.Hour%12;
            minutes = CurDate.Minute - 15;
            seconds = CurDate.Second - 15;
        }
        void refreshStuff()
        {
            updateTime();
            drawHands();
        }
        void drawClock()
        {
            var g = pictureBox1.CreateGraphics();
            Pen clock = new Pen(Color.Black, 2);
            g.DrawEllipse(clock, 25, 25, 200, 200);

 
            Pen smallLines = new Pen(Color.Black, 1);
            Brush letterBrush = new SolidBrush(Color.Black);
           
            for (int i = 1; i <= 60; i++)
            {
                g.TranslateTransform(125, 125);
               
                g.RotateTransform(i * 6);
                
                if (i % 5==0)
                {
                    
                    g.DrawLine(smallLines, 75, 0, 100, 0);

                }
                else
                {
                    g.DrawLine(smallLines, 90, 0, 100, 0);
                }
                g.ResetTransform();
            }
            Font font = new Font("Arial", 12);
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;
            
            for (int i = 1; i <= 12; i++)
            {
                g.TranslateTransform(125, 125);
                g.RotateTransform(i * 30);
                g.DrawString(i.ToString(), font, letterBrush, new PointF(0, -110),format);
                g.ResetTransform();
            }
            
           
        }
        void eraseHands()
        {
            var g = pictureBox1.CreateGraphics();
            Pen secondHand = new Pen(Color.White, 1);
            g.TranslateTransform(125, 125);
            g.RotateTransform((seconds) * 6);
            g.DrawLine(secondHand, 0, 0, 80, 0);
            g.ResetTransform();

            Pen minuteHand = new Pen(Color.White, 2);
            g.TranslateTransform(125, 125);
            g.RotateTransform(minutes * 6);
            g.DrawLine(minuteHand, 0, 0, 65, 0);
            g.ResetTransform();

            Pen hourHand = new Pen(Color.White, 3);
            g.TranslateTransform(125, 125);
            g.RotateTransform((hours+ (minutes + 15) / 60) * 30 - 90);
            g.DrawLine(hourHand, 0, 0, 55, 0);
            g.ResetTransform();
        }
        void drawHands()
        {
            var g = pictureBox1.CreateGraphics();

            Pen minuteHand = new Pen(Color.Black, 2);
            g.TranslateTransform(125, 125);
            g.RotateTransform(minutes * 6);
            g.DrawLine(minuteHand, 0, 0, 65, 0);
            g.ResetTransform();

            Pen hourHand = new Pen(Color.Black, 3);
            g.TranslateTransform(125, 125);
            g.RotateTransform((hours + (minutes + 15)/60) * 30 -90);
            g.DrawLine(hourHand, 0, 0, 55, 0);
            g.ResetTransform();

            Pen secondHand = new Pen(Color.Red, 1);
            g.TranslateTransform(125, 125);
            g.RotateTransform((seconds) * 6);
            g.DrawLine(secondHand, 0, 0, 80, 0);
            g.ResetTransform();

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
           
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.BackColor = Color.White;
            Application.DoEvents();
            drawClock();
            refreshStuff();
            button1.Visible = true;
            label1.Visible = true;
            label2.Visible = true;
            label3.Visible = true;
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.LimeGreen;
            this.TransparencyKey = Color.LimeGreen;
            Application.DoEvents();
            drawClock();
            refreshStuff();
            button1.Visible = false;
            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            MessageBox.Show("Pro obnovu okna, klikni na hodiny");
        }
    }
}
