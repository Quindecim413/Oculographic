using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GlassesReader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LoadPorts();
        }

        string selectedPort = "";

        private void buttonConnectServer_Click(object sender, EventArgs e)
        {
            if (clientControl.Client.IsRunning)
            {
                clientControl.Client.StopClient();
            }
            else
            {
                clientControl.Client.StartClient();
            }
        }

        private void clientControl_Started(object sender, EventArgs e)
        {
            buttonConnectServer.Text = "Отключить";
        }

        private void clientControl_Stopped(object sender, EventArgs e)
        {
            buttonConnectServer.Text = "Подключить";
        }
        void LoadPorts()
        {
            var names = SerialPort.GetPortNames();
            comboBoxPorts.Items.Clear();
            comboBoxPorts.Items.AddRange(names);
            if (selectedPort != "" && names.Contains(selectedPort))
            {
                comboBoxPorts.SelectedItem = selectedPort;
            }
            else
            {
                comboBoxPorts.SelectedIndex = -1;
            }
        }
        private void buttonReloadPorts_Click(object sender, EventArgs e)
        {
            LoadPorts();
        }

        private void buttonReadPort_Click(object sender, EventArgs e)
        {
            if (serialPortGlasses.IsOpen)
            {
                serialPortGlasses.Close();
                buttonReadPort.Text = "Подключить";
            }
            else
            {
                serialPortGlasses.PortName = selectedPort;
                serialPortGlasses.Open();
                buttonReadPort.Text = "Отключить";
            }
        }

        private void comboBoxPorts_SelectedValueChanged(object sender, EventArgs e)
        {
            selectedPort = comboBoxPorts.SelectedItem.ToString();
        }

        private void serialPortGlasses_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            while (serialPortGlasses.BytesToRead > 200)
            {
                string uiSep = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
                string line = serialPortGlasses.ReadLine().Replace(".", uiSep);
                float[] vals;
                try
                {
                    vals = line.Trim().Split('\t').Select(el => float.Parse(el)).ToArray();
                }
                catch
                {
                    return;
                }
                if (vals.Length < 4)
                {
                    return;
                }
                SendData(vals[0], vals[1], vals[2], vals[3]);
                Console.WriteLine("AAA" + vals[0].ToString() + "|" + vals[1].ToString() + "|" + vals[2].ToString() + "|" + vals[3].ToString() + "BBB");
            }
        }

        void SendData(float v1, float v2, float v3, float v4)
        {
            var addresses = clientControl.CheckedClientAddresses;
            byte[] bytesArr = new byte[24];
            Int32 code = 23;
            Array.Copy(BitConverter.GetBytes(code), 0, bytesArr, 0, 4);

            Array.Copy(BitConverter.GetBytes(v1), 0, bytesArr, 8, 4);
            Array.Copy(BitConverter.GetBytes(v2), 0, bytesArr, 12, 4);
            Array.Copy(BitConverter.GetBytes(v3), 0, bytesArr, 16, 4);
            Array.Copy(BitConverter.GetBytes(v4), 0, bytesArr, 20, 4);
            
            if (clientControl.Client.IsRunning)
                clientControl.Client.SendData(addresses, bytesArr);
        }
    }
}
