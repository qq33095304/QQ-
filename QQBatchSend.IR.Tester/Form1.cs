using QQBatchSend.IR.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace QQBatchSend.IR.Tester
{
    public partial class Form1 : Form
    {
        public OutStatusDelegate outStatus;
        public Form1()
        {
            InitializeComponent();
            outStatus = doOutStatus;
        }


        public void doOutStatus(string text)
        {
            if (InvokeRequired)
            {
                OutStatusDelegate outStatusDelegate = new OutStatusDelegate(doOutStatus);
                BeginInvoke(outStatusDelegate, new object[] { text });
            }
            else
            {
                richTextBox1.Text += string.Format("第{0}个任务输出\r\n", text);
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            var rnd = new Random();
            var taskList = new TaskList();
            for (var i = 0; i < 100; i++)
            {
                var s = rnd.Next(10);
                var j = i;
                var testTask = new Action(() =>
                {
                    Console.WriteLine(string.Format("第{0}个任务（用时{1}秒）已经开始", j, s));
                    Thread.Sleep(s * 1000);
                    outStatus?.Invoke(j.ToString());
                    Console.WriteLine(string.Format("第{0}个任务（用时{1}秒）已经结束", j, s));
                });
                taskList.Tasks.Add(testTask);
            }
            taskList.Completed += () => Console.WriteLine("____________________没有更多的任务了！");
            taskList.Start();
            


        }

        private void button2_Click(object sender, EventArgs e)
        {
            //string str = "[IR:Voi=D:\\dds模板.amr]";
            //str = str.Substring(8);
            //str = str.Substring(0, str.Length - 1);
            //MessageBox.Show(str);
            //for (int i = 1; i < 1001; i++)
            //{
            //    richTextBox1.Text += string.Format("10000{0}----10000{1}\r\n", new object[] { i,i});
            //}
            string str = "3607711652;997190917;";
            string[] strs = str.Split(';');
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //string str = "43534[IR:Voi=D:\\dds模板.amr]3245[IR:Voi=D:\\dds模板.amr]5555";
            //]333[IR:Voi=D:\\dds模板11.amr][IR:Voi=D:\\dds模板22.amr]22
            string str = "[IR:Voi=D:\\dds模板11.amr]333[IR:Voi=D:\\dds模板11.amr][IR:Voi=D:\\dds模板22.amr]22";
            string pattern = "\\[IR:Voi=(.)*?\\]";
            //匹配的结果
            Match match = Regex.Match(str, pattern);
            if (match.Length == 0)
            {
                MessageBox.Show("没有匹配到,直接发送：" + str);
            }
            else
            {
                MessageBox.Show("匹配到");
                MatchCollection matchCollection = Regex.Matches(str, pattern);
                string[] messages = Regex.Split(str, pattern);
                for (int i = 0; i < messages.Length; i ++)
                {
                    if (!string.IsNullOrWhiteSpace(messages[i]))
                    {
                        if (messages[i] == "r")
                        {
                            //发送语音
                            //获取当前文本 
                            //0 1 2 3 4 5 6 7  x   x - 1 / 2
                            //0 1 2 3 y 
                            MessageBox.Show("发送语音：" + matchCollection[(i - 1) / 2]);
                        }
                        else
                        {
                            //直接发当前文本
                            MessageBox.Show("发送文本：" + messages[i]);
                        }
                    }
                }
            }
            //string[] strs = Regex.Split(str, pattern);
            //foreach (var item in collection)
            //{

            //}
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string str = "[分段]111[分段]2323[分段]456788";
            string[] strs = Regex.Split(str, "\\[分段\\]");
            MessageBox.Show(strs.Length + "");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string message = "aaa[随机表情]bbb[随机表情][随机表情]";
            MessageBox.Show(FaceHelper.ReplaceRandomFace(message));
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string json = "{\"ec\":0,\"result\":{\"0\":{\"mems\":[{\"name\":\"┍&nbsp;???﹏???　\",\"uin\":997190917},{\"name\":\"昔远老师\",\"uin\":1067658602},{\"name\":\"返半佣\",\"uin\":3107711652},{\"name\":\"好券九块九\",\"uin\":3607711652}]},\"1\":{\"gname\":\"朋友\"},\"2\":{\"gname\":\"家人\"},\"3\":{\"gname\":\"同学\"}}}";
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            Dictionary<string, object> keys = serializer.Deserialize<Dictionary<string, object>>(json);
            keys = (keys["result"] as Dictionary<string, object>);
            foreach (var item in keys)
            {
               object o = item.Value;
              System.Collections.ArrayList list =  ((Dictionary<string, object>)o)["mems"] as System.Collections.ArrayList;
                foreach (var item2 in list)
                {
                    Dictionary<string, object> keys2 = item2 as Dictionary<string, object>;
                    MessageBox.Show(Convert.ToString(keys2["uin"]));
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //CancellationTokenSource cts = new CancellationTokenSource();
            //ParallelOptions pOptions = new ParallelOptions() { CancellationToken = cts.Token };
            //pOptions.MaxDegreeOfParallelism = 3;//设置并发线程数量

            //Parallel.For(0, 1000, pOptions, i =>
            //{
            //    //richTextBox1.Text += i + "\r\n";
            //    Console.WriteLine(i);
            //    Thread.Sleep(1000);
            //});
            TaskDemo.TaskContinueDemo();
            MessageBox.Show("完成了");
        }
    }
    public delegate void OutStatusDelegate(string text);
}
