using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountingService.ModelConfiguration
{

    public class AccountingCustomerConfig : IEntityTypeConfiguration<AccountingCCustomer>
    {
        public void Configure(EntityTypeBuilder<AccountingCCustomer> builder)
        {
            builder.Property(c => c.Id).ValueGeneratedNever();
            builder.Property(c => c.FirstName).HasMaxLength(200);
            builder.Property(c => c.LastName).HasMaxLength(200);
            builder.Property(c => c.CreateDateTime).HasDefaultValueSql("GETDATE()");

        }
    }
}