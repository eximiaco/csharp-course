using Eximia.CsharpCourse.SeedWork;

namespace Eximia.CsharpCourse.Orders.DomainServices.CalculateOrderValue;

public interface ICalculateOrderValueDomainService : IService<ICalculateOrderValueDomainService>
{
    void Calculate(Order order, IEnumerable<Product> products);
}
