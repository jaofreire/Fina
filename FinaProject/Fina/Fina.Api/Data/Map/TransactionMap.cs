using Fina.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fina.Api.Data.Map
{
    public class TransactionMap : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .IsRequired()
                .HasColumnType("NVARCHAR")
                .HasMaxLength(80);

            builder.Property(x => x.Type)
                .IsRequired()
                .HasColumnType("SMALLINT");

            builder.Property(x => x.Value)
                .IsRequired()
                .HasColumnType("MONEY");

            builder.Property(x => x.CreatedAt)
                .IsRequired()
                .HasColumnType("DATETIME2");

            builder.Property(x => x.UserId)
                .IsRequired()
                .HasColumnType("VARCHAR")
                .HasMaxLength(160);
          
        }
    }
}
