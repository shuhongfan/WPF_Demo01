﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.18063
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Client.ServiceReference1 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="WcfServiceExamples", ConfigurationName="ServiceReference1.IService1")]
    public interface IService1 {
        
        [System.ServiceModel.OperationContractAttribute(Action="WcfServiceExamples/IService1/SayHello", ReplyAction="WcfServiceExamples/IService1/SayHelloResponse")]
        string SayHello(string name);
        
        [System.ServiceModel.OperationContractAttribute(Action="WcfServiceExamples/IService1/SayHello", ReplyAction="WcfServiceExamples/IService1/SayHelloResponse")]
        System.Threading.Tasks.Task<string> SayHelloAsync(string name);
        
        [System.ServiceModel.OperationContractAttribute(Action="WcfServiceExamples/IService1/Add", ReplyAction="WcfServiceExamples/IService1/AddResponse")]
        double Add(double d1, double d2);
        
        [System.ServiceModel.OperationContractAttribute(Action="WcfServiceExamples/IService1/Add", ReplyAction="WcfServiceExamples/IService1/AddResponse")]
        System.Threading.Tasks.Task<double> AddAsync(double d1, double d2);
        
        [System.ServiceModel.OperationContractAttribute(Action="WcfServiceExamples/IService1/Divide", ReplyAction="WcfServiceExamples/IService1/DivideResponse")]
        double Divide(double d1, double d2);
        
        [System.ServiceModel.OperationContractAttribute(Action="WcfServiceExamples/IService1/Divide", ReplyAction="WcfServiceExamples/IService1/DivideResponse")]
        System.Threading.Tasks.Task<double> DivideAsync(double d1, double d2);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IService1Channel : Client.ServiceReference1.IService1, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class Service1Client : System.ServiceModel.ClientBase<Client.ServiceReference1.IService1>, Client.ServiceReference1.IService1 {
        
        public Service1Client() {
        }
        
        public Service1Client(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public Service1Client(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Service1Client(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Service1Client(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string SayHello(string name) {
            return base.Channel.SayHello(name);
        }
        
        public System.Threading.Tasks.Task<string> SayHelloAsync(string name) {
            return base.Channel.SayHelloAsync(name);
        }
        
        public double Add(double d1, double d2) {
            return base.Channel.Add(d1, d2);
        }
        
        public System.Threading.Tasks.Task<double> AddAsync(double d1, double d2) {
            return base.Channel.AddAsync(d1, d2);
        }
        
        public double Divide(double d1, double d2) {
            return base.Channel.Divide(d1, d2);
        }
        
        public System.Threading.Tasks.Task<double> DivideAsync(double d1, double d2) {
            return base.Channel.DivideAsync(d1, d2);
        }
    }
}
