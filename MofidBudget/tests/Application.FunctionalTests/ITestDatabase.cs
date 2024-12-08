using System.Data.Common;

namespace MofidBudget.Application.FunctionalTests;
public interface ITestDatabase
{
    Task InitialiseAsync();

    DbConnection GetConnection();

    Task ResetAsync();

    Task DisposeAsync();
}
