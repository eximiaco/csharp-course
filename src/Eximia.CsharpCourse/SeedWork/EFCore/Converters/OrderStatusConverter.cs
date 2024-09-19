using Eximia.CsharpCourse.Orders.Status;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Eximia.CsharpCourse.SeedWork.EFCore.Converters;

public class OrderStatusConverter : ValueConverter<IOrderStatus, string>
{
    public OrderStatusConverter()
        :base(
            status => status.Name,
            name => ToOrderStatus(name))
    { }

    private static IOrderStatus ToOrderStatus(string status)
    {
        // Adicionar o sufixo "Status" ao nome vindo do banco
        var typeName = $"{typeof(IOrderStatus).Namespace}.{status}Status";

        // Obter o tipo correspondente ao status
        var type = Type.GetType(typeName, throwOnError: false);
        if (type == null)
            throw new InvalidOperationException($"Status type '{typeName}' not found.");

        // Verificar se o tipo implementa IOrderStatus
        if (!typeof(IOrderStatus).IsAssignableFrom(type))
            throw new InvalidOperationException($"Status type '{typeName}' does not implement IOrderStatus.");

        // Criar a instância dinamicamente
        return (IOrderStatus)Activator.CreateInstance(type)!;
    }
}
