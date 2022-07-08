using System;
using System.Threading;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Diagnostics;
using System.Linq;
using Be.Windows.Forms;
using System.Drawing;
using System.Media;
using PS3Lib;

namespace PS3_SPRX_Loader {
    public partial class Form1 : Form {
        private static TMAPI PS333 = new TMAPI();
        private static PS3RPC PS3RPC = new PS3RPC(PS333);

        public Form1() {
            InitializeComponent();
            textBox1.Text = Properties.Settings.Default.Module;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e) {
            Properties.Settings.Default.Module = textBox1.Text;
          
            Properties.Settings.Default.Save();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            System.Diagnostics.Process.Start("https://www.youtube.com/channel/UC4WMVSZfKT5MZMMuoWAP7cg?view_as=subscriber");
        }

        private void restartSystemBtn_Click(object sender, EventArgs e) {
            try {
                if (!PS333.ConnectTarget())
                    return;
                string IP = PS333.GetTargetName();
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(new IPEndPoint(IPAddress.Parse(IP), 80));
                socket.Send(System.Text.Encoding.ASCII.GetBytes("GET /restart.ps3 HTTP/1.1\nHost: localhost\nContent-Length: 512\n\r\n\r\n"));
                socket.Close();
        
            }
            catch {
                MessageBox.Show("You must use webman and have the PS3 IP as the target name in target manager");
            }
        }

        private void connectToPS3Button_Click(object sender, EventArgs e) {
           
            try {
               
                if (PS333.ConnectTarget() && PS333.AttachProcess()) {
                   
                    refreshModules();
                    
                    label1.Text = "Current Game: " + PS333.GetCurrentGame();
                    label13.Text = TMAPI.Parameters.Info;
                    

                }
            }
            catch {
               
                MessageBox.Show("Unable to connect and attach to PS3");
            }
        }

        private void disconnectFromPS3Button_Click(object sender, EventArgs e) {
            dataGridView1.Rows.Clear();
        
            label1.Text = "Current Game: No Game Detected";
            label13.Text = "No Process Attached";
        }

        private void refreshModules() {
            dataGridView1.Rows.Clear();

            uint[] modules = PS3RPC.GetModules();
            for (int i = 0; i < modules.Length; i++) {
                if (modules[i] != 0x0) {
                    string Name = PS333.GetModuleName(modules[i]);
                    string ID = "0x" + modules[i].ToString("X");
                    string Start = "0x" + PS333.GetModuleStartAddress(modules[i]).ToString("X");
                    string Size = "0x" + PS333.GetModuleSize(modules[i]).ToString("X");
                    dataGridView1.Rows.Add(new object[] { Name, ID, Start, Size, null });
                }
            }
        }

        private void loadModuleBtn_Click(object sender, EventArgs e) {
            string modulePath = textBox1.Text;
            if (!modulePath.Contains("hdd0"))
                modulePath = "/host_root/" + textBox1.Text;
          
            modulePath.Replace("\\", "/");

            ulong error = PS3RPC.LoadModule(modulePath);

            Thread.Sleep(150);

            refreshModules();
           

            if (error != 0x0)
                MessageBox.Show("Load Module Error: 0x" + error.ToString("X"));
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) {
            if (e.ColumnIndex == 4) {
                uint moduleId = Convert.ToUInt32(dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString(), 16);

                ulong error = PS3RPC.UnloadModule(moduleId);

                Thread.Sleep(150);

                refreshModules();

                if (error != 0x0)
                    MessageBox.Show("Unload Module Error: 0x" + error.ToString("X"));
            }
        }

        private void browseFilesBtn(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "SPRX Files|*.sprx";
            openFileDialog1.Title = "Select a File (sprx only)";
           

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
            }
        }

        private void systemCallBtn_Click(object sender, EventArgs e) {

       
        }

        private object GetParameter(string typeName, string value) {
            try {
                if (typeName.Equals("long"))
                    return Convert.ToUInt64(value, 16);
                if (typeName.Equals("float"))
                    return Convert.ToSingle(value);
                if (typeName.Equals("string"))
                    return value;
                return 0;
            }
            catch {
                return 0;
            }
        }

        private void callFunctionBtn_Click(object sender, EventArgs e) {
            object[] registers = new object[9];

            }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
           
        }

        Point lastPoint;

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

		private void Panel2_Paint(object sender, EventArgs e)
		{
		}

		private void Button2_Click(object sender, EventArgs e)
		{
			SoundPlayer soondPlayer = new SoundPlayer(Properties.Resources.testson);
			soondPlayer.Play();
		}

		private void Panel2_Paint(object sender, PaintEventArgs e)
		{
			SoundPlayer soondPlayer = new SoundPlayer(Properties.Resources.Sans_titre);
			soondPlayer.Play();
		}

		private void PictureBox1_Click(object sender, EventArgs e)
		{

		}

        private void Button2_Click_1(object sender, EventArgs e)
        {

        }

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Label4_Click(object sender, EventArgs e)
        {

        }
    }



    }

