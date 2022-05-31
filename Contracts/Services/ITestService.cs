using System.ServiceModel;

namespace GrpcLiteWasm.Contracts.Services;

[ServiceContract]
public interface ITestService
{
    [OperationContract] string Echo(string message);
    [OperationContract] IObservable<string> Subscribe(IObservable<string> requests);
}