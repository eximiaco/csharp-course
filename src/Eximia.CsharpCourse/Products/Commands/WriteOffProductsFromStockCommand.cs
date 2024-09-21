using Eximia.CsharpCourse.SeedWork;

namespace Eximia.CsharpCourse.Products.Commands;

public record WriteOffProductsFromStockCommand(int OrderId, IEnumerable<int> ProductIds) : ICommand { }
