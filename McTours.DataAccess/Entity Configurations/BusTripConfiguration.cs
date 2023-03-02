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
    internal class BusTripConfiguration : IEntityTypeConfiguration<BusTrip>
    {
        private static readonly DateTime registrationVehicle1 = DateTime.ParseExact("2023-02-18 10:00:00", "yyyy-MM-dd HH:mm:ss", null);
        private static readonly DateTime registrationVehicle2 = DateTime.ParseExact("2022-12-31 12:30:00", "yyyy-MM-dd HH:mm:ss", null);
        private static readonly DateTime registrationVehicle3 = DateTime.ParseExact("2023-03-01 17:45:00", "yyyy-MM-dd HH:mm:ss", null);
        public void Configure(EntityTypeBuilder<BusTrip> builder)
        {
            builder.ToTable(nameof(BusTrip));
            builder.HasKey(b => b.Id);
            //Ignore -> Göz ardı et, DB'de oluşturma
            builder.Ignore(trip => trip.HasBreakTime);
            builder.Property(b => b.TicketPrice).IsRequired().HasColumnType("money");
            builder.Property(b => b.EstimatedDuration).IsRequired();
            builder.Property(b => b.Date).IsRequired().HasColumnType("datetime2(0)");
            builder.HasOne(b => b.Vehicle).WithMany(v => v.BusTrips).HasForeignKey(b => b.VehicleId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(b => b.DepartureCity).WithMany(c => c.DepartureBusTrips).HasForeignKey(b => b.DepartureCityId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(b => b.ArrivalCity).WithMany(c => c.ArrivalBusTrips).HasForeignKey(b => b.ArrivalCityId).OnDelete(DeleteBehavior.NoAction);
            builder.HasData(
                new BusTrip() { Id = 1, Date = registrationVehicle1, DepartureCityId = 3, ArrivalCityId = 2, TicketPrice = 250, VehicleId = 2, EstimatedDuration = 240, BreakTimeDuration = 15 },
                new BusTrip() { Id = 2, Date = registrationVehicle2, DepartureCityId = 4, ArrivalCityId = 7, TicketPrice = 350, VehicleId = 4, EstimatedDuration = 420, BreakTimeDuration = 20 },
                new BusTrip() { Id = 3, Date = registrationVehicle3, DepartureCityId = 1, ArrivalCityId = 6, TicketPrice = 550, VehicleId = 3, EstimatedDuration = 360, BreakTimeDuration = 25 }
                );
            //Dikkat !! herşeyde autoinclude kullanılmaz ! Tekil navigation propertylerde sıkıntı yapmaz gibi ama koleksiyon olan navigationlarda sıkıntı yapar !
            //Şöyle bir mantık olması gerek 1 seyahate giden 1 araç vardır. 1 seyahat 1 şehre gider ve 1 şehirden kalkar aynı şekilde 1 modelin 1 markası vardır.
            builder.Navigation(b => b.Vehicle).AutoInclude();
            builder.Navigation(b => b.DepartureCity).AutoInclude();
            builder.Navigation(b => b.ArrivalCity).AutoInclude();
        }
    }
}
