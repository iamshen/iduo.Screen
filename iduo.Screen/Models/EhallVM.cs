using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iduo.Screen.Models
{

    /// <summary>
    /// 办事大厅视图模型
    /// </summary>
    public class EhallVM : UserVM
    {
        /// <summary>
        /// 获取或设置 服务分类
        /// </summary>
        public List<AppClassModel> ClassList { get; set; }
        /// <summary>
        /// 获取或设置 部门分类
        /// </summary>
        public List<string> DeptClassList { get; set; }
        /// <summary>
        /// 获取或设置 服务对象
        /// </summary>
        public List<ServiceTion> TionClassList { get; set; }
        /// <summary>
        /// 获取或设置 责任部门
        /// </summary>
        public List<DeptTion> DeptTionList { get; set; }
        /// <summary>
        /// 按拼音首字母应用列表
        /// </summary>
        public Dictionary<string, List<APPModel>> AppList { get; set; }

        /// <summary>
        /// 未排序的应用列表
        /// </summary>
        public List<APPModel> AllAppList { get; set; }

        public EhallVM()
        {
            DeptClassList = new List<string>();
            ClassList = new List<AppClassModel>();
            AppList = new Dictionary<string, List<APPModel>>();
            AllAppList = new List<APPModel>();
            TionClassList = new List<ServiceTion>();
            DeptTionList = new List<DeptTion>();
        }
    }


    /// <summary>
    /// 应用信息类
    /// </summary>
    public class APPModel
    {
        public int ID { get; set; }
        /// <summary>
        /// 唯一标识符（如需要获取应用ID，请使用PROCESSNAME）
        /// </summary>
        public string GUID { get; set; }
        /// <summary>
        /// 应用分类
        /// </summary>
        public string CLASSID { get; set; }
        /// <summary>
        /// 应用ID
        /// </summary>
        public string PROCESSNAME { get; set; }
        /// <summary>
        /// 应用名称(CN）
        /// </summary>
        public string SHOWNAME_CN { get; set; }
        /// <summary>
        /// 应用名称（EN)
        /// </summary>
        public string SHOWNAME_EN { get; set; }
        private string _iconName;
        /// <summary>
        /// PC端图标
        /// </summary>
        public string ICONNAME
        {
            get
            {
                if (string.IsNullOrEmpty(_iconName))
                    _iconName = "fa fa-fw fa-building-o";
                return _iconName;
            }
            set
            {
                _iconName = value;
            }
        }
        public string LABEL { get; set; }
        public int ORDERNO { get; set; }
        public string EXT1 { get; set; }
        public string EXT2 { get; set; }
        public string EXT3 { get; set; }
        public string EXT4 { get; set; }
        public string EXT5 { get; set; }
        public string EXT6 { get; set; }
        public DateTime CREATE_TIME { get; set; }
        public string ISSHOWPORTAL { get; set; }
        public string APPICO { get; set; }
        public string SSOTYPE { get; set; }
        public string ISSHOWPC { get; set; }
        public string GROUPNAME_CN { get; set; }
        public string GROUPNAME_EN { get; set; }
        public string GROUPICON { get; set; }
        public string PARENTID { get; set; }
        int GROUPORDER { get; set; }
        /// <summary>
        /// 评价平均分数
        /// </summary>
        public string PJ_AVG_SCORE { get; set; }
        /// <summary>
        /// 评价总人数
        /// </summary>
        public string PJ_COUNT { get; set; }
        /// <summary>
        /// 责任部门
        /// </summary>
        public string APPZRBM { get; set; }
        /// <summary>
        /// 是否收藏 1：收藏 0 未收藏
        /// </summary>
        public string INMYAPP { get; set; }
        /// <summary>
        /// 责任部门
        /// </summary>
        public string ZRBM_ZRBM { get; set; }
        /// <summary>
        /// 责任部门ID
        /// </summary>
        public string ZRBM_ID { get; set; }
        /// <summary>
        /// 服务对象
        /// </summary>
        public string APPFWDX { get; set; }
        /// <summary>
        /// 服务对象ID
        /// </summary>
        public string FWDX_ID { get; set; }
        /// <summary>
        /// 可用类型
        /// </summary>
        public string EHALLTYPE { get; set; }
    }

    /// <summary>
    /// 服务对象
    /// </summary>
    public class ServiceTion
    {
        /// <summary>
        /// 服务对象
        /// </summary>
        public string FWDX_FWDX { get; set; }
        /// <summary>
        /// 排序ID
        /// </summary>
        public string FWDX_ID { get; set; }
        /// <summary>
        /// 分类ID
        /// </summary>
        public string GUID { get; set; }
    }
    /// <summary>
    /// 责任部门
    /// </summary>
    public class DeptTion
    {
        /// <summary>
        /// 责任部门
        /// </summary>
        public string ZRBM_ZRBM { get; set; }
        /// <summary>
        /// 排序ID
        /// </summary>
        public string ZRBM_ID { get; set; }
        /// <summary>
        /// 分类ID
        /// </summary>
        public string GUID { get; set; }
    }
    /// <summary>
    /// 应用分类信息类
    /// </summary>
    public class AppClassModel
    {
        /// <summary>
        /// 分类ID
        /// </summary>
        public string GUID { get; set; }
        /// <summary>
        /// 上级分类
        /// </summary>
        public string PARENTID { get; set; }
        /// <summary>
        /// 分类名（中文）
        /// </summary>
        public string GROUPNAME_CN { get; set; }
        /// <summary>
        /// 分类名（英文）
        /// </summary>
        public string GROUPNAME_EN { get; set; }
        /// <summary>
        /// 分类线
        /// </summary>
        public string CLASS_LIST { get; set; }
        /// <summary>
        /// 分类图标
        /// </summary>
        public string ICONNAME { get; set; }
        /// <summary>
        /// 显示终端
        ///    0：移动端&PC端
        ///    1：PC端
        ///    2：移动端
        /// </summary>
        public string EXT4 { get; set; }
        /// <summary>
        /// 移动端图标
        /// </summary>
        public string EXT3 { get; set; }
        /// <summary>
        /// 是否在门户中显示
        /// </summary>
        public string ISSHOWPORTAL { get; set; }
        /// <summary>
        /// 排序号
        /// </summary>
        public string ORDERNO { get; set; }
    }
    /// <summary>
    /// 应用接口返回结果
    /// </summary>
    public class AppResult<T>
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string Msg { get; set; }
        /// <summary>
        /// 总条数
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 单页集合
        /// </summary>
        public T Items { get; set; }
    }
}