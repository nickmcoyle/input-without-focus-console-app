using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace ConsoleApp1
{
    static class Program
    {             
        
        [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(string lpClassName,
       string lpWindowName);

        // Activate an application window.
        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        private static string GetActiveWindowTitle()
        {
            const int nChars = 256;
            StringBuilder Buff = new StringBuilder(nChars);
            IntPtr handle = GetForegroundWindow();
            
            if (GetWindowText(handle, Buff, nChars) > 0)
            {
                return Buff.ToString();
            }
            return null;
        }

        [STAThread]
        static void Main()
        {
            while (true)
            {
                Process[] processes = Process.GetProcessesByName("Receiver");

                foreach (Process proc in processes)
                {                    
                    IntPtr prevFocus = (GetForegroundWindow());
                    SetForegroundWindow(proc.MainWindowHandle);
                    SendKeys.SendWait("{Enter}");
                    SetForegroundWindow(prevFocus);
                    Console.WriteLine(proc.ProcessName);
                    break; //hack to make it not a loop until I figure this out
                }
                Console.WriteLine(GetActiveWindowTitle());
                Thread.Sleep(2000);
            }
        }    
    }
}

