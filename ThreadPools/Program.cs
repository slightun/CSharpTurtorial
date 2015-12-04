using System;
using System.Threading;

// WaitHandle的数量 <= 64

public class Fibonacci
{
    public int Num
    {
        get { return _num; }
    }
    private int _num;

    public int Result
    {
        get { return _result; }
    }
    private int _result;
    private ManualResetEvent _doneEvent;

    // constructor
    public Fibonacci(int num, ManualResetEvent doneEvent)
    {
        _num = num;
        _doneEvent = doneEvent;
    }

    // thread callback
    public void ThreadCallback(object state)
    {
        int threadIndex = (int)state;
        Console.WriteLine("thread {0} started..", threadIndex);
        _result = Caculate(_num);
        Console.WriteLine("thread {0} completed..", threadIndex);
        _doneEvent.Set();
    }

    private int Caculate(int num)
    {
        if (num <= 1)
        {
            return num;
        }
        //Thread.Sleep(1);
        return Caculate(num - 1) + Caculate(num - 2);
    }

}

namespace ThreadPools
{
    class Program
    {
        static void Main(string[] args)
        {
            const int fibCaculations = 10;
            //const int fibCaculations = 65;

            Fibonacci[] fibs = new Fibonacci[fibCaculations];
            ManualResetEvent[] doneEvents = new ManualResetEvent[fibCaculations];
            Random r = new Random();

            Console.WriteLine("launching {0} tasks..", fibCaculations);
            for (int nIdx = 0; nIdx < fibCaculations; ++nIdx)
            {
                doneEvents[nIdx] = new ManualResetEvent(false);

                int num = r.Next(20, 30);
                Fibonacci f = new Fibonacci(num, doneEvents[nIdx]);
                fibs[nIdx] = f;

                // ThreadPool
                ThreadPool.QueueUserWorkItem(f.ThreadCallback, nIdx);
            }

            // WaitHandle的数量 <= 64
            WaitHandle.WaitAll(doneEvents);
            Console.WriteLine("Calculation completed..");

            // result
            for (int nIdx = 0; nIdx < fibCaculations; ++nIdx)
            {
                Fibonacci f = fibs[nIdx];
                Console.WriteLine("Fibonacci({0}) = {1}", f.Num, f.Result);
            }

            Console.ReadLine();
        }
    }
}
