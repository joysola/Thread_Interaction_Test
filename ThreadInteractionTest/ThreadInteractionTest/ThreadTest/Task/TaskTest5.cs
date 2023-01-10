using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadInteractionTest
{
    /// <summary>
    /// 异步，可能同一个线程执行
    /// </summary>
    internal class TaskTest5 : TaskTestBase
    {
        private readonly SemaphoreSlim _evenMRES = new(0, 1);
        private readonly SemaphoreSlim _oddMRES = new(0, 1);

        protected async Task ShowEvenResult(object str)
        {
            while (_count <= 1000)
            {
                await _evenMRES.WaitAsync();
                var remainder = _count % 2;
                if (remainder == 0 && _count <= 1000)
                {
                    Console.Write($"{_count++:d5} {Thread.CurrentThread.ManagedThreadId:d2} {_evenStr} {remainder} {Environment.NewLine}");
                    _oddMRES.Release();
                }
            }
        }

        protected async Task ShowOddResult(object str)
        {
            while (_count <= 1000)
            {
                await _oddMRES.WaitAsync();
                var remainder = _count % 2;
                if (remainder == 1 && _count <= 1000)
                {
                    Console.Write($"{_count++:d5} {Thread.CurrentThread.ManagedThreadId:d2} {_oddStr} {remainder} {Environment.NewLine}");
                    _evenMRES.Release();
                }
            }
        }




        protected override async Task InitAsync()
        {
            _evenTask = Task.Run(async () => await ShowEvenResult(_evenStr));//Task.Factory.StartNew(ShowEvenResult, _evenStr);
            _oddTask = Task.Run(async () => await ShowOddResult(_oddStr));//Task.Factory.StartNew(ShowOddResult, _oddStr);
            _evenMRES.Release();
            await Task.WhenAll(_evenTask, _oddTask);
            _evenMRES.Release();
        }
    }
}
