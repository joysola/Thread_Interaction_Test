using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ThreadInteractionTest
{
    internal class SemaphoreTest2 : TestParent
    {
        private static readonly int _maxSize = 1;

        private readonly Semaphore _evenSemaphone = new Semaphore(0, _maxSize);
        private readonly Semaphore _oddSemaphone = new Semaphore(0, _maxSize);

        protected override void ShowEvenResult()
        {
            while (_count <= 1000)
            {
                var ressult = _evenSemaphone.WaitOne();
                var remainder = _count % 2;
                if (remainder == 0 && _count <= 1000)
                {
                    Console.WriteLine($"{_count++:d5} {Thread.CurrentThread.Name} {remainder}");
                    _oddSemaphone.Release();
                }
            }
        }

        protected override void ShowOddResult()
        {
            while (_count <= 1000)
            {
                _oddSemaphone.WaitOne();
                var remainder = _count % 2;
                if (remainder == 1 && _count <= 1000)
                {
                    Console.WriteLine($"{_count++:d5} {Thread.CurrentThread.Name} {remainder}");
                    _evenSemaphone.Release();
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
            _evenSemaphone.Release();
        }
    }
}
