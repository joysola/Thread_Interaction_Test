using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadInteractionTest
{
    /// <summary>
    /// 
    /// </summary>
    internal class TaskTest4 : TaskTestBase
    {
        private readonly SemaphoreSlim _evenMRES = new(0, 1);
        private readonly SemaphoreSlim _oddMRES = new(0, 1);

        protected void ShowEvenResult(object str)
        {
            while (_count <= 1000)
            {
                _evenMRES.Wait();
                var remainder = _count % 2;
                if (remainder == 0 && _count <= 1000)
                {
                    Console.WriteLine($"{_count++:d5} {Thread.CurrentThread.ManagedThreadId} {_evenStr} {remainder}");
                    _oddMRES.Release();
                }
            }
        }

        protected void ShowOddResult(object str)
        {
            while (_count <= 1000)
            {
                _oddMRES.Wait();
                var remainder = _count % 2;
                if (remainder == 1 && _count <= 1000)
                {
                    Console.WriteLine($"{_count++:d5} {Thread.CurrentThread.ManagedThreadId} {_oddStr} {remainder}");
                    _evenMRES.Release();
                }
            }
        }



        protected override void Init()
        {
            _evenTask = Task.Factory.StartNew(ShowEvenResult, _evenStr);
            _oddTask = Task.Factory.StartNew(ShowOddResult, _oddStr);
            _evenMRES.Release();
            Task.WaitAll(_evenTask, _oddTask);
            //_evenMRES.Release();
        }
    }
}
