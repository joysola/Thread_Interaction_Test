using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ThreadInteractionTest
{
    /// <summary>
    /// 线程均先等待
    /// </summary>
    internal class AutoResetEventTest2 : TestParent
    {

        private readonly AutoResetEvent _evenResetEvent = new AutoResetEvent(false);
        private readonly AutoResetEvent _oddResetEvent = new AutoResetEvent(false);

        protected override void ShowEvenResult()
        {
            while (_count <= 1000)
            {
                _evenResetEvent.WaitOne();
                var remainder = _count % 2;
                if (/*remainder == 0 && */_count <= 1000)
                {
                    Console.WriteLine($"{_count++:d5} {Thread.CurrentThread.Name} {remainder}");
                    _oddResetEvent.Set();
                    //_evenResetEvent.WaitOne();
                }
            }
        }

        protected override void ShowOddResult()
        {
            while (_count <= 1000)
            {
                _oddResetEvent.WaitOne();
                var remainder = _count % 2;
                if (/*remainder == 1 &&*/ _count <= 1000)
                {
                    Console.WriteLine($"{_count++:d5} {Thread.CurrentThread.Name} {remainder}");
                    _evenResetEvent.Set();
                    //_oddResetEvent.WaitOne();
                }
            }
        }

        protected override void Init()
        {
            _evenThread = new Thread(ShowEvenResult) { Name = _evenStr };
            _oddThread = new Thread(ShowOddResult) { Name = _oddStr };
            _evenThread.Start();
            //Thread.Sleep(1);
            _oddThread.Start();
            //Thread.Sleep(1);
            _evenResetEvent.Set();
        }






    }
}
