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
    internal class PassengerConfiguration : IEntityTypeConfiguration<Passenger>
    {
        public void Configure(EntityTypeBuilder<Passenger> builder)
        {
            builder.ToTable(nameof(Passenger));
            builder.HasKey(p => p.Id);
            builder.Property(p => p.FirstName).IsRequired().HasColumnType("nvarchar(50)");
            builder.Property(p => p.LastName).IsRequired().HasColumnType("nvarchar(50)");
            //IdentityNumber unique olarak tanımladık burada id ayrı identity ayrı unique
            builder.HasIndex(p => p.IdentityNumber).IsUnique();
            builder.Property(p => p.IdentityNumber).IsRequired().HasColumnType("nvarchar(20)");
            builder.Property(p => p.DateOfBirth).IsRequired().HasColumnType("date");
            builder.Property(p => p.Gender).IsRequired().HasColumnType("smallint");
            builder.HasData(
                new Passenger() {Id=1,FirstName="Matias",LastName="Delgada",IdentityNumber="3648951235",Gender=Gender.Male,DateOfBirth=DateTime.ParseExact("1994-05-30","yyyy-MM-dd",null)},
                new Passenger() {Id=2,FirstName="Rodrigo",LastName="Tello",IdentityNumber="45698535684",Gender=Gender.Female,DateOfBirth=DateTime.ParseExact("1997-05-12","yyyy-MM-dd",null)},
                new Passenger() {Id=3,FirstName="Mehmet",LastName="Kaya",IdentityNumber="15468532651",Gender=Gender.Male,DateOfBirth=DateTime.ParseExact("2022-06-09","yyyy-MM-dd",null)}
                );
            //Burada otomatik include ettim bunu service katmanın da yapmıştım çünkü entity tarafının bana null döndürmemesi için şimdi bu şekildde yapınca
            //servis katmanında yapmama gerek kalmadı direkt entityde yaptığımda her zmaan otomatik include edecek
            //builder.Navigation(pas => pas.LastTicket.BusTrip.ArrivalCity).AutoInclude();
        }
    }
}
