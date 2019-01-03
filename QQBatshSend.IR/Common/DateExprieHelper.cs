using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QQBatchSend.IR.Common
{
    public static class DateExprieHelper
    {
        /// <summary>
        /// 根据签约时间和签约时长来判断是否到期
        /// </summary>
        /// <param name="joinDate">签约时间</param>
        /// <param name="duration">签约时长</param>
        /// <returns></returns>
        public static bool IsExpriredByDay(DateTime joinDate, double duration)
        {
            return DateTime.Now - joinDate > TimeSpan.FromDays(duration);
        }
    }
}
