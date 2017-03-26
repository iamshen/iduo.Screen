using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using iduo.Screen.Models;
using iduo.Screen.APP;
using iduo.Screen.Common;
using System.Runtime.Serialization.Json;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace iduo.Screen.Controllers
{
    public class HomeController : Controller
    {

        /// <summary>
        /// 课程集合
        /// </summary>
        public static List<ClassVM> ClassVMdata;

        public static List<AppViewModel> AppViewModel;

        /// <summary>
        /// 教学通知
        /// </summary>
        public static List<Notice> teachingNoticeList;
        /// <summary>
        /// 教学动态
        /// </summary>
        public static List<Notice> teachingTrendsList;
        /// <summary>
        /// 讲座预告
        /// </summary>
        public static List<Notice> lectureNoticeList;

        /// <summary>
        /// 教学通知
        /// </summary>
        public static string teachingNoticeLists;
        /// <summary> 
        /// 教学动态 
        /// </summary>
        public static string teachingTrendsLists;
        /// <summary> 
        /// 讲座预告  
        /// </summary>
        public static string lectureNoticeLists;

        /// <summary>
        /// 校历json
        /// </summary>
        public static string xl;

        /// <summary>
        /// 获取校历URL
        /// </summary>
        private static string ServiceUrl = ConfigurationManager.AppSettings["iduo.screen.xiaoli"];

        //获取新闻数量：http://112.16.69.191:8810/Pages/news/news.ashx?action=getNewsNum&newsType=1



        //获取新闻：http://112.16.69.191:8810/Pages/news/news.ashx?action=getNews&newsType=1&size=10&page=1&order=time
        //newsType:1=教学通知 2=教学动态 3=讲座预告
        private static string newsCountUrl = ConfigurationManager.AppSettings["iduo.screen.newscount"];

        private static string newsUrl = ConfigurationManager.AppSettings["iduo.screen.news"];



        /// <summary>
        /// 获取应用
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public List<AppViewModel> GetAppModel(string json)
        {

            List<AppViewModel> appListVM = new List<AppViewModel>();
            List<APPModel> appList = new List<APPModel>();
            AppResult<List<APPModel>> appResult = new AppResult<List<APPModel>>();
            try
            {
                var reuslt= JsonHelper.FormatJson<AppResult< List<APPModel> >> (json);
                if (reuslt.Code != 1)
                    throw new Exception(reuslt.Msg);

                if (reuslt.Count>0)
                    appResult = reuslt;
                appList = appResult.items;
                if (appList.Count > 0)
                {
                    foreach (var item in appList)
                    {
                        var App = new AppViewModel()
                        {
                            Id = item.ID,
                            title = item.SHOWNAME_CN,
                            appName = item.SHOWNAME_CN,
                            url = item.EXT5,
                            bg = item.EXT3,
                            bgicon = item.ICONNAME
                        };
                        appListVM.Add(App);
                    }
                }
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
          

            return appListVM;

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
        public string GetNotice(string url,string order, int newsType,int size=4,int page=1)
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
        public string GetNoticeCount(string url,int newsType)
        {
            var str = "";
            try
            {
                if (string.IsNullOrEmpty(url))
                    return "";

                url = url + "&newsType=" + newsType;
                str = GetJsonData(url);
            }
            catch (Exception ex)
            {

                return "";
            }

            return str;
        }


        /// <summary>
        /// 大屏首页
        /// </summary>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        public ActionResult Index(int height = 3, int width = 3)
        {
            #region 默认九宫格布局3X3
            var num = height * width;
            var lessNum = num - 1;
            ViewBag.num = num;
             ViewBag.lessNum = lessNum;
            #endregion

            #region 获取应用

            APPSoapClient _app = new APPSoapClient();
            MySoapHeader header = new MySoapHeader() { UserName = "iduo/zdw", PassWord = "1" };
            //应用
            string app = _app.GetAppByClass(header, "", 1, 1000, 3);

            List<AppViewModel> appData = GetAppModel(app);
            AppViewModel = appData;

            #endregion

            #region 新闻通告
            //教学通知
            var teachingnotice = "";
            //教学动态
            var teachingTrends = "";
            //讲座预告
            var lectureNotice = "";
            //数量
            var teachingnoticeCount = "";
            var teachingTrendsCount = "";
            var lectureNoticeCount = "";

           
      

            //获取新闻通告
            if (!string.IsNullOrEmpty(newsUrl))
            {
                teachingnotice = GetNotice(newsUrl, "", 1,4, 1);
                teachingTrends = GetNotice(newsUrl, "", 2, 4, 1);
                lectureNotice = GetNotice(newsUrl, "", 3, 4, 1);

            }
            //获取新闻的数量
            if (!string.IsNullOrEmpty(newsCountUrl))
            {

                teachingnoticeCount = GetNoticeCount(newsCountUrl, 1);
                teachingTrendsCount = GetNoticeCount(newsCountUrl, 2);
                lectureNoticeCount = GetNoticeCount(newsCountUrl, 3);
            }

            #region ForTest
            teachingnotice = "[{id:'1',title:'教学通知',content:'教学通知的内容！',author:'Dave',time:'2017-03-25',url:'http://www.baidu.com'},    {id:'2',title:'教学通知2',content:'教学通知的内容2！',author:'Dave2',time:'2017-03-26',url:'http://www.baidu.com'}]";

            teachingTrends = "[{id:'3',title:'教学动态',content:'教学动态的内容！',author:'Dave',time:'2017-03-25',url:'http://www.baidu.com'},    {id:'4',title:'教学动态2',content:'教学动态的内容2！',author:'Dave2',time:'2017-03-26',url:'http://www.baidu.com'}]";

            lectureNotice = "[{id:'5',title:'讲座预告',content:'讲座预告的内容！',author:'Dave',time:'2017-03-25',url:'http://www.baidu.com'},    {id:'6',title:'讲座预告2',content:'讲座预告的内容2！',author:'Dave2',time:'2017-03-26',url:'http://www.baidu.com'}]";
            #endregion
            var tz = FormatNotice(teachingnotice);
            var dt= FormatNotice(teachingTrends);
            var yg= FormatNotice(lectureNotice);

            teachingnoticeCount = tz.Count().ToString();
            teachingTrendsCount = dt.Count().ToString();
            lectureNoticeCount = yg.Count().ToString();


            ViewBag.teachingNoticeList = tz;
            ViewBag.teachingTrendsList = dt;
            ViewBag.lectureNoticeList = yg;

            #endregion

            #region 校历
            var xiaoli = new SchoolCalendarVM();
            try
            {
                var result = GetJsonData(ServiceUrl);
                xiaoli = GetCNXL(result);
                if (xiaoli != null)
                    xl = xiaoli.xn + "学年" + "第" + xiaoli.xq + "学期" + "第" + xiaoli.djz + "周";
            }
            catch (Exception)
            {

                xiaoli = null;
            }

            ViewBag.xiaoli = xl;
            #endregion

            return View();
        }

        public List<Notice> FormatNotice(string str)
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

        /// <summary>
        /// 获取中文校历
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public SchoolCalendarVM GetCNXL(string result)
        {
            SchoolCalendarVM data = new SchoolCalendarVM();
            try
            {
                JavaScriptSerializer ser = new JavaScriptSerializer();
                List<SchoolCalendarVM> objs = ser.Deserialize<List<SchoolCalendarVM>>(result);
                if (objs!=null)
                    data = objs.FirstOrDefault();

                switch (data.xq)
                {
                    case "1":
                        data.xq = "一";
                        break;
                    case "2":
                        data.xq = "二";
                        break;
                    default:
                        break;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
           
            return data;
        }

        /// <summary>
        /// 课程表
        /// </summary>
        /// <param name="url"></param>
        /// <param name="t">课程表页面标题</param>
        /// <returns></returns>
        public ActionResult Calendar(string url, string t)
        {
            if (!string.IsNullOrEmpty(url))
            {
                var reUrl = Server.UrlDecode(url);
                var str = GetJsonData(url);
                var listvm = GetClassVM(str);
                ClassVMdata = listvm;
            }

            ViewBag.xiaoli = xl;
            ViewBag.classTableName = t;
            return View();
        }

        /// <summary>
        /// 
        /// 请求一般处理程序接口数据
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <returns></returns>
        public string GetJsonData(string url)
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
        /// 获取课程数据模型
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public List<ClassVM> GetClassVM(string data)
        {
            var list = new List<Source>();//从接口获取到的数据集合
            var listVM = new List<ClassVM>();//临时处理的数据模型集合
            var listvmData = new List<ClassVM>();//发送前台的数据模型集合
            try
            {
                //序列化json 数据
                JavaScriptSerializer ser = new JavaScriptSerializer();
                List<Source> objs = ser.Deserialize<List<Source>>(data);
                list = objs;
                if (list != null)
                {
                    //把序列化后的数根据节次设置上课的开始时间和结束时间
                    foreach (var item in list)
                    {
                        var vm = GetModel((Classtitle)Enum.Parse(typeof(Classtitle), item.Title), item.Date);
                        vm.title = item.Content;
                        vm.Date = item.Date;
                        listVM.Add(vm);//把处理后的课程数据添加到集合中
                    }

                    listVM = listVM.OrderByDescending(x => x.Date).ToList();

                    //处理连堂课程
                    foreach (var item in listVM)
                    {
                        //同一天内容相同的课程
                        var dayClass = listVM.Where(x => x.title == item.title && x.Date == item.Date).OrderBy(x => x.Id).ToList();
                        //如果存在，查询出连堂课
                        var id = dayClass.Select(x => x.Id).ToArray();//所有id
                        var result = SortId(id);
                        //相邻的课程id
                        var intID = GetIntXLID(id);
                        //不相邻的课程id
                        var diff = id.Where(x => !intID.Contains(x)).ToArray();
                        //处理连堂课程合并
                        if (intID.Count() > 0)
                        {
                            var length = intID.Length;
                            var f = intID[0];
                            var l = intID[length - 1];
                            var first = dayClass.SingleOrDefault(x => x.Id == f);
                            var last = dayClass.SingleOrDefault(x => x.Id == l);
                            var newVM = new ClassVM()
                            {
                                Date = first.Date,
                                start = first.start,
                                startDate = first.startDate,
                                end = last.end,
                                endDate = last.endDate,
                                title = first.title
                            };
                            var qwert = listvmData.SingleOrDefault(x => x.title == newVM.title && (x.startDate <= newVM.startDate && x.endDate <= newVM.endDate));
                            //遍历到连堂中的课程则不处理，否则合并后添加到集合中
                            if (qwert == null)
                                listvmData.Add(newVM);
                        }
                        if (diff.Count() > 0)
                        {
                            //查询非连堂的课程，添加到集合中
                            foreach (var di in diff)
                            {
                                var newVM = dayClass.SingleOrDefault(x => x.Id == di);
                                var qwert = listvmData.SingleOrDefault(x => x.title == newVM.title && (x.startDate == newVM.startDate && x.endDate == newVM.endDate));
                                if (qwert == null)
                                    listvmData.Add(newVM);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return listvmData;
            }
            return listvmData;
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

        //获取课程
        [HttpGet]
        public ActionResult GetClass(string start, string end)
        {
            var data = new List<ClassVM>();
            var startDate = DateTime.ParseExact(start, "yyyy-MM-dd", CultureInfo.CurrentCulture);
            var endDate = DateTime.ParseExact(end, "yyyy-MM-dd", CultureInfo.CurrentCulture);
            if (ClassVMdata != null)
              data = ClassVMdata.Where(x => x.startDate >= startDate && x.endDate <= endDate).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetApp()
        {
            var data = new List<AppViewModel>();
            var result = "";
            try
            {
                if (AppViewModel != null)
                {
                    data = AppViewModel;
                    result = data.ToJson();
                }
                  
            }
            catch (Exception ex)
            {
                return Content(result);
            }


            return Content(result);
        }

        /// <summary>
        /// 根据节次获取课程时间
        /// </summary>
        /// <param name="classtitle"></param>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public ClassVM GetModel(Classtitle classtitle, string datetime)
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