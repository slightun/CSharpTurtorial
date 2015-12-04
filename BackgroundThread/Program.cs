using System;
using System.Threading;

namespace BackgroundThread
{
    class Program
    {
        static void ThreadWork()
        {
            Console.WriteLine("Thread {0} started..", Thread.CurrentThread.Name);
            Thread.Sleep(3000);
            Console.WriteLine("Thread {0} completed..", Thread.CurrentThread.Name);
        }
        static void Main(string[] args)
        {
            //Thread td = new Thread(ThreadWork) { Name= "ThreadNamed", IsBackground = false};
            Thread td = new Thread(ThreadWork) { Name = "ThreadNamed", IsBackground = true };
            td.Start();

            Console.WriteLine("main thread ending..");
        }
    }
}
