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
    internal class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.ToTable(nameof(Ticket));
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Price).IsRequired().HasColumnType("money");
            builder.Property(t => t.SeatNumber).IsRequired().HasColumnType("smallint");
            builder.HasOne(t => t.BusTrip).WithMany(b => b.Tickets).HasForeignKey(t => t.BusTripId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(t => t.Passenger).WithMany(p => p.Tickets).HasForeignKey(t => t.PassengerId).OnDelete(DeleteBehavior.NoAction);
            //İki kolonu unique yaptık çünkü aynı koltuk aynı otobüste 2 defa satılmaması lazım
            builder.HasIndex(ticket => new
            {
                ticket.BusTripId,
                ticket.SeatNumber
            }).IsUnique();

            builder.Navigation(t => t.BusTrip).AutoInclude();

        }
    }
}
