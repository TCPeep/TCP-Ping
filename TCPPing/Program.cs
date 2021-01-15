using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Threading;

namespace TCPPing
{
    class Program
    {
        private static Stopwatch stopWatch = new Stopwatch();

        static void Main(string[] args)
        {
            Console.Title = "CSharp TCP Ping";

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("      ___           ___           ___                    ___                       ___           ___     ");
            Console.WriteLine("     /\\  \\         /\\  \\         /\\  \\                  /\\  \\          ___        /\\__\\         /\\  \\    ");
            Console.WriteLine("     \\:\\  \\       /::\\  \\       /::\\  \\                /::\\  \\        /\\  \\      /::|  |       /::\\  \\   ");
            Console.WriteLine("      \\:\\  \\     /:/\\:\\  \\     /:/\\:\\  \\              /:/\\:\\  \\       \\:\\  \\    /:|:|  |      /:/\\:\\  \\  ");
            Console.WriteLine("      /::\\  \\   /:/  \\:\\  \\   /::\\~\\:\\  \\            /::\\~\\:\\  \\      /::\\__\\  /:/|:|  |__   /:/  \\:\\  \\ ");
            Console.WriteLine("     /:/\\:\\__\\ /:/__/ \\:\\__\\ /:/\\:\\ \\:\\__\\          /:/\\:\\ \\:\\__\\  __/:/\\/__/ /:/ |:| /\\__\\ /:/__/_\\:\\__\"");
            Console.WriteLine("    /:/  \\/__/ \\:\\  \\  \\/__/ \\/__\\:\\/:/  /          \\/__\\:\\/:/  / /\\/:/  /    \\/__|:|/:/  / \\:\\  /\\ \\/__/");
            Console.WriteLine("   /:/  /       \\:\\  \\            \\::/  /                \\::/  /  \\::/__/         |:/:/  /   \\:\\ \\:\\__\\  ");
            Console.WriteLine("   \\/__/         \\:\\  \\            \\/__/                  \\/__/    \\:\\__\\         |::/  /     \\:\\/:/  /  ");
            Console.WriteLine("                  \\:\\__\\                                            \\/__/         /:/  /       \\::/  /   ");
            Console.WriteLine("                   \\/__/                                                          \\/__/         \\/__/    ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("");
            Console.Write("Enter a target: ");
            target = Console.ReadLine();
            Console.Write("Enter a port: ");
            port = Console.ReadLine();

            ping(target, port);

            Console.ReadKey();
        }

        static string target;
        static string port;

        static public void Tick(Object stateInfo)
        {
            var client = new TcpClient();
            stopWatch.Start();
            if (client.ConnectAsync(target, Convert.ToInt32(port)).Wait(1000))
            {
                stopWatch.Stop();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{target}:{port} is online! | Time={stopWatch.ElapsedMilliseconds}ms");
                stopWatch.Reset();
                client.Dispose();
                Console.ForegroundColor = ConsoleColor.White;
            }
            else 
            {
                stopWatch.Stop();
                stopWatch.Reset();
                client.Dispose();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{target}:{port} is not responding!");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        static void ping(string ip, string port) 
        {
            Console.WriteLine();
            TimerCallback callback = new TimerCallback(Tick);
            Console.WriteLine($"TCP Ping Started ( {DateTime.Now.ToString("h:mm:ss")} )");
            Timer timer = new Timer(callback, null, 0, 1000);

            for (; ; ) 
            {
                Thread.Sleep(1000);
            }
        }
    }
}
