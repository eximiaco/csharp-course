namespace Eximia.CsharpCourse.SeedWork.EFCore;

public class EximiaCsharpCourseDbContextAccessor : IEFDbContextAccessor<EximiaCsharpCourseDbContext>
{
    private EximiaCsharpCourseDbContext? _context;

    public EximiaCsharpCourseDbContext Get()
        => _context ?? throw new InvalidOperationException("Contexto deve ser registrado!");

    public void Register(EximiaCsharpCourseDbContext context)
    {
        if (_context is not null)
            return;

        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task ClearAsync()
    {
        await DisposeAsync();
    }

    public void Dispose()
    {
        DisposeCore();
        GC.SuppressFinalize(this);
    }

    public async ValueTask DisposeAsync()
    {
        await DisposeAsyncCore().ConfigureAwait(false);
        GC.SuppressFinalize(this);
    }

    private async ValueTask DisposeAsyncCore()
    {
        if (_context is null)
            return;

        await _context.DisposeAsync().ConfigureAwait(false);
        _context = null!;
    }

    private void DisposeCore()
    {
        if (_context is null)
            return;

        _context.Dispose();
        _context = null!;
    }
}
