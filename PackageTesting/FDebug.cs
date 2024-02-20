using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PacketSender
{
    public partial class FDebug : Form
    {
        private IniFile INI = new IniFile(Path.Combine(Application.StartupPath, "config.ini"));
        private List<Button> buttons;
        private List<CheckBox> checkboxes;
        private const string ListButton = "ListButton.txt";
        private const string ListCheckBox = "ListCheckBox.txt";

        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

        public int repeat = 0;
        public int SendResponse = 0;

        uUDP_Receive UdpRc;

        byte[] DataBuffer;

        public FDebug()
        {
            InitializeComponent();

            buttons = new List<Button>();
            checkboxes = new List<CheckBox>();
            LoadButtons();
            LoadCheckBoxes();

            // Инициализация  объектов UDP
            UdpRc = new uUDP_Receive(Convert.ToInt32(tbPort.Text)); // установка локального порта приема
            UdpRc.uUDP_Receive_Event += new uUDP_Event(my_DataRecieve);

        }

        private void FDebug_Load(object sender, EventArgs e)
        {
            this.FormClosing += FDebug_FormClosing;
            this.Text = INI.ReadINI("НАЗВАНИЕ ПРОГРАММЫ", "name");
        }

        // Прием данных
        public void my_DataRecieve(object sender, uLeinghtEventrgs e, byte[] Ip)
        {
            DataBuffer = new byte[e.Leinght];
            UdpRc.ReadBuffer(DataBuffer);
            uBuffer_my uBuffer_My = new uBuffer_my(DataBuffer);
            NetworkInterface[] adap = NetworkInterface.GetAllNetworkInterfaces();
            string time = DateTime.Now.ToString("HH:mm:ss.fff");

            string localIP =IP_addr_local(adap) + "";

            lbReceive.Invoke(new Action<string>((s) => lbReceive.Items.Insert(0, s)), "<-  " + time
                + " ||  localIP " + tbAddress.Text + " ||  localPort " + tbPort.Text + " ||  remoteIP " + localIP +
                " ||  remotePort " + tbPort.Text + " ||  Data " + uBuffer_My.DataToText_space().ToUpper());
        }
        private string IP_addr_rec(byte[] ip)
        {
            string ips = ip[0].ToString() + "." + ip[1].ToString() + "." + ip[2].ToString() + "." + ip[3].ToString();
            if ((ip[0] == 127) && (ip[1] == 0) && (ip[2] == 0) && (ip[3] == 1))
            {
                return ("ПЭВМ"); //localhost
            }
            else
            {
                return (Convert.ToString(ip[0]) + "." + Convert.ToString(ip[1]) + "." +
                    Convert.ToString(ip[2]) + "." + Convert.ToString(ip[3]));
            }
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

        //загрузка кнопки из сохраненного файла ListCheckBox.txt
        private void LoadCheckBoxes()
        {
            if (File.Exists(ListCheckBox))
            {
                using (StreamReader reader = new StreamReader(ListCheckBox))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        CheckBox checkBox = new CheckBox
                        {
                            Name = line,
                            Location = new System.Drawing.Point(10, 50 * checkboxes.Count),
                            Size = new System.Drawing.Size(70, 30)
                        };

                        checkBox.CheckedChanged += CheckBox_CheckedChanged;
                        checkboxes.Add(checkBox);
                        pButton.Controls.Add(checkBox);
                    }
                }
            }
        }

        // Динамическое создание кнопки и checkbox
        private void CreateButton(string section)
        {
            int count = buttons.Count + 1;
            string dataTime = DateTime.Now.ToString();

            NetworkInterface[] adap = NetworkInterface.GetAllNetworkInterfaces();
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            string localIP = IP_addr_local(adap) + "";
            string protocolType = DetermineProtocolType(socket);

            Button button = new Button
            {
                Name = tbName.Text,
                Location = new System.Drawing.Point(30, 50 * (buttons.Count - 1)),
                Size = new System.Drawing.Size(200, 40),
                MaximumSize = new Size(200, int.MaxValue),
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowOnly
            };

            string buttonText = tbName.Text;

            if (buttonText.Length > 18)
            {
                StringBuilder sb = new StringBuilder();
                int charCount = 0;
                foreach (char c in buttonText)
                {
                    if (c == ' ' && charCount > 18)
                    {
                        sb.Append("\n");
                        charCount = 0;
                    }
                    else
                    {
                        sb.Append(c);
                        charCount++;
                    }
                }
                button.Text = sb.ToString();
            }
            else
            {
                button.Text = tbName.Text;
            }

            CheckBox checkBox = new CheckBox
            {
                Name = tbName.Text,
                Location = new System.Drawing.Point(10, button.Location.Y),
                Size = new System.Drawing.Size(70, button.Size.Height)
            };

            button.Click += Button_Click;
            checkBox.CheckedChanged += CheckBox_CheckedChanged;

            buttons.Add(button);
            checkboxes.Add(checkBox);

            this.Controls.Add(button);
            this.Controls.Add(checkBox);

            pButton.Controls.Add(button);
            pButton.Controls.Add(checkBox);

            ButtonLocations();

            INI.WriteINI("Name", count.ToString() + " name", tbName.Text);

            INI.WriteINI(section, "name", tbName.Text);
            INI.WriteINI(section, "remoteIP", tbAddress.Text);
            INI.WriteINI(section, "remotePort", tbPort.Text);
            INI.WriteINI(section, "localIP", localIP);
            INI.WriteINI(section, "localPort", tbPort.Text);
            INI.WriteINI(section, "hexString", tbHexString.Text);
            INI.WriteINI(section, "protocolType", protocolType);
            INI.WriteINI(section, "repeat", repeat.ToString());
            INI.WriteINI(section, "sendResponse", SendResponse.ToString());
            INI.WriteINI(section, "dataTime", dataTime);
        }

        private string DetermineProtocolType(Socket socket)
        {
            if (socket.ProtocolType == ProtocolType.Tcp)
            {
                return "TCP";
            }
            else if (socket.ProtocolType == ProtocolType.Udp)
            {
                return "UDP";
            }
            return "Unknown";
        }

        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox changedCheckBox = (CheckBox)sender;
            if (changedCheckBox != null)
            {
                SaveCheckBoxes();
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            if (clickedButton != null)
            {
                string section = clickedButton.Name;
                if (INI.KeyExists("name", section))
                {
                    tbName.Text = INI.ReadINI(section, "name");
                }
                else
                {
                    buttons.Remove(clickedButton);
                    pButton.Controls.Remove(clickedButton);
                }
                if (INI.KeyExists("remoteIP", section))
                {
                    tbAddress.Text = INI.ReadINI(section, "remoteIP");
                }
                else
                {
                    buttons.Remove(clickedButton);
                    pButton.Controls.Remove(clickedButton);
                }
                if (INI.KeyExists("remotePort", section))
                {
                    tbPort.Text = INI.ReadINI(section, "remotePort");
                }
                else
                {
                    buttons.Remove(clickedButton);
                    pButton.Controls.Remove(clickedButton);
                }
                if (INI.KeyExists("hexString", section))
                {
                    tbHexString.Text = INI.ReadINI(section, "hexString");
                }
                else
                {
                    buttons.Remove(clickedButton);
                    pButton.Controls.Remove(clickedButton);
                }
            }
        }

        // загрузка кнопки из сохраненного файла ListButton.txt
        private void LoadButtons()
        {
            if (File.Exists(ListButton))
            {
                using (StreamReader reader = new StreamReader(ListButton))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        Button button = new Button
                        {
                            Name = line,
                            Location = new System.Drawing.Point(30, 50 * buttons.Count),
                            Size = new System.Drawing.Size(200, 40),
                            MaximumSize = new Size(200, int.MaxValue),
                            AutoSize = true,
                            AutoSizeMode = AutoSizeMode.GrowOnly
                        };

                        string buttonText = line;
                        if (buttonText.Length > 18)
                        {
                            StringBuilder sb = new StringBuilder();
                            int charCount = 0;
                            foreach (char c in buttonText)
                            {
                                if (c == ' ' && charCount > 18)
                                {
                                    sb.Append("\n");
                                    charCount = 0;
                                }
                                else
                                {
                                    sb.Append(c);
                                    charCount++;
                                }
                            }
                            button.Text = sb.ToString();
                        }
                        else
                        {
                            button.Text = line;
                        }

                        button.Click += Button_Click;
                        buttons.Add(button);
                        pButton.Controls.Add(button);
                    }
                }
            }
            foreach (Button button in buttons)
            {
                string section = button.Name;
                if (INI.KeyExists("name", section))
                {
                    tbName.Text = INI.ReadINI(section, "name");
                }
                if (INI.KeyExists("remoteIP", section))
                {
                    tbAddress.Text = INI.ReadINI(section, "remoteIP");
                }
                if (INI.KeyExists("remotePort", section))
                {
                    tbPort.Text = INI.ReadINI(section, "remotePort");
                }
                if (INI.KeyExists("hexString", section))
                {
                    tbHexString.Text = INI.ReadINI(section, "hexString");
                }
            }
        }

        private void SaveCheckBoxes()
        {
            using (StreamWriter writer = new StreamWriter(ListCheckBox))
            {
                foreach (CheckBox checkBox in checkboxes)
                {
                    writer.WriteLine(checkBox.Name);
                }
            }
        }

        // Сохранение кнопки
        private void bSave_Click(object sender, EventArgs e)
        {
            string buttonName = tbName.Text.Trim();
            if (!string.IsNullOrWhiteSpace(buttonName) && !ButtonExists(buttonName))
            {
                CreateButton(tbName.Text);
                tbName.Text = "";
                using (StreamWriter writer = new StreamWriter(ListButton))
                {
                    foreach (Button button in buttons)
                    {
                        writer.WriteLine(button.Name);
                    }
                }
                SaveCheckBoxes();
            }
        }

        // Проверка на наличие кнопок с одинаковым именем
        private bool ButtonExists(string name)
        {
            return buttons.Any(button => button.Name == name);
        }

        private void OpenModeToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void tbName_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void tbHexString_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0') && (e.KeyChar <= '9') || (e.KeyChar == 'a') || (e.KeyChar == 'b') 
                || (e.KeyChar == 'c') || (e.KeyChar == 'd') || (e.KeyChar == 'e') || 
                (e.KeyChar == 'f') || (e.KeyChar == 8))
            {
                return;
            }
            e.Handled = true;
        }

        private void tbAddress_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void bSend_Click(object sender, EventArgs e)
        {
            Validate();
            Send(tbHexString.Text);
        } 
        
        // Передача сообщения
        private void Send(string message)
        {
            string time = DateTime.Now.ToString("HH:mm:ss.fff");
            NetworkInterface[] adap = NetworkInterface.GetAllNetworkInterfaces();
            string localIP = IP_addr_local(adap);
            if (tbAddress.Text.Length != 0)
            {
                if (message.Length != 0)
                {
                    uBuffer_my TransmitMessage = new uBuffer_my(message);

                    int port;
                    // Преобразовние значение порта в из текста в целое число
                    bool isValidPort = int.TryParse(tbPort.Text, out port);
                    port = Convert.ToInt32(tbPort.Text);

                    if (isValidPort && port != 49152)
                    {
                        // отправка диаграммы по протоколу UDP 
                        if (UdpRc.SendDatagramm(TransmitMessage.Buffer, tbAddress.Text, Convert.ToInt32(tbPort.Text)))
                        {
                            repeat++;
                            lbTransmit.Items.Insert(0,"->  " + time + " ||  localIP " + localIP + " ||  localPort " + tbPort.Text + " ||  remoteIP "
                            + tbAddress.Text + " ||  remotePort " + tbPort.Text + " ||  Data "
                            + TransmitMessage.DataToText_space().ToUpper());
                        }
                    }
                    else if (UdpRc.SendDatagramm(TransmitMessage.Buffer, tbAddress.Text, Convert.ToInt32(tbPort.Text)))
                    {
                        SendResponse++;
                        lbTransmit.Items.Insert(0, "->  " + time + " ||  localIP " + localIP + " ||  localPort " + tbPort.Text + " ||  remoteIP "
                           + tbAddress.Text + " ||  remotePort " + tbPort.Text + " ||  Data "
                           + TransmitMessage.DataToText_space().ToUpper());
                    }
                }
            }
        }

        private void bDelete_Click(object sender, EventArgs e)
        {
            for (int i = checkboxes.Count - 1; i >= 0; i--)
            {
                if (checkboxes[i].Checked)
                {
                    List<string> list = new List<string>();

                    string section = checkboxes[i].Name;
                    int keyNumber = i + 1;

                    // Удаление секции и ключа
                    INI.DeleteSection(section);
                    INI.DeleteKey(keyNumber + " name", "Name");

                    if (INI.GetSectionKeys("Name").ToList().Count == 0)
                    {
                        INI.DeleteSection("Name");
                    }

                    // Удаление элементов управления с формы
                    pButton.Controls.Remove(checkboxes[i]);
                    pButton.Controls.Remove(buttons[i]);

                    // Удаление элементов из списка
                    checkboxes.RemoveAt(i);
                    buttons.RemoveAt(i);

                    if (!SectionHasKey("Name", keyNumber + " name"))
                    {
                        INI.DeleteSection(section);
                    }
                    // Обновление ключей в разделе "Name"
                    for (int j = i; j < checkboxes.Count; j++)
                    {
                        int updatedKeyNumber = j + 1;
                        string originalKey = updatedKeyNumber + 1 + " name";
                        string newKey = updatedKeyNumber + " name";

                        // Обновление ключа в файле INI
                        INI.WriteINI("Name", newKey, INI.ReadINI("Name", originalKey));
                        INI.DeleteKey(originalKey, "Name");
                    }

                    // смещение кнопок на панели
                    for (int j = 0; j < buttons.Count; j++)
                    {
                        buttons[j].Location = new System.Drawing.Point(30, 50 * j);
                        buttons[j].Size = new System.Drawing.Size(150, 30);

                        checkboxes[j].Location = new System.Drawing.Point(10, 50 * j);
                        checkboxes[j].Size = new System.Drawing.Size(70, 30);
                    }
                }
            }
            ButtonLocations();
            SaveCheckBoxes();

            // Сохранение обновленного списка кнопок в файл
            using (StreamWriter writer = new StreamWriter(ListButton))
            {
                foreach (Button button in buttons)
                {
                    writer.WriteLine(button.Name);
                }
            }
        }

        // Определение местоположения кнопки  
        private void ButtonLocations()
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                if (i == 0)
                {
                    buttons[i].Location = new System.Drawing.Point(30, 0);
                }
                else
                {
                    buttons[i].Location = new System.Drawing.Point(30, buttons[i - 1].Bottom + 10);
                }
                checkboxes[i].Location = new System.Drawing.Point(10, buttons[i].Location.Y);
            }
        }

        private bool SectionHasKey(string section, string key)
        {
            return INI.KeyExists(key, section);
        }

        private void ShowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Остановка работы сервера Udp
            UdpRc.StopUdp();
            this.Hide();

            FShow fShow = new FShow();
            fShow.FormClosed += (s, args) => this.Close();
            fShow.Show();
        }

        private void FDebug_FormClosing(object sender, FormClosingEventArgs e)
        {
            UdpRc.StopUdp();
            Application.Exit();
        }

        private void tbPort_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0') && (e.KeyChar <= '9') || (e.KeyChar == 8))
            {
                return;
            }
            e.Handled = true;
        }

        private void lbTransmit_DoubleClick(object sender, EventArgs e)
        {
            ListBox newList = new ListBox();
            newList = (ListBox)sender;
            newList.Items.Clear();
        }

        private void tbHexString_TextChanged(object sender, EventArgs e)
        {

        }

        private void bSaveFile_Click(object sender, EventArgs e)
        {
            string data = "";
            string filePath = Path.Combine(Application.StartupPath, "OutputFile.txt");

            if(lbTransmit.Items.Count != 0 && lbReceive.Items.Count != 0)
            {
                for (int i = 0; i < lbTransmit.Items.Count; i++)
                {
                    data += "Передача" + " \n" + lbTransmit.Items[i].ToString() + " \n";
                    data += "Прием" + " \n" + lbTransmit.Items[i].ToString() + " \n";
                    data += " \n";
                }
            }
            System.IO.File.WriteAllText(filePath, data, Encoding.Default);
            MessageBox.Show("Данные успешно сохранены в файле.");
        }
    }
}