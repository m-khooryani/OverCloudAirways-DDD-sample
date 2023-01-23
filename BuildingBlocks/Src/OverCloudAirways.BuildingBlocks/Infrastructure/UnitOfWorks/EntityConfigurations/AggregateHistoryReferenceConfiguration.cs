using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OverCloudAirways.BuildingBlocks.Domain.Models;

namespace DArch.Samples.AppointmentService.Infrastructure.Configuration.DArchMapping;

internal class AggregateHistoryReferenceConfiguration : IEntityTypeConfiguration<AggregateRootHistoryItem>
{
    public void Configure(EntityTypeBuilder<AggregateRootHistoryItem> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasPartitionKey(x => x.AggregateId);

        builder.HasIndex(x => new { x.AggregateId, x.Version })
            .IsUnique();

        builder.Property(x => x.Id)
            .ToJsonProperty("id");

        builder.Property(x => x.AggregateId)
            .ToJsonProperty("aggregateId");

        builder.Property(x => x.Version)
            .ToJsonProperty("version");

        builder.Property(x => x.EventType)
            .ToJsonProperty("eventType");

        builder.Property(x => x.AggregateType)
            .ToJsonProperty("aggregateType");

        builder.Property(x => x.Datetime)
            .ToJsonProperty("datetime");

        builder.Property(x => x.Data)
            .ToJsonProperty("data");

        builder.Property(x => x.UserId)
            .ToJsonProperty("userId");

        builder.Property(x => x.Username)
            .ToJsonProperty("username");

        builder.HasNoDiscriminator();

        builder.ToContainer("AggregateRootsHistory");
    }
}
