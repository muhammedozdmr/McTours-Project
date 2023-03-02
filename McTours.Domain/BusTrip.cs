using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McTours.Domain
{
    public class BusTrip
    {
        public BusTrip()
        {
            //Encapsulation
            Tickets = new List<Ticket>();
            //HashSet ile 1000 küsür veriden sonra List e göre daha fazla performans sağlarsın
            //List girilen verileri index olarak sıralayıp tutarken HashSet ise hash kodları oluşturur 32 bitlik sayısal veri
            //bu şekilde hash koduna karşılık veriyi tutar.
            //Tickets = new HashSet<Ticket>();
        }
        public int Id { get; set; }
        public string Name
        {
            get
            {
                return string.Concat(DepartureCity.Name, " - ", ArrivalCity.Name, " / ", Date.ToString("dd.MM.yyyy HH:mm"));
            }
        }
        public DateTime Date { get; set; }
        public int VehicleId { get; set; }
        public int DepartureCityId { get; set; }
        public int ArrivalCityId { get; set; }
        public int EstimatedDuration { get; set; }
        public decimal TicketPrice { get; set; }
        //readonly property sadece get metodu var yani
        public bool HasBreakTime => BreakTimeDuration != null;
        //public bool HasBreakTime => BreakTimeDuration.HasValue;
        public int? BreakTimeDuration { get; set; }
        public City DepartureCity { get; set; }
        public City ArrivalCity { get; set; }
        public Vehicle Vehicle { get; set; }
        //Concrete => Katı, somut
        //List, Collection, HashSet
        //Abstract => Soyut
        //IEnumarable, ICollection, IList

        //inteface bir Soyutlama (abstraction) yapısıdır.
        //interface ile bir tipte bulunması gereken davranışlar bildirilir.
        //interface üzerinde hangi üyeler tanımlanabilir => Property, Method
        //Ancak o davranışın nasıl yerine getirildiğini bildirmez.
        //Diğer bir ifadeyle; interface bir davranışı (property, method) implement etmez.

        //Bu özelliğinde doalyı interfacelere SÖZLEŞME (kontrat) da denir.

        //Interface NE olması gerektiğini SÖYLER, NASIL olması gerektiğini SÖYLEMEZ !.
        public ICollection<Ticket> Tickets { get; }
    }
}
