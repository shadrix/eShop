using Infrastructure.Services.Interfaces;

namespace Infrastructure.Services;

public class DbContextWrapper<T> : IDbContextWrapper<T>
    where T : DbContext
{
    private readonly T _dbContext;

    public DbContextWrapper(
        IDbContextFactory<T> dbContextFactory)
    {
        _dbContext = dbContextFactory.CreateDbContext();
    }

    public T DbContext => _dbContext;

    public IDbContextTransaction BeginTransaction()
    {
        return _dbContext.Database.BeginTransaction();
    }
}
