//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.18052
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Service.AirportMessageStatusServiceReference {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="WcfMsmqExamples", ConfigurationName="AirportMessageStatusServiceReference.IAirportMessageStatusService")]
    public interface IAirportMessageStatusService {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="WcfMsmqExamples/IAirportMessageStatusService/AirportMessageStatus")]
        void AirportMessageStatus(string id, string status);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="WcfMsmqExamples/IAirportMessageStatusService/AirportMessageStatus")]
        System.Threading.Tasks.Task AirportMessageStatusAsync(string id, string status);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IAirportMessageStatusServiceChannel : Service.AirportMessageStatusServiceReference.IAirportMessageStatusService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class AirportMessageStatusServiceClient : System.ServiceModel.ClientBase<Service.AirportMessageStatusServiceReference.IAirportMessageStatusService>, Service.AirportMessageStatusServiceReference.IAirportMessageStatusService {
        
        public AirportMessageStatusServiceClient() {
        }
        
        public AirportMessageStatusServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public AirportMessageStatusServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AirportMessageStatusServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AirportMessageStatusServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void AirportMessageStatus(string id, string status) {
            base.Channel.AirportMessageStatus(id, status);
        }
        
        public System.Threading.Tasks.Task AirportMessageStatusAsync(string id, string status) {
            return base.Channel.AirportMessageStatusAsync(id, status);
        }
    }
}
