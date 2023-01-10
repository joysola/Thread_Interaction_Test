using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ThreadInteractionTest
{
    internal class AutoResetEventTest : TestParent
    {

        private AutoResetEvent _evenResetEvent = new AutoResetEvent(false);
        private AutoResetEvent _oddResetEvent = new AutoResetEvent(false);



        //public AutoResetEventTest()
        //{
        //    _evenThread = new Thread(ShowResult) { Name = _evenStr };
        //    _oddnThread = new Thread(ShowResult) { Name = _oddStr };
        //    _evenThread.Start();
        //    _oddnThread.Start();
        //}

        protected void ShowResult1()
        {
            if (_count == 0 && Thread.CurrentThread == _oddThread)
            {
                _oddResetEvent.WaitOne();
            }
            while (_count <= 1000)
            {
                var remainder = _count % 2;
                Console.WriteLine($"{Interlocked.Increment(ref _count):d5} {Thread.CurrentThread.Name} {remainder}");
                if (remainder == 0) // 偶数
                {
                    _oddResetEvent.Set(); // 释放奇数线程
                    _evenResetEvent.WaitOne(); // 阻塞偶数线程
                }
                else // 奇数
                {
                    _evenResetEvent.Set();
                    _oddResetEvent.WaitOne();
                }
            }
            _evenResetEvent.Set();
            _oddResetEvent.Set();
        }

        protected override void Init()
        {
            base.Init();
            
        }

        protected override void ShowResult()
        {
            while (_count <= 1000)
            {
                var remainder = _count % 2;
                if (remainder == 0) // 偶数
                {
                    Console.WriteLine($"{_count++:d5} {Thread.CurrentThread.Name} {remainder}");
                    _oddResetEvent.Set(); // 释放奇数线程
                    _evenResetEvent.WaitOne(); // 阻塞偶数线程
                }
                else // 奇数
                {
                    _evenResetEvent.Set();
                    _oddResetEvent.WaitOne();
                }
            }
            _evenResetEvent.Set();
            _oddResetEvent.Set();
        }
    }
}
