namespace Eximia.CsharpCourse.Orders.Status;

public class AwaitingProcessingStatus : IOrderStatus
{
    public string Name => "AwaitingProcessing";

    public void Handle(Order order)
    {
        throw new NotImplementedException();
    }
}
