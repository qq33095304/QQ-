using NiLiuShui.IRQQ.CSharp;
using QQBatchSend.IR.Common;
using QQBatchSend.IR.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace QQBatchSend.IR
{
    /// <summary>
    /// 处理帮助类
    /// </summary>
    public class ProcessHelper
    {
        public ProcessHelper()
        {
        }

        public SendParamModel SendParam { get; set; }
        public OutStatusDelegate OnOutStatus { get; set; }

        public GetCommandDelegate OnGetCommand { get; set; }

        public TaskCompletedDelegate OnTaskCompleted { get; set; }

        

        public void ThreadProc()
        {
            DateTime beginTime = DateTime.Now;
            //发送总数
            int sendCount = 0;
            string objectText = SendParam.SendObject == 1 ? "好友" : (SendParam.SendObject == 2 ? "群" : (SendParam.SendObject == 4 ? "群成员" : ""));
            int friendCount = 0;
            try
            {
                int? commandExce = 1;
                commandExce = OnGetCommand?.Invoke();
                //如果收到停止指令，则直接退出
                if (commandExce == 4)
                {
                    throw new Exception("收到停止指令，任务自动取消！");
                }
                //获取好友或者群数据
                DataTable dataTable = null;
                if (SendParam.SendObject == 1)
                {
                    OnOutStatus?.Invoke(string.Format("准备获取机器人[{0}]的好友数据！", new object[] { SendParam.RobotQQ }));
                    dataTable = DataHelper.GetFriends(SendParam.RobotQQ);
                    OnOutStatus?.Invoke(string.Format("获取机器人[{0}]的好友数据成功！", new object[] { SendParam.RobotQQ }));
                }
                else if (SendParam.SendObject == 2)
                {
                    OnOutStatus?.Invoke(string.Format("准备获取机器人[{0}]的群组数据！", new object[] { SendParam.RobotQQ }));
                    dataTable = DataHelper.GetGroups(SendParam.RobotQQ);
                    OnOutStatus?.Invoke(string.Format("获取机器人[{0}]的群组数据成功！", new object[] { SendParam.RobotQQ }));
                }
                else if (SendParam.SendObject == 4)
                {

                }
                if (dataTable == null)
                {
                    OnOutStatus?.Invoke(string.Format("机器人[{0}]获取数据失败！", new object[] { SendParam.RobotQQ }));
                    return;
                }

                //待输出界面的消息列表
                List<string> infos = new List<string>();
                friendCount = dataTable.Rows.Count;
                OnOutStatus?.Invoke(string.Format("机器人[{0}]开始发送数据,{1}总数：{2}", new object[] { SendParam.RobotQQ, objectText, friendCount }));
                
                //从第二行开始遍历
                for (int i = 0; i <= friendCount; i++)
                {
                    bool outCommandInfo = true;

                    //获取命令，判断当前是否执行
                    labelCommand:
                    int? command = 1;
                    command = OnGetCommand?.Invoke();
                    switch (command)
                    {
                        case 1:
                            break;
                        case 2:
                            if (outCommandInfo)
                            {
                                OnOutStatus?.Invoke(string.Format("机器人[{0}]收到暂停指令，当前任务已暂停！", new object[] { SendParam.RobotQQ }));
                            }
                            outCommandInfo = false;
                            break;
                        case 3:
                            OnOutStatus?.Invoke(string.Format("机器人[{0}]收到恢复指令，当前任务已恢复！", new object[] { SendParam.RobotQQ }));
                            break;
                        case 4:
                            OnOutStatus?.Invoke(string.Format("机器人[{0}]收到停止指令，当前任务已取消！", new object[] { SendParam.RobotQQ }));
                            break;
                    }
                    //如果是暂停指令，则不停检测是否收到恢复指令或者停止指令
                    if (command == 2)
                    {
                        Thread.Sleep(1000);
                        goto labelCommand;
                    }
                    //如果是停止执行，则停止当前处理
                    else if (command == 4)
                    {
                        break;
                    }

                    string uin = Convert.ToString(dataTable.Rows[i]["uin"]);
                    string name = Convert.ToString(dataTable.Rows[i]["name"]);
                    //发送消息
                    SendMsg(uin,uin, name, sendCount, objectText, SendParam);
                    sendCount++;

                    //回传当前状态
                    OnOutStatus?.Invoke(string.Format("机器人[{0}]成功给第{1}个{2}[{3}-{4}]发送消息！", new object[] { SendParam.RobotQQ, sendCount, objectText, uin, name }));

                    //如果当前已发送完，则停止
                    if (sendCount == dataTable.Rows.Count)
                    {
                        break;
                    }

                    //当到达每组发送总数时进行暂停
                    if (sendCount % SendParam.GroupNum == 0)
                    {
                        int groupSleep = PublicUtils.GetRandom(SendParam.GroupSleepDown, SendParam.GroupSleepUp + 1);
                        OnOutStatus?.Invoke(string.Format("机器人[{0}]进入组休眠{1}秒！", new object[] { SendParam.RobotQQ, groupSleep }));
                        Thread.Sleep(groupSleep * 1000);
                    }
                    else
                    {
                        //算出每个休眠时间
                        int interval = PublicUtils.GetRandom(SendParam.FriendIntervalDown, SendParam.FriendIntervalUp + 1);
                        OnOutStatus?.Invoke(string.Format("机器人[{0}]进入好友休眠{1}秒！", new object[] { SendParam.RobotQQ, interval }));
                        Thread.Sleep(interval * 1000);
                    }
                }
            }
            catch (Exception ex)
            {
                OnOutStatus?.Invoke(string.Format("机器人[{0}]执行任务出错了，任务自动取消！成功发送{1}，未发送{2}，错误详情：{3}", new object[] { SendParam.RobotQQ, sendCount, friendCount - sendCount, ex.Message }));
            }
            finally
            {
                OnTaskCompleted?.Invoke(string.Format("机器人[{0}]任务已完成！用时：{1:F}秒，共发送{2}个{3}", new object[] { SendParam.RobotQQ, (DateTime.Now - beginTime).TotalSeconds, sendCount, objectText }));
            }
        }

        /// <summary>
        /// 获取随机消息
        /// </summary>
        /// <param name="SendParam"></param>
        /// <returns></returns>
        private string GetRandomMessage(SendParamModel SendParam)
        {
            string tempMessage = "";
            //固定消息，只会传一条消息模板过来
            if (SendParam.TemplateRule == 0)
            {
                tempMessage = SendParam.Message[0];
            }
            //随机消息，随机获取
            else if (SendParam.TemplateRule == 1)
            {
                int messageIndex = PublicUtils.GetRandom(0, SendParam.Message.Count);
                tempMessage = SendParam.Message[messageIndex];
            }

            //替换时间变量
            tempMessage = tempMessage.Replace("[时间]", "[Time]");
            //替换昵称变量
            tempMessage = tempMessage.Replace("[昵称]", "[ObjName]");
            //替换时段
            tempMessage = tempMessage.Replace("[时段]", "[TimePer]");
            //替换分段
            //自己进行分段
            //tempMessage = tempMessage.Replace("[分段]", "[Next]");

            //替换随机表情
            //tempMessage = tempMessage.Replace("[随机表情]", "[RFace]");
            return FaceHelper.ReplaceRandomFace(tempMessage);
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="RobotQQ"></param>
        /// <param name="MsgType"></param>
        /// <param name="MsgTo"></param>
        /// <param name="ObjQQ"></param>
        /// <param name="Msg"></param>
        /// <param name="ABID"></param>
        /// <param name="SendParam"></param>
        private void SendMsg(string MsgTo,string ObjQQ, string name, int sendCount, string objectText, SendParamModel SendParam)
        {
            //每发一次消息获取一个随机消息
            string tempMessage = GetRandomMessage(SendParam);
            //如果有语音，则拆分发送
            string pattern = "\\[IR:Voi=(.)*?\\]";
            //当前模板存在语音
            if (Regex.Match(tempMessage, pattern).Length > 0)
            {
                MatchCollection matchCollection = Regex.Matches(tempMessage, pattern);
                string[] messages = Regex.Split(tempMessage, pattern);
                for (int j = 0; j < messages.Length; j++)
                {
                    if (!string.IsNullOrEmpty(messages[j]))
                    {
                        if (messages[j] == "r")
                        {
                            //发送语音
                            //获取当前文本 
                            //0 1 2 3 4 5 6 7  x   x - 1 / 2
                            //0 1 2 3 y 
                            //如果语音数据列表存在，则发送，否则提示不存在
                            //获取语音标签
                            string voiceKey = matchCollection[(j - 1) / 2].Value;
                            if (SendParam.Voices.ContainsKey(voiceKey))
                            {
                                if (SendParam.SendObject == 1)
                                {
                                    IRQQApi.Api_SendVoice(SendParam.RobotQQ, ObjQQ, SendParam.Voices[voiceKey]);
                                }
                                else
                                {
                                    string guid = IRQQApi.Api_UpLoadVoice(SendParam.RobotQQ, 2, ObjQQ, SendParam.Voices[voiceKey]);
                                    OnOutStatus?.Invoke(string.Format("获取到的GUID:{0}", new string[] { guid }));
                                    IRQQApi.Api_SendMsg(SendParam.RobotQQ, 2, MsgTo, ObjQQ, guid, 0);
                                }
                                OnOutStatus?.Invoke(string.Format("机器人[{0}]发送语音给第{1}个{2}[{3}-{4}]发送语音成功：{5}", new object[] { SendParam.RobotQQ, sendCount + 1, objectText, ObjQQ, name, voiceKey }));
                            }
                            else
                            {
                                OnOutStatus?.Invoke(string.Format("机器人[{0}]发送语音给第{1}个{2}[{3}-{4}]失败，找不到语音文件{5}", new object[] { SendParam.RobotQQ, sendCount + 1, objectText, ObjQQ, name, voiceKey }));
                            }
                        }
                        else
                        {
                            //直接发当前文本
                            SendTextMsg(MsgTo, ObjQQ, messages[j], 1, SendParam);
                            OnOutStatus?.Invoke(string.Format("机器人[{0}]发送语音给第{1}个{2}[{3}-{4}]发送拆分文本成功：{5}", new object[] { SendParam.RobotQQ, sendCount + 1, objectText, ObjQQ, name, messages[j] }));
                        }
                        //算出分段间隔时间
                        int interval = PublicUtils.GetRandom(SendParam.NextIntervalDown, SendParam.NextIntervalUp + 1);
                        OnOutStatus?.Invoke(string.Format("机器人[{0}]进入语音分段休眠{1}秒！", new object[] { SendParam.RobotQQ, interval }));
                        Thread.Sleep(interval * 1000);
                    }
                }
            }
            else
            {
                SendTextMsg(MsgTo, ObjQQ, tempMessage, 1, SendParam);
            }
        }



        /// <summary>
        /// 发送文本消息，主要处理框架分段发送会缺失以及顺序会乱的问题，自己分段并且加上延时
        /// </summary>
        /// <param name="RobotQQ"></param>
        /// <param name="MsgType"></param>
        /// <param name="MsgTo"></param>
        /// <param name="ObjQQ"></param>
        /// <param name="Msg"></param>
        /// <param name="ABID"></param>
        /// <param name="SendParam"></param>
        private void SendTextMsg(string MsgTo,string ObjQQ, string Msg, int ABID, SendParamModel SendParam)
        {
            string pattern = "\\[分段\\]";
            string[] msessages = Regex.Split(Msg, pattern);
            for(int i = 0; i < msessages.Length; i ++)
            {
                if (!string.IsNullOrEmpty(msessages[i]))
                {
                    IRQQApi.Api_SendMsg(SendParam.RobotQQ, SendParam.SendObject, MsgTo, ObjQQ, msessages[i], 1);
                    //如果不是最后一次，则进入文本分段休眠
                    if (i != msessages.Length - 1)
                    {
                        int interval = PublicUtils.GetRandom(SendParam.NextIntervalDown, SendParam.NextIntervalUp + 1);
                        OnOutStatus?.Invoke(string.Format("机器人[{0}]进入文本分段休眠{1}秒！", new object[] { SendParam.RobotQQ, interval }));
                        Thread.Sleep(interval * 1000);
                    }
                }
            }
        }


    }
    /// <summary>
    /// 输出当前状态委托
    /// </summary>
    /// <param name="text"></param>
    public delegate void OutStatusDelegate(string text);

    /// <summary>
    /// 输出当前完成的任务
    /// </summary>
    /// <param name="text"></param>
    public delegate void TaskCompletedDelegate(string text);


    /// <summary>
    /// 获取命令委托 1正常运行 2暂停 3恢复 4 停止
    /// </summary>
    /// <returns></returns>
    public delegate int GetCommandDelegate();
}
