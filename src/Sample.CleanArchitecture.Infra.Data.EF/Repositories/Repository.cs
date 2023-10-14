using Sample.CleanArchitecture.Domain.Entities;
using Sample.CleanArchitecture.Domain.Interfaces;

namespace Sample.CleanArchitecture.Infra.Data.EF.Repositories;

public class Repository<T> : IRepository<T> where T : class, IEntidade
{
    public Repository(AppDbContext context) => Context = context;

    protected AppDbContext Context { get; }

    public async Task SaveChanges()
        => await Context.SaveChangesAsync();

    public async Task<T?> GetById(Guid id, CancellationToken cancellationToken)
        => await Context.Set<T>().FindAsync(id, cancellationToken);

    public async Task Add(T entidade)
        => await ValueTask.FromResult(Context.Set<T>().AddAsync(entidade));

    public async Task Delete(T entidade)
          => await Task.FromResult(Context.Set<T>().Remove(entidade));

    public async Task<IReadOnlyList<T>> GetAll()
     => await Task.FromResult(Context.Set<T>().ToList());

    public async Task Update(T entidade)
      => await Task.FromResult(Context.Set<T>().Update(entidade));
}
