using McTours.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace McTours.DataAccess.Entity_Configurations
{
    internal class VehicleModelConfiguration : IEntityTypeConfiguration<VehicleModel>
    {
        public void Configure(EntityTypeBuilder<VehicleModel> builder)
        {
            builder.ToTable("VehicleModel");
            builder.HasKey(v => v.Id);
            builder.Property(v => v.Name).IsRequired().HasColumnType("nvarchar(50)");
            builder.HasOne(vehicleModel => vehicleModel.VehicleMake).WithMany(vehicleMake => vehicleMake.VehicleModels)
                .HasForeignKey(vehicleModel => vehicleModel.VehicleMakeId).OnDelete(DeleteBehavior.NoAction);

            builder.HasData(
                new VehicleModel() { Id = 1, Name = "Travego", VehicleMakeId = 1 },
                new VehicleModel() { Id = 2, Name = "Tourismo", VehicleMakeId = 1 },
                new VehicleModel() { Id = 3, Name = "Novociti", VehicleMakeId = 5 },
                new VehicleModel() { Id = 4, Name = "Fortuna", VehicleMakeId = 2 },
                new VehicleModel() { Id = 5, Name = "Lions", VehicleMakeId = 2 },
                new VehicleModel() { Id = 6, Name = "Cityliner", VehicleMakeId = 3 },
                new VehicleModel() { Id = 7, Name = "Tourliner", VehicleMakeId = 3 },
                new VehicleModel() { Id = 8, Name = "S Serisi", VehicleMakeId = 4 },
                new VehicleModel() { Id = 9, Name = "Maraton", VehicleMakeId = 6 },
                new VehicleModel() { Id = 10, Name = "Safir", VehicleMakeId = 6 }
                );
            builder.Navigation(vm => vm.VehicleMake).AutoInclude();

        }
    }
}
