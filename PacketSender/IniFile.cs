using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PacketSender
{
    internal class IniFile
    {
        private IniFile iniFile;

        private Dictionary<string, Dictionary<string, string>> sections;
        // имя файла
        private string FileName;

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string value, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string defaultValue,
                StringBuilder returnData, int size, string filePath);

        // С помощью конструктора записываем путь до файла и его имя
        public IniFile(string FileName = null)
        {
            sections = new Dictionary<string, Dictionary<string, string>>();
            this.FileName = new FileInfo(FileName).FullName.ToString();
        }

        // Записываем в ini - файл. Запись происходит в выбранную секцию в выбранный ключ
        public void WriteINI(string section, string key, string value)
        {
            WritePrivateProfileString(section, key, value, FileName);
        }

        // Читаем ini - файл и возвращаем значение указанного ключа из заданной секции 
        public string ReadINI(string section, string key)
        {
            var buffer = new StringBuilder(255);
            GetPrivateProfileString(section, key, "", buffer, 255, FileName);
            return buffer.ToString();
        }

        // Удаляем ключ из выбранной секции 
        public void DeleteKey(string key, string section = null)
        {
            WriteINI(section, key, null);
        }

        // Удаляем выбранную секцию 
        public void DeleteSection(string section = null)
        {
            WriteINI(section, null, null);
        }

        // Проверяем есть ли такой ключ, в этой секции
        public bool KeyExists(string key, string section = null)
        {
            return ReadINI(section, key).Length > 0;
        }

        public List<string> GetSectionKeys(string section)
        {
            const int bufferSize = 2048;
            StringBuilder buffer = new StringBuilder(bufferSize);
            int length = GetPrivateProfileString(section, null, "", buffer, bufferSize, FileName);

            if (length == 0)
            {
                return new List<string>();
            }
            List<string> keys = buffer.ToString().Split(new char[] { '\0' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            return keys;
        }


        public bool SectionExists(string section)
        {
            List<string> keys = GetSectionKeys(section);
            return keys.Count > 0;
        }

    }
}
