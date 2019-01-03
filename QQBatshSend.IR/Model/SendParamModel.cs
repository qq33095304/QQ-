using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QQBatchSend.IR.Model
{
    public class SendParamModel
    {
        /// <summary>
        /// 发送消息模板
        /// </summary>
        public List<string> Message { get; set; }

        /// <summary>
        /// 模板规则，1固定选中，则上面只有一条记录 2：列表随机
        /// </summary>
        public int TemplateRule { get; set; }
        /// <summary>
        /// 机器人QQ
        /// </summary>
        public string RobotQQ { get; set; }

        /// <summary>
        /// 好友间隔下限
        /// </summary>
        public int FriendIntervalDown { get; set; }

        /// <summary>
        /// 好友间隔上限
        /// </summary>
        public int FriendIntervalUp { get; set; }

        /// <summary>
        /// 分段间隔下限
        /// </summary>
        public int NextIntervalDown { get; set; }

        /// <summary>
        /// 分段间隔上限
        /// </summary>
        public int NextIntervalUp { get; set; }

        /// <summary>
        /// 每组数量
        /// </summary>
        public int GroupNum { get; set; }

        /// <summary>
        /// 每组休眠下限
        /// </summary>
        public int GroupSleepDown { get; set; }

        /// <summary>
        /// 每组休眠上限
        /// </summary>
        public int GroupSleepUp { get; set; }

        /// <summary>
        /// 发送对象 1：好友 2：群 4：群成员
        /// </summary>
        public int SendObject { get; set; }
        /// <summary>
        /// 是否识别重复发送
        /// </summary>
        public bool IdentifyRepeatSend { get; set; }

        /// <summary>
        /// 每个群最大发送人数 0：无限制 大于0：根据具体数值限制发送数量
        /// </summary>
        public int GroupMaxSendNum { get; set; }

        /// <summary>
        /// 性别 0：全部 1：男 2：女
        /// </summary>
        public int Sex { get; set; }

        /// <summary>
        /// 在线状态 0：全部 1：在线 2：离线
        /// </summary>
        public int Online { get; set; }

        /// <summary>
        /// 所有模板中的语音集合
        /// </summary>
        public Dictionary<string, byte[]> Voices { get; set; }


    }
}
