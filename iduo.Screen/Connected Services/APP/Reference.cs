﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace iduo.Screen.APP {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="APP.APPSoap")]
    public interface APPSoap {
        
        // CODEGEN: 消息 AppClassRequest 以后生成的消息协定具有标头
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/AppClass", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        iduo.Screen.APP.AppClassResponse AppClass(iduo.Screen.APP.AppClassRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/AppClass", ReplyAction="*")]
        System.Threading.Tasks.Task<iduo.Screen.APP.AppClassResponse> AppClassAsync(iduo.Screen.APP.AppClassRequest request);
        
        // CODEGEN: 消息 GetAppByClassRequest 以后生成的消息协定具有标头
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetAppByClass", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        iduo.Screen.APP.GetAppByClassResponse GetAppByClass(iduo.Screen.APP.GetAppByClassRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetAppByClass", ReplyAction="*")]
        System.Threading.Tasks.Task<iduo.Screen.APP.GetAppByClassResponse> GetAppByClassAsync(iduo.Screen.APP.GetAppByClassRequest request);
        
        // CODEGEN: 消息 GetAppByNameRequest 以后生成的消息协定具有标头
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetAppByName", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        iduo.Screen.APP.GetAppByNameResponse GetAppByName(iduo.Screen.APP.GetAppByNameRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetAppByName", ReplyAction="*")]
        System.Threading.Tasks.Task<iduo.Screen.APP.GetAppByNameResponse> GetAppByNameAsync(iduo.Screen.APP.GetAppByNameRequest request);
        
        // CODEGEN: 消息 MyAppByClassRequest 以后生成的消息协定具有标头
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/MyAppByClass", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        iduo.Screen.APP.MyAppByClassResponse MyAppByClass(iduo.Screen.APP.MyAppByClassRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/MyAppByClass", ReplyAction="*")]
        System.Threading.Tasks.Task<iduo.Screen.APP.MyAppByClassResponse> MyAppByClassAsync(iduo.Screen.APP.MyAppByClassRequest request);
        
        // CODEGEN: 消息 MyAppAddRequest 以后生成的消息协定具有标头
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/MyAppAdd", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        iduo.Screen.APP.MyAppAddResponse MyAppAdd(iduo.Screen.APP.MyAppAddRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/MyAppAdd", ReplyAction="*")]
        System.Threading.Tasks.Task<iduo.Screen.APP.MyAppAddResponse> MyAppAddAsync(iduo.Screen.APP.MyAppAddRequest request);
        
        // CODEGEN: 消息 HotAppByClassRequest 以后生成的消息协定具有标头
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/HotAppByClass", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        iduo.Screen.APP.HotAppByClassResponse HotAppByClass(iduo.Screen.APP.HotAppByClassRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/HotAppByClass", ReplyAction="*")]
        System.Threading.Tasks.Task<iduo.Screen.APP.HotAppByClassResponse> HotAppByClassAsync(iduo.Screen.APP.HotAppByClassRequest request);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1586.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class MySoapHeader : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string userNameField;
        
        private string passWordField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string UserName {
            get {
                return this.userNameField;
            }
            set {
                this.userNameField = value;
                this.RaisePropertyChanged("UserName");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string PassWord {
            get {
                return this.passWordField;
            }
            set {
                this.passWordField = value;
                this.RaisePropertyChanged("PassWord");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
                this.RaisePropertyChanged("AnyAttr");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="AppClass", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class AppClassRequest {
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://tempuri.org/")]
        public iduo.Screen.APP.MySoapHeader MySoapHeader;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public string parentId;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=1)]
        public int type;
        
        public AppClassRequest() {
        }
        
        public AppClassRequest(iduo.Screen.APP.MySoapHeader MySoapHeader, string parentId, int type) {
            this.MySoapHeader = MySoapHeader;
            this.parentId = parentId;
            this.type = type;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="AppClassResponse", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class AppClassResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public string AppClassResult;
        
        public AppClassResponse() {
        }
        
        public AppClassResponse(string AppClassResult) {
            this.AppClassResult = AppClassResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="GetAppByClass", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class GetAppByClassRequest {
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://tempuri.org/")]
        public iduo.Screen.APP.MySoapHeader MySoapHeader;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public string classId;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=1)]
        public int page;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=2)]
        public int pagesize;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=3)]
        public int type;
        
        public GetAppByClassRequest() {
        }
        
        public GetAppByClassRequest(iduo.Screen.APP.MySoapHeader MySoapHeader, string classId, int page, int pagesize, int type) {
            this.MySoapHeader = MySoapHeader;
            this.classId = classId;
            this.page = page;
            this.pagesize = pagesize;
            this.type = type;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="GetAppByClassResponse", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class GetAppByClassResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public string GetAppByClassResult;
        
        public GetAppByClassResponse() {
        }
        
        public GetAppByClassResponse(string GetAppByClassResult) {
            this.GetAppByClassResult = GetAppByClassResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="GetAppByName", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class GetAppByNameRequest {
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://tempuri.org/")]
        public iduo.Screen.APP.MySoapHeader MySoapHeader;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public string appName;
        
        public GetAppByNameRequest() {
        }
        
        public GetAppByNameRequest(iduo.Screen.APP.MySoapHeader MySoapHeader, string appName) {
            this.MySoapHeader = MySoapHeader;
            this.appName = appName;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="GetAppByNameResponse", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class GetAppByNameResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public string GetAppByNameResult;
        
        public GetAppByNameResponse() {
        }
        
        public GetAppByNameResponse(string GetAppByNameResult) {
            this.GetAppByNameResult = GetAppByNameResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="MyAppByClass", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class MyAppByClassRequest {
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://tempuri.org/")]
        public iduo.Screen.APP.MySoapHeader MySoapHeader;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public string classId;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=1)]
        public int page;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=2)]
        public int pagesize;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=3)]
        public int type;
        
        public MyAppByClassRequest() {
        }
        
        public MyAppByClassRequest(iduo.Screen.APP.MySoapHeader MySoapHeader, string classId, int page, int pagesize, int type) {
            this.MySoapHeader = MySoapHeader;
            this.classId = classId;
            this.page = page;
            this.pagesize = pagesize;
            this.type = type;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="MyAppByClassResponse", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class MyAppByClassResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public string MyAppByClassResult;
        
        public MyAppByClassResponse() {
        }
        
        public MyAppByClassResponse(string MyAppByClassResult) {
            this.MyAppByClassResult = MyAppByClassResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="MyAppAdd", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class MyAppAddRequest {
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://tempuri.org/")]
        public iduo.Screen.APP.MySoapHeader MySoapHeader;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public string myApply;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=1)]
        public string action;
        
        public MyAppAddRequest() {
        }
        
        public MyAppAddRequest(iduo.Screen.APP.MySoapHeader MySoapHeader, string myApply, string action) {
            this.MySoapHeader = MySoapHeader;
            this.myApply = myApply;
            this.action = action;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="MyAppAddResponse", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class MyAppAddResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public string MyAppAddResult;
        
        public MyAppAddResponse() {
        }
        
        public MyAppAddResponse(string MyAppAddResult) {
            this.MyAppAddResult = MyAppAddResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="HotAppByClass", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class HotAppByClassRequest {
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://tempuri.org/")]
        public iduo.Screen.APP.MySoapHeader MySoapHeader;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public string classId;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=1)]
        public int page;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=2)]
        public int pagesize;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=3)]
        public int type;
        
        public HotAppByClassRequest() {
        }
        
        public HotAppByClassRequest(iduo.Screen.APP.MySoapHeader MySoapHeader, string classId, int page, int pagesize, int type) {
            this.MySoapHeader = MySoapHeader;
            this.classId = classId;
            this.page = page;
            this.pagesize = pagesize;
            this.type = type;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="HotAppByClassResponse", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class HotAppByClassResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public string HotAppByClassResult;
        
        public HotAppByClassResponse() {
        }
        
        public HotAppByClassResponse(string HotAppByClassResult) {
            this.HotAppByClassResult = HotAppByClassResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface APPSoapChannel : iduo.Screen.APP.APPSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class APPSoapClient : System.ServiceModel.ClientBase<iduo.Screen.APP.APPSoap>, iduo.Screen.APP.APPSoap {
        
        public APPSoapClient() {
        }
        
        public APPSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public APPSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public APPSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public APPSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        iduo.Screen.APP.AppClassResponse iduo.Screen.APP.APPSoap.AppClass(iduo.Screen.APP.AppClassRequest request) {
            return base.Channel.AppClass(request);
        }
        
        public string AppClass(iduo.Screen.APP.MySoapHeader MySoapHeader, string parentId, int type) {
            iduo.Screen.APP.AppClassRequest inValue = new iduo.Screen.APP.AppClassRequest();
            inValue.MySoapHeader = MySoapHeader;
            inValue.parentId = parentId;
            inValue.type = type;
            iduo.Screen.APP.AppClassResponse retVal = ((iduo.Screen.APP.APPSoap)(this)).AppClass(inValue);
            return retVal.AppClassResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<iduo.Screen.APP.AppClassResponse> iduo.Screen.APP.APPSoap.AppClassAsync(iduo.Screen.APP.AppClassRequest request) {
            return base.Channel.AppClassAsync(request);
        }
        
        public System.Threading.Tasks.Task<iduo.Screen.APP.AppClassResponse> AppClassAsync(iduo.Screen.APP.MySoapHeader MySoapHeader, string parentId, int type) {
            iduo.Screen.APP.AppClassRequest inValue = new iduo.Screen.APP.AppClassRequest();
            inValue.MySoapHeader = MySoapHeader;
            inValue.parentId = parentId;
            inValue.type = type;
            return ((iduo.Screen.APP.APPSoap)(this)).AppClassAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        iduo.Screen.APP.GetAppByClassResponse iduo.Screen.APP.APPSoap.GetAppByClass(iduo.Screen.APP.GetAppByClassRequest request) {
            return base.Channel.GetAppByClass(request);
        }
        
        public string GetAppByClass(iduo.Screen.APP.MySoapHeader MySoapHeader, string classId, int page, int pagesize, int type) {
            iduo.Screen.APP.GetAppByClassRequest inValue = new iduo.Screen.APP.GetAppByClassRequest();
            inValue.MySoapHeader = MySoapHeader;
            inValue.classId = classId;
            inValue.page = page;
            inValue.pagesize = pagesize;
            inValue.type = type;
            iduo.Screen.APP.GetAppByClassResponse retVal = ((iduo.Screen.APP.APPSoap)(this)).GetAppByClass(inValue);
            return retVal.GetAppByClassResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<iduo.Screen.APP.GetAppByClassResponse> iduo.Screen.APP.APPSoap.GetAppByClassAsync(iduo.Screen.APP.GetAppByClassRequest request) {
            return base.Channel.GetAppByClassAsync(request);
        }
        
        public System.Threading.Tasks.Task<iduo.Screen.APP.GetAppByClassResponse> GetAppByClassAsync(iduo.Screen.APP.MySoapHeader MySoapHeader, string classId, int page, int pagesize, int type) {
            iduo.Screen.APP.GetAppByClassRequest inValue = new iduo.Screen.APP.GetAppByClassRequest();
            inValue.MySoapHeader = MySoapHeader;
            inValue.classId = classId;
            inValue.page = page;
            inValue.pagesize = pagesize;
            inValue.type = type;
            return ((iduo.Screen.APP.APPSoap)(this)).GetAppByClassAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        iduo.Screen.APP.GetAppByNameResponse iduo.Screen.APP.APPSoap.GetAppByName(iduo.Screen.APP.GetAppByNameRequest request) {
            return base.Channel.GetAppByName(request);
        }
        
        public string GetAppByName(iduo.Screen.APP.MySoapHeader MySoapHeader, string appName) {
            iduo.Screen.APP.GetAppByNameRequest inValue = new iduo.Screen.APP.GetAppByNameRequest();
            inValue.MySoapHeader = MySoapHeader;
            inValue.appName = appName;
            iduo.Screen.APP.GetAppByNameResponse retVal = ((iduo.Screen.APP.APPSoap)(this)).GetAppByName(inValue);
            return retVal.GetAppByNameResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<iduo.Screen.APP.GetAppByNameResponse> iduo.Screen.APP.APPSoap.GetAppByNameAsync(iduo.Screen.APP.GetAppByNameRequest request) {
            return base.Channel.GetAppByNameAsync(request);
        }
        
        public System.Threading.Tasks.Task<iduo.Screen.APP.GetAppByNameResponse> GetAppByNameAsync(iduo.Screen.APP.MySoapHeader MySoapHeader, string appName) {
            iduo.Screen.APP.GetAppByNameRequest inValue = new iduo.Screen.APP.GetAppByNameRequest();
            inValue.MySoapHeader = MySoapHeader;
            inValue.appName = appName;
            return ((iduo.Screen.APP.APPSoap)(this)).GetAppByNameAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        iduo.Screen.APP.MyAppByClassResponse iduo.Screen.APP.APPSoap.MyAppByClass(iduo.Screen.APP.MyAppByClassRequest request) {
            return base.Channel.MyAppByClass(request);
        }
        
        public string MyAppByClass(iduo.Screen.APP.MySoapHeader MySoapHeader, string classId, int page, int pagesize, int type) {
            iduo.Screen.APP.MyAppByClassRequest inValue = new iduo.Screen.APP.MyAppByClassRequest();
            inValue.MySoapHeader = MySoapHeader;
            inValue.classId = classId;
            inValue.page = page;
            inValue.pagesize = pagesize;
            inValue.type = type;
            iduo.Screen.APP.MyAppByClassResponse retVal = ((iduo.Screen.APP.APPSoap)(this)).MyAppByClass(inValue);
            return retVal.MyAppByClassResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<iduo.Screen.APP.MyAppByClassResponse> iduo.Screen.APP.APPSoap.MyAppByClassAsync(iduo.Screen.APP.MyAppByClassRequest request) {
            return base.Channel.MyAppByClassAsync(request);
        }
        
        public System.Threading.Tasks.Task<iduo.Screen.APP.MyAppByClassResponse> MyAppByClassAsync(iduo.Screen.APP.MySoapHeader MySoapHeader, string classId, int page, int pagesize, int type) {
            iduo.Screen.APP.MyAppByClassRequest inValue = new iduo.Screen.APP.MyAppByClassRequest();
            inValue.MySoapHeader = MySoapHeader;
            inValue.classId = classId;
            inValue.page = page;
            inValue.pagesize = pagesize;
            inValue.type = type;
            return ((iduo.Screen.APP.APPSoap)(this)).MyAppByClassAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        iduo.Screen.APP.MyAppAddResponse iduo.Screen.APP.APPSoap.MyAppAdd(iduo.Screen.APP.MyAppAddRequest request) {
            return base.Channel.MyAppAdd(request);
        }
        
        public string MyAppAdd(iduo.Screen.APP.MySoapHeader MySoapHeader, string myApply, string action) {
            iduo.Screen.APP.MyAppAddRequest inValue = new iduo.Screen.APP.MyAppAddRequest();
            inValue.MySoapHeader = MySoapHeader;
            inValue.myApply = myApply;
            inValue.action = action;
            iduo.Screen.APP.MyAppAddResponse retVal = ((iduo.Screen.APP.APPSoap)(this)).MyAppAdd(inValue);
            return retVal.MyAppAddResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<iduo.Screen.APP.MyAppAddResponse> iduo.Screen.APP.APPSoap.MyAppAddAsync(iduo.Screen.APP.MyAppAddRequest request) {
            return base.Channel.MyAppAddAsync(request);
        }
        
        public System.Threading.Tasks.Task<iduo.Screen.APP.MyAppAddResponse> MyAppAddAsync(iduo.Screen.APP.MySoapHeader MySoapHeader, string myApply, string action) {
            iduo.Screen.APP.MyAppAddRequest inValue = new iduo.Screen.APP.MyAppAddRequest();
            inValue.MySoapHeader = MySoapHeader;
            inValue.myApply = myApply;
            inValue.action = action;
            return ((iduo.Screen.APP.APPSoap)(this)).MyAppAddAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        iduo.Screen.APP.HotAppByClassResponse iduo.Screen.APP.APPSoap.HotAppByClass(iduo.Screen.APP.HotAppByClassRequest request) {
            return base.Channel.HotAppByClass(request);
        }
        
        public string HotAppByClass(iduo.Screen.APP.MySoapHeader MySoapHeader, string classId, int page, int pagesize, int type) {
            iduo.Screen.APP.HotAppByClassRequest inValue = new iduo.Screen.APP.HotAppByClassRequest();
            inValue.MySoapHeader = MySoapHeader;
            inValue.classId = classId;
            inValue.page = page;
            inValue.pagesize = pagesize;
            inValue.type = type;
            iduo.Screen.APP.HotAppByClassResponse retVal = ((iduo.Screen.APP.APPSoap)(this)).HotAppByClass(inValue);
            return retVal.HotAppByClassResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<iduo.Screen.APP.HotAppByClassResponse> iduo.Screen.APP.APPSoap.HotAppByClassAsync(iduo.Screen.APP.HotAppByClassRequest request) {
            return base.Channel.HotAppByClassAsync(request);
        }
        
        public System.Threading.Tasks.Task<iduo.Screen.APP.HotAppByClassResponse> HotAppByClassAsync(iduo.Screen.APP.MySoapHeader MySoapHeader, string classId, int page, int pagesize, int type) {
            iduo.Screen.APP.HotAppByClassRequest inValue = new iduo.Screen.APP.HotAppByClassRequest();
            inValue.MySoapHeader = MySoapHeader;
            inValue.classId = classId;
            inValue.page = page;
            inValue.pagesize = pagesize;
            inValue.type = type;
            return ((iduo.Screen.APP.APPSoap)(this)).HotAppByClassAsync(inValue);
        }
    }
}
