using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Projekt_3
{

    public partial class Form1 : Form
    {
        private Dictionary<string, string> loginInfo;
        private Dictionary<ProgressBar, int> progresBars;
        public static bool isLoggedIn = false;
        public int ticks = 0;
        public int remaining = 10;
        public int eventTick = 0;
        bool Fans = false;
        double temperature = 60;
        Random random = new Random();


        public Form1()
        {
            InitializeComponent();
            loginInfo = new Dictionary<string, string>();
            loginInfo.Add("1", "1");
            loginInfo.Add("Jan_Kowalski", "haslo");

        }

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            textBox2.UseSystemPasswordChar = false;
        }

        private void pictureBox2_MouseUp(object sender, MouseEventArgs e)
        {
            textBox2.UseSystemPasswordChar = true;

        }

        private void button5_Click(object sender, EventArgs e)
        {
            string login = textBox1.Text;
            string password = textBox2.Text;

            if (loginInfo.ContainsKey(login) && loginInfo[login] == password)
            {
                isLoggedIn = true;

                button4.Enabled = true;
                button2.Enabled = true;
                button3.Enabled = true;
                label3.Text = login;
                timer1.Enabled = true;
                randomEvent.Enabled = true;
                barChange.Enabled = true;

            }
            else
            {
                MessageBox.Show("Niepoprawne hasło!", "Błąd", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                textBox2.Clear();
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel7.Visible = true;
            panel6.Visible = false;
            panel8.Visible = false;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            panel8.Visible = true;
            panel6.Visible = false;
            panel7.Visible = false;

            temperature = random.Next(70, 80);
            label10.Text = temperature.ToString() + "°C";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            panel6.Visible = true;
            panel7.Visible = false;
            panel8.Visible = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            logout();

        }

        private void logout()
        {
            isLoggedIn = false;
            panel6.Visible = true;
            panel7.Visible = false;
            panel8.Visible = false;
            button4.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            textBox1.Clear();
            textBox2.Clear();
            timer1.Enabled = false;
            barChange.Enabled = false;
            randomEvent.Enabled = false;
            panel9.Visible = false;

            ticks = 0;
            remaining = 10;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ticks++;
            if (ticks > 20)
            {
                panel9.Visible = true;
                button8.Text = "Pozostało " + remaining.ToString() + " sekund";
                remaining--;

                if (ticks > 30)
                {
                    logout();
                    MessageBox.Show("Nastąpiło wylogowanie! Produkcja wstrzymana!", "Błąd", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);

                }

            }
            else panel9.Visible = false;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            ticks = 0;
            remaining = 10;
        }

        private void randomEvent_Tick(object sender, EventArgs e)
        {
            eventTick++;
            if (eventTick > 100)
            {

                awaria();
                eventTick = 0;
            }
        }

        private void awaria()
        {
            switch (random.Next(0, 4))
            {
                case 0:
                    int n1 = random.Next(85, 100);
                    if (progressBar1.Value <= n1)
                        progressBar1.Value = n1;
                    else progressBar1.Value = random.Next(n1, 100);
                    ProgressBarColor.SetState(progressBar1, 2);
                    break;
                case 1:
                    int n2 = random.Next(85, 100);
                    if (progressBar3.Value <= n2)
                        progressBar3.Value = n2;
                    else progressBar3.Value = random.Next(n2, 100);
                    ProgressBarColor.SetState(progressBar3, 2);
                    break;
                case 2:
                    int n3 = random.Next(85, 100);
                    if (progressBar2.Value <= n3)
                        progressBar2.Value = n3;
                    else progressBar2.Value = random.Next(n3, 100);
                    ProgressBarColor.SetState(progressBar2, 2);
                    break;
                case 3:
                    temperature = random.Next(90, 130);
                    label10.Text = temperature.ToString() + "°C";
                    label10.ForeColor = System.Drawing.Color.Red;
                    break;

            }


        }
        private void updateLabel()
        {
            label11.Text = progressBar1.Value.ToString() + "%";
            label12.Text = progressBar2.Value.ToString() + "%";
            label13.Text = progressBar3.Value.ToString() + "%";
        }

        private void barChange_Tick(object sender, EventArgs e)
        {
            progressBar1.Step = PlusMinus();
            progressBar1.PerformStep();
            progressBar2.Step = PlusMinus();
            progressBar2.PerformStep();
            progressBar3.Step = PlusMinus();
            progressBar3.PerformStep();
            updateLabel();
            if (temperature > 50)
            {
                temperature += PlusMinus() * 0.1 * random.Next(0, 10);
                label10.Text = temperature.ToString();

                if (Fans)
                {
                    temperature += -2;
                    label10.Text = temperature.ToString();
                }
            }

        }
        private int PlusMinus()
        {
            int sign = random.Next(2);
            return sign == 0 ? 1 : -1;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var prgbr = GetProgressBarWithLargestValue(progressBar1, progressBar2, progressBar3);
            prgbr.Step =-10;
            prgbr.PerformStep();
            if (prgbr.Value < 80) { ProgressBarColor.SetState(prgbr, 1); }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Fans = !Fans;
            if (Fans) {
                button9.BackColor = System.Drawing.Color.DarkSlateGray;
                button9.ForeColor = System.Drawing.Color.White;
            } else
            {
                button9.BackColor = System.Drawing.Color.White;
                button9.ForeColor = System.Drawing.Color.Black;
            }
        }
        public static ProgressBar GetProgressBarWithLargestValue(ProgressBar progressBar1, ProgressBar progressBar2, ProgressBar progressBar3)
        {
            int value1 = progressBar1.Value;
            int value2 = progressBar2.Value;
            int value3 = progressBar3.Value;

            if (value1 > value2 && value1 > value3)
            {
                return progressBar1;
            }
            else if (value2 > value1 && value2 > value3)
            {
                return progressBar2;
            }
            else
            {
                return progressBar3;
            }
        }

    }

}

