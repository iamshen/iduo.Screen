using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iduo.Screen.Models
{
    /// <summary>
    /// 课程数据模型
    /// </summary>
    public class ClassVM
    {
        /// <summary>
        /// int 型的节次(表示：第Id节课)
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 相当于Source 的Content
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// 开始时间 查询
        /// </summary>
        public DateTime startDate { get; set; }

        /// <summary>
        /// 结束时间 查询
        /// </summary>
        public DateTime endDate { get; set; }

        /// <summary>
        /// 开始时间 （前端显示使用字段）
        /// </summary>
        public string start { get; set; }

        /// <summary>
        /// 结束时间（前端显示使用字段）
        /// </summary>
        public string end { get; set; }

        public string Date { get; set; }
    }
}