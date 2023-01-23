using Xunit;

namespace DArch.Samples.AppointmentService.IntegrationTests._SeedWork;

[CollectionDefinition("Database collection")]
public class DatabaseCollection : ICollectionFixture<TestFixture>
{
}
