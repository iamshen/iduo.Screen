using iduo.Screen.Extensions;
using iduo.Screen.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
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
        /// <param name="url"></param>
        /// <param name="t">课程表页面标题</param>
        /// <returns></returns>
        public ActionResult Calendar(string url,string t)
        {

            if (!string.IsNullOrEmpty(url))
            {
                var reUrl = Server.UrlDecode(url);
                var str = GetJsonData(url);
                var listvm = GetClassVM(str);
                ClassVMdata = listvm;
            }

            ViewBag.classTableName = t;
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
                        var dayClass = listVM.Where(x => x.title == item.title && x.Date == item.Date).OrderBy(x=>x.Id).ToList();
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
                            if (qwert==null)
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
        public int[] SortId(int[] id)
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
        public int[] GetIntXLID(int[] id)
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
        public ActionResult GetClass(string start,string end)
        {
            var startDate = DateTime.ParseExact(start, "yyyy-MM-dd", CultureInfo.CurrentCulture);
            var endDate = DateTime.ParseExact(end, "yyyy-MM-dd", CultureInfo.CurrentCulture);
            var data = ClassVMdata.Where(x => x.startDate >=startDate && x.endDate <= endDate).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
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