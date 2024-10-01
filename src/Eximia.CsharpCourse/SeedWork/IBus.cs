namespace Eximia.CsharpCourse.SeedWork;

public interface IBus
{
    Task Publish(object message, CancellationToken cancellationToken = default);
}