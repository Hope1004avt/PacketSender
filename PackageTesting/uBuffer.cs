using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacketSender
{
    class uBuffer_my
    {
        private byte[] pr_Buffer;
        public byte[] Buffer { get { return pr_Buffer; } }

        private int pr_Leinght;
        public int Leinght { get { return pr_Leinght; } }

        // Конструктор (входной параметр текстовая переменная)
        public uBuffer_my(string message)
        {
            if (!CheckToEven(message.Length))
            {
                message = message.Insert(message.Length - 1, "0");
            }
            pr_Leinght = message.Length / 2;
            pr_Buffer = new byte[pr_Leinght];
            if (!TextToData(message))
            {
                pr_Buffer = null;
            }
        }

        // Конструктор (массив данных)
        public uBuffer_my(byte[] message)
        {
            this.pr_Leinght = message.Length;
            this.pr_Buffer = new byte[pr_Leinght];
            message.CopyTo(this.pr_Buffer, 0);
        }

        // Проверка числа на четность 
        private bool CheckToEven(int value)
        {
            int res;
            Math.DivRem(value, 2, out res);
            if (res == 0)
                return true;
            return false;
        }

        // Преобразование данных в текст 
        public string DataToText()
        {
            StringBuilder sb = new StringBuilder(this.pr_Leinght * 2);
            foreach (byte hex in this.pr_Buffer)
                sb.AppendFormat("{0:x2}", hex);
            return sb.ToString();
        }

        // Преобразование данных в текст с пробелом
        public string DataToText_space()
        {
            string str = "";
            string str1 = "";
            StringBuilder sb = new StringBuilder(this.pr_Leinght * 2);
            // перебор каждого байта в массиве this.pr_Buffer (данные которые были переданы мной)
            foreach (byte hex in this.pr_Buffer)
            {
                // преобразование в 16ое представление и добавление к экземпляру stringBuilder
                sb.AppendFormat("{0:x2}", hex);
            }
            str = sb.ToString();
            // значение длины массива 
            int len = this.pr_Leinght;
            // выполняется столько раз какого значение len
            for (int n = 0; n < len; n++)
            {   // добавляется подстрока из переменной str с длиной 2 и индексом 0
                str1 += str.Substring(0, 2);
                //str1 += str.Substring(0, 2) + " ";
                // удаляется добавленная подстрока
                str = str.Remove(0, 2);
            }
            return str1;
        }

        // Преобразование текста в данные
        private bool TextToData(string text)
        {
            if (CheckText(text))
            {
                int pr_Length = 0;
                while (pr_Length < text.Length / 2)
                {
                    pr_Buffer[pr_Length] = Convert.ToByte((text.Substring(pr_Length * 2, 2)), 16);
                    pr_Length++;
                }
                return true;
            }
            else
            {
                System.Windows.Forms.MessageBox.Show(("Неверно введен текст сообщения: " + text), "Внимание!");
                return false;
            }
        }

		// Проверка на правильность символов
        private bool CheckText(string text)
        {
            byte[] ar = Encoding.GetEncoding(1251).GetBytes(text);
            foreach (byte b in ar)
            {
                if (!((b >= 48 && b <= 57) ||
                    (b >= 65 && b <= 70) ||
                    (b >= 97 && b <= 102) ||
                    (b >= 128 && b <= 175) ||
                    (b >= 224 && b <= 255)))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
