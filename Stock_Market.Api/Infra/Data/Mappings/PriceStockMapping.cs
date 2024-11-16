
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Stock_Market.Api.Domain.Models.Entities;
using Stock_Market.Api.Domain.Models.Entities.Ids;

namespace Stock_Market.Api.Infra.Data.Mappings
{
    public class PriceStockMapping : IEntityTypeConfiguration<PriceStock>
    {
        public void Configure(EntityTypeBuilder<PriceStock> builder)
        {
            builder.ToTable("PricesStock");

            var converter = new ValueConverter<PriceStockId, Guid>(
                    id => id.Value,
                    guidValue => new PriceStockId(guidValue));

            builder.HasKey(e => e.PriceStockId);
            builder.Property(e => e.PriceStockId)
                .HasConversion(converter)
                .ValueGeneratedOnAdd();
            
            var converterSockId = new ValueConverter<StockId, Guid>(
                id => id.Value,
                guidValue => new StockId(guidValue));
            builder.Property(c=> c.StockId).HasConversion(converterSockId);

            builder.Property(c => c.Regular)
                         .IsRequired()
                         .HasColumnType("decimal(10,2)");

            builder.Property(c => c.RegularDayLow)
                .IsRequired()
                .HasColumnType("decimal(10,2)");
            builder.Property(c => c.RegularDayHigh)
                .IsRequired()
                .HasColumnType("decimal(10,2)");
            
            builder.Property(c => c.RegularVolume)
                .IsRequired()
                .HasColumnType("int");
            
            builder.Property(c => c.ReferenceTime)
                .IsRequired()
                .HasColumnType("datetime2");
        }
    }
}
