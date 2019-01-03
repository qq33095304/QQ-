using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace QQBatchSend.IR.Tester
{
    public class TaskList
    {
        public List<Action> Tasks = new List<Action>();
        /// <summary>
        /// 定义一个最大任务数量，默认为5
        /// </summary>
        public int maxTaskNum = 5;

        public void Start()
        {
            for (var i = 0; i < maxTaskNum; i++)
                StartAsync();
        }

        public event Action Completed;

        public void StartAsync()
        {
            lock (Tasks)
            {
                if (Tasks.Count > 0)
                {
                    var task = Tasks[Tasks.Count - 1];
                    Tasks.Remove(task);
                    ThreadPool.QueueUserWorkItem(h =>
                    {
                        task();
                        StartAsync();
                    });
                }
                else if (Completed != null)
                    Completed();
            }
        }
    }
}
