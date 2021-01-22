using System;
using System.IO.Ports;

namespace Serial2Console
{
    class PortDataReceived
    {
        public static void Main()
        {
			string COMPort = selectCOMPort();
			int baudRate = selectBaudRate();

            SerialPort mySerialPort = new SerialPort(COMPort);

            mySerialPort.BaudRate = baudRate;
            mySerialPort.Parity = Parity.None;
            mySerialPort.StopBits = StopBits.One;
            mySerialPort.DataBits = 8;
            mySerialPort.Handshake = Handshake.None;
            mySerialPort.RtsEnable = true;
            mySerialPort.ReadBufferSize = 4096;
            
            mySerialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
            try 
            {
                Console.WriteLine("Baudrate: " + mySerialPort.BaudRate);
                Console.WriteLine("ReadBufferSize: " + mySerialPort.ReadBufferSize);
                mySerialPort.Open(); 
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

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

		public static string selectCOMPort()
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
			return portsList[selectedIndex];
		}

		public static int selectBaudRate()
		{
			Console.WriteLine("Select desired baud rate:");

			for (int i = 0; i < SupportedBaudRates.Length; i++)
			{
				Console.WriteLine("[" + i.ToString() + "] :" + SupportedBaudRates[i]);
			}
			int selectedBaudRateIndex = -1;
			string response = Console.ReadLine();
			bool selected = int.TryParse(response, out selectedBaudRateIndex);
			return SupportedBaudRates[selectedBaudRateIndex];
		}

        public static readonly int [] SupportedBaudRates = new int []
        {
            300,
            600,
            1200,
            2400,
            4800,
            9600,
            19200,
            38400,
            57600,
            115200,
            230400,
            460800,
            921600,
            1000000
        };
    }
}
