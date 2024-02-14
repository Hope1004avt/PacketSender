using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PacketSender
{

    delegate void uUDP_Event(object sender, uLeinghtEventrgs e, byte[] Ip);
    class uUDP_Receive
    {
        //----------Настройки клиента--------
        //	UDP-клиент
        private UdpClient pr_Udp;
        //	IP-Адрес
        private IPEndPoint pr_ReceiveInfo;
        //	Локальный порт
        private int pr_LocalPort;
        //	IP-Адрес
        private string pr_IpAddress;
        //	Удаленный порт
        private int pr_RemotePort;

        //---------------Данные-------------
        //	Принятые данные
        private byte[] pr_Data;
        private Queue<byte[]> pr_QueueDatagramm;

        //---------------Потоки-------------
        //	Рабочий поток который слушает линию
        private Thread pr_ThreadUdpServer;
        //	Флаг разрешения приема
        private bool pr_fEnabledReceive;

        //---------------События-------------
        //	Описание события
        public event uUDP_Event uUDP_Receive_Event;

        // Конструктор 
        public uUDP_Receive(int localPort)
        {
            try
            {
                //	Установки
                this.pr_LocalPort = localPort;
                this.pr_QueueDatagramm = new Queue<byte[]>();
                this.pr_ThreadUdpServer = new Thread(pr_ListenerUdpLine);

                //	Запуск приема
                this.pr_fEnabledReceive = true;
                this.pr_Udp = new UdpClient(this.pr_LocalPort);
                this.pr_ThreadUdpServer.Start();
            }
            catch (InvalidCastException error)
            {
                MessageBox.Show(("Произошло исключение: " + error), "Внимание!");
                StopUdp();
            }
        }

        // Изменить настройки 
        public void ChengeSettings(int localPort)
        {
            StopUdp();
            //	Установки
            this.pr_LocalPort = localPort;
            this.pr_QueueDatagramm.Clear();

            //	Запуск приема
            this.pr_fEnabledReceive = true;
            this.pr_Udp = new UdpClient(this.pr_LocalPort);
            if (pr_ThreadUdpServer != null)
            {
                pr_ThreadUdpServer = null;
                this.pr_ThreadUdpServer = new Thread(pr_ListenerUdpLine);
            }
            this.pr_ThreadUdpServer.Start();
        }

        // Слушаем линию 
        private void pr_ListenerUdpLine()
        {
            // если флаг приема стоит 
            while (this.pr_fEnabledReceive)
            {
                try
                {
                    pr_AddToQueue(this.pr_Udp.Receive(ref pr_ReceiveInfo));
                    uLeinghtEventrgs l = new uLeinghtEventrgs(this.pr_QueueDatagramm.Peek().Length);
                    EventDataRecieve(l, pr_ReceiveInfo);
                }
                catch { }
            }
        }

        // Поставить в очередь 
        private void pr_AddToQueue(byte[] data)
        {
            this.pr_QueueDatagramm.Enqueue(data);
        }

        // Получить данные
        public void ReadBuffer(byte[] buffer)
        {
            Array.Copy(this.pr_QueueDatagramm.Peek(), buffer, this.pr_QueueDatagramm.Peek().Length);
            this.pr_QueueDatagramm.Dequeue();
        }

        // Генератор события
        public void EventDataRecieve(uLeinghtEventrgs l, IPEndPoint IpInfo)
        {
            if (uUDP_Receive_Event != null)
            {
                byte[] ip = new byte[4];
                ip = IpInfo.Address.GetAddressBytes();
                uUDP_Receive_Event(this, l, ip);
            }
        }

        // Остановить прием
        public void StopUdp()
        {
            this.pr_fEnabledReceive = false;
            this.pr_Udp.Close();
            this.pr_ThreadUdpServer.Join();
        }

        // Передача данных 
        public bool SendDatagramm(byte[] data, string ipAddress, int remotePort)
        {
            // если массив данных не пуст
            if (data != null)
            {
                // изменение настройки передачи
                pr_ChengeUdpSettings(ipAddress, remotePort);
                // создание нового экземпляра класса IPEndPoint 
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(pr_IpAddress), pr_RemotePort);
                //передача данных через UDP
                pr_Udp.Send(data, data.Length, endPoint);
                return true;
            }
            return false;
        }

        // Изменение настроек передачи данных через UDP
        private void pr_ChengeUdpSettings(string ipAddress, int remotePort)
        {
            this.pr_IpAddress = ipAddress;
            this.pr_RemotePort = remotePort;
        }
    }

    //Класс параметра события
    public class uLeinghtEventrgs : EventArgs
    {
        // Свойства
        private int pr_Leinght = 0;
        public int Leinght
        {
            get { return pr_Leinght; }
            set { pr_Leinght = value; }
        }

        // Конструктор
        public uLeinghtEventrgs(int leinght)
        {
            pr_Leinght = leinght;
        }
    }
}
