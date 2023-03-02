using McTours.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace McTours.DataAccess.Entity_Configurations
{
    internal class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder.ToTable(nameof(Vehicle));
            builder.HasKey(v => v.Id);
            builder.Property(v => v.PlateNumber).IsRequired().HasColumnType("nvarchar(10)");
            builder.Property(v => v.RegistrationNumber).IsRequired().HasColumnType("nvarchar(8)");
            builder.Property(v => v.RegistrationDate).HasColumnType("date");
            builder.HasOne(vehicleDefinition=>vehicleDefinition.VehicleDefinition).WithMany(vehicle=>vehicle.Vehicles)
                .HasForeignKey(vehicle=>vehicle.VehicleDefinitionId).OnDelete(DeleteBehavior.NoAction);
            builder.HasData(
                new Vehicle() {Id=1,VehicleDefinitionId=1,PlateNumber="34TJ0350",RegistrationDate=new DateTime(2020,10,15),RegistrationNumber="AA658549" },
                new Vehicle() {Id=2,VehicleDefinitionId=2,PlateNumber="34CRN470",RegistrationDate= new DateTime(2021, 11, 25), RegistrationNumber="AB123478" },
                new Vehicle() {Id=3,VehicleDefinitionId=3,PlateNumber="53OZD47",RegistrationDate= new DateTime(2023, 5, 7), RegistrationNumber="CD854621" },
                new Vehicle() {Id=4,VehicleDefinitionId=4,PlateNumber="21ABC737",RegistrationDate= new DateTime(2022, 9, 10), RegistrationNumber="DE854746" },
                new Vehicle() {Id=5,VehicleDefinitionId=5,PlateNumber="47ZHR939",RegistrationDate= new DateTime(2019, 7, 8), RegistrationNumber="EE217634" }
                );
            builder.Navigation(v => v.VehicleDefinition).AutoInclude();
        }
    }
}
