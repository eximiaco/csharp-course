using Eximia.CsharpCourse.Orders.States;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Eximia.CsharpCourse.SeedWork.EFCore.Converters;

public class OrderStatusConverter : ValueConverter<IOrderState, string>
{
    public OrderStatusConverter()
        :base(
            status => status.Name,
            name => ToOrderStatus(name))
    { }

    private static IOrderState ToOrderStatus(string status)
    {
        // Adicionar o sufixo "Status" ao nome vindo do banco
        var typeName = $"{typeof(IOrderState).Namespace}.{status}State";

        // Obter o tipo correspondente ao status
        var type = Type.GetType(typeName, throwOnError: false);
        if (type == null)
            throw new InvalidOperationException($"Status type '{typeName}' not found.");

        // Verificar se o tipo implementa IOrderStatus
        if (!typeof(IOrderState).IsAssignableFrom(type))
            throw new InvalidOperationException($"Status type '{typeName}' does not implement IOrderStatus.");

        // Criar a instância dinamicamente
        return (IOrderState)Activator.CreateInstance(type)!;
    }
}
