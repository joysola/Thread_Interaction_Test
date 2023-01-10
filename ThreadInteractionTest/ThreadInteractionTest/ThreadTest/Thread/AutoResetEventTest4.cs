using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace ThreadInteractionTest
{
    /// <summary>
    /// N个线程顺序切换
    /// </summary>
    internal class AutoResetEventTest4
    {
        private readonly List<AutoResetEvent> _autoResetEventList = new();

        private readonly List<Thread> _threadList = new();

        private int _threadCount = 20;
        private int _count = 0;
        private int _maxLoop = 1000;

        public AutoResetEventTest4()
        {
            for (int i = 0; i < 10; i++)
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                stopwatch.Start();
                _count = 0;
                Init();
                while (_threadList.Any(x => x.ThreadState != System.Threading.ThreadState.Stopped))
                {
                    Thread.Sleep(1);
                }
                stopwatch.Stop();
                Console.WriteLine($"消耗时间：{stopwatch.ElapsedMilliseconds},第{i}轮");
                _threadList.ForEach(x => x.Interrupt());
                //_autoResetEventList.ForEach(x => x.Set());
            }

        }

        private void Init()
        {
            _threadList.Clear();
            _autoResetEventList.Clear();
            for (int i = 0; i < _threadCount; i++)
            {
                AutoResetEvent autoResetEvent = new(false);
                _autoResetEventList.Add(autoResetEvent);
            }
            for (int i = 0; i < _threadCount; i++)
            {
                var index = i; // 闭包问题
                var str = $"线程{i:d3}号";
                var thread = new Thread(() =>
                {
                    while (_count <= _maxLoop)
                    {
                        _autoResetEventList[index].WaitOne();
                        if (_count <= _maxLoop)
                        {
                            Console.WriteLine($"{_count++:d5} {Thread.CurrentThread.Name}");
                        }
                        if (index == _threadCount - 1)
                        {
                            _autoResetEventList[0].Set();
                        }
                        else
                        {
                            _autoResetEventList[index + 1].Set();
                        }
                    }
                })
                { Name = str };
                _threadList.Add(thread);
            }
            _threadList.ForEach(x => x.Start());
            _autoResetEventList[0].Set();
        }
    }
}
