using IniParser.Model;
using IniParser;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Net;

namespace PacketSender
{
    public partial class FShow : Form
    {
        public event EventHandler<string> UDPTransmissionEnabled;

        private static FileIniDataParser parser = new FileIniDataParser();

        private IniFile INI = new IniFile(Path.Combine(Application.StartupPath, "inputData.ini"));
        private string iniFilePath = (Path.Combine(Application.StartupPath, "inputData.ini"));
        IniData data = parser.ReadFile(Path.Combine(Application.StartupPath, "inputData.ini"));

        public Dictionary<string, Button> buttons = new Dictionary<string, Button>();
        Socket soket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

        public int repeat = 0;
        public int SendResponse = 0;

        private string currentSection = "";
        public string DataIn = "";
        public string DataOut = "";

        private int localPort;

        uUDP_Receive UdpRc;
        byte[] DataBuffer;

        public FShow()
        {
            File.WriteAllText("InfoMessage.txt", string.Empty);

            InitializeComponent();

            localPort = GetConstPortFromIni();




            var configData = ReadConfigFile();
            CreateButtons(configData);

            // Инициализация объектов UDP
            UdpRc = new uUDP_Receive(localPort); // установка локального порта приема 
            UdpRc.uUDP_Receive_Event += new uUDP_Event(My_DataRecieve);

            UDPTransmissionEnabled += (sender, sectionKey) =>
            {
                currentSection = sectionKey;
            };

            this.FormClosing += FShow_FormClosing;
        }

        private void FShow_FormClosing(object sender, FormClosingEventArgs e)
        {
            UdpRc.StopUdp();
        }

        private void FShow_Load(object sender, EventArgs e)
        {
            NetworkInterface[] adap = NetworkInterface.GetAllNetworkInterfaces();


            this.Text = data["НАЗВАНИЕ ПРОГРАММЫ"]["name"];




        }

        private int GetConstPortFromIni()
        {
            if (int.TryParse(data["Основные настройки"]["localPort"], out int port))
            {
                return port;
            }
            return port;
        }

        private Dictionary<string, Dictionary<string, string>> ReadConfigFile()
        {
            var configData = new Dictionary<string, Dictionary<string, string>>();
            var lines = File.ReadLines(Path.Combine(Application.StartupPath, "inputData.ini"), Encoding.UTF8);

            foreach (string line in lines)
            {
                if (line.StartsWith("[") && line.EndsWith("]"))
                {
                    currentSection = line.Trim('[', ']');
                    configData[currentSection] = new Dictionary<string, string>();
                }
                else if (!string.IsNullOrEmpty(line) && !line.StartsWith("["))
                {
                    var parts = line.Split('=');
                    if (parts.Length == 2)
                    {
                        var key = parts[0].Trim();
                        var value = parts[1].Trim();
                        if (currentSection != null)
                        {
                            configData[currentSection][key] = value;
                        }
                        else
                        {
                            throw new InvalidOperationException("Секция не найдена");
                        }
                    }
                }
            }
            return configData;
        }

        // Создание кнопки 
        private void CreateButtons(Dictionary<string, Dictionary<string, string>> configData)
        {
            NetworkInterface[] adap = NetworkInterface.GetAllNetworkInterfaces();
            int y = 1;

            foreach (var section in configData)
            {
                if (!section.Key.StartsWith("Основные настройки") && !section.Key.StartsWith("НАЗВАНИЕ ПРОГРАММЫ") 
                    && !section.Key.StartsWith("ВЫХОДНОЕ СООБЩЕНИЕ"))
                {
                    var buttonConfig = section.Value;
                    var button = new Button();
                    {
                        Name = buttonConfig.TryGetValue("name", out string name) ? name : "";
                        button.Text = buttonConfig.TryGetValue("name", out string text) ? text : "";
                        button.Size = new Size(200, 40);
                        button.Font = new Font(button.Font.FontFamily, 10);
                        button.Location = new Point(0, y);
                    };

                    y += button.Height + 10;

                    // добавление обработчика события для кнопки 
                    button.Click += (sender, e) =>
                    {
                        this.Invoke(new Action(() =>
                        {
                            currentSection = buttonConfig["name"];
                        }));

                        int port;
                        int ports = Convert.ToInt32(data["Основные настройки"]["localPort"]);
                        string time = DateTime.Now.ToString("HH:mm:ss.fff");

                        string protocolType = DetermineProtocolType(soket);
                        string nameComand = buttonConfig["name"];
                        string[] sectionNames = { buttonConfig["name"] };
                        string FromAddress = IP_addr_local(adap);
                        string toAddress = (data["Основные настройки"]["remoteIP"]);

                        DataIn = buttonConfig["hexString"];

                        if (toAddress.Length != 0 && DataIn.Length != 0)
                        {
                            uBuffer_my TransmitMessage = new uBuffer_my(DataIn);
                            bool isValidPort = int.TryParse(data["Основные настройки"]["localPort"], out port);
                            if (isValidPort && port != 49152)
                            {
                                if (UdpRc.SendDatagramm(TransmitMessage.Buffer, toAddress, ports))
                                {
                                    repeat++;
                                    lbShow.Items.Add("->  " + time + "   " + currentSection);
                                    lbShow.Items.Add(" \n ");
                                }
                            }
                            else if (UdpRc.SendDatagramm(TransmitMessage.Buffer, toAddress, ports))
                            {
                                SendResponse++;
                                lbShow.Items.Add("->  " + time + "   " + currentSection);
                            }
                        }
                        UDPTransmissionEnabled?.Invoke(this, buttonConfig["name"]);
                    };
                    pShow.Controls.Add(button);
                    buttons[section.Key] = button;
                }
            }
        }

        // Вывод данных 
        public void My_DataRecieve(object sender, uLeinghtEventrgs e, byte[] Ip)
        {
            DataBuffer = new byte[e.Leinght];
            UdpRc.ReadBuffer(DataBuffer);
            uBuffer_my uBuffer_My = new uBuffer_my(DataBuffer);
            NetworkInterface[] adap = NetworkInterface.GetAllNetworkInterfaces();
            string time = DateTime.Now.ToString("HH:mm:ss.fff");

            string protocolType = DetermineProtocolType(soket);
            string FromAddress = IP_addr_local(adap);
            string toAddress = IP_addr(Ip) + "";
            string fromIP = IP_addr_local(adap) + "";

            IPEndPoint remoteEndPoint = UdpRc.GetRemoveEndPoint();
            string remoteHostAddress = remoteEndPoint.Address.ToString();
            int remoteHostPort = remoteEndPoint.Port;

            string remoteHostInfo = $"Remote HostAddress: {remoteHostAddress}, Port: {remoteHostPort}";


            this.BeginInvoke(new Action(() =>
            {            
                DataOut = uBuffer_My.DataToText_space().ToUpper();

                ShowIniSection(currentSection);
                string ports = data["Основные настройки"]["toPort"];

                string opcode = DataOut.Substring(0, 2).ToUpper();
                string registerNumber = DataOut.Substring(2, 2).ToUpper();
                string dataRegister = DataOut.Substring(4, 2).ToUpper();



                lbShow.Invoke(new Action(() =>
                {

                    using (StreamWriter writer = new StreamWriter("InfoMessage.txt", true))
                    {
                        writer.WriteLine($"Время поступления сообщения {time} \n" +
                        $"Адрес отправителя {toAddress}  порт отправителя {ports} \n" +
                        $"Адрес получателя {FromAddress}  порт получателя {ports} \n" +
                        $"Протокол {protocolType} \n" +
                        $"Сообщение {DataOut},где {opcode} - код получателя, {registerNumber} - номер регистра, " +
                        $"{dataRegister} - данные полученные после чтения регистра. \n" +
                        $"Длина сообщения {DataOut.Length} \n");
                    }
                }));
            }));
        }

        // Определение протокола передачи
        private string DetermineProtocolType(Socket socket)
        {
            if (socket.ProtocolType == ProtocolType.Udp)
            {
                return "UDP";
            }
            else if (socket.ProtocolType == ProtocolType.Tcp)
            {
                return "TCP";
            }
            return "Unknown";
        }

        // Нахождение локального адреса 
        private string IP_addr_local(NetworkInterface[] adap)
        {
            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
            string localAddress = "";
            foreach (NetworkInterface adapter in adapters)
            {
                IPInterfaceProperties adapterProperties = adapter.GetIPProperties();
                foreach (UnicastIPAddressInformation ips in adapterProperties.UnicastAddresses)
                {
                    string ipAddress = ips.Address.ToString();
                    byte[] ipBytes = ips.Address.GetAddressBytes();
                    if (ipBytes[0] == 0xAC && ipBytes[1] == 0x01)
                    {
                        localAddress = ipBytes[0].ToString() + "." + ipBytes[1].ToString() + "."
                            + ipBytes[2].ToString() + "." + ipBytes[3].ToString();
                    }
                }
            }
            return localAddress;
        }

        private string IP_addr(byte[] ip)
        {
            string ips = ip[0].ToString() + "." + ip[1].ToString() + "." + ip[2].ToString() + "+" + ip[3].ToString();
            return (Convert.ToString(ip[0]) + "." + Convert.ToString(ip[1]) + "." + Convert.ToString(ip[2]) + ".")
                + Convert.ToString(ip[3]);
        }

        private void ShowIniSection(string section)
        {
            var reader = new StreamReader(iniFilePath, Encoding.UTF8);
            string line;
            string message = $"Section: [{section}]\n";
            string time = DateTime.Now.ToString("HH:mm:ss.fff");
            bool dataFound = false;


            while ((line = reader.ReadLine()) != null)
            {
                if (line.Trim().StartsWith($"[{section}]"))
                {
                    while ((line = reader.ReadLine()) != null)
                    {
                        line = line.Trim();
                        var keyValue = line.Split('=');
                        if (keyValue.Length == 2)
                        {
                            string key = keyValue[0].Trim();
                            string value = keyValue[1].Trim();
                            message += $"{key} = {value}\n";

                            if (section == currentSection && key == "hexString" && value == data[currentSection]["hexString"])
                            {
                                string homeValue = GetValueInSection("ВЫХОДНОЕ СООБЩЕНИЕ", DataOut);
                                {
                                    if (homeValue != null)
                                    {
                                        lbShow.Items.Add("<-  " + time + "   "+ data["ВЫХОДНОЕ СООБЩЕНИЕ"][DataOut]);
                                        lbShow.Items.Add(" \n ");
                                        dataFound = true;
                                    }
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    break;
                }
            }
            reader.Close();
            if (!dataFound)
            {
                lbShow.Items.Add(" \n ");
            }
        }

        private string GetValueInSection(string section, string key)
        {
            var reader = new StreamReader(iniFilePath);
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                if ((line.Trim().StartsWith($"[{section}]")))
                {
                    while ((line = reader.ReadLine()) != null)
                    {
                        line = line.Trim();
                        var KeyValue = line.Split('=');
                        if (KeyValue.Length == 2)
                        {
                            string currentKey = KeyValue[0].Trim();
                            string value = KeyValue[1].Trim();
                            if (currentKey == key)
                            {
                                reader.Close();
                                return value;
                            }
                        }

                    }
                }
            }
            reader.Close();
            return null;
        }

        private void pShow_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lbShow_DoubleClick(object sender, EventArgs e)
        {
            ListBox newList = new ListBox();
            newList = (ListBox)sender;
            newList.Items.Clear();
        }

        private void Debug_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Остановка работы сервера Udp
            UdpRc.StopUdp();
            FDebug fDebug = new FDebug();
            fDebug.Show();
            this.Close();
        }

        private void lbShow_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
