using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QQBatchSend.IR.TaskUtils
{
    public class TaskList
    {
        public List<Action> Tasks = new List<Action>();

        public object lockObj = new object();
        /// <summary>
        /// 定义一个最大任务数量，默认为5
        /// </summary>
        public int maxTaskNum = 5;

        public int currentCount = 0;

        public void TaskContinueDemo()
        {
            while (currentCount < maxTaskNum && Tasks.Count > 0)
            {
                lock (lockObj)
                {
                    if (currentCount < maxTaskNum && Tasks.Count > 0)
                    {
                        Interlocked.Increment(ref currentCount);
                        var number = Tasks.FirstOrDefault();
                        if (number != null)
                        {
                            Tasks.Remove(number);
                            var task = Task.Factory.StartNew(() =>
                            {
                                number();
                            }, TaskCreationOptions.LongRunning).ContinueWith(t =>
                            {//在ContinueWith中恢复计数
                                Interlocked.Decrement(ref currentCount);
                                TaskContinueDemo();
                            });
                        }
                        Thread.Sleep(100);
                    }
                }
            }
        }
    }
}
