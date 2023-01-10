using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ThreadInteractionTest
{
    internal abstract class TestParent: ITestInterface
    {
        protected const string _evenStr = "偶数线程";
        protected const string _oddStr = "奇数线程";

        internal Thread _evenThread;
        internal Thread _oddThread;
        protected int _count = 0;

        public TestParent()
        {
            Init();
        }

        protected virtual void Init()
        {
            _evenThread = new Thread(ShowResult) { Name = _evenStr };
            _oddThread = new Thread(ShowResult) { Name = _oddStr };
            _evenThread.Start();
            //Thread.Sleep(1);
            _oddThread.Start();
        }

        protected virtual void ShowResult()
        {
            
        }


        protected virtual void ShowEvenResult() { }
        protected virtual void ShowOddResult() { }
    }
}
