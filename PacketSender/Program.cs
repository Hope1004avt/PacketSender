using System;
using System.Windows.Forms;


namespace PacketSender
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            FShow f1 = new FShow();
            f1.Show();
            Application.Run();
        }
    }
}
