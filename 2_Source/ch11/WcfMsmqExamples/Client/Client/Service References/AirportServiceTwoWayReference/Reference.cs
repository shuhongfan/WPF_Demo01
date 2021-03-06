//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.18052
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Client.AirportServiceTwoWayReference {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="AirportMessage", Namespace="WcfMsmqExamples")]
    [System.SerializableAttribute()]
    public partial class AirportMessage : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string AirportIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime ForecastTimeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ShortMessageField;
        
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
        public string AirportId {
            get {
                return this.AirportIdField;
            }
            set {
                if ((object.ReferenceEquals(this.AirportIdField, value) != true)) {
                    this.AirportIdField = value;
                    this.RaisePropertyChanged("AirportId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime ForecastTime {
            get {
                return this.ForecastTimeField;
            }
            set {
                if ((this.ForecastTimeField.Equals(value) != true)) {
                    this.ForecastTimeField = value;
                    this.RaisePropertyChanged("ForecastTime");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ShortMessage {
            get {
                return this.ShortMessageField;
            }
            set {
                if ((object.ReferenceEquals(this.ShortMessageField, value) != true)) {
                    this.ShortMessageField = value;
                    this.RaisePropertyChanged("ShortMessage");
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
    [System.ServiceModel.ServiceContractAttribute(Namespace="WcfMsmqExamples", ConfigurationName="AirportServiceTwoWayReference.IAirportServiceTwoWay")]
    public interface IAirportServiceTwoWay {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="WcfMsmqExamples/IAirportServiceTwoWay/SubmitInfo")]
        void SubmitInfo(string info);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="WcfMsmqExamples/IAirportServiceTwoWay/SubmitInfo")]
        System.Threading.Tasks.Task SubmitInfoAsync(string info);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="WcfMsmqExamples/IAirportServiceTwoWay/SubmitAirportMessageTwoWay")]
        void SubmitAirportMessageTwoWay(Client.AirportServiceTwoWayReference.AirportMessage message, string reportStatusTo);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="WcfMsmqExamples/IAirportServiceTwoWay/SubmitAirportMessageTwoWay")]
        System.Threading.Tasks.Task SubmitAirportMessageTwoWayAsync(Client.AirportServiceTwoWayReference.AirportMessage message, string reportStatusTo);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IAirportServiceTwoWayChannel : Client.AirportServiceTwoWayReference.IAirportServiceTwoWay, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class AirportServiceTwoWayClient : System.ServiceModel.ClientBase<Client.AirportServiceTwoWayReference.IAirportServiceTwoWay>, Client.AirportServiceTwoWayReference.IAirportServiceTwoWay {
        
        public AirportServiceTwoWayClient() {
        }
        
        public AirportServiceTwoWayClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public AirportServiceTwoWayClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AirportServiceTwoWayClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AirportServiceTwoWayClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void SubmitInfo(string info) {
            base.Channel.SubmitInfo(info);
        }
        
        public System.Threading.Tasks.Task SubmitInfoAsync(string info) {
            return base.Channel.SubmitInfoAsync(info);
        }
        
        public void SubmitAirportMessageTwoWay(Client.AirportServiceTwoWayReference.AirportMessage message, string reportStatusTo) {
            base.Channel.SubmitAirportMessageTwoWay(message, reportStatusTo);
        }
        
        public System.Threading.Tasks.Task SubmitAirportMessageTwoWayAsync(Client.AirportServiceTwoWayReference.AirportMessage message, string reportStatusTo) {
            return base.Channel.SubmitAirportMessageTwoWayAsync(message, reportStatusTo);
        }
    }
}
