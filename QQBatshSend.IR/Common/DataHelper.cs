using NiLiuShui.IRQQ.CSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace QQBatchSend.IR
{
    public class DataHelper
    {

        /// <summary>
        /// 获取指定机器人所有好友数据
        /// </summary>
        /// <param name="RobotQQ"></param>
        /// <returns></returns>
        public static DataTable GetFriends(string RobotQQ, DataTable dataTable = null)
        {
            if (dataTable == null)
            {
                dataTable = GetFriendDataTableModel();
            }

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            IntPtr ipFriendList = IRQQApi.Api_GetFriendList(RobotQQ);
            string strFriendList = Marshal.PtrToStringAnsi(ipFriendList);
            MoonLogger.Instance.Debug("获取到的好友信息：" + strFriendList);
            Dictionary<string, object> myFriendInfo = serializer.Deserialize<Dictionary<string, object>>(strFriendList);
            if (myFriendInfo["ec"].ToString() == "0")
            {
                myFriendInfo = (Dictionary<string, object>)myFriendInfo["result"];
                //这个只是获取一个分组，可能存在多个分组
                //获取分组好友
                foreach (var friendroup in myFriendInfo)
                {
                    myFriendInfo = (Dictionary<string, object>)friendroup.Value;
                    //分组不为空时获取数据
                    if (myFriendInfo.ContainsKey("mems"))
                    {
                        System.Collections.ArrayList mems = (System.Collections.ArrayList)myFriendInfo["mems"];
                        foreach (var item in mems)
                        {
                            DataRow dataRow = dataTable.NewRow();
                            Dictionary<string, object> keyValuePairs = (Dictionary<string, object>)item;
                            dataRow["RobotQQ"] = RobotQQ;
                            dataRow["uin"] = keyValuePairs["uin"];
                            dataRow["name"] = keyValuePairs["name"];
                            dataTable.Rows.Add(dataRow);
                        }
                    }
                }
            }
            else
            {
                throw new Exception(string.Format("获取机器人{0}好友列表失败！{1}", new string[] { RobotQQ, strFriendList }));
            }
            return dataTable;
        }

        /// <summary>
        /// 获取指定机器人的所有群组数据
        /// </summary>
        /// <param name="RobotQQ"></param>
        /// <returns></returns>
        public static DataTable GetGroups(string RobotQQ, DataTable dataTable = null)
        {
            if (dataTable == null)
            {
                dataTable = GetFriendDataTableModel();
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            IntPtr ipGroupList = IRQQApi.Api_GetGroupList(RobotQQ);
            string strGroupList = Marshal.PtrToStringAnsi(ipGroupList);
            MoonLogger.Instance.Debug("获取到的群组信息：" + strGroupList);
            string regex = "\\{(.+)\\}";
            Match match = Regex.Match(strGroupList, regex);
            Dictionary<string, object> myGroupInfo = serializer.Deserialize<Dictionary<string, object>>(match.Value);
            if (myGroupInfo["code"].ToString() == "0")
            {
                myGroupInfo = (Dictionary<string, object>)myGroupInfo["data"];
                System.Collections.ArrayList mems = (System.Collections.ArrayList)myGroupInfo["group"];
                foreach (var item in mems)
                {
                    DataRow dataRow = dataTable.NewRow();
                    Dictionary<string, object> keyValuePairs = (Dictionary<string, object>)item;
                    dataRow["RobotQQ"] = RobotQQ;
                    dataRow["uin"] = keyValuePairs["groupid"];
                    dataRow["name"] = keyValuePairs["groupname"];
                    dataTable.Rows.Add(dataRow);
                }
            }
            else
            {
                throw new Exception(string.Format("获取机器人{0}群列表失败！{1}", new string[] { RobotQQ, strGroupList }));
            }
            return dataTable;
        }


        /// <summary>
        /// 获取指定机器人的所有群成员
        /// </summary>
        /// <param name="RobotQQ"></param>
        /// <returns></returns>
        public static DataTable GetGroupMember(string RobotQQ, DataTable dataTable = null)
        {
            if (dataTable == null)
            {
                dataTable = GetFriendDataTableModel();
            }

            DataTable groups = GetGroups(RobotQQ);
            foreach (DataRow row in groups.Rows)
            {
                string groupId = Convert.ToString(row["uin"]);
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                IntPtr ipGroupMemberList = IRQQApi.Api_GetGroupMemberList(RobotQQ, groupId);
                string strGroupMemberList = Marshal.PtrToStringAnsi(ipGroupMemberList);
                MoonLogger.Instance.Debug("获取到的群成员信息：" + strGroupMemberList);
                Dictionary<string, object> myGroupMemberInfo = serializer.Deserialize<Dictionary<string, object>>(strGroupMemberList);
                if (myGroupMemberInfo["ec"].ToString() == "0")
                {
                    System.Collections.ArrayList mems = (System.Collections.ArrayList)myGroupMemberInfo["mems"];
                    foreach (var item in mems)
                    {
                        Dictionary<string, object> keyValuePairs = (Dictionary<string, object>)item;
                        string role = keyValuePairs["role"].ToString();
                        DataRow dataRow = dataTable.NewRow();
                        dataRow["RobotQQ"] = RobotQQ;
                        dataRow["groupId"] = groupId;
                        //0 群主 1管理员
                        dataRow["isAdmin"] = (role == "0" || role == "1");
                        dataRow["uin"] = keyValuePairs["uin"];
                        dataRow["name"] = keyValuePairs["nick"];
                        dataTable.Rows.Add(dataRow);
                    }
                }
                else
                {
                    throw new Exception(string.Format("获取机器人{0}群成员列表失败！{1}", new string[] { RobotQQ, strGroupMemberList }));
                }
            }
            return dataTable;
        }

        public static void MergeData(DataTable desDataTable, DataTable sourceDataTable)
        {
            foreach (var item in sourceDataTable.Rows)
            {
                desDataTable.Rows.Add(item);
            }
        }

        public static DataTable GetFriendDataTableModel()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn("RobotQQ", Type.GetType("System.String")));
            //dataTable.Columns.Add(new DataColumn("groupId", Type.GetType("System.String")));
            //dataTable.Columns.Add(new DataColumn("isAdmin", Type.GetType("System.Boolean")));
            dataTable.Columns.Add(new DataColumn("uin", Type.GetType("System.String")));
            dataTable.Columns.Add(new DataColumn("name", Type.GetType("System.String")));
            return dataTable;
        }
    }
}
