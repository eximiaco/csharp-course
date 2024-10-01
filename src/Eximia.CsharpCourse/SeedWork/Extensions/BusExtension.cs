﻿using Microsoft.EntityFrameworkCore;

namespace Eximia.CsharpCourse.SeedWork.Extensions;

public static class BusExtension
{
    internal static async Task DispatchDomainEventsAsync(this IBus bus, DbContext ctx, CancellationToken cancellationToken)
    {
        var domainEntities = ctx.ChangeTracker
            .Entries<IAggregateRoot>()
            .Where(x => x.Entity?.DomainEvents?.Any() == true)
            .ToList();

        if (!domainEntities.Any())
            return;

        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.DomainEvents)
            .ToList();

        foreach (var entity in domainEntities)
            entity.Entity.ClearDomainEvents();

        foreach (var domainEvent in domainEvents)
            await bus.Publish(domainEvent, cancellationToken).ConfigureAwait(false);
    }
}