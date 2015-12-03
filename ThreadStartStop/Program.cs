using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

// Turtorals : https://msdn.microsoft.com/en-us/library/aa645740(v=vs.71).aspx

public class Worker
{
    private volatile bool _stop;

    public void ReqestStop()
    {
        _stop = true;
    }

    public void DoWork()
    {
        while(!_stop)
        {
            Console.WriteLine("worker thread: working...");
        }
        Console.WriteLine("worker thread: terminating gracefully.");
    }
}

namespace ThreadStartStop
{
    class Program
    {
        public static void ThreadAutoTerminate()
        {
            // instantiate Worker-Class object
            Worker workerObject = new Worker();

            // new thread
            Thread workerThread = new Thread(workerObject.DoWork);

            workerThread.Start();
            Console.WriteLine("main thread: starting worker thread.");

            // main thread sleep.
            Thread.Sleep(1);

            // request worker thread to stop automatically
            workerObject.ReqestStop();

            // 使用 Join 方法阻塞当前线程， 
            // 直至对象的线程终止。
            workerThread.Join();
            Console.WriteLine("main thread: worker thread has terminated.");
        }

        public static void ThreadAbort()
        {
            Worker workerObject = new Worker();
            Thread workerThread = new Thread(workerObject.DoWork);
            
            workerThread.Start();
            Console.WriteLine("main thread: starting worker thread.");

            // wait worker thread to be alive.
            while (!workerThread.IsAlive) ;
            
            Thread.Sleep(1);

            // Request that oThread be stopped
            workerThread.Abort();
           
            workerThread.Join();
            Console.WriteLine("main thread: worker thread has terminated.");
        }

        static void Main(string[] args)
        {
            //ThreadAutoTerminate();
            ThreadAbort();

            Console.ReadLine();

        }
    }
}
