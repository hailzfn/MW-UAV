using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing;

namespace UAV_tool
{
    public partial class Form1 : Form
    {

        [DllImport("kernel32.dll")]
        public static extern bool WriteProcessMemory(IntPtr hProcess, Int64 lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesWritten);

        [DllImport("kernel32.dll")]
        public static extern bool ReadProcessMemory(IntPtr hProcess, Int64 lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesRead);        
        
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);


   
        public static long UAV_Base = ModuleBase(0x16CA0FB8); 
        public long Offset2 = UAV_Base + 0x20;       
        public long Offset3 = UAV_Base + 0x18;     
        public long Offset4 = UAV_Base + 0x1C;    
        public long Offset5 = UAV_Base + 0x38;       
        public long Offset6 = UAV_Base + 0x3C;


        public bool trainerOn = false;


        public Form1()
        {
            InitializeComponent();

            Process p = Process.GetProcessesByName("ModernWarfare")[0];
            
            string message = "Warzone Found!";
            MessageBox.Show(message);
        }


        public static Int64 ModuleBase(long BaseOffset)
        {
            Process p = Process.GetProcessesByName("ModernWarfare")[0];
            IntPtr processHandle = p.Handle;
            Byte[] buffer = new Byte[8];
            Int32 bytesRead = 0;

            long BaseAddress = p.MainModule.BaseAddress.ToInt64() + BaseOffset;
            ReadProcessMemory(processHandle, BaseAddress, buffer, buffer.Length, ref bytesRead);
            Int64 baseValue = BitConverter.ToInt64(buffer, 0);

            return ((baseValue + 0x10) - p.MainModule.BaseAddress.ToInt64());
        }

        private void UAV_ON_Click(object sender, EventArgs e)
        {
            

            trainerOn = !trainerOn;

            if (trainerOn)
            {
                UAV_ON.ForeColor = Color.Green;
                UAV_ON.Text = ("TRAINER (OFF)");
            }
            else
            {
                UAV_ON.ForeColor = Color.Red;
                UAV_ON.Text = ("TRAINER (ON)");
            }

            if (checkBox1.Checked)
            {
                listBox1.Items.Clear();
                listBox1.Items.Add("");
                listBox1.Items.Add("UAV ON !");
            }
            if (!checkBox1.Checked)
            {
                listBox1.Items.Clear();
                listBox1.Items.Add("");
                listBox1.Items.Add("UAV OFF !");
            }
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {

            while (true)
            {
                try
                {
                    if (checkBox1.Checked)
                    {
                        int BytesWritten = 0;
                        Process p = Process.GetProcessesByName("ModernWarfare")[0];

                        IntPtr processHandle = p.Handle;
                        Int64 BaseAddress = p.MainModule.BaseAddress.ToInt64();

                        var ON_1 = new byte[] { 0x01, 0x00, 0x00, 0x00 };
                        var ON_2 = new byte[] { 0x01, 0x00, 0x00, 0x00 };
                        var ON_3 = new byte[] { 0x4f, 0xbb, 0x55, 0x51 };
                        var ON_4 = new byte[] { 0xf6, 0x6d, 0xae, 0x79 };
                        var ON_5 = new byte[] { 0x4f, 0xbb, 0x55, 0x51 };
                        var ON_6 = new byte[] { 0xf6, 0x6d, 0xae, 0x79 };


                        WriteProcessMemory(processHandle, BaseAddress + UAV_Base, ON_1, ON_1.Length, ref BytesWritten);
                        WriteProcessMemory(processHandle, BaseAddress + Offset2, ON_2, ON_2.Length, ref BytesWritten);
                        WriteProcessMemory(processHandle, BaseAddress + Offset3, ON_3, ON_3.Length, ref BytesWritten);
                        WriteProcessMemory(processHandle, BaseAddress + Offset4, ON_4, ON_4.Length, ref BytesWritten);
                        WriteProcessMemory(processHandle, BaseAddress + Offset5, ON_5, ON_5.Length, ref BytesWritten);
                        WriteProcessMemory(processHandle, BaseAddress + Offset6, ON_6, ON_6.Length, ref BytesWritten);

                        
                    }
                    if (!checkBox1.Checked)
                    {
                        int BytesWritten = 0;
                        Process p = Process.GetProcessesByName("ModernWarfare")[0];

                        IntPtr processHandle = p.Handle;
                        Int64 BaseAddress = p.MainModule.BaseAddress.ToInt64();

                        var OFF_1 = new byte[] { 0x00, 0x00, 0x00, 0x00 };
                        var OFF_2 = new byte[] { 0x00, 0x00, 0x00, 0x00 };
                        var OFF_3 = new byte[] { 0x00, 0x00, 0x00, 0x00 };
                        var OFF_4 = new byte[] { 0x00, 0x00, 0x00, 0x00 };
                        var OFF_5 = new byte[] { 0x00, 0x00, 0x00, 0x00 };
                        var OFF_6 = new byte[] { 0x00, 0x00, 0x00, 0x00 };


                        WriteProcessMemory(processHandle, BaseAddress + UAV_Base, OFF_1, OFF_1.Length, ref BytesWritten);
                        WriteProcessMemory(processHandle, BaseAddress + Offset2, OFF_2, OFF_2.Length, ref BytesWritten);
                        WriteProcessMemory(processHandle, BaseAddress + Offset3, OFF_3, OFF_3.Length, ref BytesWritten);
                        WriteProcessMemory(processHandle, BaseAddress + Offset4, OFF_4, OFF_4.Length, ref BytesWritten);
                        WriteProcessMemory(processHandle, BaseAddress + Offset5, OFF_5, OFF_5.Length, ref BytesWritten);
                        WriteProcessMemory(processHandle, BaseAddress + Offset6, OFF_6, OFF_6.Length, ref BytesWritten);
                    }
                }
                catch (Exception err)
                {
                    listBox1.Items.Add(err);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!backgroundWorker1.IsBusy) backgroundWorker1.RunWorkerAsync();
        }
    }
}
