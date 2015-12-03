using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsynchronousDelegates
{
    class Program
    {
        public static int ThreadFunc(int data, int ms)
        {
            Console.WriteLine("sub thread start...");
            Thread.Sleep(ms);
            Console.WriteLine("sub thread end...");
            // ++data
            return ++data;
        }

        public delegate int ThreadFuncDelegate(int data, int ms);

        static void Main(string[] args)
        {
            ThreadFuncDelegate dl = new ThreadFuncDelegate(ThreadFunc);

            IAsyncResult ar = dl.BeginInvoke(10, 1500, null, null);

            // poll to check if sub-thread already finished
            while(!ar.IsCompleted)
            {
                Console.WriteLine("main thread working...");
                Thread.Sleep(50);
            }

            int result = dl.EndInvoke(ar);

            Console.WriteLine("result: {0}", result);

            Console.ReadLine();
        }
    }
}
