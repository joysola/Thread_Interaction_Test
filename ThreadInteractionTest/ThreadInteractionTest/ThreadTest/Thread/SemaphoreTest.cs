using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ThreadInteractionTest
{
    internal class SemaphoreTest : TestParent
    {
        private static readonly int _maxSize = 1;

        private Semaphore _evenSemaphone = new Semaphore(0, _maxSize);
        private Semaphore _oddSemaphone = new Semaphore(0, _maxSize);

        protected override void ShowResult()
        {
            if (_count == 0 && Thread.CurrentThread == _oddThread)
            {
                _oddSemaphone.WaitOne();
            }

            while (_count <= 1000)
            {
                var remainder = _count % 2;
                Console.WriteLine($"{_count++:d5} {Thread.CurrentThread.Name} {remainder}");
                if (remainder == 0) // 偶数
                {
                    _oddSemaphone.Release(); // 释放奇数线程
                    _evenSemaphone.WaitOne(); // 阻塞偶数线程
                }
                else // 奇数
                {
                    _evenSemaphone.Release();
                    _oddSemaphone.WaitOne();
                }
            }
            _evenSemaphone.Release();
        }
    }
}
