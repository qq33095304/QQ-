using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace NiLiuShui.IRQQ.CSharp
{
    public class IRQQMain
    {
        [DllExport(ExportName = nameof(IR_Create), CallingConvention = CallingConvention.StdCall)]
        public static string IR_Create()
        {
            string szBuffer = "插件名称{QQBatchSend}\n插件版本{1.0.0}\n插件作者{安然}\n插件说明{批量QQ好友群发工具 - 技术支持QQ：2707711652}\n插件skey{8956RTEWDFG3216598WERDF3}插件sdk{S3}";
            return szBuffer;
        }
        [DllExport(ExportName = nameof(IR_Message), CallingConvention = CallingConvention.StdCall)]
        public static int IR_Message(string RobotQQ, int MsgType, string Msg, string Cookies, string SessionKey, string ClientKey)
        {
            return 1;
        }
        [DllExport(ExportName = nameof(IR_Event), CallingConvention = CallingConvention.StdCall)]
        ///此子程序会分发IRC_机器人QQ接收到的所有：事件，消息；您可在此函数中自行调用所有参数
        public static int IR_Event(string RobotQQ, int MsgType, int MsgCType, string MsgFrom, string TigObjF, string TigObjC, string Msg,string MsgNum ,string MsgID,string RawMsg,string Json, int pText)
        {
            if(MsgType == 1)
            {

            }
            else if (MsgType == 2)
            {

            }
            else if (MsgType == 3)
            {

            }
            else if (MsgType == 4)
            {

            }
            else if (MsgType == 5)
            {

            }
            else if (MsgType == 6)
            {

            }
            return 1;
            ///RobotQQ		机器人QQ				多Q版用于判定哪个QQ接收到该消息
            ///MsgType		消息类型				接收到消息类型，该类型可在常量表中查询具体定义，此处仅列举： - 1 未定义事件 1 好友信息 2, 群信息 3, 讨论组信息 4, 群临时会话 5, 讨论组临时会话 6, 财付通转账
            ///MsgCType		消息子类型			此参数在不同消息类型下，有不同的定义，暂定：接收财付通转账时 1为好友 2为群临时会话 3为讨论组临时会话    有人请求入群时，不良成员这里为1
            ///MsgFrom		消息来源				此消息的来源，如：群号、讨论组ID、临时会话QQ、好友QQ等
            ///TigObjF		触发对象_主动			主动发送这条消息的QQ，踢人时为踢人管理员QQ
            ///TigObjC		触发对象_被动			被动触发的QQ，如某人被踢出群，则此参数为被踢出人QQ
            ///MsgNum		消息序号				此参数暂定用于消息回复，消息撤回
            ///MsgID		消息ID				此参数暂定用于消息回复，消息撤回
            ///Msg			消息内容				常见为：对方发送的消息内容，但当IRC_消息类型为 某人申请入群，则为入群申请理由
            ///RawMsg		原始信息				特殊情况下会返回JSON结构（本身返回的就是JSON）
            ///Json			Json信息				为后期新参数预留，方便无限扩展
            ///pText		信息回传文本指针		此参数用于插件加载拒绝理由  用法：写到内存（“拒绝理由”，IRC_信息回传文本指针_Out）

            //char tenpay[512];
            //当IRC_消息类型为接收到财付通消息时候，IRC_消息内容将以：#换行符分割，1：金额；2：留言；3：单号；无留言时：1：金额；2：单号

            ///版权声明：此SDK是应{续写}邀请为IRQQ\CleverQQ编写，请合理使用无用于黄赌毒相关方面。
            ///作者QQ：1276986643,铃兰
            ///如果您对CleverQQ感兴趣，欢迎加入QQ群：476715371，进行讨论
            ///最后修改时间：2017年7月22日10:49:15

        }

        [DllExport(ExportName = nameof(IR_SetUp), CallingConvention = CallingConvention.StdCall)]
        ///启动窗体
        public static void IR_SetUp()
        {
            FormMain fm = new FormMain();
            fm.Show();
        }

        [DllExport(ExportName = nameof(IR_DestroyPlugin), CallingConvention = CallingConvention.StdCall)]
        //插件即将被销毁
        public static int IR_DestroyPlugin()
        {

            return 0;
        }
    }
}
