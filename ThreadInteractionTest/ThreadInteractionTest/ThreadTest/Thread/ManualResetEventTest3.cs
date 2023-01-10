using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ThreadInteractionTest
{
    /// <summary>
    /// ManualResetEvent 在set方法后，需要手动调用Reset方法才能将状态置为false
    /// </summary>
    internal class ManualResetEventTest3 : TestParent
    {
        private readonly ManualResetEvent _manualResetEvent = new(false);


        protected override void ShowEvenResult()
        {
            while (_count <= 1000)
            {
                if (_count != 0)
                {
                    _manualResetEvent.WaitOne();
                    _manualResetEvent.Reset();
                }

                var remainder = _count % 2;
                if (remainder == 0 && _count <= 1000)
                {
                    Console.WriteLine($"{_count++:d5} {Thread.CurrentThread.Name} {remainder}");
                }
                _manualResetEvent.Set();
                _manualResetEvent.Reset();
            }
        }

        protected override void ShowOddResult()
        {
            while (_count <= 1000)
            {
                _manualResetEvent.WaitOne();
                _manualResetEvent.Reset();

                var remainder = _count % 2;
                if (remainder == 1 && _count <= 1000)
                {
                    Console.WriteLine($"{_count++:d5} {Thread.CurrentThread.Name} {remainder}");
                    //_oddResetEvent.WaitOne();
                }
                _manualResetEvent.Set();
                _manualResetEvent.Reset();
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
        }

    }
}
