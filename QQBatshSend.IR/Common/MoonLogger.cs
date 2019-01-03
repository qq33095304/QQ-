using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QQBatchSend.IR
{
    public class MoonLogger
    {
        private static readonly object SynObject = new object();
        private static MoonLogger _instance = null;
        private static List<string> infoList = new List<string>();
        private static Dictionary<long, long> lockDic = new Dictionary<long, long>();
        public int WriteInterval = 1000;
        public WriteLogDelegate OnWriteLog { get; set; }
        public WriteStatusDelegate OnWriteStatus { get; set; }

        /// <summary>  
        /// 获取或设置文件名称  
        /// </summary>  
        public string FileName { get; set; }


        private MoonLogger()
        {
            //开启一个定时写日志的任务
            Task.Run(new Action(() =>
            {
                while (true)
                {
                    lock (SynObject)
                    {
                        if (infoList.Count > 0)
                        {
                            StringBuilder stringBuilder = new StringBuilder();
                            for (int i = 0; i < infoList.Count; i++)
                            {
                                if (i < 200)
                                {
                                    stringBuilder.Append(infoList[i] + "\r\n");
                                }
                                WriteLine(infoList[i]);
                            }
                            OnWriteLog?.Invoke(stringBuilder.ToString());
                            OnWriteStatus?.Invoke(infoList[0]);
                            infoList.Clear();
                        }
                        
                    }
                    Thread.Sleep(WriteInterval);
                }
            }));
        }


        /// <summary>
        /// 获取一个实例
        /// </summary>
        public static MoonLogger Instance
        {
            get
            {
                lock (SynObject)
                {
                    return _instance ?? (_instance = new MoonLogger());
                }
            }
        }

        /// <summary>  
        /// 创建文件  
        /// </summary>  
        /// <param name="fileName"></param>  
        public void Create(string fileName)
        {
            if (!System.IO.File.Exists(fileName))
            {
                using (System.IO.FileStream fs = System.IO.File.Create(fileName))
                {
                    fs.Close();
                }
            }
        }

        /// <summary>  
        /// 写入文本  
        /// </summary>  
        /// <param name="content">文本内容</param>  
        private void Write(string content, string newLine)
        {
            if (string.IsNullOrEmpty(FileName))
            {
                throw new Exception("FileName不能为空！");
            }
            using (System.IO.FileStream fs = new System.IO.FileStream(FileName, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite, System.IO.FileShare.ReadWrite, 8, System.IO.FileOptions.Asynchronous))
            {
                Byte[] dataArray = System.Text.Encoding.Default.GetBytes(content + newLine);
                bool flag = true;
                long slen = dataArray.Length;
                long len = 0;
                while (flag)
                {
                    try
                    {
                        if (len >= fs.Length)
                        {
                            fs.Lock(len, slen);
                            lockDic[len] = slen;
                            flag = false;
                        }
                        else
                        {
                            len = fs.Length;
                        }
                    }
                    catch (Exception ex)
                    {
                        while (!lockDic.ContainsKey(len))
                        {
                            len += lockDic[len];
                        }
                    }
                }
                fs.Seek(len, System.IO.SeekOrigin.Begin);
                fs.Write(dataArray, 0, dataArray.Length);
                fs.Close();
            }
        }
        /// <summary>  
        /// 写入文件内容  
        /// </summary>  
        /// <param name="content"></param>  
        public void WriteLine(string content)
        {
            this.Write(content, System.Environment.NewLine);
        }
        /// <summary>  
        /// 写入文件  
        /// </summary>  
        /// <param name="content"></param>  
        public void Write(string content)
        {
            this.Write(content, "");
        }

       

        public void Debug(string info)
        {
            FlushInfo("Debug", info);
        }

        public void Info(string info)
        {
            FlushInfo("Info", info);
        }

        public void Warn(string info)
        {
            FlushInfo("Warn", info);
        }

        public void Error(string info)
        {
            FlushInfo("Error", info);
            MessageBox.Show(info);
        }

        public void ErrorNoDialog(string info)
        {
            FlushInfo("Error", info);
        }

        private void FlushInfo(string level, string info)
        {
                string content = DateTime.Now.ToString("F") + " [" + level + "] " + info;
                    lock(SynObject)
                    {
                        infoList.Insert(0, content);
                    }              
        }
    }

    public delegate void WriteLogDelegate(string text);
    public delegate void WriteStatusDelegate(string text);
}
