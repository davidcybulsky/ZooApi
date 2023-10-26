using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zoo.Entities;

namespace ZooApi.Data.Configuration
{
    public class AnimalConfiguration : IEntityTypeConfiguration<Animal>
    {
        public void Configure(EntityTypeBuilder<Animal> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Caretaker)
                .WithMany(x => x.Animals)
                .HasForeignKey(x => x.CaretakerId);
        }
    }
}
