﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17929
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RegisterDeviceMVC.PacpDataService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ClsAlerts", Namespace="http://Pacp.com")]
    [System.SerializableAttribute()]
    public partial class ClsAlerts : RegisterDeviceMVC.PacpDataService.ClsMaster {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private byte[] ContentField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<System.DateTime> DateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DescriptionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ImageField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string LanguageField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TitleField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int UserIdField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public byte[] Content {
            get {
                return this.ContentField;
            }
            set {
                if ((object.ReferenceEquals(this.ContentField, value) != true)) {
                    this.ContentField = value;
                    this.RaisePropertyChanged("Content");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.DateTime> Date {
            get {
                return this.DateField;
            }
            set {
                if ((this.DateField.Equals(value) != true)) {
                    this.DateField = value;
                    this.RaisePropertyChanged("Date");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Description {
            get {
                return this.DescriptionField;
            }
            set {
                if ((object.ReferenceEquals(this.DescriptionField, value) != true)) {
                    this.DescriptionField = value;
                    this.RaisePropertyChanged("Description");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Image {
            get {
                return this.ImageField;
            }
            set {
                if ((object.ReferenceEquals(this.ImageField, value) != true)) {
                    this.ImageField = value;
                    this.RaisePropertyChanged("Image");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Language {
            get {
                return this.LanguageField;
            }
            set {
                if ((object.ReferenceEquals(this.LanguageField, value) != true)) {
                    this.LanguageField = value;
                    this.RaisePropertyChanged("Language");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Title {
            get {
                return this.TitleField;
            }
            set {
                if ((object.ReferenceEquals(this.TitleField, value) != true)) {
                    this.TitleField = value;
                    this.RaisePropertyChanged("Title");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int UserId {
            get {
                return this.UserIdField;
            }
            set {
                if ((this.UserIdField.Equals(value) != true)) {
                    this.UserIdField = value;
                    this.RaisePropertyChanged("UserId");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ClsMaster", Namespace="http://Pacp.com")]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(RegisterDeviceMVC.PacpDataService.ClsAbout))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(RegisterDeviceMVC.PacpDataService.Device))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(RegisterDeviceMVC.PacpDataService.ClsAlerts))]
    public partial class ClsMaster : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ClsAbout", Namespace="http://Pacp.com")]
    [System.SerializableAttribute()]
    public partial class ClsAbout : RegisterDeviceMVC.PacpDataService.ClsMaster {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ATextField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TextField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TitleField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string AText {
            get {
                return this.ATextField;
            }
            set {
                if ((object.ReferenceEquals(this.ATextField, value) != true)) {
                    this.ATextField = value;
                    this.RaisePropertyChanged("AText");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Text {
            get {
                return this.TextField;
            }
            set {
                if ((object.ReferenceEquals(this.TextField, value) != true)) {
                    this.TextField = value;
                    this.RaisePropertyChanged("Text");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Title {
            get {
                return this.TitleField;
            }
            set {
                if ((object.ReferenceEquals(this.TitleField, value) != true)) {
                    this.TitleField = value;
                    this.RaisePropertyChanged("Title");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Device", Namespace="http://Pacp.com")]
    [System.SerializableAttribute()]
    public partial class Device : RegisterDeviceMVC.PacpDataService.ClsMaster {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string deviceRegistrationIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int deviceTypeField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string deviceRegistrationId {
            get {
                return this.deviceRegistrationIdField;
            }
            set {
                if ((object.ReferenceEquals(this.deviceRegistrationIdField, value) != true)) {
                    this.deviceRegistrationIdField = value;
                    this.RaisePropertyChanged("deviceRegistrationId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int deviceType {
            get {
                return this.deviceTypeField;
            }
            set {
                if ((this.deviceTypeField.Equals(value) != true)) {
                    this.deviceTypeField = value;
                    this.RaisePropertyChanged("deviceType");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="clsUser", Namespace="http://Pacp.com")]
    [System.SerializableAttribute()]
    public partial class clsUser : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CountryField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string EmailField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PasswordField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UserNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UserTypeField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Country {
            get {
                return this.CountryField;
            }
            set {
                if ((object.ReferenceEquals(this.CountryField, value) != true)) {
                    this.CountryField = value;
                    this.RaisePropertyChanged("Country");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Email {
            get {
                return this.EmailField;
            }
            set {
                if ((object.ReferenceEquals(this.EmailField, value) != true)) {
                    this.EmailField = value;
                    this.RaisePropertyChanged("Email");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Password {
            get {
                return this.PasswordField;
            }
            set {
                if ((object.ReferenceEquals(this.PasswordField, value) != true)) {
                    this.PasswordField = value;
                    this.RaisePropertyChanged("Password");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UserName {
            get {
                return this.UserNameField;
            }
            set {
                if ((object.ReferenceEquals(this.UserNameField, value) != true)) {
                    this.UserNameField = value;
                    this.RaisePropertyChanged("UserName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UserType {
            get {
                return this.UserTypeField;
            }
            set {
                if ((object.ReferenceEquals(this.UserTypeField, value) != true)) {
                    this.UserTypeField = value;
                    this.RaisePropertyChanged("UserType");
                }
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
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://Pacp.com", ConfigurationName="PacpDataService.IPacp")]
    public interface IPacp {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://Pacp.com/IPacp/GetAllAlerts", ReplyAction="http://Pacp.com/IPacp/GetAllAlertsResponse")]
        RegisterDeviceMVC.PacpDataService.ClsAlerts[] GetAllAlerts();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://Pacp.com/IPacp/GetAlertById", ReplyAction="http://Pacp.com/IPacp/GetAlertByIdResponse")]
        RegisterDeviceMVC.PacpDataService.ClsAlerts GetAlertById(System.Nullable<int> Id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://Pacp.com/IPacp/InsertAlert", ReplyAction="http://Pacp.com/IPacp/InsertAlertResponse")]
        string InsertAlert(RegisterDeviceMVC.PacpDataService.ClsAlerts obj);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://Pacp.com/IPacp/GetInsertAlertResponse", ReplyAction="http://Pacp.com/IPacp/GetInsertAlertResponseResponse")]
        string GetInsertAlertResponse(RegisterDeviceMVC.PacpDataService.ClsAlerts obj);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://Pacp.com/IPacp/UpdateAlert", ReplyAction="http://Pacp.com/IPacp/UpdateAlertResponse")]
        string UpdateAlert(RegisterDeviceMVC.PacpDataService.ClsAlerts obj);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://Pacp.com/IPacp/GetUpdateAlertResponse", ReplyAction="http://Pacp.com/IPacp/GetUpdateAlertResponseResponse")]
        string GetUpdateAlertResponse(RegisterDeviceMVC.PacpDataService.ClsAlerts obj);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://Pacp.com/IPacp/DeleteAlert", ReplyAction="http://Pacp.com/IPacp/DeleteAlertResponse")]
        string DeleteAlert(int Id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://Pacp.com/IPacp/GetDeleteAlertResponse", ReplyAction="http://Pacp.com/IPacp/GetDeleteAlertResponseResponse")]
        string GetDeleteAlertResponse(int Id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://Pacp.com/IPacp/GetAllAbout", ReplyAction="http://Pacp.com/IPacp/GetAllAboutResponse")]
        RegisterDeviceMVC.PacpDataService.ClsAbout[] GetAllAbout();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://Pacp.com/IPacp/GetAboutById", ReplyAction="http://Pacp.com/IPacp/GetAboutByIdResponse")]
        RegisterDeviceMVC.PacpDataService.ClsAbout GetAboutById(System.Nullable<int> Id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://Pacp.com/IPacp/InsertAbout", ReplyAction="http://Pacp.com/IPacp/InsertAboutResponse")]
        string InsertAbout(RegisterDeviceMVC.PacpDataService.ClsAbout obj);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://Pacp.com/IPacp/UpdateAbout", ReplyAction="http://Pacp.com/IPacp/UpdateAboutResponse")]
        string UpdateAbout(RegisterDeviceMVC.PacpDataService.ClsAbout obj);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://Pacp.com/IPacp/DeleteAbout", ReplyAction="http://Pacp.com/IPacp/DeleteAboutResponse")]
        string DeleteAbout(int Id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://Pacp.com/IPacp/ForgotPwd", ReplyAction="http://Pacp.com/IPacp/ForgotPwdResponse")]
        string ForgotPwd(System.Nullable<int> Id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://Pacp.com/IPacp/GetAllUsers", ReplyAction="http://Pacp.com/IPacp/GetAllUsersResponse")]
        RegisterDeviceMVC.PacpDataService.clsUser[] GetAllUsers();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://Pacp.com/IPacp/GetUserById", ReplyAction="http://Pacp.com/IPacp/GetUserByIdResponse")]
        RegisterDeviceMVC.PacpDataService.clsUser GetUserById(System.Nullable<int> Id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://Pacp.com/IPacp/GetUserByNamePwd", ReplyAction="http://Pacp.com/IPacp/GetUserByNamePwdResponse")]
        RegisterDeviceMVC.PacpDataService.clsUser GetUserByNamePwd(string UserName, string Password, string UserType);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://Pacp.com/IPacp/InsertUser", ReplyAction="http://Pacp.com/IPacp/InsertUserResponse")]
        string InsertUser(RegisterDeviceMVC.PacpDataService.clsUser obj);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://Pacp.com/IPacp/UpdateUser", ReplyAction="http://Pacp.com/IPacp/UpdateUserResponse")]
        string UpdateUser(RegisterDeviceMVC.PacpDataService.clsUser obj);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://Pacp.com/IPacp/DeleteUser", ReplyAction="http://Pacp.com/IPacp/DeleteUserResponse")]
        string DeleteUser(int Id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://Pacp.com/IPacp/getAlerts", ReplyAction="http://Pacp.com/IPacp/getAlertsResponse")]
        RegisterDeviceMVC.PacpDataService.ClsAlerts[] getAlerts(string title, string desc);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://Pacp.com/IPacp/GetAllDevices", ReplyAction="http://Pacp.com/IPacp/GetAllDevicesResponse")]
        RegisterDeviceMVC.PacpDataService.Device[] GetAllDevices();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://Pacp.com/IPacp/GetDevicesByType", ReplyAction="http://Pacp.com/IPacp/GetDevicesByTypeResponse")]
        RegisterDeviceMVC.PacpDataService.Device[] GetDevicesByType(int deviceType);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://Pacp.com/IPacp/GetDevice", ReplyAction="http://Pacp.com/IPacp/GetDeviceResponse")]
        RegisterDeviceMVC.PacpDataService.Device GetDevice(string deviceRegistrationId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://Pacp.com/IPacp/insertDevice", ReplyAction="http://Pacp.com/IPacp/insertDeviceResponse")]
        string insertDevice(RegisterDeviceMVC.PacpDataService.Device device);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://Pacp.com/IPacp/updateDevice", ReplyAction="http://Pacp.com/IPacp/updateDeviceResponse")]
        string updateDevice(string deviceRegistrationId, int deviceType);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://Pacp.com/IPacp/deleteDevice", ReplyAction="http://Pacp.com/IPacp/deleteDeviceResponse")]
        string deleteDevice(string deviceRegistrationId);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IPacpChannel : RegisterDeviceMVC.PacpDataService.IPacp, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class PacpClient : System.ServiceModel.ClientBase<RegisterDeviceMVC.PacpDataService.IPacp>, RegisterDeviceMVC.PacpDataService.IPacp {
        
        public PacpClient() {
        }
        
        public PacpClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public PacpClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public PacpClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public PacpClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public RegisterDeviceMVC.PacpDataService.ClsAlerts[] GetAllAlerts() {
            return base.Channel.GetAllAlerts();
        }
        
        public RegisterDeviceMVC.PacpDataService.ClsAlerts GetAlertById(System.Nullable<int> Id) {
            return base.Channel.GetAlertById(Id);
        }
        
        public string InsertAlert(RegisterDeviceMVC.PacpDataService.ClsAlerts obj) {
            return base.Channel.InsertAlert(obj);
        }
        
        public string GetInsertAlertResponse(RegisterDeviceMVC.PacpDataService.ClsAlerts obj) {
            return base.Channel.GetInsertAlertResponse(obj);
        }
        
        public string UpdateAlert(RegisterDeviceMVC.PacpDataService.ClsAlerts obj) {
            return base.Channel.UpdateAlert(obj);
        }
        
        public string GetUpdateAlertResponse(RegisterDeviceMVC.PacpDataService.ClsAlerts obj) {
            return base.Channel.GetUpdateAlertResponse(obj);
        }
        
        public string DeleteAlert(int Id) {
            return base.Channel.DeleteAlert(Id);
        }
        
        public string GetDeleteAlertResponse(int Id) {
            return base.Channel.GetDeleteAlertResponse(Id);
        }
        
        public RegisterDeviceMVC.PacpDataService.ClsAbout[] GetAllAbout() {
            return base.Channel.GetAllAbout();
        }
        
        public RegisterDeviceMVC.PacpDataService.ClsAbout GetAboutById(System.Nullable<int> Id) {
            return base.Channel.GetAboutById(Id);
        }
        
        public string InsertAbout(RegisterDeviceMVC.PacpDataService.ClsAbout obj) {
            return base.Channel.InsertAbout(obj);
        }
        
        public string UpdateAbout(RegisterDeviceMVC.PacpDataService.ClsAbout obj) {
            return base.Channel.UpdateAbout(obj);
        }
        
        public string DeleteAbout(int Id) {
            return base.Channel.DeleteAbout(Id);
        }
        
        public string ForgotPwd(System.Nullable<int> Id) {
            return base.Channel.ForgotPwd(Id);
        }
        
        public RegisterDeviceMVC.PacpDataService.clsUser[] GetAllUsers() {
            return base.Channel.GetAllUsers();
        }
        
        public RegisterDeviceMVC.PacpDataService.clsUser GetUserById(System.Nullable<int> Id) {
            return base.Channel.GetUserById(Id);
        }
        
        public RegisterDeviceMVC.PacpDataService.clsUser GetUserByNamePwd(string UserName, string Password, string UserType) {
            return base.Channel.GetUserByNamePwd(UserName, Password, UserType);
        }
        
        public string InsertUser(RegisterDeviceMVC.PacpDataService.clsUser obj) {
            return base.Channel.InsertUser(obj);
        }
        
        public string UpdateUser(RegisterDeviceMVC.PacpDataService.clsUser obj) {
            return base.Channel.UpdateUser(obj);
        }
        
        public string DeleteUser(int Id) {
            return base.Channel.DeleteUser(Id);
        }
        
        public RegisterDeviceMVC.PacpDataService.ClsAlerts[] getAlerts(string title, string desc) {
            return base.Channel.getAlerts(title, desc);
        }
        
        public RegisterDeviceMVC.PacpDataService.Device[] GetAllDevices() {
            return base.Channel.GetAllDevices();
        }
        
        public RegisterDeviceMVC.PacpDataService.Device[] GetDevicesByType(int deviceType) {
            return base.Channel.GetDevicesByType(deviceType);
        }
        
        public RegisterDeviceMVC.PacpDataService.Device GetDevice(string deviceRegistrationId) {
            return base.Channel.GetDevice(deviceRegistrationId);
        }
        
        public string insertDevice(RegisterDeviceMVC.PacpDataService.Device device) {
            return base.Channel.insertDevice(device);
        }
        
        public string updateDevice(string deviceRegistrationId, int deviceType) {
            return base.Channel.updateDevice(deviceRegistrationId, deviceType);
        }
        
        public string deleteDevice(string deviceRegistrationId) {
            return base.Channel.deleteDevice(deviceRegistrationId);
        }
    }
}
