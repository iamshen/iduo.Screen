using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using iduo.Screen.Models;

namespace iduo.Screen.Common
{
    public static class Common
    {
        /// <summary>
        /// 
        /// 请求一般处理程序接口数据
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <returns></returns>
        public static string GetJsonData(string url)
        {
            try
            {
                string data = "";
                HttpWebRequest Request = (HttpWebRequest)WebRequest.Create(url);
                Request.Method = "POST";
                Request.ContentType = "application/json";
                Stream newStream = Request.GetRequestStream(); // Send the data.
                HttpWebResponse response = (HttpWebResponse)Request.GetResponse();
                StreamReader sr = new StreamReader(response.GetResponseStream(), System.Text.Encoding.UTF8);
                data = sr.ReadToEnd();
                if (data == "]" || data == "[]")
                    data = "";
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /// <summary>
        /// 获取通告
        /// </summary>
        /// <param name="url">获取通告的Url</param>
        /// <param name="newsType">获取通告的类型： 1=教学通知 2=教学动态 3=讲座预告</param>
        /// <param name="order">时间</param>
        /// <param name="size">数量</param>
        /// <param name="page">第几页</param>
        /// <returns></returns>
        public static string GetNotice(string url, string order, int newsType, int size = 4, int page = 1)
        {
            var str = "";
            try
            {
                if (string.IsNullOrEmpty(url))
                    return "";

                url = url + "&newsType=" + newsType + "&size=" + size + "&page=" + page + "&order=" + order;
                str = GetJsonData(url);
            }
            catch (Exception ex)
            {

                return "";
            }

            return str;
        }

        /// <summary>
        /// 获取新闻的数量
        /// </summary>
        /// <param name="url">获取新闻的数量的Url</param>
        ///<param name="newsType">获取通告的类型： 1=教学通知 2=教学动态 3=讲座预告</param>
        /// <returns></returns>
        public static string GetNoticeCount(string url, int newsType)
        {
            var str = "";
            try
            {
                if (string.IsNullOrEmpty(url))
                    return "";

                url = url + "&newsType=" + newsType;
                str =GetJsonData(url);
            }
            catch (Exception ex)
            {

                return "";
            }

            return str;
        }

        /// <summary>
        /// 新闻json 转对象
        /// </summary>
        /// <param name="str">json</param>
        /// <returns></returns>
        public static List<Notice> FormatNotice(string str)
        {
            List<Notice> list = new List<Notice>();
            try
            {
                JavaScriptSerializer ser = new JavaScriptSerializer();

                List<Notice> obj = ser.Deserialize<List<Notice>>(str);

                list = obj;
            }
            catch (Exception)
            {
                return list;
            }
            return list;

        }

        public static List<NoticeCount> FormatNoticeCount(string str)
        {
            List<NoticeCount> list = new List<NoticeCount>();

            try
            {
                JavaScriptSerializer ser = new JavaScriptSerializer();

                List<NoticeCount> obj = ser.Deserialize<List<NoticeCount>>(str);

                list = obj;
            }
            catch (Exception)
            {
                return list;
            }
            return list;

        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int[] SortId(int[] id)
        {
            int temp = 0;
            for (int i = 0; i < id.Length - 1; i++)
            {
                for (int j = 0; j < id.Length - 1 - i; j++)
                {
                    if (id[j] > id[j + 1])
                    {
                        temp = id[j];
                        id[j] = id[j + 1];
                        id[j + 1] = temp;
                    }
                }
            }
            return id;
        }

        /// <summary>
        /// 查找相邻的课程ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int[] GetIntXLID(int[] id)
        {
            List<int> ids = new List<int>();
            for (int i = 0; i < id.Length - 1; i++)
            {
                var item = id[i];
                var next = id[i + 1];
                var temp = next - item;
                if (temp == 1)
                {
                    var q = ids.Contains(item);
                    var a = ids.Contains(next);
                    if (!q)
                        ids.Add(item);
                    if (!a)
                        ids.Add(next);
                }
            }
            var idArray = ids.ToArray();
            return idArray;
        }

        /// <summary>
        /// 根据节次获取课程时间
        /// </summary>
        /// <param name="classtitle"></param>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static ClassVM GetModel(Classtitle classtitle, string datetime)
        {
            var vm = new ClassVM();
            try
            {
                var date = DateTime.Now;
                switch (classtitle)
                {
                    case Classtitle.第一节课:
                        vm.start = datetime + "T00:00:00";
                        vm.end = datetime + "T02:00:00";
                        break;

                    case Classtitle.第二节课:
                        vm.start = datetime + "T02:00:00";
                        vm.end = datetime + "T04:00:00";
                        break;

                    case Classtitle.第三节课:
                        vm.start = datetime + "T04:00:00";
                        vm.end = datetime + "T06:00:00";
                        break;

                    case Classtitle.第四节课:
                        vm.start = datetime + "T06:00:00";
                        vm.end = datetime + "T08:00:00";
                        vm.startDate = DateTime.Parse(vm.start);
                        vm.endDate = DateTime.Parse(vm.end);
                        break;

                    case Classtitle.第五节课:
                        vm.start = datetime + "T08:00:00";
                        vm.end = datetime + "T10:00:00";
                        break;

                    case Classtitle.第六节课:
                        vm.start = datetime + "T10:00:00";
                        vm.end = datetime + "T12:00:00";
                        break;

                    case Classtitle.第七节课:
                        vm.start = datetime + "T12:00:00";
                        vm.end = datetime + "T14:00:00";
                        break;

                    case Classtitle.第八节课:
                        vm.start = datetime + "T14:00:00";
                        vm.end = datetime + "T16:00:00";
                        break;

                    case Classtitle.第九节课:
                        vm.start = datetime + "T16:00:00";
                        vm.end = datetime + "T18:00:00";
                        break;

                    case Classtitle.第十节课:
                        vm.start = datetime + "T18:00:00";
                        vm.end = datetime + "T20:00:00";
                        break;

                    case Classtitle.第十一节课:
                        vm.start = datetime + "T20:00:00";
                        vm.end = datetime + "T22:00:00";
                        break;

                    case Classtitle.第十二节课:
                        vm.start = datetime + "T22:00:00";
                        vm.end = datetime + "T23:59:00";
                        break;

                    default:
                        break;
                }
                vm.Id = (int)classtitle;
                vm.startDate = DateTime.Parse(vm.start);
                vm.endDate = DateTime.Parse(vm.end);
            }
            catch (Exception ex)
            {
                //throw new Exception(ex.Message);
                return vm;
            }

            return vm;
        }

    }
}