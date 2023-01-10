using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ThreadInteractionTest
{
    /// <summary>
    /// 线程有浪费计算，需要判断余数
    /// </summary>
    internal class AutoResetEventTest3 : TestParent
    {

        private readonly AutoResetEvent _autoResetEvent = new AutoResetEvent(false);


        protected override void ShowEvenResult()
        {
            while (_count <= 1000)
            {
                _autoResetEvent.WaitOne();
                var remainder = _count % 2;
                if (remainder == 0 && _count <= 1000)
                {
                    Console.WriteLine($"{_count++:d5} {Thread.CurrentThread.Name} {remainder}");
                    //_evenResetEvent.WaitOne();
                }
                _autoResetEvent.Set();
            }
        }

        protected override void ShowOddResult()
        {
            while (_count <= 1000)
            {
                _autoResetEvent.WaitOne();
                var remainder = _count % 2;
                if (remainder == 1 && _count <= 1000)
                {
                    Console.WriteLine($"{_count++:d5} {Thread.CurrentThread.Name} {remainder}");
                    //_oddResetEvent.WaitOne();
                }
                _autoResetEvent.Set();
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
            _autoResetEvent.Set();
        }






    }
}
