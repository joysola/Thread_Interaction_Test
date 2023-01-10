using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ThreadInteractionTest
{
    /// <summary>
    /// Mutex是互斥锁，一定偶数线程先行，因为从0开始，余数就是0，就算基数线程获取也不能输出值
    /// </summary>
    internal class MutexTest : TestParent
    {
        private readonly Mutex _mutex = new();

        protected override void ShowEvenResult()
        {
            while (_count <= 1000)
            {
                _mutex.WaitOne();
                var remainder = _count % 2;
                if (remainder == 0 && _count <= 1000)
                {
                    Console.WriteLine($"{_count++:d5} {Thread.CurrentThread.Name} {remainder}");
                }
                _mutex.ReleaseMutex();
            }
        }

        protected override void ShowOddResult()
        {
            while (_count <= 1000)
            {
                _mutex.WaitOne();
                var remainder = _count % 2;
                if (remainder == 1 && _count <= 1000)
                {
                    Console.WriteLine($"{_count++:d5} {Thread.CurrentThread.Name} {remainder}");
                }
                _mutex.ReleaseMutex();
            }
        }

        protected override void Init()
        {
            _evenThread = new Thread(ShowEvenResult) { Name = _evenStr };
            _oddThread = new Thread(ShowOddResult) { Name = _oddStr };
            _evenThread.Start();
            //Thread.Sleep(1);
            _oddThread.Start();
        }
    }
}
