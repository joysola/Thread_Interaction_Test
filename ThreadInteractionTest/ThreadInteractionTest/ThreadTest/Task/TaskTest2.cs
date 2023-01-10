using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadInteractionTest
{
    internal class TaskTest2 : TaskTestBase
    {

        private static readonly object _locker = new object();
        protected void ShowEvenResult(object str)
        {
            while (_count <= 1000)
            {
                lock (_locker)
                {
                    var remainder = _count % 2;
                    if (remainder == 0)
                    {
                        Console.WriteLine($"{_count++:d5} {Thread.CurrentThread.ManagedThreadId} {remainder}");
                    }
                }
            }
        }

        protected void ShowOddResult(object str)
        {
            while (_count <= 1000)
            {
                lock (_locker)
                {
                    var remainder = _count % 2;
                    if (remainder == 1)
                    {
                        Console.WriteLine($"{_count++:d5} {Thread.CurrentThread.ManagedThreadId} {remainder}");
                    }
                }
            }
        }


        protected void ShowResult(object str)
        {

        }

        protected override void Init()
        {
            _evenTask = Task.Factory.StartNew(ShowEvenResult, _evenStr);
            _oddTask = Task.Factory.StartNew(ShowOddResult, _oddStr);
          
            Task.WaitAll(_evenTask, _oddTask);
        }
    }
}
