using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ThreadInteractionTest
{
    /// <summary>
    /// ManualResetEvent 在set方法后，需要手动调用Reset方法才能将状态置为false
    /// </summary>
    internal class ManualResetEventTest2 : TestParent
    {
        private readonly ManualResetEvent _evenResetEvent = new ManualResetEvent(false);
        private readonly ManualResetEvent _oddResetEvent = new ManualResetEvent(false);

        //public ManualResetEventTest()
        //{
        //    _evenThread = new Thread(ShowResult) { Name = _evenStr };
        //    _oddnThread = new Thread(ShowResult) { Name = _oddStr };
        //    _evenThread.Start();
        //    _oddnThread.Start();
        //}

        protected override void ShowEvenResult()
        {
            while (_count <= 1000)
            {
                var ressult = _evenResetEvent.WaitOne();
                _evenResetEvent.Reset();
                var remainder = _count % 2;
                if (/*remainder == 0 &&*/ _count <= 1000)
                {
                    Console.WriteLine($"{_count++:d5} {Thread.CurrentThread.Name} {remainder}");
                    _oddResetEvent.Set();
                    //_oddResetEvent.Reset();
                    //_evenResetEvent.WaitOne();
                }
            }
        }

        protected override void ShowOddResult()
        {
            while (_count <= 1000)
            {
                _oddResetEvent.WaitOne();
                _oddResetEvent.Reset();
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
