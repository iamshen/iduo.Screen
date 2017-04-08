using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace iduo.Screen
{
    /// <summary>
    /// WebService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    [System.Web.Script.Services.ScriptService]
    public class WebService : System.Web.Services.WebService
    {

        [WebMethod(Description = "获取cookie")]
        public string GetCookies(string cookiename)
        {
            var value = "";
            try
            {
               value = HttpContext.Current.Request.Cookies[cookiename].Value;

            }
            catch (Exception)
            {

                value = "";
            }

            string callbackMethodName = HttpContext.Current.Request.Params["jsoncallback"] ?? "";

            HttpContext.Current.Response.Write(value);
            HttpContext.Current.Response.End();


            return value;
        }

        [WebMethod(Description = "设置cookie")]
        public void setCookies(string username,string keyname)
        {
            ClearCookie(keyname);
            var result = "";
           
            try
            {
                HttpCookie cookie = new HttpCookie(keyname);
                cookie.Value = username;
                cookie.Expires = DateTime.Now.AddMonths(12);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
            catch (Exception)
            {

                result = "no";
            }
            result = "yes";

            string callbackMethodName = HttpContext.Current.Request.Params["jsoncallback"] ?? "";
            HttpContext.Current.Response.Write(result);
            HttpContext.Current.Response.End();
        }
        [WebMethod(Description = "清除ookie")]
        public bool ClearCookie(string keyname)
        {
            HttpCookie cookie = new HttpCookie(keyname);
            cookie.Expires = DateTime.Now.AddMonths(-2);
            HttpContext.Current.Response.Cookies.Add(cookie);

            return true;
        }
    }
}
