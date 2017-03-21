using iduo.Screen.Extensions;
using iduo.Screen.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace iduo.Screen.Controllers
{
    public class HomeController : Controller
    {
        public static List<ClassVM> ClassVMdata;
        public ActionResult Index(int height = 3, int width = 3)
        {
            ViewBag.height = height;
            ViewBag.width = width;

            //EhallVM vm = new EhallVM();
            //try
            //{
            //    //应用名称
            //    Dictionary<string, string> url = new Dictionary<string, string>();
            //    //获取所有应用
            //    AppResult<List<APPModel>> appList = GetApp("", "GetAppByClass",3, 1, 1000);
            //    if (appList.Items != null && appList.Items.Count > 0)
            //    {
            //        vm.AllAppList = appList.Items;
            //        foreach (var app in appList.Items.OrderBy(m => m.PROCESSNAME))
            //        {
            //            url.Add(app.SHOWNAME_CN, app.EXT5);
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    KFLibrary.Log.LoggorHelper.WriteLog(ex);
            //}

            return View();
        }

        /// <summary>
        /// 课程表
        /// </summary>
        /// <returns></returns>
        public ActionResult Calendar(string url)
        {
#if DEBUG
            var listvm2 = GetClassVM("");
            ClassVMdata = listvm2;
#endif

            if (!string.IsNullOrEmpty(url))
            {
                var reUrl = Server.UrlDecode(url);
                var str = GetJsonData(url);
                var listvm = GetClassVM(str);
                ClassVMdata = listvm;
            }

            return View();
        }
     
        /// <summary>
        /// 发送请求，获取课程数据
        /// </summary>
        /// <param name="url"></param>
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
            var listVM = new List<ClassVM>();//发送前台的数据模型集合
            try
            {
#if DEBUG
                //for (int i = 13; i < 20; i++)
                //{
                //    for (int j = 1; j < 13; j++)
                //    {
                //        var source = new Source()
                //        {
                //            Title = GetTitle(j),
                //            Content = "\r\n" + j + "课" + " \r\n" + j + "老师",
                //            Date = "2017-03-" + i
                //        };
                //        list.Add(source);
                //    }

                //}

                data = "[{title:'第四节课',content:'高分子化学\r\n教师：刘晓欢/刘丽娜/宋平安\r\n班级：高分子材料152;高分子材料151;\r\n上课地点:教5207(多媒体)',date:'2017-03-13'},{title:'第四节课',content:'高分子化学\r\n教师：刘晓欢/刘丽娜/宋平安\r\n班级：高分子材料152;高分子材料151;\r\n上课地点:教5204(多媒体)',date:'2017-03-15'},{title:'第三节课',content:'高分子化学\r\n教师：刘晓欢/刘丽娜/宋平安\r\n班级：高分子材料152;高分子材料151;\r\n上课地点:教5207(多媒体)',date:'2017-03-13'},{title:'第三节课',content:'高分子化学\r\n教师：刘晓欢/刘丽娜/宋平安\r\n班级：高分子材料152;高分子材料151;\r\n上课地点:教5204(多媒体)',date:'2017-03-15'}]";



#endif

                JavaScriptSerializer ser = new JavaScriptSerializer();
                List<Source> objs = ser.Deserialize<List<Source>>(data);
                list = objs;
                if (list != null)
                {
                    //根据课程节次，获取课程的具体上课时间
                    foreach (var item in list)
                    {
                        var titleInt = (int)(Classtitle)Enum.Parse(typeof(Classtitle), item.Title);
                        var num = titleInt % 2;
                        //只获取奇数节次的课程
                        if (num != 0)
                        {
                            var vm = GetModel((Classtitle)Enum.Parse(typeof(Classtitle), item.Title), item.Date);
                            vm.title = item.Content;
                            listVM.Add(vm);
                        }

                    }
                }
            }
            catch (Exception)
            {

                return listVM;
            }
          

            return listVM;
        }


        //获取课程
        [HttpGet]
        public ActionResult GetClass()
        {
            return Json(ClassVMdata, JsonRequestBehavior.AllowGet);
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
                    //第一、第二节课
                    case Classtitle.第一节课:
                        vm.start = datetime + "T08:00:00";
                        vm.end   = datetime + "T10:00:00";
                        break;
                    case Classtitle.第二节课:
                        vm.start = datetime + "T08:00:00";
                        vm.end   = datetime + "T10:00:00";

                        break;
                    case Classtitle.第三节课:
                        vm.start = datetime + "T10:00:00";
                        vm.end   = datetime + "T12:00:00";
                        break;
                    case Classtitle.第四节课:
                        vm.start = datetime + "T12:00:00";
                        vm.end   = datetime + "T14:00:00";
                        break;
                    case Classtitle.第五节课:
                        vm.start = datetime + "T12:00:00";
                        vm.end   = datetime + "T14:00:00";
                        break;
                    case Classtitle.第六节课:
                        vm.start = datetime + "T14:00:00";
                        vm.end   = datetime + "T16:00:00";
                        break;
                    case Classtitle.第七节课:
                        vm.start = datetime + "T14:00:00";
                        vm.end   = datetime + "T16:00:00";
                        break;
                    case Classtitle.第八节课:
                        vm.start = datetime + "T16:00:00";
                        vm.end   = datetime + "T18:00:00";
                        break;
                    case Classtitle.第九节课:
                        vm.start = datetime + "T16:00:00";
                        vm.end   = datetime + "T18:00:00";
                        break;
                    case Classtitle.第十节课:
                        vm.start = datetime + "T18:00:00";
                        vm.end   = datetime + "T20:00:00";
                        break;
                    case Classtitle.第十一节课:
                        vm.start = datetime + "T18:00:00";
                        vm.end = datetime + "T20:00:00";
                        break;
                    case Classtitle.第十二节课:
                        vm.start = datetime + "T18:00:00";
                        vm.end = datetime + "T20:00:00";
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return vm;
        }


       
        /// <summary>
        /// 模拟数据时使用
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public string GetTitle(int i)
        {
            var title = "";
            switch (i)
            {
                case 1:
                    title = "第一节课";
                    break;
                case 2:
                    title = "第二节课";
                    break;
                case 3:
                    title = "第三节课";
                    break;
                case 4:
                    title = "第四节课";
                    break;
                case 5:
                    title = "第五节课";
                    break;
                case 6:
                    title = "第六节课";
                    break;
                case 7:
                    title = "第七节课";
                    break;
                case 8:
                    title = "第八节课";
                    break;
                case 9:
                    title = "第九节课";
                    break;
                case 10:
                    title = "第十节课";
                    break;
                case 11:
                    title = "第十一节课";
                    break;
                case 12:
                    title = "第十二节课";
                    break;
            }
            return title;
        }

        #region Method

        /// <summary>
        /// 根据分类ID，方法名称请求应用数据源
        /// </summary>
        /// <param name="classId"></param>
        /// <param name="methodName"></param>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        //private AppResult<List<APPModel>> GetApp(string classId, string methodName, int type, int page = 1, int pagesize = 10)
        //{
        //    AppResult<List<APPModel>> result = new AppResult<List<APPModel>>();
        //    try
        //    {
        //        APP app = new APP();
        //        app.header = new APP.MySoapHeader();
        //        string json = app.GetAppByClass(classId, page, pagesize, 3);
        //        result = KFLibrary.Utils.DataToJSON.FromJson<AppResult<List<APPModel>>>(json);
        //        if (result.Code != 1)
        //            KFLibrary.Log.LoggorHelper.WriteLog("调用APP的接口GetAppByClass成功，返回出错信息：" + result.Msg);
        //    }
        //    catch (Exception ex)
        //    {
        //        KFLibrary.Log.LoggorHelper.WriteLog("调用APP的接口GetAppByClass出错：" + ex.Message);
        //    }
        //    return result;
        //}
        #endregion

    }
}