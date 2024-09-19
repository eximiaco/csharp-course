using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Eximia.CsharpCourse.SeedWork.EFCore.Converters;

public class DateTimeUtcConverter : ValueConverter<DateTime, DateTime>
{
    public DateTimeUtcConverter()
        : base(
            v => v.ToUniversalTime(),
            v => DateTime.SpecifyKind(v, DateTimeKind.Utc))
    { }
}
