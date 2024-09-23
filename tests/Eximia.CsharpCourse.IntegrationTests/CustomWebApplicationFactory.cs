using Eximia.CsharpCourse.Migrations;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Testcontainers.MsSql;

namespace Eximia.CsharpCourse.IntegrationTests
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {
        private readonly MsSqlContainer _sqlContainer = new MsSqlBuilder()
            .Build();

        private readonly MockacoContainer _mockacoContainer = new MockacoContainer();

        public async Task InitializeAsync()
        {
            await _sqlContainer.StartAsync();
            await _mockacoContainer.InitializeAsync();
            Environment.SetEnvironmentVariable("ConnectionStrings:DefaultConnection", _sqlContainer.GetConnectionString());
            Environment.SetEnvironmentVariable("StockApi:Uri", _mockacoContainer.ContainerUri);

            var dbContext = new MigrationsDbContextFactory().Create(_sqlContainer.GetConnectionString());
            await dbContext.Database.MigrateAsync();
        }

        async Task IAsyncLifetime.DisposeAsync()
        {
            await _mockacoContainer.DisposeAsync();
            await _sqlContainer.DisposeAsync();
        }
    }
}
