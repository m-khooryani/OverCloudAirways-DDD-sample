using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OverCloudAirways.BuildingBlocks.Domain.Models;

namespace DArch.Samples.AppointmentService.Infrastructure.Configuration.DArchMapping.OutboxMessages;

internal class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasPartitionKey("partitionKey");
        builder.Property<string>("partitionKey");

        builder.Property(x => x.Id).ToJsonProperty("id").HasConversion(x => x.ToString(), y => Guid.Parse(y));
        builder.Property(x => x.Data).ToJsonProperty("data");
        builder.Property(x => x.SessionId).ToJsonProperty("sessionId");
        builder.Property(x => x.OccurredOn).ToJsonProperty("occurredOn");
        builder.Property(x => x.ProcessedDate).ToJsonProperty("processedDate");
        builder.Property(x => x.Type).ToJsonProperty("type");
        builder.Property(x => x.Error).ToJsonProperty("error");

        builder.HasNoDiscriminator();

        builder.ToContainer("outboxMessages");
    }
}
