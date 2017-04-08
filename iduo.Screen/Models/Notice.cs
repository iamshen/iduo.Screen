using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iduo.Screen.Models
{
    /// <summary>
    /// 新闻
    /// </summary>
    public class Notice
    {
        /// <summary>
        /// 获取或设置 新闻的ID
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 获取或设置 新闻标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        ///获取或设置  新闻内容
        /// </summary>
        public string content { get; set; }
        /// <summary>
        /// 获取或设置 新闻的作者
        /// </summary>
        public string author { get; set; }
        /// <summary>
        /// 获取或设置 新闻的时间
        /// </summary>
        public string time { get; set; }
        /// <summary>
        /// 获取或设置 时间
        /// </summary>
        public DateTime DateTime { get; set; }
        /// <summary>
        /// 获取或设置 Url
        /// </summary>
        public string url { get; set; }

        public string Ext01 { get; set; }
        public string Ext02 { get; set; }
        public string Ext03 { get; set; }
        public string Ext04 { get; set; }
        public string Ext05 { get; set; }



    }

    /// <summary>
    /// 新闻数量
    /// </summary>
    public class NoticeCount
    {
        public string num { get; set; }
    }
}