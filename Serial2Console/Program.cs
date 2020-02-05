using System;
using System.IO.Ports;

namespace Serial2Console
{
    class PortDataReceived
    {
        public static void Main()
        {
			Console.WriteLine("Select one of available COM ports:");
			string[] portsList = SerialPort.GetPortNames();
			for (int i = 0; i<portsList.Length; i++)
			{
				Console.WriteLine("["+i.ToString() + "] :" + portsList[i]);
			}
			int selectedIndex = -1;
			string response = Console.ReadLine();
			bool selected = int.TryParse(response, out selectedIndex);
			
            SerialPort mySerialPort = new SerialPort(portsList[selectedIndex]);

            mySerialPort.BaudRate = 115200;
            mySerialPort.Parity = Parity.None;
            mySerialPort.StopBits = StopBits.One;
            mySerialPort.DataBits = 8;
            mySerialPort.Handshake = Handshake.None;
            mySerialPort.RtsEnable = true;

            mySerialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);

            mySerialPort.Open();

            Console.WriteLine("Press any key to exit...");
            Console.WriteLine();
            Console.ReadKey();
            mySerialPort.Close();
        }

        private static void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string indata = sp.ReadExisting();
            //Console.WriteLine("Data Received:");
            Console.Write(indata);
        }
    }
}
