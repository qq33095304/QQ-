//using Sunisoft.IrisSkin;
using Common;
using Microsoft.Win32;
using QQBatchSend.IR;
using QQBatchSend.IR.AuthTool.Common;
using QQBatchSend.IR.Common;
using QQBatchSend.IR.Model;
using QQBatchSend.IR.TaskUtils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NiLiuShui.IRQQ.CSharp
{
    public partial class FormMain : Form
    {
        string messageTemplatePath = Application.StartupPath + "\\plugin\\" + "QQBatchSend.IR.Template.xml";
        RegistryHelper registryHelper = new RegistryHelper();
        private string serviceQQ = "客服QQ：2707711652";
        private DateTime beginTime = DateTime.Now;
        private int taskCount = 0;
        private int taskCompletedCount = 0;
        //标识一个多线程执行指令
        private int command = 1;

        //SkinEngine skinEngine;
        public FormMain()
        {
            InitializeComponent();
            Application.ThreadException += Application_ThreadException;
            MoonLogger.Instance.FileName = Application.StartupPath + "\\plugin\\" + "QQBatchSend.IR." +  DateTime.Now.ToString("yyyyMMdd") + ".log";
            MoonLogger.Instance.OnWriteLog += WriteLog;
            MoonLogger.Instance.OnWriteStatus += WriteStatus;
            tbRegCode.Text = ComputerInfo.GetComputerInfo();
            //读取授权信息
            string authCode = registryHelper.GetRegistryData(Registry.LocalMachine, "SOFTWARE\\QQBatchSend.IR", "License");
            if (authCode != null && authCode != "")
            {
                tbAuthCode.Text = authCode;
            }
            GetAuthState();
            ////初始化皮肤
            //skinEngine = new SkinEngine
            //{
            //    SkinFile = Application.StartupPath + "//Skins//DeepGreen.ssk"
            //};
        }

        public void WriteLog(string text)
        {
            if (InvokeRequired)
            {
                WriteLogDelegate writeLog = new WriteLogDelegate(WriteLog);
                BeginInvoke(writeLog, new object[] { text });
            }
            else
            {
                if (rtbLog.Lines.Length > 200)
                {
                    rtbLog.Text = "";
                }
                rtbLog.Text = text + rtbLog.Text;
            }
        }

        public void WriteStatus(string text)
        {
            if (InvokeRequired)
            {
                WriteStatusDelegate writeStatus = new WriteStatusDelegate(WriteStatus);
                BeginInvoke(writeStatus, new object[] { text });
            }
            else
            {
                tsslStatus.Text = text;
            }
        }
        private void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            MoonLogger.Instance.Error("未经处理的异常：\r\n"
                + "错误信息：" + e.Exception.Message + "\r\n"
                + "堆栈信息：" + e.Exception.StackTrace);
            MessageBox.Show(string.Format("出现未经处理的异常，请联系技术支持QQ：{0}处理！\r\n{1}", new string[] { "2707711652", e.Exception.Message }));
        }

        #region 授权管理
        /// <summary>
        /// 获取授权状态
        /// </summary>
        /// <returns></returns>
        private bool GetAuthState()
        {
            string authCode = registryHelper.GetRegistryData(Registry.LocalMachine, "SOFTWARE\\QQBatchSend.IR", "License");
            if (authCode != null && authCode != "")
            {
                RSACryption cryption = new RSACryption();
                string regCode = cryption.RSADecrypt(RSACryption.privateKey, authCode);
                if (regCode == ComputerInfo.GetComputerInfo())
                {
                    tsslAuthState.Text = "已授权";
                    tsslAuthState.ForeColor = Color.Green;
                    return true;
                }
            }
            tsslAuthState.Text = "未授权";
            tsslAuthState.ForeColor = Color.Red;
            return false;
        }

        private void btnAuth_Click(object sender, EventArgs e)
        {
            try
            {
                RSACryption cryption = new RSACryption();
                string regCode = cryption.RSADecrypt(RSACryption.privateKey, tbAuthCode.Text);
                if (regCode != null && regCode != "")
                {
                    //写注册表
                    registryHelper.SetRegistryData(Registry.LocalMachine, "SOFTWARE\\QQBatchSend.IR", "License", tbAuthCode.Text);
                }
                if (GetAuthState())
                {
                    MessageBox.Show("授权成功！");
                }
                else
                {
                    MessageBox.Show(string.Format("授权码不正确，请联系客服购买！{0}", new string[] { serviceQQ }));
                }
            }
            catch (Exception ex)
            {
                MoonLogger.Instance.Error("授权码解析错误：" + ex.Message);
                MessageBox.Show(string.Format("请输入正确的授权码，如需购买，请联系客服购买！{0}", new string[] { serviceQQ }));
            }
        }

        #endregion
        private void FormMain_Load(object sender, EventArgs e)
        {
            BindServiceQQ();
            tbRegCode.Text = ComputerInfo.GetComputerInfo();

            cbTemplateRule.SelectedIndex = 1;

            cbObjectType.SelectedIndexChanged -= cbList_SelectedIndexChanged;
            cbObjectType.SelectedIndex = 0;
            cbObjectType.SelectedIndexChanged += cbList_SelectedIndexChanged;

            
            InitQQList();
            

            //手动触发一次
            cbList_SelectedIndexChanged(sender, e);

            if (File.Exists(messageTemplatePath))
            {
                DataGridViewHelper.ReadXml(dgvMessageTemplate, messageTemplatePath);
            } 
        }

        /// <summary>
        /// 绑定客服QQ
        /// </summary>
        private void BindServiceQQ()
        {
            this.Text += serviceQQ;
            rtbStatement.Text += serviceQQ;
        }


        /// <summary>
        /// 初始化机器人列表
        /// </summary>
        private void InitQQList()
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            IntPtr ipQQList = IRQQApi.Api_GetQQList();
            string strQQList = Marshal.PtrToStringAnsi(ipQQList);
            if (string.IsNullOrWhiteSpace(strQQList))
            {
                MessageBox.Show("获取已登陆QQ列表失败！");
                return;
            }
            string[] robotList = Regex.Split(strQQList, "\r\n", RegexOptions.IgnoreCase);
            clbRobotList.ItemCheck -= cbList_SelectedIndexChanged;
            clbRobotList.SuspendLayout();
            clbRobotList.Items.AddRange(robotList);
            DealRobotSelect(0);
            clbRobotList.ResumeLayout();
            clbRobotList.ItemCheck += cbList_SelectedIndexChanged;
        }
        /// <summary>
        /// 初始化好友表格
        /// </summary>
        /// <param name="RobotQQ"></param>
        private void InitFriendGrid(string [] RobotQQList)
        {
            DataTable dataTable = DataHelper.GetFriendDataTableModel();
            MoonLogger.Instance.Info(string.Format("准备获取{0}个机器人的好友信息！", new object[] { RobotQQList.Length }));
            foreach (var item in RobotQQList)
            {
                DataHelper.GetFriends(item, dataTable);
            }
            dgvFriendList.DataSource = dataTable;
        }

        /// <summary>
        /// 初始化群表格
        /// </summary>
        /// <param name="RobotQQ"></param>
        private void InitGroupGrid(string[] RobotQQList)
        {
            DataTable dataTable = DataHelper.GetFriendDataTableModel();
            MoonLogger.Instance.Info(string.Format("准备获取{0}个机器人的群组信息！", new object[] { RobotQQList.Length }));
            foreach (var item in RobotQQList)
            {
                DataHelper.GetGroups(item, dataTable);
            }
            dgvFriendList.DataSource = dataTable;
        }


        /// <summary>
        /// 初始化群成员表格
        /// </summary>
        /// <param name="RobotQQ"></param>
        private void InitGroupMemberGrid(string[] RobotQQList)
        {
            DataTable dataTable = DataHelper.GetFriendDataTableModel();
            MoonLogger.Instance.Info(string.Format("准备获取{0}个机器人的群组信息！", new object[] { RobotQQList.Length }));
            foreach (var item in RobotQQList)
            {
                DataHelper.GetGroupMember(item, dataTable);
            }
            dgvFriendList.DataSource = dataTable;
        }
        #region 任务操作事件
        private void btnStart_Click(object sender, EventArgs e)
        {
            //5天测试时使用
            //if (DateExprieHelper.IsExpriredByDay(new DateTime(2018, 12, 29), 5))
            //{
            //    return;
            //}

            //检测授权
            if (!GetAuthState())
            {
                MoonLogger.Instance.Error(string.Format("软件未授权，请联系客服购买！{0}",new string[] { serviceQQ }));
                return;
            }
            string[] RobotList = GetSelectedRobot();
            if (RobotList.Length == 0)
            {
                MoonLogger.Instance.Error("未选中任何机器人，任务终止！");
            }


            //判断消息模板列表是否为空
            if (dgvMessageTemplate.Rows.Count == 0)
            {
                MoonLogger.Instance.Error("消息模板列表中暂无数据，任务终止，请先新增消息模板！");
                return;
            }

            //计算写日志间隔
            MoonLogger.Instance.WriteInterval = Convert.ToInt32(nudMaxSendNum.Value) * 10;
            if (MoonLogger.Instance.WriteInterval < 1000)
            {
                MoonLogger.Instance.WriteInterval = 1000;
            }


            //获取模板规则
            int templateRule = cbTemplateRule.SelectedIndex;
            
            List<string> messageTemplate = new List<string>();
            switch (templateRule)
            {
                case 0:
                    //如果当前没有选中，则直接退出
                    if (dgvMessageTemplate.SelectedRows.Count == 0)
                    {
                        MoonLogger.Instance.Error("当前未选中任何消息模板，消息规则为【固定选中】时必须选中一个模板，任务终止！");
                        return;
                    }
                    messageTemplate.Add(Convert.ToString(dgvMessageTemplate.SelectedRows[0].Cells["Message"].Value));
                    break;
               case 1:
                    foreach (DataGridViewRow item in dgvMessageTemplate.Rows)
                    {
                        messageTemplate.Add(Convert.ToString(item.Cells["Message"].Value));
                    }
                    break;
            }
            //设置多线程指令为运行
            command = 1;
            beginTime = DateTime.Now;
            taskCount = RobotList.Length;
            taskCompletedCount = 0;

            //获取模板中的的语音
           Dictionary<string, byte[]> Voices =  GetAllVoiceFormTemplate(messageTemplate);


            var taskList = new TaskList();
            //设置允许最大线程总数
            taskList.maxTaskNum = Convert.ToInt32(nudMaxSendNum.Value);
            //获取参数值
            int friendIntervalDown = Convert.ToInt32(nudSendIntervalDown.Value);
            int friendIntervalUp = Convert.ToInt32(nudSendIntervalUp.Value);
            int nextIntervalDown = Convert.ToInt32(nudNextIntervalDown.Value);
            int nextIntervalUp = Convert.ToInt32(nudNextIntervalUp.Value);
            int groupNum = Convert.ToInt32(nudSendGroupNum.Value);
            int groupSleepDown = Convert.ToInt32(nudSendSleepDown.Value);
            int groupSleepUp = Convert.ToInt32(nudSendSleepUp.Value);
            int sendObject = cbObjectType.SelectedIndex;
            sendObject = sendObject == 0 ? 1 : (sendObject == 1 ? 2 : 4);
            bool identifyRepeatSend = cbIdentifyRepeatSend.Checked;
            int groupMaxSendNum = Convert.ToInt32(nudGroupMaxSendNum.Value);
            int sex = cbSex.SelectedIndex;
            int online = cbOnline.SelectedIndex;
            for (int i = 0; i < RobotList.Length; i++)
            {
                ProcessHelper processHelper = new ProcessHelper
                {
                    SendParam = new SendParamModel()
                    {
                        TemplateRule = templateRule,
                        Message = messageTemplate,
                        RobotQQ = RobotList[i],
                        FriendIntervalDown = friendIntervalDown,
                        FriendIntervalUp = friendIntervalUp,
                        NextIntervalDown = nextIntervalDown,
                        NextIntervalUp = nextIntervalUp,
                        GroupNum = groupNum,
                        GroupSleepDown = groupSleepDown,
                        GroupSleepUp = groupSleepUp,
                        //1好友 2 群 4群成员
                        SendObject = sendObject,
                        Voices = Voices,
                        IdentifyRepeatSend = identifyRepeatSend,
                        GroupMaxSendNum = groupMaxSendNum,
                        Sex = sex,
                        Online = online
                    },
                    OnOutStatus = new OutStatusDelegate(OnOutStatus),
                    OnGetCommand = new GetCommandDelegate(OnGetCommand),
                    OnTaskCompleted = new TaskCompletedDelegate(OnTaskCompleted)
                };
                var testTask = new Action(() =>
                {
                    processHelper.ThreadProc();
                });
                taskList.Tasks.Add(testTask);
                Application.DoEvents();
            }
            //taskList.Start();
            Task.Run(new Action(() =>
                {
                    taskList.TaskContinueDemo();
                }
            ));
                
            SetButtonState(1);
            //ParallelOptions parallelOptions = new ParallelOptions();
            //parallelOptions.MaxDegreeOfParallelism = 2;
            //Parallel.ForEach(taskList.Tasks, parallelOptions, task =>
            //{
            //    task();
            //});
            //SetButtonState(4);
        }

        /// <summary>
        /// 获取所有消息模板中的语音
        /// </summary>
        /// <param name="messageTemplate"></param>
        /// <returns></returns>
        private Dictionary<string, byte[]> GetAllVoiceFormTemplate(List<string> messageTemplate)
        {
            Dictionary<string, byte[]> Voices = new Dictionary<string, byte[]>();
            foreach (var template in messageTemplate)
            {
                string pattern = @"\[IR:Voi=(.)*?\]";

                foreach (Match match in Regex.Matches(template, pattern))
                {
                    //获取语音路径
                    string path = match.Value;
                    path = path.Substring(8);
                    path = path.Substring(0, path.Length - 1);
                    if (!Voices.ContainsKey(match.Value))
                    {
                        byte[] bytes = FileUtils.GetFileData(path);
                        //直接用[IR:Voi=xxx.amr]作为key
                        Voices.Add(match.Value, bytes);
                    }
                }
            }
            return Voices;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            btnStop.Enabled = false;
            btnPause.Enabled = false;
            btnRest.Enabled = false;
            command = 4;
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            btnStop.Enabled = false;
            btnPause.Enabled = false;
            btnRest.Enabled = false;
            command = 2;
            SetButtonState(2);
        }

        private void btnRest_Click(object sender, EventArgs e)
        {
            btnStop.Enabled = false;
            btnPause.Enabled = false;
            btnRest.Enabled = false;
            command = 3;
            SetButtonState(3);
        }
        #endregion

        #region 发送线程委托事件
        /// <summary>
        /// 输出状态信息
        /// </summary>
        /// <param name="list">状态描述</param>
        public void OnOutStatus(string text)
        {
            if (InvokeRequired)
            {
                OutStatusDelegate outStatusDelegate = new OutStatusDelegate(OnOutStatus);
                BeginInvoke(outStatusDelegate, new object[] { text });
            }
            else
            {
                MoonLogger.Instance.Info(text);
                //tsslStatus.Text = text;
            }
        }

        /// <summary>
        /// 获取当前多线程指令
        /// </summary>
        /// <param name="list">状态描述</param>
        public int OnGetCommand()
        {
            return command;
        }

        public void OnTaskCompleted(string text)
        {
            if (InvokeRequired)
            {
                TaskCompletedDelegate taskCompletedDelegate = new TaskCompletedDelegate(OnTaskCompleted);
                BeginInvoke(taskCompletedDelegate, new object[] { text });
            }
            else
            {
                MoonLogger.Instance.Info(text);
                Interlocked.Increment(ref taskCompletedCount);
                //多线程时会出错
                //taskCompletedCount++;

                text = string.Format("当前已完成{0}个任务,还剩{1}个任务！", new object[] { taskCompletedCount, taskCount - taskCompletedCount });
                MoonLogger.Instance.Info(text);
                //tsslStatus.Text = text;

                //如果完成任务总数等于任务总数
                if (taskCount == taskCompletedCount)
                {
                    text = string.Format("所有机器人发送任务已完成，总用时：{0:F}秒", new object[] { (DateTime.Now - beginTime).TotalSeconds });
                    MoonLogger.Instance.Info(text);
                   // tsslStatus.Text = text;
                    SetButtonState(4);
                }
            }
        }
        #endregion

        #region 控件状态控制
        /// <summary>
        /// 设置按钮状态
        /// </summary>
        /// <param name="type"></param>
        private void SetButtonState(int type)
        {
            switch (type)
            {
                //启动线程
                case 1:
                    btnStart.Enabled = false;
                    btnPause.Enabled = true;
                    btnRest.Enabled = false;
                    btnStop.Enabled = true;
                    AllowEdit(false);

                    break;
                //暂停线程
                case 2:
                    btnStart.Enabled = false;
                    btnPause.Enabled = false;
                    btnRest.Enabled = true;
                    btnStop.Enabled = true;
                    AllowEdit(false);
                    break;
                //恢复线程
                case 3:
                    btnStart.Enabled = false;
                    btnPause.Enabled = true;
                    btnRest.Enabled = false;
                    btnStop.Enabled = true;
                    AllowEdit(false);
                    break;
                //任务完成
                case 4:
                    btnStart.Enabled = true;
                    btnPause.Enabled = false;
                    btnRest.Enabled = false;
                    btnStop.Enabled = false;
                    AllowEdit(true);
                    break;
            }
        }

        /// <summary>
        /// 是否同意编辑
        /// </summary>
        /// <param name="allow"></param>
        private void AllowEdit(bool allow)
        {
            nudSendIntervalDown.Enabled = allow;
            nudSendIntervalUp.Enabled = allow;
            nudNextIntervalDown.Enabled = allow;
            nudNextIntervalUp.Enabled = allow;
            nudSendGroupNum.Enabled = allow;
            nudSendSleepDown.Enabled = allow;
            nudSendSleepUp.Enabled = allow;
            cbObjectType.Enabled = allow;
            cbTemplateRule.Enabled = allow;
            cbChangedGetFriend.Enabled = allow;
            //cbcbRobotList.Enabled = allow;
            clbRobotList.Enabled = allow;
            nudMaxSendNum.Enabled = allow;
            btnRobotAll.Enabled = allow;
            btnRobotReverse.Enabled = allow;
            btnRobotClear.Enabled = allow;
            btnRobotOnline.Enabled = allow;
        }
        #endregion

        #region 模板消息变量操作
        private void InsertMessage(string msg)
        {
            int i = rtbMessage.SelectionStart;
            rtbMessage.Focus();
            rtbMessage.Text = rtbMessage.Text.Insert(i, msg);
            rtbMessage.Select(i + msg.Length, 0);
        }

        private void ilTimePer_Click(object sender, EventArgs e)
        {
            InsertMessage("[时段]");
        }

        private void ilTime_Click(object sender, EventArgs e)
        {
            InsertMessage("[时间]");
        }

        private void ilName_Click(object sender, EventArgs e)
        {
            InsertMessage("[昵称]");
        }
        private void ilVoice_Click(object sender, EventArgs e)
        {
            ofdInsertImage.Filter = "语音文件(*.amr)|*.amr";
            if (ofdInsertImage.ShowDialog() == DialogResult.OK)
            {
                InsertMessage("[IR:Voi=" + ofdInsertImage.FileName + "]");
            }
        }
        private void ilInsertImage_Click(object sender, EventArgs e)
        {
            ofdInsertImage.Filter = "图片文件(*.jpg;*.png;*.gif;*.bmp)|*.jpg;*.png;*.gif;*.bmp";
            if (ofdInsertImage.ShowDialog() == DialogResult.OK)
            {
                InsertMessage("[IR:pic=" + ofdInsertImage.FileName + "]");
            }
        }

        private void ilRandomFace_Click(object sender, EventArgs e)
        {
            InsertMessage("[随机表情]");
        }

        private void ilNext_Click(object sender, EventArgs e)
        {
            InsertMessage("[分段]");
        }
        #endregion

        #region 模板消息编辑事件
        private void btnAdd_Click(object sender, EventArgs e)
        {
            dgvMessageTemplate.Rows.Add(new object[] { dgvMessageTemplate.Rows.Count + 1, rtbMessage.Text });
            DataGridViewHelper.WriteXml(dgvMessageTemplate, messageTemplatePath);
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            dgvMessageTemplate.SelectedRows[0].Cells["Message"].Value = rtbMessage.Text;
            DataGridViewHelper.WriteXml(dgvMessageTemplate, messageTemplatePath);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            dgvMessageTemplate.Rows.RemoveAt(dgvMessageTemplate.SelectedRows[0].Index);
            DataGridViewHelper.WriteXml(dgvMessageTemplate, messageTemplatePath);
        }

        private void dgvMessageTemplate_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvMessageTemplate.SelectedRows.Count > 0)
            {
                btnModify.Enabled = true;
                btnDelete.Enabled = true;
                rtbMessage.Text = dgvMessageTemplate.SelectedRows[0].Cells["Message"].Value.ToString();
            }
            else
            {
                btnModify.Enabled = false;
                btnDelete.Enabled = false;
                rtbMessage.Text = "";
            }
        }
        #endregion


        
        /// <summary>
        /// 获取选中的机器人
        /// </summary>
        /// <returns></returns>
        private string[] GetSelectedRobot()
        {
            string[] checkeds = new string[clbRobotList.CheckedItems.Count];
            clbRobotList.CheckedItems.CopyTo(checkeds, 0);
            return checkeds;
        }

        private void cbList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //先清空好友列表
            DataTable dataTable = dgvFriendList.DataSource as DataTable;
            if (dataTable != null)
            {
                dataTable.Clear();
                dgvFriendList.DataSource = dataTable;
            }

            if (!cbChangedGetFriend.Checked)
            {
                return;
            }
            string[] robotList = GetSelectedRobot();
            //获取选中的机器人列表
            //List<string> selectedRobot = new List<string>();
            MoonLogger.Instance.Info("获取选中机器人的值：" + string.Join(",",robotList));
            //return;

            if (robotList.Length == 0)
            {
                return;
            }

            if (cbObjectType.SelectedIndex == 0)
            {
                dgvFriendList.Columns[1].HeaderText = "QQ";
                dgvFriendList.Columns[2].HeaderText = "昵称";
                gbFriendList.Text = "好友列表";
                InitFriendGrid(robotList);
            }
            else if (cbObjectType.SelectedIndex == 1)
            {
                dgvFriendList.Columns[1].HeaderText = "群号";
                dgvFriendList.Columns[2].HeaderText = "群名称";
                gbFriendList.Text = "群列表";
                InitGroupGrid(robotList);
            }
            else if (cbObjectType.SelectedIndex == 2)
            {
                dgvFriendList.Columns[1].HeaderText = "QQ";
                dgvFriendList.Columns[2].HeaderText = "昵称";
                gbFriendList.Text = "群成员列表";
                InitGroupMemberGrid(robotList);
            }
            
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }




        #region 机器人列表操作事件
        /// <summary>
        /// 处理机器人列表按钮单击
        /// </summary>
        /// <param name="type"></param>
        private void DealRobotSelect(int type)
        {
            clbRobotList.ItemCheck -= cbList_SelectedIndexChanged;
            clbRobotList.SuspendLayout();
            string[] onlineList = new string[0];
            if (type == 3)
            {
                IntPtr ipOnlineList = IRQQApi.Api_GetOnLineList();
                string strOnlineList = Marshal.PtrToStringAnsi(ipOnlineList);
                if (!string.IsNullOrWhiteSpace(strOnlineList))
                {
                    strOnlineList = (new Regex("\r\n")).Replace(strOnlineList, ";");
                    MoonLogger.Instance.Info("获取到在线机器人数据：" + strOnlineList + strOnlineList.Length);
                    onlineList = strOnlineList.Split(';');
                    MoonLogger.Instance.Info("获取到在线机器人总数：" + onlineList.Length);
                }
            }

            if (type == 3)
            {
                for (var i = 0; i < clbRobotList.Items.Count; i++)
                {
                    foreach (var item in onlineList)
                    {
                        bool found = clbRobotList.GetItemText(clbRobotList.Items[i]).Equals(item);
                        clbRobotList.SetItemChecked(i, found);
                        if (found)
                        {
                            break;
                        }
                    }
                }
            }
            else
            {
                for (var i = 0; i < clbRobotList.Items.Count; i++)
                {
                    //选择在线
                    clbRobotList.SetItemChecked(i, type == 0 ? true : (type == 1 ? !clbRobotList.GetItemChecked(i) : false));
                    Application.DoEvents();
                }
            }
            clbRobotList.ResumeLayout();
            //手动触发一次
            cbList_SelectedIndexChanged(null, null);
            clbRobotList.ItemCheck += cbList_SelectedIndexChanged;
        }
        private void btnRobotAll_Click(object sender, EventArgs e)
        {
            DealRobotSelect(0);
        }

        private void btnRobotReverse_Click(object sender, EventArgs e)
        {
            DealRobotSelect(1);
        }

        private void btnRobotClear_Click(object sender, EventArgs e)
        {
            DealRobotSelect(2);
        }

        private void btnRobotOnline_Click(object sender, EventArgs e)
        {
            DealRobotSelect(3);
        }

        #endregion


    }
}
