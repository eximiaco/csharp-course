using CSharpFunctionalExtensions;

namespace Eximia.CsharpCourse.Orders.States;

public class AwaitingProcessingState : IOrderState
{
    public string Name => "AwaitingProcessing";

    public Result Cancel(Order order)
    {
        order.ChangeState(new CanceledState());
        return Result.Success();
    }
}
