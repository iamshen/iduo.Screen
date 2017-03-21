using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Logic;
using Entity;

namespace iduo.Screen.Models
{
   public class UserVM
        {
            /// <summary>
            /// 当前时间
            /// </summary>
            public DateTime Now = DateTime.Now;
            /// <summary>
            /// 周一
            /// </summary>
            public DateTime StartWeek { get; set; }
            /// <summary>
            /// 周日
            /// </summary>
            public DateTime EndWeek { get; set; }

            /// <summary>
            /// 姓名
            /// </summary>
            public string UserName { get; set; }
            /// <summary>
            /// 登录名
            /// </summary>
            public string LoginName { get; set; }
            /// <summary>
            /// 域名
            /// </summary>
            public string Domain { get; set; }
            /// <summary>
            /// 用户邮箱
            /// </summary>
            public string UserEmail { get; set; }
            /// <summary>
            /// 手机号
            /// </summary>
            public string UserPhone { get; set; }

            /// <summary>
            /// 主题
            /// </summary>
            public string UserThemes { get; set; }

            SYS_Client_MenuLogic cml = new SYS_Client_MenuLogic();

            //private List<SYS_Client_MenuEntity> _menus;
            /// <summary>
            /// 用户菜单
            /// </summary>
            public List<SYS_Client_MenuEntity> Menus
            {
                get;
                set;
            }
            /// <summary>
            /// 首页横向菜单
            /// </summary>
            public List<SYS_Client_MenuEntity> TopMenus { get; set; }

            public UserVM()
            {
                if (KFLibrary.Security.SecurityHelper.isLogin())
                {
                    UserName = KFLibrary.Security.SecurityHelper.LoginUserName;
                    LoginName = KFLibrary.Security.SecurityHelper.LoginUserID.Split('/')[1];
                    UserEmail = KFLibrary.Security.SecurityHelper.EmailAddress;
                    UserPhone = KFLibrary.Security.SecurityHelper.PHONE;
                    Domain = KFLibrary.Security.SecurityHelper.DomainName;
                }
                UserThemes = KFLibrary.Configuration.Configer.Setting["THEMES"];
                StartWeek = Now.AddDays(1 - Convert.ToInt32(Now.DayOfWeek.ToString("d")));  //本周周一
                EndWeek = StartWeek.AddDays(6);  //本周周日
                Menus = cml.GetUserMenuList(KFLibrary.Security.SecurityHelper.StrFullGroups);
                if (Menus != null)
                {
                    //取横向菜单
                    TopMenus = Menus.Where(m => m.selected == 1).OrderBy(m => m.seque).ToList();
                    HttpContext.Current.Session["sys_menus"] = Menus;
                }
            }
            /// <summary>
            /// 根据时间获取周几
            /// </summary>
            /// <param name="date"></param>
            /// <returns></returns>
            public string GetXQJ(DateTime date)
            {
                string[] Day = new string[] { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
                return Day[Convert.ToInt32(date.DayOfWeek.ToString("d"))].ToString();
            }
        }

}