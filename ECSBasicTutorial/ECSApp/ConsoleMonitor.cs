using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ECSApp
{
    /// <summary>
    /// Tiny async console monitor.
    /// </summary>
    public static class ConsoleMonitor
    {
        public static bool IsRunning { get; set; }

        public delegate void KeyPressEventHandler(ConsoleKeyPressEventArgs e);
        public static event KeyPressEventHandler OnKeyPress;

        public static void Start()
        {
            if(!IsRunning)
            {
                IsRunning = true;

                var t = Task.Run(() =>
                {
                    while (IsRunning)
                    {
                        var key = Console.ReadKey(true);
                        OnKeyPress?.Invoke(new ConsoleKeyPressEventArgs(key));
                    }
                });
            }
        }

        public static  void Stop()
        {
            IsRunning = false;
        }

        public class ConsoleKeyPressEventArgs : EventArgs
        {
            public ConsoleKeyInfo KeyInfo { get; }
            public ConsoleKeyPressEventArgs(ConsoleKeyInfo value)
            {
                KeyInfo = value;
            }
        }
    }
}
