using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using iduo.Screen.Common;
using iduo.Screen.Models;

namespace iduo.Screen.Controllers
{
    public class NoticeController : Controller
    {
        //新闻数量Url
        private static string newsCountUrl = ConfigurationManager.AppSettings["iduo.screen.newscount"];
        //新闻Url
        private static string newsUrl = ConfigurationManager.AppSettings["iduo.screen.news"];
        //新闻数量
        private static string newsCountJson { get; set; }
        private static string noticeJson { get; set; }



        /// <summary>
        /// 新闻列表
        /// </summary>
        /// <param name="newsType">1=教学通知 2=教学动态 3=讲座预告</param>
        /// <param name="size">数量</param>
        /// <param name="page">页码</param>
        /// <returns></returns>
        public ActionResult Index(int newsType=0, int size = 10, int page = 1)
        {
            try
            {
                var title = "";
                List<Notice> formatNotices = new List<Notice>();
                if (newsType!=0) {
                    //获取新闻
                    if (!string.IsNullOrEmpty(newsUrl))
                        noticeJson = Common.Common.GetNotice(newsUrl, "", newsType, size, page);

                    //获取新闻数数量
                    if (!string.IsNullOrEmpty(newsCountUrl))
                        newsCountJson = Common.Common.GetNoticeCount(newsCountUrl, newsType);

                    //新闻
                    formatNotices = Common.Common.FormatNotice(noticeJson);
                    //新闻数量
                    List<NoticeCount> formatNoticeCount = Common.Common.FormatNoticeCount(newsCountJson);

                    var newsCount = formatNoticeCount.FirstOrDefault();

                    switch (newsType)
                    {
                        case 1:
                            title = "教学通知";
                            break;
                        case 2:
                            title = "教学动态";
                            break;
                        case 3:
                            title = "讲座预告";
                            break;
                    }
                }

             
                ViewBag.title = title;
                ViewBag.notices = formatNotices;
                ViewBag.page = page;
                ViewBag.size = size;
                ViewBag.newsType = newsType;
                ViewBag.newsCount = formatNotices == null ? 0 : formatNotices.Count();
            }
            catch (Exception ex)
            {

                throw;
            }
         

            return View();
        }
    }
}