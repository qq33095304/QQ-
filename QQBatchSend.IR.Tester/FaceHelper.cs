using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QQBatchSend.IR.Common
{
    /// <summary>
    /// 表情帮助类
    /// </summary>
    public class FaceHelper
    {
        /// <summary>
        /// 兼容最新版表情列表
        /// </summary>
        private static List<string> FaceList = new List<string>() {
            "[Face14.gif]","[Face1.gif]","[Face2.gif]","[Face3.gif]","[Face4.gif]","[Face5.gif]","[Face6.gif]","[Face7.gif]","[Face8.gif]","[Face9.gif]","[Face10.gif]","[Face11.gif]","[Face12.gif]","[Face13.gif]","[Face0.gif]","[Face15.gif]","[Face16.gif]","[Face96.gif]","[Face18.gif]","[Face19.gif]","[Face20.gif]","[Face21.gif]","[Face22.gif]","[Face23.gif]","[Face24.gif]","[Face25.gif]","[Face26.gif]","[Face27.gif]","[Face28.gif]","[Face29.gif]","[Face30.gif]","[Face31.gif]","[Face32.gif]","[Face33.gif]","[Face34.gif]","[Face35.gif]","[Face36.gif]","[Face37.gif]","[Face38.gif]","[Face39.gif]","[Face97.gif]","[Face98.gif]","[Face99.gif]","[Face100.gif]","[Face101.gif]","[Face102.gif]","[Face103.gif]","[Face104.gif]","[Face105.gif]","[Face106.gif]","[Face107.gif]","[Face108.gif]","[Face109.gif]","[Face110.gif]","[Face111.gif]","[Face172.gif]","[Face182.gif]","[Face179.gif]","[Face173.gif]","[Face174.gif]","[Face212.gif]","[Face175.gif]","[Face178.gif]","[Face177.gif]","[Face180.gif]","[Face181.gif]","[Face176.gif]","[Face183.gif]","[Face112.gif]","[Face89.gif]","[Face113.gif]","[Face114.gif]","[Face115.gif]","[Face171.gif]","[Face60.gif]","[Face61.gif]","[Face46.gif]","[Face63.gif]","[Face64.gif]","[Face116.gif]","[Face66.gif]","[Face67.gif]","[Face53.gif]","[Face54.gif]","[Face55.gif]","[Face56.gif]","[Face57.gif]","[Face117.gif]","[Face59.gif]","[Face75.gif]","[Face74.gif]","[Face69.gif]","[Face49.gif]","[Face76.gif]","[Face77.gif]","[Face78.gif]","[Face79.gif]","[Face118.gif]","[Face119.gif]","[Face120.gif]","[Face121.gif]","[Face122.gif]","[Face123.gif]","[Face124.gif]","[Face42.gif]","[Face85.gif]","[Face43.gif]","[Face41.gif]","[Face86.gif]","[Face125.gif]","[Face126.gif]","[Face127.gif]","[Face128.gif]","[Face129.gif]","[Face130.gif]","[Face131.gif]","[Face132.gif]","[Face133.gif]","[Face134.gif]","[Face136.gif]","[Face137.gif]","[Face138.gif]","[Face140.gif]","[Face144.gif]","[Face146.gif]","[Face147.gif]","[Face148.gif]","[Face151.gif]","[Face158.gif]","[Face168.gif]","[Face169.gif]","[Face188.gif]","[Face192.gif]","[Face184.gif]","[Face185.gif]","[Face190.gif]","[Face187.gif]","[Face193.gif]","[Face194.gif]","[Face197.gif]","[Face198.gif]","[Face199.gif]","[Face200.gif]","[Face201.gif]","[Face202.gif]","[Face203.gif]","[Face204.gif]","[Face205.gif]","[Face206.gif]","[Face207.gif]","[Face208.gif]","[Face210.gif]","[Face211.gif]"
        };

        /// <summary>
        /// 获取一个随机表情
        /// </summary>
        /// <returns></returns>
        public static string GetRandomFace()
        {
            return FaceList[(new Random(PublicUtils.GetRandomSeed())).Next(0, FaceList.Count)];
        }

        /// <summary>
        /// 替换随机表情
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static string ReplaceRandomFace(string message)
        {
            while (message.Contains("[随机表情]"))
            {
                message = new Regex("\\[随机表情\\]").Replace(message, GetRandomFace(), 1);
            }
            return message;
        }
    }
}
