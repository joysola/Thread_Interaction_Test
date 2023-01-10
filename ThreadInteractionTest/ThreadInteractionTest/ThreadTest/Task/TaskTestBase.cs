using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadInteractionTest
{
    internal class TaskTestBase: ITestInterface
    {
        protected const string _evenStr = "偶数线程";
        protected const string _oddStr = "奇数线程";

        internal Task _evenTask;
        internal Task _oddTask;
        protected int _count = 0;


        public TaskTestBase()
        {
            Init();
            InitAsync().GetAwaiter().GetResult();
        }

        protected virtual void Init() { }
        protected virtual async Task InitAsync() { }


        protected virtual void ShowEvenResult() { }
        protected virtual void ShowOddResult() { }
    }
}
