using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadInteractionTest
{
    internal class TaskTest3 : TaskTestBase
    {
        private readonly ManualResetEventSlim _evenMRES = new(false);
        private readonly ManualResetEventSlim _oddMRES = new(false);

        protected void ShowEvenResult(object str)
        {
            while (_count <= 1000)
            {
                _evenMRES.Wait();
                _evenMRES.Reset();
                var remainder = _count % 2;
                if (remainder == 0 && _count <= 1000)
                {
                    Console.WriteLine($"{_count++:d5} {Thread.CurrentThread.ManagedThreadId} {_evenStr} {remainder}");
                    _oddMRES.Set();
                }

            }
        }

        protected void ShowOddResult(object str)
        {
            while (_count <= 1000)
            {
                _oddMRES.Wait();
                _oddMRES.Reset();
                var remainder = _count % 2;
                if (remainder == 1 && _count <= 1000)
                {
                    Console.WriteLine($"{_count++:d5} {Thread.CurrentThread.ManagedThreadId} {_oddStr} {remainder}");
                    _evenMRES.Set();
                }
            }
        }



        protected override void Init()
        {
            _evenTask = Task.Factory.StartNew(ShowEvenResult, _evenStr);
            _oddTask = Task.Factory.StartNew(ShowOddResult, _oddStr);
            _evenMRES.Set();
            Task.WaitAll(_evenTask, _oddTask);
        }
    }
}
