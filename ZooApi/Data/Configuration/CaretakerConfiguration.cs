using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zoo.Entities;

namespace ZooApi.Data.Configuration
{
    public class CaretakerConfiguration : IEntityTypeConfiguration<Caretaker>
    {
        public void Configure(EntityTypeBuilder<Caretaker> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Address)
                .WithOne()
                .HasForeignKey<Address>(x => x.Id);
        }
    }
}
