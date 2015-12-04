using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

namespace AsynchronousDelegates
{
    class Program
    {
        static int ThreadFunc(int data, int ms)
        {
            Console.WriteLine("sub thread start...");
            Thread.Sleep(ms);
            Console.WriteLine("sub thread end...");
            // ++data
            return ++data;
        }

        delegate int ThreadFuncDelegate(int data, int ms);

        // method 1: Polling
        static void ThreadPolling()
        {
            ThreadFuncDelegate dl = new ThreadFuncDelegate(ThreadFunc);

            IAsyncResult ar = dl.BeginInvoke(10, 1500, null, null);

            // polling to check if sub-thread already finished
            while (!ar.IsCompleted)
            {
                Console.WriteLine("main thread working...");
                Thread.Sleep(50);
            }

            int result = dl.EndInvoke(ar);

            Console.WriteLine("result: {0}", result);

            Console.ReadLine();
        }

        // method 2: Wait Handle
        static void ThreadWaitHandle()
        {
            ThreadFuncDelegate dl = new ThreadFuncDelegate(ThreadFunc);

            IAsyncResult ar = dl.BeginInvoke(10, 1500, null, null);
            
            while (true)
            {
                Console.WriteLine("main thread working...");
                Thread.Sleep(50);

                // WaitHandle
                if (ar.AsyncWaitHandle.WaitOne(1, false))
                {
                    Console.WriteLine("AsyncWaitHandle.WaitOne succeed.");
                    break;
                }
            }

            int result = dl.EndInvoke(ar);

            Console.WriteLine("result: {0}", result);
            
            Console.ReadLine();
        }

        //method 3: Asynchronous Callback
        static void TakeAWhileCompleted(IAsyncResult ar)
        {
            if (ar == null)
            {
                throw new ArgumentNullException("ar");
            }

            ThreadFuncDelegate dl = ar.AsyncState as ThreadFuncDelegate;
            //Trace.Assert(dl != null, "invalid null object");

            int result = dl.EndInvoke(ar);
            Console.WriteLine("result: {0}", result);
        }

        static void ThreadAsynchronousCallback()
        {
            ThreadFuncDelegate dl = new ThreadFuncDelegate(ThreadFunc);

            dl.BeginInvoke(10, 3000, TakeAWhileCompleted, null);

            for(int idx = 1; idx < 10; ++idx)
            {
                Console.WriteLine("main thread working...");
                Thread.Sleep(50);
            }

            Console.ReadLine();
        }


        //method 3: Asynchronous Callback
        static void ThreadAsynchronousCallback_Lambda()
        {
            ThreadFuncDelegate dl = new ThreadFuncDelegate(ThreadFunc);

            dl.BeginInvoke(10, 3000, 
                ar => {
                    int result = dl.EndInvoke(ar);
                    Console.WriteLine("result: {0}", result);
                }, 
                null);

            for (int idx = 1; idx < 10; ++idx)
            {
                Console.WriteLine("main thread working...");
                Thread.Sleep(50);
            }

            Console.ReadLine();
        }


        static void Main(string[] args)
        {
            // method 1: Polling
            //ThreadPolling();

            // method 2: Wait Handle
            //ThreadWaitHandle();

            //method 3: Asynchronous Callback
            //ThreadAsynchronousCallback();

            //method 3: Asynchronous Callback (use lambda)
            ThreadAsynchronousCallback_Lambda();
        }
    }
}
