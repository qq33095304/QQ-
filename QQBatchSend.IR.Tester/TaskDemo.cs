using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QQBatchSend.IR.Tester
{
    class TaskDemo
    {
        static object lockObj = new object();
        static int maxTask = 3;
        static int currentCount = 0;
        //假设要处理的数据源
        static List<int> numbers = Enumerable.Range(1, 20).ToList();
        public static void TaskContinueDemo()
        {
            while (currentCount < maxTask && numbers.Count > 0)
            {
                lock (lockObj)
                {
                    if (currentCount < maxTask && numbers.Count > 0)
                    {
                        Interlocked.Increment(ref currentCount);
                        var task = Task.Factory.StartNew(() =>
                        {
                            var number = numbers.FirstOrDefault();
                            if (number > 0)
                            {
                                numbers.Remove(number);
                                Thread.Sleep(1000);//假设执行一秒钟
                                Console.WriteLine("Task id {0} Time{1} currentCount{2} dealNumber{3}", Task.CurrentId, DateTime.Now, currentCount, number);
                            }
                        }, TaskCreationOptions.LongRunning).ContinueWith(t =>
                        {//在ContinueWith中恢复计数
                            Interlocked.Decrement(ref currentCount);
                            Console.WriteLine("Continue Task id {0} Time{1} currentCount{2}", Task.CurrentId, DateTime.Now, currentCount);
                            TaskContinueDemo();
                        });
                    }
                }
            }
        }
    }
}
