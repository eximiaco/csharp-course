﻿using Eximia.CsharpCourse.SeedWork.EFCore;

namespace Eximia.CsharpCourse.SeedWork;

public interface IRepository<T> where T : IAggregateRoot
{
    IUnitOfWork UnitOfWork { get; }
}
