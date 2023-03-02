using McTours.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McTours.DataAccess.Entity_Configurations
{
    internal class VehicleDefinitionConfiguration : IEntityTypeConfiguration<VehicleDefinition>
    {
        public void Configure(EntityTypeBuilder<VehicleDefinition> builder)
        {
            builder.ToTable(nameof(VehicleDefinition));
            builder.HasKey(v => v.Id);
            builder.Property(v => v.Year).IsRequired().HasColumnType("smallint");
            builder.HasOne(vehicleDefinition => vehicleDefinition.VehicleModel).WithMany(vehicleModel => vehicleModel.VehicleDefinitions)
                .HasForeignKey(vehicleDefinition => vehicleDefinition.VehicleModelId).OnDelete(DeleteBehavior.NoAction);

            builder.HasData(
                new VehicleDefinition() { Id = 1, VehicleModelId = 1, Year = 2023, SeatingType = SeatingType.Vip, LineCount = 10, FuelType = FuelType.Diesel, Utilities = Utility.TV | Utility.Socket},
                new VehicleDefinition() { Id = 2, VehicleModelId = 2, Year = 2021, SeatingType = SeatingType.Single, LineCount = 11, FuelType = FuelType.Electricity, Utilities = Utility.Toilet | Utility.Enternet},
                new VehicleDefinition() { Id = 3, VehicleModelId = 3, Year = 2020, SeatingType = SeatingType.Vip, LineCount = 12, FuelType = FuelType.Diesel, Utilities = Utility.TV },
                new VehicleDefinition() { Id = 4, VehicleModelId = 4, Year = 2019, SeatingType = SeatingType.Normal, LineCount = 10, FuelType = FuelType.Gasoline, Utilities = Utility.Toilet },
                new VehicleDefinition() { Id = 5, VehicleModelId = 5, Year = 2022, SeatingType = SeatingType.Vip, LineCount = 11, FuelType = FuelType.Gasoline, Utilities = Utility.TV | Utility.Enternet },
                new VehicleDefinition() { Id = 6, VehicleModelId = 6, Year = 2021, SeatingType = SeatingType.Single, LineCount = 12, FuelType = FuelType.Electricity, Utilities = Utility.Socket | Utility.Toilet }
                );
            builder.Navigation(vd => vd.VehicleModel).AutoInclude();
        }
    }
}
