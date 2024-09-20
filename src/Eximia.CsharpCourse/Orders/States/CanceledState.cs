﻿using CSharpFunctionalExtensions;

namespace Eximia.CsharpCourse.Orders.States;

public class CanceledState : IOrderState
{
    public string Name => "Canceled";

    public Result Cancel(Order order)
        => Result.Failure("Pedido já está cancelado.");
}
