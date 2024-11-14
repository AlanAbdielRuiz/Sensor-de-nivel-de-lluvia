using System;
using System.IO.Ports;
using System.ComponentModel;
using System.Media;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sensor_de_nivel_de_lluvia
{
    public partial class Form1 : Form
    {
        private SerialPort serialPort;
        private bool isBlinking;
        private SoundPlayer soundPlayer;
        public Form1()
        {
            InitializeComponent();
            serialPort = new SerialPort("COM4", 9600);
            serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
            button1.Click += button1_Click;
            timer1.Interval = 500;//intervalo de parpadeo en milisegundos
            timer1.Tick += timer1_Tick;
            soundPlayer = new SoundPlayer("C:\\Users\\Alan Abdiel\\Downloads\\emergency_effects\\Emergency Effects\\Emergency Siren Police High Low In City From Side 01.wav");


        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (serialPort.IsOpen)
            {
                serialPort.Close();
            }
        }
        void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            string data = serialPort.ReadLine();
            this.Invoke(new Action(() =>
            {
                label1.Text = "sensor status: " + (data.Trim() == "1" ? "water detected" : "No water");
                if (data.Trim() == "1")
                {
                    if (!isBlinking)
                    {
                        timer1.Start();
                        soundPlayer.PlayLooping();
                        pictureBox1.BackColor = System.Drawing.Color.Aquamarine;
                        isBlinking = true;
                    }
                }
                else
                {
                    if (isBlinking)
                    {
                        soundPlayer.Stop();
                        pictureBox1.BackColor = System.Drawing.Color.Gray;
                        isBlinking = false;
                    }
                }
            }));
        }
          

        private void button1_Click(object sender, EventArgs e)
        {
            if (!serialPort.IsOpen)
            {
                serialPort.Open();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            pictureBox1.BackColor = pictureBox1.BackColor == System.Drawing.Color.Gray ?
                System.Drawing.Color.Yellow : System.Drawing.Color.Gray;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            serialPort.Close();
            Application.Exit();
        }
    }
}
