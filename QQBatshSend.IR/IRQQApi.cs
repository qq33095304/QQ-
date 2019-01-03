using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace NiLiuShui.IRQQ.CSharp
{
    public static class IRQQApi
    {
        [DllImport("../IRapi.dll", EntryPoint = "Api_GetAge")]
        public static extern int ApiGetAge(string RobotQQ, string ObjQQ);


        [DllImport("../IRapi.dll")]
        ///<summary>
        ///将好友拉入黑名单，成功返回真，失败返回假
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        ///<param name="ObjQQ">欲拉黑的QQ</param>
        public static extern bool Api_AddBkList(string RobotQQ, string ObjQQ);
        [DllImport("../IRapi.dll")]
        ///<summary>
        ///向框架帐号列表增加一个登录QQ，成功返回真（CleverQQ可用）
        ///</summary>
        ///<param name="RobotQQ">帐号</param>
        ///<param name="PassWord">密码</param>
        ///<param name="Auto">自动登录</param>
        public static extern string Api_AddQQ(string RobotQQ, string PassWord, bool Auto);
        [DllImport("../IRapi.dll")]
        ///<summary>
        ///管理员邀请对象入群，每次只能邀请一个对象，频率过快会失败
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        ///<param name="ObjQQ">被邀请对象QQ</param>
        ///<param name="GroupNum">欲邀请加入的群号</param>
        public static extern void Api_AdminInviteGroup(string RobotQQ, string ObjQQ, string GroupNum);
        [DllImport("../IRapi.dll")]
        ///<summary>
        ///创建一个讨论组，成功返回讨论组ID，失败返回空
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        ///<param name="DisGroupName">讨论组名称</param>
        public static extern string Api_CreateDisGroup(string RobotQQ, string DisGroupName);
        [DllImport("../IRapi.dll")]
        ///<summary>
        ///<param name="RobotQQ">响应的QQ</param>
        ///<param name="ObjQQ">对象QQ</param>
        public static extern void Api_DelBkList(string RobotQQ, string ObjQQ);
        [DllImport("../IRapi.dll")]
        ///<summary>
        ///删除好友，成功返回真，失败返回假
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        ///<param name="ObjQQ">欲删除对象QQ</param>
        public static extern bool Api_DelFriend(string RobotQQ, string ObjQQ);
        [DllImport("../IRapi.dll")]
        ///<summary>
        ///请求禁用插件自身
        ///</summary>
        public static extern void Api_DisabledPlugin();
        [DllImport("../IRapi.dll")]
        ///<summary>
        ///取得机器人网页操作用参数Bkn或G_tk
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        public static extern string Api_GetBkn(string RobotQQ);
        [DllImport("../IRapi.dll")]
        ///<summary>
        ///取得机器人网页操作用参数长Bkn或长G_tk
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        public static extern string Api_GetBkn32(string RobotQQ);
        [DllImport("../IRapi.dll")]
        ///<summary>
        ///取得腾讯微博页面操作用参数P_skey
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        public static extern string Api_GetBlogPsKey(string RobotQQ);
        [DllImport("../IRapi.dll")]
        ///<summary>
        ///取得腾讯课堂页面操作用参数P_skye
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        public static extern string Api_GetClassRoomPsKey(string RobotQQ);
        [DllImport("../IRapi.dll")]
        ///<summary>
        ///取得机器人网页操作用的Clientkey
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        public static extern string Api_GetClientkey(string RobotQQ);
        [DllImport("../IRapi.dll")]
        ///<summary>
        ///取得机器人网页操作用的Cookies
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        public static extern string Api_GetCookies(string RobotQQ);
        [DllImport("../IRapi.dll")]
        ///<summary>
        ///取得讨论组列表
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        public static extern string Api_GetDisGroupList(string RobotQQ);
        [DllImport("../IRapi.dll")]
        ///<summary>
        ///取邮箱，当对象QQ不为10000@qq.com时，可用于获取正确邮箱
        ///</summary>
        ///<param name="RobotQQ">响应的QQ</param>
        ///<param name="ObjQQ">对象QQ</param>
        public static extern string Api_GetEmail(string RobotQQ, string ObjQQ);
        [DllImport("../IRapi.dll")]
        ///<summary>
        ///取得好友列表，返回获取到的原始JSON格式信息，需自行解析
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        public static extern IntPtr Api_GetFriendList(string RobotQQ);
        [DllImport("../IRapi.dll")]
        ///<summary>
        ///取对象性别 1男 2女 未知或失败返回-1
        ///</summary>
        ///<param name="RobotQQ">响应的QQ</param>
        ///<param name="ObjQQ">对象QQ</param>
        public static extern int Api_GetGender(string RobotQQ, string ObjQQ);
        [DllImport("../IRapi.dll")]
        ///<summary>
        ///取得群管理员，返回获取到的原始JSON格式信息，需自行解析
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        ///<param name="GroupNum">欲取管理员列表群号</param>
        public static extern string Api_GetGroupAdmin(string RobotQQ, string GroupNum);
        [DllImport("../IRapi.dll")]
        ///<summary>
        ///取对象群名片
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        ///<param name="GroupNum">群号</param>
        ///<param name="ObjQQ">欲取得群名片的QQ号码</param>
        public static extern string Api_GetGroupCard(string RobotQQ, string GroupNum, string ObjQQ);
        [DllImport("../IRapi.dll")]
        ///<summary>
        ///取得群列表，返回获取到的原始JSON格式信息，需自行解析
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        public static extern IntPtr Api_GetGroupList(string RobotQQ);
        [DllImport("../IRapi.dll")]
        ///<summary>
        ///取得群成员列表，返回获取到的原始JSON格式信息，需自行解析
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        ///<param name="GroupNum">欲取群成员列表群号</param>
        public static extern IntPtr Api_GetGroupMemberList(string RobotQQ, string GroupNum);
        [DllImport("../IRapi.dll")]
        ///<summary>
        ///取QQ群名
        ///</summary>
        ///<param name="RobotQQ">响应的QQ</param>
        ///<param name="GroupNum">群号</param>
        public static extern string Api_GetGroupName(string RobotQQ, string GroupNum);
        [DllImport("../IRapi.dll")]
        ///<summary>
        ///取得QQ群页面操作用参数P_skye
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        public static extern string Api_GetGroupPsKey(string RobotQQ);
        [DllImport("../IRapi.dll")]
        ///<summary>
        ///取框架日志
        ///</summary>
        public static extern string Api_GetLog();
        [DllImport("../IRapi.dll")]
        ///<summray>
        ///取得机器人操作网页用的长Clientkey
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        public static extern string Api_GetLongClientkey(string RobotQQ);
        [DllImport("../IRapi.dll")]
        ///<summary>
        ///取得机器人操作网页用参数长Ldw
        ///</summray>
        ///<param name="RobotQQ">机器人QQ</param>
        public static extern string Api_GetLongLdw(string RobotQQ);
        [DllImport("../IRapi.dll")]
        ///<summary>
        ///取对象昵称
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        ///<param name="ObjQQ">欲取得的QQ号码</param>
        public static extern string Api_GetNick(string RobotQQ, string ObjQQ);
        [DllImport("../IRapi.dll")]
        ///<summary>
        ///取群公告，返回该群所有公告，JSON格式，需自行解析
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        ///<param name="GroupNum">欲取得公告的群号</param>
        public static extern string Api_GetNotice(string RobotQQ, string GroupNum);
        [DllImport("../IRapi.dll")]
        ///<summary>
        ///获取对象资料，此方式为http，调用时应自行注意控制频率（成功返回JSON格式需自行解析）
        ///</summary>
        ///<param name="RobotQQ">响应的QQ</param>
        ///<param name="ObjQQ">对象QQ</param>
        public static extern string Api_GetObjInfo(string RobotQQ, string ObjQQ);
        [DllImport("../IRapi.dll")]
        ///<summary>
        ///取对象QQ等级，成功返回等级，失败返回-1
        ///</summary>
        ///<param name-="RobotQQ">机器人QQ</param>
        ///<param name="ObjQQ">欲取得的QQ号码</param>
        public static extern int Api_GetObjLevel(string RobotQQ, string ObjQQ);
        [DllImport("../IRapi.dll")]
        ///<summary>
        ///获取对象当前赞数量，石板返回-1，成功返回赞数量（获取频繁会出现失败现象，请自行写判断处理失败问题）
        ///</summary>
        ///<param name="RobotQQ">响应的QQ</param>
        ///<param name="ObjQQ">对象QQ</param>
        public static extern long Api_GetObjVote(string RobotQQ, string ObjQQ);
        [DllImport("../IRapi.dll")]
        ///<summary>
        ///取框架离线QQ号（多Q版可用）
        ///</summary>
        public static extern string Api_GetOffLineList();
        [DllImport("../IRapi.dll")]
        ///<summary>
        ///取框架在线QQ号（多Q版可用）
        ///</summary>
        public static extern IntPtr Api_GetOnLineList();
        [DllImport("../IRapi.dll")]
        ///<summary>
        ///取个人说明
        ///</summary>
        ///<param name="RobotQQ">响应的QQ</param>
        ///<param name="ObjQQ">对象QQ</param>
        public static extern string Api_GetPerExp(string RobotQQ, string ObjQQ);
        [DllImport("../IRapi.dll")]
        ///<summary>
        ///根据图片GUID取得图片下载链接
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        ///<param name="PicType">图片类型</param>
        ///<param name="ReferenceObj">参考对象</param>
        ///<param name="PicGUID">图片GUID</param>
        public static extern string Api_GetPicLink(string RobotQQ, int PicType, string ReferenceObj, string PicGUID);
        [DllImport("../IRapi.dll")]
        ///<summary>
        ///取Q龄，成功返回Q龄，失败返回-1
        ///</summary>
        ///<param name="RobotQQ">响应的QQ</param>
        ///<param name="ObjQQ">对象QQ</param>
        public static extern int Api_GetQQAge(string RobotQQ, string ObjQQ);
        [DllImport("../IRapi.dll")]
        ///<summary>
        ///取框架所有QQ号
        ///</summary>
        public static extern IntPtr Api_GetQQList();
        [DllImport("../IRapi.dll")]
        ///<summary>
        ///获取机器人状态信息，成功返回：昵称、账号、在线状态、速度、收信、发信、在线时间，失败返回空
        ///</summary>
        ///<param name="RobotQQ">响应的QQ</param>
        public static extern string Api_GetRInf(string RobotQQ);
        [DllImport("../IRapi.dll")]
        ///<summary>
        ///取个性签名
        ///</summary>
        ///<param name="RobotQQ>响应的QQ</param>
        ///<param name="ObjQQ">对象QQ</param>
        public static extern string Api_GetSign(string RobotQQ, string ObjQQ);
        [DllImport("../IRapi.dll")]
        ///<summary>
        ///获取当前框架内部时间戳
        ///</summary>
        public static extern long Api_GetTimeStamp();
        [DllImport("../IRapi.dll")]
        ///<summary>
        ///取框架版本号
        ///</summary>
        public static extern string Api_GetVer();
        [DllImport("../IRapi.dll")]
        ///<summary>
        ///取得QQ空间页面操作有用参数P_skye
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        public static extern string Api_GetZonePsKey(string RobotQQ);
        [DllImport("../IRapi.dll")]
        ///<summary>
        ///群ID转群号
        ///</summary>
        ///<param name="GroupID">群ID</param>
        public static extern string Api_GIDTransGN(string GroupID);
        [DllImport("../IRapi.dll")]
        ///<summaray>
        ///群号转群ID
        ///</summary>
        ///<param name="GroupNum">群号</param>
        public static extern string Api_GNTransGID(string GroupNum);
        [DllImport("../IRapi.dll")]
        ///<summary>
        ///处理框架所有事件请求
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        ///<param name="ReQuestType">请求类型：213请求入群，214我被邀请加入某群，215某人被邀请加入群，101某人请求添加好友</param>
        ///<param name="ObjQQ">对象QQ：申请入群，被邀请人，请求添加好友人的QQ（当请求类型为214时这里请为空）</param>
        ///<param name="GroupNum">群号：收到请求的群号（好友添加时留空）</param>
        ///<param name="Handling">处理方式：10同意 20拒绝 30忽略</param>
        ///<param name="AdditionalInfo">附加信息：拒绝入群附加信息</param>
        public static extern void Api_HandleEvent(string RobotQQ, int ReQuestType,
            string ObjQQ, string GroupNum,
            int Handling, string AddintionalInfo);
        [DllImport("../IRapi.dll")]
        ///<summary>
        ///是否QQ好友，好友返回真，非好友或获取失败返回假
        ///</summary>
        ///<param name="RobotQQ">响应的QQ</param>
        ///<param name="OBjQQ">对象QQ</param>
        public static extern bool Api_IfFriend(string RobotQQ, string ObjQQ);
        [DllImport("../IRapi.dll")]
        ///<summary>
        ///邀请对象加入讨论组，成功返回空，失败返回理由
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        ///<param name="DisGroupID>讨论组ID</param>
        ///<param name="ObjQQ">被邀请对象QQ：多个用 换行符 分割</param>
        public static extern string Api_InviteDisGroup(string RobotQQ, string DisGroupID, string ObjQQ);
        [DllImport("../IRapi.dll")]
        ///<summary>
        ///取得插件自身启用状态，启用真，禁用假
        ///</summary>
        public static extern bool Api_IsEnable();
        [DllImport("../IRapi.dll")]
        ///<summary>
        ///查询对象或自己是否被禁言，禁言返回真，失败或未禁言返回假
        ///</summary>
        ///<param name="RobotQQ">响应的QQ</param>
        ///<param name="GroupNum">群号</param>
        ///<param name="ObjQQ">对象QQ</param>
        public static extern bool Api_IsShutUp(string RobotQQ, string GroupNum, string ObjQQ);
        [DllImport("../IRapi.dll")]
        ///<summary>
        ///申请加群
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        ///<param name="GroupNum">群号</param>
        ///<param name="Reason">附加理由，可留空</param>
        public static extern void Api_JoinGroup(string RobotQQ, string GroupNUm, string Reason);
        [DllImport("../IRapi.dll")]
        ///<summary>
        ///将对象移除讨论组，成功返回空，失败返回理由
        ///</summray>
        ///<param name="RobotQQ">机器人QQ</param>
        ///<param name="DisGroupID">需要执行的讨论组ID</param>
        ///<param name="ObjQQ">被执行对象</param>
        public static extern string Api_KickDisGroupMBR(string RobotQQ, string DisGroupID, string ObjQQ);
        [DllImport("../IRapi.dll")]
        ///<summary>
        ///将对象移出群
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        ///<param name="GroupNum">群号</param>
        ///<param name="ObjQQ">被执行对象</param>
        public static extern void Api_KickGroupMBR(string RobotQQ, string GroupNum, string ObjQQ);
        [DllImport("../IRapi.dll")]
        ///<summary>
        ///载入插件
        ///</summary>
        public static extern void Api_LoadPlugin();
        [DllImport("../IRapi.dll")]
        ///<summary>
        ///登录指定QQ，应确保QQ号码在列表中已经存在
        ///</summary>
        ///<param name="RobotQQ">欲登录的QQ</param>
        public static extern void Api_LoginQQ(string RobotQQ);
        [DllImport("../IRapi.dll")]
        ///<summary>
        ///非管理员邀请对象入群，每次只能邀请一个对象，频率过快会失败
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        ///<param name="ObjQQ">被邀请人QQ号码</param>
        ///<param name="GroupNum">群号</param>
        public static extern void Api_NoAdminInviteGroup(string RobotQQ, string ObjQQ, string GroupNum);
        [DllImport("../IRapi.dll")]
        ///<summary>
        ///令指定QQ下线，应确保QQ号码已在列表中且在线
        ///</summary>
        ///<param name="RobotQQ">欲下线的QQ</param>
        public static extern void Api_OffLineQQ(string RobotQQ);
        [DllImport("../IRapi.dll")]
        ///<summary>
        ///向IRQQ日志窗口发送一条本插件的日志，可用于调试输出需要的内容，或定位插件错误与运行状态
        ///</summary>
        ///<param name="Log">日志信息</param>
        public static extern void Api_OutPutLog(string Log);
        [DllImport("../IRapi.dll")]
        ///<summary>
        ///发布群公告（成功返回真，失败返回假），调用此API应保证响应QQ为管理员
        ///<summary>
        ///<param name="RobotQQ">机器人QQ</param>
        ///<param name="GroupNum">欲发布公告的群号</param>
        ///<param name="Title">公告标题</param>
        ///<param name="Content">内容</param>
        public static extern bool Api_PBGroupNotic(string RobotQQ, string GroupNum, string Title, string Content);
        [DllImport("../IRapi.dll")]
        ///<summary>
        ///发布QQ群作业
        ///</summary>
        ///<param name="RobotQQ">响应的QQ</param>
        ///<param name="GroupNum">群号</param>
        ///<param name="HomeWorkName">作业名</param>
        ///<param name="HomdWorkTitle">作业标题</param>
        ///<param name="Text">作业内容</param>
        public static extern string Api_PBHomeWork(string RobotQQ, string GroupNum, string HomeWorkName, string HomeWorkTitle, string Text);
        [DllImport("../IRapi.dll")]
        ///<summary>
        ///发送QQ说说
        ///</summary>
        ///<param name="RobotQQ">响应的QQ</param>
        ///<param name="Text">发送内容</param>
        public static extern string Api_PBTaoTao(string RobotQQ, string Text);
        [DllImport("../IRapi.dll")]
        ///<summary>
        ///退出讨论组
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        ///<param name="DisGroupID">需要退出的讨论组ID</param>
        public static extern void Api_QuitDisGroup(string RobotQQ, string DisGroupID);
        [DllImport("../IRapi.dll")]
        ///<summary>
        ///退群
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        ///<param name="GroupNum">欲退出的群号</param>
        public static extern void Api_QuitGroup(string RobotQQ, string GroupNum);
        [DllImport("../IRapi.dll")]
        ///<summary>
        ///发送JSON结构消息
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        ///<param name="SendType">发送方式：1普通 2匿名（匿名需要群开启）</param>
        ///<param name="MsgType">信息类型：1好友 2群 3讨论组 4群临时会话 5讨论组临时会话</param>
        ///<param name="MsgTo">收信对象所属群_讨论组（消息来源），发送群、讨论组、临时会话填写、如发送对象为好友可留空</param>
        ///<param name="ObjQQ">收信对象QQ</param>
        ///<param name="Json">Json结构内容</param>
        public static extern void Api_SendJSON(string RobotQQ, int SendType, int MsgType,
           string MsgTo, string ObjQQ, string Json);
        [DllImport("../IRapi.dll")]
        ///<summary>
        ///发送普通文本消息
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        ///<param name="MsgType">信息类型：1好友 2群 3讨论组 4群临时会话 5讨论组临时会话</param>
        ///<param name="MsgTo">收信对象群_讨论组：发送群、讨论组、临时会话时填写</param>
        ///<param name="ObjQQ">收信QQ</param>
        ///<param name="Msg">内容</param>
        ///<param name="ABID">气泡ID</param>
        public static extern void Api_SendMsg(string RobotQQ, int MsgType, string MsgTo,
           string ObjQQ, string Msg, int ABID);
        [DllImport("../IRapi.dll")]
        ///<summary>
        ///向腾讯发送原始封包（成功返回腾讯返回的包）
        ///</summary>
        ///<param name="PcakText">封包内容</param>
        public static extern string Api_SendPack(string PcakText);
        [DllImport("../IRapi.dll")]
        ///<summary>
        ///好友语音上传并发送（成功返回真，失败返回假）
        ///<summary>
        ///<param name="RobotQQ">响应的QQ</param>
        ///<param name="ObjQQ">接收QQ</param>
        ///<param name="pAmr">语音数据的指针</param>
        public static extern bool Api_SendVoice(string RobotQQ, string ObjQQ, byte[]  pAmr);
        [DllImport("../IRapi.dll")]
        ///<summary>
        ///发送XML消息
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        ///<param name="SendType">发送方式：1普通 2匿名（匿名需要群开启）</param>
        ///<param name="MsgType">信息类型：1好友 2群 3讨论组 4群临时会话 5讨论组临时会话</param>
        ///<param name="MsgTo">收信对象群、讨论组：发送群、讨论组、临时时填写，如MsgType为好友可空</param>
        ///<param name="ObjQQ">收信对象QQ</param>
        ///<param name="ObjectMsg">XML代码</param>
        ///<param name="ObjCType">结构子类型：00基本 02点歌</param>
        public static extern void Api_SendXML(string RobotQQ, int SendType, int MsgFrom,
           string MsgTo, string ObjQQ, string ObjectMsg, int ObjeCType);
        [DllImport("../IRapi.dll")]
        ///<summary>
        ///获取会话SessionKey密匙
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        public static extern string Api_SessionKey(string RobotQQ);
        [DllImport("../IRapi.dll")]
        ///<summary>
        ///设置或取消管理员，成功返回空，失败返回理由
        ///</summary>
        ///<param name="RobotQQ">响应的QQ</param>
        ///<param name="GroupNum">群号</param>
        ///<param name="ObjQQ">对象QQ</param>
        ///<param name="SetWay">操作方式，真为设置管理，假为取消管理</param>
        public static extern string Api_SetAdmin(string RobotQQ, string GroupNum, string ObjQQ, bool SetWay);
        [DllImport("../IRapi.dll")]
        ///<summary>
        ///开关群匿名消息发送功能，成功返回真，失败返回假
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        ///<param name="GroupNum">群号</param>
        ///<param name="Swit">开关：真开 假关</param>
        public static extern bool Api_SetAnon(string RobotQQ, string GroupNum, bool swit);
        [DllImport("../IRapi.dll")]
        ///<summary>
        ///修改对象群名片，成功返回真，失败返回假
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        ///<param name="GroupNum">群号</param>
        ///<param name="ObjQQ">对象QQ：被修改名片人QQ</param>
        ///<param name="NewCard">需要修改的群名片</param>
        public static extern bool Api_SetGroupCard(string RobotQQ, string GroupNum,
           string ObjQQ, string NewCard);
        [DllImport("../IRapi.dll")]
        ///<summary>
        ///修改机器人在线状态，昵称，个性签名等
        ///</summary>
        ///<param name="RobotQQ">响应的QQ</param>
        ///<param name="type">1 我在线上 2 Q我吧 3 离开 4 忙碌 5 请勿打扰 6 隐身 7 修改昵称 8 修改个性签名</param>
        ///<param name="ChangeText">修改内容，类型7和8时填写，其他为""</param>
        public static extern void Api_SetRInf(string RobotQQ, int type, string ChangeText);
        [DllImport("../IRapi.dll")]
        ///<summary>
        ///屏蔽或接收某群消息
        ///</summary>
        ///<param name="RobotQQ">响应的QQ</param>
        ///<param name="GroupNum">群号</param>
        ///<param name="type">真为屏蔽接收，假为接收拼不提醒</param>
        public static extern void Api_SetShieldedGroup(string RobotQQ, string GroupNum, bool type);
        [DllImport("../IRapi.dll")]
        ///<summary>
        ///向好友发起窗口抖动，调用此Api腾讯会限制频率
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        ///<param name="ObjQQ">接收抖动消息的QQ</param>
        public static extern bool Api_ShakeWindow(string RobotQQ, string ObjQQ);
        [DllImport("../IRapi.dll")]
        ///<summary>
        ///禁言群内某人
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        ///<param name="GroupNum">欲操作的群号</param>
        ///<param name="ObjQQ">欲禁言对象，如留空且机器人QQ为管理员，将设置该群为全群禁言</param>
        ///<param name="Time">禁言时间：0解除（秒），如为全群禁言，参数为非0，解除全群禁言为0</param>
        public static extern void Api_ShutUP(string RobotQQ, string GroupNum,
           string ObjQQ, int Time);
        [DllImport("../IRapi.dll")]
        ///<summary>
        ///QQ群签到，成功返回真失败返回假
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        ///<param name="GroupNum">群号</param>
        public static extern bool Api_SignIn(string RobotQQ, string GroupNum);
        [DllImport("../IRapi.dll")]
        ///<summary>
        ///腾讯Tea加密
        ///</summary>
        ///<param name="Text">需要加密的内容</param>
        ///<param name="SessionKey">会话Key，从Api_SessionKey获得</param>
        public static extern string Api_Tea加密(string Text, string SessionKey);
        [DllImport("../IRapi.dll")]
        ///<summary>
        ///腾讯Tea解密
        ///</summary>
        ///<param name="Text">需解密的内容</param>
        ///<param name="SessionKey">会话Key，从Api_SessionKey获得</param>
        public static extern string Api_Tea解密(string Text, string SessionKey);
        [DllImport("../IRapi.dll")]
        ///<summary>
        ///卸载插件自身
        ///</summary>
        public static extern void Api_UninstallPlugin();
        [DllImport("../IRapi.dll")]
        ///<summary>
        ///上传图片（通过读入字节集方式），可使用网页链接或本地读入，成功返回该图片GUID,失败返回空</param>
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        ///<param name="UpLoadType">上传类型：1好友2群PS:好友临时用1，群讨论组用2；当填写错误时，图片GUID发送不会成功</param>
        ///<param name="UpTo">参考对象，上传该图片所属群号或QQ</param>
        ///<param name="Pic">图片字节集数据</param>
        public static extern string Api_UpLoadPic(string RobotQQ, int UpLoadType,
           string UpTo, int hInstance);
        [DllImport("../IRapi.dll")]
        ///<summary>
        ///上传QQ语音，成功返回语音GUID，失败返回空
        ///</summary>
        ///<param name="RobotQQ">响应的QQ</param>
        ///<param name="type">上传类型 2 QQ群</param>
        ///<param name="GroupNum">接收的群号</param>
        ///<param name="pAmr">语音数据指针</param>
        public static extern string Api_UpLoadVoice(string RobotQQ, int type, string GroupNum, byte[] pAmr);
        [DllImport("../IRapi.dll")]
        ///<summary>
        ///调用一次点一下，成功返回空，失败返回理由如：每天最多给他点十个赞哦，调用此Api时应注意频率，每人每日10次，至多50人</param>
        ///</summary>
        ///<param name="RobotQQ">机器人QQ</param>
        ///<param name="ObjQQ">被赞人QQ</param>
        public static extern string Api_UpVote(string RobotQQ, string ObjQQ);
    }
}
