using WorkflowCore.Models;
using WorkflowCore.Interface;

public class NoOpStep : StepBody
{
    public override ExecutionResult Run(IStepExecutionContext context)
    {
        return ExecutionResult.Next();
    }
} 