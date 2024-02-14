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

namespace PacketSender
{
    public partial class FShow : Form
    {
        public event EventHandler<string> UDPTransmissionEnabled;

        private static FileIniDataParser parser = new FileIniDataParser();

        private IniFile INI = new IniFile($"D:\\PacketSender\\PacketSender\\PacketSender\\inputData.ini");
        private string iniFilePath = $"D:\\PacketSender\\PacketSender\\PacketSender\\inputData.ini";
        IniData data = parser.ReadFile($"D:\\PacketSender\\PacketSender\\PacketSender\\inputData.ini");

        public Dictionary<string, Button> buttons = new Dictionary<string, Button>();
        Socket soket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

        private int ConstPort = 49152;
        public int repeat = 0;
        public int SendResponse = 0;

        private string currentSection = "";
        public string DataIn = "";
        public string DataOut = "";

        uUDP_Receive UdpRc;
        byte[] DataBuffer;

        public FShow()
        {
            File.WriteAllText("output.txt", string.Empty);

            InitializeComponent();

            var configData = ReadConfigFile();
            CreateButtons(configData);

            // Инициализация объектов UDP
            UdpRc = new uUDP_Receive(ConstPort); // установка локального порта приема 
            UdpRc.uUDP_Receive_Event += new uUDP_Event(My_DataRecieve);

            UDPTransmissionEnabled += (sender, sectionKey) =>
            {
                currentSection = sectionKey;
            };
        }

        private void FShow_Load(object sender, EventArgs e)
        {
            this.Text = data["НАЗВАНИЕ ПРОГРАММЫ"]["name"];
        }

        private Dictionary<string, Dictionary<string, string>> ReadConfigFile()
        {
            var configData = new Dictionary<string, Dictionary<string, string>>();
            var lines = File.ReadLines(($"D:\\PacketSender\\PacketSender\\PacketSender\\inputData.ini"), Encoding.UTF8);

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
                if (!section.Key.StartsWith("COMAND") && !section.Key.StartsWith("НАЗВАНИЕ ПРОГРАММЫ"))
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
                        int ports = Convert.ToInt32(buttonConfig["port"]);
                        string time = DateTime.Now.ToString("HH:mm:ss.fff");

                        string protocolType = DetermineProtocolType(soket);
                        string nameComand = buttonConfig["name"];
                        string[] sectionNames = { buttonConfig["name"] };
                        string FromAddress = IP_addr_local(adap);
                        string toAddress = buttonConfig["toIP"];

                        DataIn = buttonConfig["hexString"];

                        if (toAddress.Length != 0 && DataIn.Length != 0)
                        {
                            uBuffer_my TransmitMessage = new uBuffer_my(DataIn);
                            bool isValidPort = int.TryParse(buttonConfig["port"], out port);
                            if (isValidPort && port != 49152)
                            {
                                if (UdpRc.SendDatagramm(TransmitMessage.Buffer, toAddress, ports))
                                {
                                    repeat++;
                                    lbShow.Items.Add("-> " + time + "   " + currentSection.ToUpper());
                                    using (StreamWriter writer = new StreamWriter("output.txt", true))
                                    {
                                        writer.WriteLine($"Время поступления сообщения {time} \n" +
                                            $"Адрес отправителя {toAddress}  порт отправителя {ports} \n" +
                                            $"Адрес получателя {FromAddress}  порт получателя {ports} \n" +
                                            $"Протокол {protocolType} \n" +
                                            $"Сообщение {DataIn}, полученные данные не коректные \n" +
                                            $"Длина сообщения {DataIn.Length}");
                                    }
                                }
                            }
                            else if (UdpRc.SendDatagramm(TransmitMessage.Buffer, toAddress, ports))
                            {
                                SendResponse++;
                                lbShow.Items.Add("-> " + time + "   " + currentSection.ToUpper());
                                string opcode = DataIn.Substring(0, 2);
                                string registerNumber = DataIn.Substring(2, 2);
                                using (StreamWriter writer = new StreamWriter("output.txt", true))
                                {
                                    writer.WriteLine($"Время поступления сообщения {time} \n" +
                                        $"Адрес отправителя {toAddress}  порт отправителя {ports} \n" +
                                        $"Адрес получателя {FromAddress}  порт получателя {ports} \n" +
                                        $"Протокол {protocolType} \n" +
                                        $"Сообщение {DataIn},где {opcode} - код получателя, {registerNumber} - номер регистра ). \n" +
                                        $"Длина сообщения {DataIn.Length}");
                                }
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


            this.BeginInvoke(new Action(() =>
            {            
                DataOut = uBuffer_My.DataToText_space().ToUpper();

                ShowIniSection(currentSection);
                string ports = INI.ReadINI(currentSection, "port");

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
                                string homeValue = GetValueInSection("COMAND", value);
                                if (homeValue != null)
                                {
                                    if (homeValue == DataOut)
                                    {
                                        lbShow.Items.Add("<- " + time + "   " + DataOut.ToUpper());
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
                lbShow.Items.Add("<- " + time + "   " + "Ошибка в полученных данных");
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

        
    }
}
