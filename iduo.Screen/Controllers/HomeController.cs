using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using iduo.Screen.APP;
using iduo.Screen.Common;
using iduo.Screen.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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

        //校历
        public string xiaoliStr { get; set; }

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
        /// 解密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string UnEscape(string str)
        {

            StringBuilder s = new StringBuilder();

            int c = str.Length;

            int i = 0;

            while (i != c)

            {

                if (Uri.IsHexEncoding(str, i))
                {
                    s.Append(Uri.HexUnescape(str, ref i));
                }
                else
                {
                    s.Append(str[i++]);
                }
            }
            return s.ToString();

        }

        [HttpGet]
        public ActionResult GetHtlmByUrl(string url)
        {
            var html =Common.Common.Getdata(url);

            var domain = "http://jwc.zafu.edu.cn/";
            var zz = @"(?is)(?<=<(a|img|script|link)\b.*?(href|src)="")";
            string output = Regex.Replace(html, zz, domain);

            return Content(output, "text/html");
        }

        /// <summary>
        /// 大屏首页
        /// </summary>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        public ActionResult Index(int height = 3, int width = 3)
        {
            var userid = "";
            var cookies = "";
            #region 默认九宫格布局3X3

            var num = height * width;
            var lessNum = num - 1;
            ViewBag.num = num;
            ViewBag.lessNum = lessNum;

            #endregion 默认九宫格布局3X3

            #region 获取应用

            List<AppViewModel> appData = new List<AppViewModel>();
            if (Request.Cookies["bigscreen"]!=null)
                cookies = Request.Cookies["bigscreen"].Value;


            if (!string.IsNullOrEmpty(cookies))
            {
                try
                {
                    cookies= cookies.Substring(0, cookies.Length - 1);
                    cookies= cookies.Substring(1);
                    JObject obj = (JObject)JsonConvert.DeserializeObject(cookies);
                    userid = obj["userid"].ToString();

                    APPSoapClient _app = new APPSoapClient();
                    MySoapHeader header = new MySoapHeader() { UserName = userid, PassWord = "admin@123" };
                    //应用
                    string app = _app.GetAppByClass(header, null, 1, 1000, 3);
                    appData = GetAppModel(app);
                    AppViewModel = appData;
                }
                catch (Exception ex)
                {

                    appData = null;
                }

            }
            else
            {
                appData = null;
            }



            #endregion 获取应用

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
                teachingnotice = Common.Common.GetNotice(newsUrl, "", 1, 4, 1);
                teachingTrends = Common.Common.GetNotice(newsUrl, "", 2, 4, 1);
                lectureNotice = Common.Common.GetNotice(newsUrl, "", 3, 4, 1);
            }
            //获取新闻的数量
            if (!string.IsNullOrEmpty(newsCountUrl))
            {
                teachingnoticeCount = Common.Common.GetNoticeCount(newsCountUrl, 1);
                teachingTrendsCount = Common.Common.GetNoticeCount(newsCountUrl, 2);
                lectureNoticeCount = Common.Common.GetNoticeCount(newsCountUrl, 3);
            }

            var tz = Common.Common.FormatNotice(teachingnotice);
            var dt = Common.Common.FormatNotice(teachingTrends);
            var yg = Common.Common.FormatNotice(lectureNotice);

            teachingnoticeCount = tz == null ? "0" : tz.ToString();
            teachingTrendsCount = dt == null ? "0" : dt.Count().ToString();
            lectureNoticeCount = yg == null ? "0" : yg.Count().ToString();

            ViewBag.teachingNoticeList = tz;
            ViewBag.teachingTrendsList = dt;
            ViewBag.lectureNoticeList = yg;

            #endregion 新闻通告

            #region 校历

            var xiaoli = new SchoolCalendarVM();
            try
            {
                var result = Common.Common.GetJsonData(ServiceUrl);//请求接口获取校历数据
                xiaoli = GetCNXL(result);
                if (xiaoli != null)
                    xl = xiaoli.xn + "学年" + "第" + xiaoli.xq + "学期" + "第" + xiaoli.djz + "周";
            }
            catch (Exception)
            {
                xiaoli = null;
            }

            ViewBag.xiaoli = xl;

            #endregion 校历

            return View();
        }
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
                var reuslt = JsonHelper.FormatJson<AppResult<List<APPModel>>>(json);
                if (reuslt.Code != 1)
                    throw new Exception(reuslt.Msg);

                if (reuslt.Count > 0)
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
                //throw new Exception(ex.Message);
                return appListVM;
            }

            return appListVM;
        }
        /// <summary>
        /// 获取应用
        /// </summary>
        /// <returns></returns>
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
                if (objs != null)
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
                var encodeurl = Server.UrlEncode(url);
                ViewBag.url = encodeurl;
                ViewBag.xiaoli = xl;
                ViewBag.classTableName = t;
            }
           
            return View();
        }
     

        /// <summary>
        /// 获取课程
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetClass(string start, string end, string url)
        {
            var data = new List<ClassVM>();
            var startDate = DateTime.ParseExact(start, "yyyy-MM-dd", CultureInfo.CurrentCulture);
            var endDate = DateTime.ParseExact(end, "yyyy-MM-dd", CultureInfo.CurrentCulture);
            var nowDate = DateTime.Now;
            var targeturl = "";
            if (!string.IsNullOrEmpty(url))
            {
                url = Server.UrlDecode(url);
                url = GetNextOrPreClassUrl(startDate, endDate, nowDate, url);
             
                try
                {
                    #region 修改url中的第几周
                    //获取当前时间是星期几
                    var nowWeekDay = GetWeeDayk(nowDate);
                    //获取当前时间是本月第几周
                    var nowWeek = GetWeekOfYear(nowDate);
                    //获取开始时间是本月第几周
                    var startWeek = GetWeekOfYear(startDate);
                    //获取相差周数
                    var timeDifference = startWeek - nowWeek;
                    // //获取url中的第几周                 
                    int urldjz = GetSubStringdjz(url);
                    //需要小改成的第几周
                    var tartgetDjz = urldjz + timeDifference;
                    //更新校历
                    var result = Common.Common.GetJsonData(ServiceUrl);//请求接口获取校历数据
                    var xiaoli = GetCNXL(result);
                    if (xiaoli != null)
                        xiaoliStr ="["+ xiaoli.xn + "学年" + "第" + xiaoli.xq + "学期" + "第" + tartgetDjz.ToString() + "周"+"]";

                    //替换url中的第几周为相差周数
                    targeturl = ReplaceUrlBydjz(url, tartgetDjz.ToString());
                    #endregion
                }
                catch (Exception)
                {
                    targeturl = url;
                }
                
               
                var str = Common.Common.GetJsonData(targeturl);
                var listvm = GetClassVM(str);
                data = listvm;
            }
          

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Getxiaoli()
        {
            if (string.IsNullOrEmpty(xiaoliStr))
            {
                var result = Common.Common.GetJsonData(ServiceUrl);//请求接口获取校历数据
                var xiaoli = GetCNXL(result);
                if (xiaoli != null)
                    xiaoliStr = "["+xiaoli.xn + "学年" + "第" + xiaoli.xq + "学期" + "第" + xiaoli.djz + "周"+"]";
            }
          
            return Json(xiaoliStr, JsonRequestBehavior.AllowGet);
        }

        #region 废弃的获取课程
        /// <summary>
        /// 获取课程
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        //[HttpGet]
        //public ActionResult GetClass(string start, string end,string url)
        //{
        //    var data = new List<ClassVM>();
        //    var startDate = DateTime.ParseExact(start, "yyyy-MM-dd", CultureInfo.CurrentCulture);
        //    var endDate = DateTime.ParseExact(end, "yyyy-MM-dd", CultureInfo.CurrentCulture);
        //    if (ClassVMdata != null)
        //        data = ClassVMdata.Where(x => x.startDate >= startDate && x.endDate <= endDate).ToList();

        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}
        #endregion

        #region 处理点击获取上下周的方法

        /// <summary>
        /// 获取指定时间是一年中的第几周
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static int GetWeekOfYear(DateTime dt)
        {
            GregorianCalendar gc = new GregorianCalendar();
            int weekOfYear = gc.GetWeekOfYear(dt, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
            return weekOfYear;
        }
        /// <summary>
        /// 获取指定时间是本月的第几周
        /// </summary>
        /// <param name="daytime"></param>
        /// <returns></returns>
        public static int getWeekNumInMonth(DateTime daytime)
        {
            int dayInMonth = daytime.Day;
            //本月第一天  
            DateTime firstDay = daytime.AddDays(1 - daytime.Day);
            //本月第一天是周几  
            int weekday = (int)firstDay.DayOfWeek == 0 ? 7 : (int)firstDay.DayOfWeek;
            //本月第一周有几天  
            int firstWeekEndDay = 7 - (weekday - 1);
            //当前日期和第一周之差  
            int diffday = dayInMonth - firstWeekEndDay;
            diffday = diffday > 0 ? diffday : 1;
            //当前是第几周,如果整除7就减一天  
            int WeekNumInMonth = ((diffday % 7) == 0
             ? (diffday / 7 - 1)
             : (diffday / 7)) + 1 + (dayInMonth > firstWeekEndDay ? 1 : 0);
            return WeekNumInMonth;
        }
        /// <summary>
        /// 获取指定时间是星期几
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string GetWeeDayk(DateTime date)
        {
            string[] weekdays = { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
            string week = weekdays[Convert.ToInt32(date.DayOfWeek)];

            return week;
        }

        /// <summary>
        /// 修改url中的第几周
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="nowDate"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetNextOrPreClassUrl(DateTime startDate, DateTime endDate, DateTime nowDate, string url)
        {
            try
            {
                //获取当前时间是星期几
                var nowWeekDay = GetWeeDayk(nowDate);
                //获取当前时间是本月第几周
                var nowWeek = GetWeekOfYear(nowDate);
                //获取开始时间是本月第几周
                var startWeek = GetWeekOfYear(startDate);
                //获取相差周数
                var timeDifference = startWeek - nowWeek;
                // //获取url中的第几周                 
                int urldjz = GetSubStringdjz(url);
                //替换url中的第几周为相差周数
                var tartgetDjz = urldjz + timeDifference;
                url = ReplaceUrlBydjz(url, tartgetDjz.ToString());
            }
            catch (Exception)
            {
                return url;
            }

            return url;
        }
        /// <summary>
        /// 获取url中的djz
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static int GetSubStringdjz(string url)
        {
            var a = url.IndexOf("&djz=");
            var b = url.IndexOf("&jxcd=");
            var djzstr = url.Substring(a);
            djzstr = djzstr.Substring(0, djzstr.IndexOf("&jxcd="));
            djzstr = djzstr.Substring(djzstr.LastIndexOf("=") + 1);
            var djz = Convert.ToInt32(djzstr);//第几周
            return djz;
        }

        /// <summary>
        /// 替换url中的第几周
        /// </summary>
        /// <param name="url">原url</param>
        /// <param name="targetDjz">替换的第几周</param>
        /// <returns>返回替换的url</returns>
        public static string ReplaceUrlBydjz(string url, string targetDjz)
        {
            Regex r = new Regex("(?<=&djz=).*?(?=&jxcd=)", RegexOptions.IgnoreCase);
            string result = r.Replace(url, targetDjz);
            return result;
        }
        #endregion


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
                        var vm = Common.Common.GetModel((Classtitle)Enum.Parse(typeof(Classtitle), item.Title), item.Date);
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
                        var result = Common.Common.SortId(id);
                        //相邻的课程id
                        var intID = Common.Common.GetIntXLID(id);
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
        /// iframe 页面，底部添加返回按钮
        /// </summary>
        /// <param name="title"></param>
        /// <param name="url"></param>
        /// <param name="htmlurl"></param>
        /// <returns></returns>
        public ActionResult Iframe(string title, string url, string htmlurl)
        {
            Response.Headers.Set("X-Frame-Options", "sameorigin");
            ViewBag.url = url;
            ViewBag.htmlurl = htmlurl;
            ViewBag.title = title;
            return View();
        }
    }
}