using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ThreadInteractionTest
{
    /// <summary>
    /// ManualResetEvent 在set方法后，需要手动调用Reset方法才能将状态置为false
    /// </summary>
    internal class ManualResetEventTest : TestParent
    {
        private ManualResetEvent _evenResetEvent = new ManualResetEvent(false);
        private ManualResetEvent _oddResetEvent = new ManualResetEvent(false);

        //public ManualResetEventTest()
        //{
        //    _evenThread = new Thread(ShowResult) { Name = _evenStr };
        //    _oddnThread = new Thread(ShowResult) { Name = _oddStr };
        //    _evenThread.Start();
        //    _oddnThread.Start();
        //}

        protected override void ShowResult()
        {
            if (_count == 0 && Thread.CurrentThread == _oddThread)
            {
                _oddResetEvent.WaitOne();
            }
            while (_count <= 1000)
            {
                var remainder = _count % 2;
                Console.WriteLine($"{_count++:d5} {Thread.CurrentThread.Name} {remainder}");
                if (remainder == 0) // 偶数
                {
                    _oddResetEvent.Set(); // 释放奇数线程
                    _oddResetEvent.Reset();

                    _evenResetEvent.Reset();
                    _evenResetEvent.WaitOne(); // 阻塞偶数线程
                }
                else // 奇数
                {
                    _evenResetEvent.Set();
                    _evenResetEvent.Reset();

                    _oddResetEvent.Reset();
                    _oddResetEvent.WaitOne();
                }
            }
            _evenResetEvent.Set();
            _evenResetEvent.Reset();
            //_oddResetEvent.Set();
            //_oddResetEvent.Reset();
        }
       
    }
}
