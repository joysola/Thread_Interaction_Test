using System;
using System.Diagnostics;
using System.Threading;

namespace ThreadInteractionTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            new AutoResetEventTest4();
            //ThreadPool.SetMinThreads(10, 10);
            for (int i = 0; i < 5; i++)
            {
                //Console.WriteLine("Hello World!");
                //Stopwatch stopwatch = new();
                //stopwatch.Start();
                ////new AutoResetEventTest2();
                ////new ManualResetEventTest2();
                ////new SemaphoreTest2();
                ////new TaskTest2();
                //ITestInterface testParent = new AutoResetEventTest2();
                //if (testParent is TestParent parent)
                //{
                //    while (parent._evenThread.ThreadState != System.Threading.ThreadState.Stopped || parent._evenThread.ThreadState != System.Threading.ThreadState.Stopped)
                //    {
                //        Thread.Sleep(1);
                //    }
                //}
                //stopwatch.Stop();
                //Console.WriteLine($"消耗时间：{stopwatch.ElapsedMilliseconds}");
                //Console.WriteLine($"循环次数：{i}");
                //if (testParent is TestParent parent2)
                //{
                //    //parent2._evenThread.Interrupt();
                //    //parent2._oddThread.Interrupt();
                //    //parent2._evenThread = null;
                //    //parent2._oddThread = null;

                //}
            }
            Console.WriteLine("Completed!");

            Console.ReadKey();
        }
    }
}
