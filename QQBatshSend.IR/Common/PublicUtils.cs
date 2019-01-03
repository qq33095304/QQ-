using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QQBatchSend.IR.Common
{
    /// <summary>
    /// 公共帮助类
    /// </summary>
    public class PublicUtils
    {
        /// <summary>
        /// 获取一个随机种子
        /// </summary>
        /// <returns></returns>
        private static int GetRandomSeed()
        {
            byte[] bytes = new byte[4];
            System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            rng.GetBytes(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }
        /// <summary>
        /// 获取一个随机数
        /// </summary>
        /// <param name="minValue">最小值</param>
        /// <param name="maxValue">最大值（不包括）</param>
        /// <returns></returns>
        public static int GetRandom(int minValue, int maxValue)
        {
            return (new Random(GetRandomSeed())).Next(minValue, maxValue);
        }
    }
}
