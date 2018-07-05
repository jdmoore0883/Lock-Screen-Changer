using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Lock_Screen_Changer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.ShowInTaskbar = false;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.TaskManagerClosing && e.CloseReason != CloseReason.WindowsShutDown && e.CloseReason != CloseReason.ApplicationExitCall)
            {
                e.Cancel = true;
                this.Hide();
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.Visible)
            {
                this.Hide();
            }
            else
            {
                this.Show();
                this.BringToFront();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Hide();

            try
            {
                StreamReader sr = new StreamReader(Application.StartupPath + "\\lsc.ini");
                textBox1.Text = sr.ReadLine();
                sr.Close();
            }
            catch (Exception ex)
            {
                if (ex is FileNotFoundException)
                {
                    textBox1.Text = "";
                }
            }

            if (textBox1.Text != "" && textBox1.Text != null)
            {
                timer1.Enabled = true;
            }

        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;

                StreamWriter sw = new StreamWriter(Application.StartupPath + "\\lsc.ini");
                sw.WriteLine(openFileDialog1.FileName);
                sw.Close();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string strSourceFile = textBox1.Text;
            string strTargetFile = "backgroundDefault.jpg";
            string strTargetPath = @"C:\Windows\System32\oobe\info\backgrounds";

            if (!System.IO.Directory.Exists(strTargetPath))
            {
                System.IO.Directory.CreateDirectory(strTargetPath);
            }

            try
            {
                if (System.IO.File.Exists(strSourceFile))
                {
                    long lFileSize = new System.IO.FileInfo(strSourceFile).Length;

                    if (lFileSize < 262144)
                    {
                        pictureBox1.Visible = true;
                        pictureBox2.Visible = false;
                        label2.Visible = true;
                        label3.Visible = false;
                        label4.Visible = false;

                        System.IO.File.Copy(strSourceFile, strTargetPath + @"\" + strTargetFile, true);
                    }

                    else
                    {
                        pictureBox1.Visible = false;
                        pictureBox2.Visible = true;
                        label2.Visible = false;
                        label3.Visible = false;
                        label4.Visible = true;
                    }

                }
                else
                {
                    pictureBox1.Visible = false;
                    pictureBox2.Visible = true;
                    label2.Visible = false;
                    label3.Visible = true;
                    label4.Visible = false;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox1.Text != null)
            {
                timer1.Enabled = true;
            }
            else if (textBox1.Text == "" || textBox1.Text == null)
            {
                timer1.Enabled = false;
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            this.BringToFront();
        }
    }
}
