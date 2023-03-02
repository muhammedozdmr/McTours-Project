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
    internal class VehicleMakeConfiguration : IEntityTypeConfiguration<VehicleMake>
    {
        public void Configure(EntityTypeBuilder<VehicleMake> builder)
        {
            builder.ToTable(nameof(VehicleMake));
            builder.HasKey(v => v.Id);
            builder.Property(v => v.Name).IsRequired().IsUnicode().HasMaxLength(50);

            builder.HasData(
                new VehicleMake() { Id = 1, Name = "Mercedes" },
                new VehicleMake() { Id = 2, Name = "Man" },
                new VehicleMake() { Id = 3, Name = "Neoplan" },
                new VehicleMake() { Id = 4, Name = "Setra" },
                new VehicleMake() { Id = 5, Name = "Isuzu" },
                new VehicleMake() { Id = 6, Name = "Temsa" }
                );
        }
    } }
