using CreditoConsignado.HttpService.Domain.SeedWork;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

public sealed class TagRepository
{
    private readonly PropostasDbContext _context;

    public TagRepository(PropostasDbContext context)
    {
        _context = context;
    }

    public async Task<Maybe<Tag>> Obter(int id, CancellationToken cancellationToken)
    {
        return (await _context.Tags
            .FirstOrDefaultAsync(t => t.Id == id, cancellationToken))!;
    }
}