namespace Infrastructure.UnitTests.Mocks;

public class MockService : BaseDataService<MockDbContext>
{
    public MockService(
        IDbContextWrapper<MockDbContext> dbContextWrapper,
        ILogger<MockService> logger)
        : base(dbContextWrapper, logger)
    {
    }

    public async Task RunWithException()
    {
        await ExecuteSafe<bool>(() =>
        {
            throw new Exception();
        });
    }

    public async Task RunWithoutException()
    {
        await ExecuteSafe<bool>(() =>
        {
            return Task.FromResult(true);
        });
    }
}