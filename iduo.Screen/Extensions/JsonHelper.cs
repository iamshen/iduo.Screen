using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.Xml;

namespace iduo.Screen.Extensions
{
    /// <summary>
    /// Json 辅助类
    /// </summary>
    public static class JsonHelper
    {
        /// <summary>
        /// 处理Json的时间格式为正常格式
        /// </summary>
        public static string JsonDateTimeFormat(string json)
        {
            json = Regex.Replace(json,
                @"\\/Date\((\d+)\)\\/",
                match =>
                {
                    DateTime dt = new DateTime(1970, 1, 1);
                    dt = dt.AddMilliseconds(long.Parse(match.Groups[1].Value));
                    dt = dt.ToLocalTime();
                    return dt.ToString("yyyy-MM-dd HH:mm:ss.fff");
                });
            return json;
        }

        /// <summary>
        /// 把对象序列化成Json字符串格式
        /// </summary>
        /// <param name="object">Json 对象</param>
        /// <returns>Json 字符串</returns>
        public static string ToJson(this object @object)
        {
            string json = JsonConvert.SerializeObject(@object);
            return JsonDateTimeFormat(json);
        }

        /// <summary>
        /// 把Json字符串转换为强类型对象
        /// </summary>
        public static T FromJson<T>(this string json)
        {
            json = JsonDateTimeFormat(json);
            return JsonConvert.DeserializeObject<T>(json);
        }
        /// <summary>
        /// 把XmlDocument对象序列化为json字符串
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static string ToJsonForXml(this XmlDocument xml)
        {
            return JsonConvert.SerializeXmlNode(xml);
        }
    }
}
