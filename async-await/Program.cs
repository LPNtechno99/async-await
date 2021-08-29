using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace async_await
{
    class Program
    {
        static void Dosomething(int second, string msg, ConsoleColor color)
        {
            lock (Console.Out)
            {
                Console.ForegroundColor = color;
                Console.WriteLine($"{msg,10} ...START");
                Console.ResetColor();
            }

            for (int i = 0; i <= second; i++)
            {
                lock (Console.Out)
                {
                    Console.ForegroundColor = color;
                    Console.WriteLine($"{msg,10} {i,2}");
                    Console.ResetColor();
                    Thread.Sleep(1000);
                }
            }
            lock (Console.Out)
            {
                Console.ForegroundColor = color;
                Console.WriteLine($"{msg,10} ...END");
                Console.ResetColor();
            }

        }
        static async Task Task2()
        {
            Task t2 = new Task(
                () =>
                {
                    Dosomething(10, "T2", ConsoleColor.Green);
                }); // () => {}
            t2.Start();
            await t2;
            Console.WriteLine("T2 da hoan thanh");
        }
        static async Task Task3()
        {
            Task t3 = new Task(
                (Object ob) =>
                {
                    string tentacvu = (string)ob;
                    Dosomething(4, tentacvu, ConsoleColor.Yellow);
                }, "T3"); // (Object ob) => {}
            t3.Start();
            await t3;
            Console.WriteLine("T3 da hoan thanh");
        }
        static async Task func()
        {
            Task task = new Task(()=> { 
                //..cac chi thi
            });
            task.Start();
            await task;
            // cac lenh phia sau
        }

        static async Task<string> Task4()
        {
            Task<string> t4 = new Task<string>(
                () =>
                {
                    Dosomething(10, "T4", ConsoleColor.Green);
                    return "Return from T4";
                }); // () => { return string; }
            t4.Start();
            var kq = await t4;
            return kq;
        }
        static async Task<string> Task5()
        {
            Task<string> t5 = new Task<string>(
                (object ob) => {
                    string t = (string)ob;
                    Dosomething(4, t, ConsoleColor.Yellow);
                    return $"Return from {t}";
                }, "T5"); // (Object ob) => { return string; }
            t5.Start();
            var kq = await t5;
            return kq;
        }
        static async Task<string> GetWeb(string url)
        {
            HttpClient httpClient = new HttpClient();
            Console.WriteLine("Bat dau tai trang web");
            HttpResponseMessage kq = await httpClient.GetAsync(url);
            Console.WriteLine("Bat dau doc kq");
            string content = await kq.Content.ReadAsStringAsync();
            Console.WriteLine("Hoan thanh");
            return content;
        }
        static async Task Main(string[] args)
        {
            //asynchronous
            //Task<T>
            //Task<string> t4 = Task4();
            //Task<string> t5 = Task5();

            //Task t2 = Task2();
            //Task t3 = Task3();

            var task = GetWeb("https://xuanthulab.net");
            Dosomething(6, "T1", ConsoleColor.Magenta);

            //Dosomething(10, "T2", ConsoleColor.Green);
            //Dosomething(4, "T3", ConsoleColor.Green);

            //t2.Wait();
            //t3.Wait();
            //Task.WaitAll(t2, t3);
            //await t2;
            //await t3;

            //var kq4 = await t4;
            //var kq5 = await t5;

            //Console.WriteLine(kq4);
            //Console.WriteLine(kq5);

            var content = await task;

            Console.WriteLine(content);
            Console.WriteLine("Press any key");
            Console.ReadKey();
        }
    }
}
