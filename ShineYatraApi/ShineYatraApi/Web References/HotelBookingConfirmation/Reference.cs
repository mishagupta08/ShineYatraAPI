﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.42000.
// 
#pragma warning disable 1591

namespace ShineYatraApi.HotelBookingConfirmation {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="HotelBookingHttpBinding", Namespace="http://booking.hotel.com")]
    public partial class HotelBooking : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback getHotelBookingOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public HotelBooking() {
            this.Url = global::ShineYatraApi.Properties.Settings.Default.ShineYatraApi_HotelBookingConfirmation_HotelBooking;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event getHotelBookingCompletedEventHandler getHotelBookingCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace="http://booking.hotel.com", ResponseNamespace="http://booking.hotel.com", Use=System.Web.Services.Description.SoapBindingUse.Literal)]
        [return: System.Xml.Serialization.XmlElementAttribute("out")]
        public string getHotelBooking(string in0, string in1) {
            object[] results = this.Invoke("getHotelBooking", new object[] {
                        in0,
                        in1});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void getHotelBookingAsync(string in0, string in1) {
            this.getHotelBookingAsync(in0, in1, null);
        }
        
        /// <remarks/>
        public void getHotelBookingAsync(string in0, string in1, object userState) {
            if ((this.getHotelBookingOperationCompleted == null)) {
                this.getHotelBookingOperationCompleted = new System.Threading.SendOrPostCallback(this.OngetHotelBookingOperationCompleted);
            }
            this.InvokeAsync("getHotelBooking", new object[] {
                        in0,
                        in1}, this.getHotelBookingOperationCompleted, userState);
        }
        
        private void OngetHotelBookingOperationCompleted(object arg) {
            if ((this.getHotelBookingCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.getHotelBookingCompleted(this, new getHotelBookingCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    public delegate void getHotelBookingCompletedEventHandler(object sender, getHotelBookingCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class getHotelBookingCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal getHotelBookingCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591