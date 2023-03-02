using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McTours.Domain
{
    public class Passenger
    {
        public Passenger()
        {
            Tickets = new List<Ticket>();
        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdentityNumber { get; set; }
        public Gender Gender { get; set; }
        public string FullName => string.Concat(FirstName, " ", LastName);
        public DateTime DateOfBirth { get; set; }

        public Ticket LastTicket
        {
            get
            {
                return Tickets.OrderByDescending(tic => tic.Date).FirstOrDefault();
            }
        }
        //IEnumarable ile foreach ile gezinebilirim length ine göre ama ekleme çıkartma yapamam
        //ICollection da ise foreach ile count una göre gezinebilir aynı zamanda ekleme çıkartma yapabilirim
        //IList ise index olarak for ile dolaşmamızı sağlar fakat ben bunu service de linq ile vs zaten dolaşabiliyorum.
        //Oyüzden iyi düşünmek gerek o yüzden ICollection ihtiyacımızı giderir burada önemli olan readonly yapmak const içerisinde instance almak
        //Çünkü ben navigationa eget seti açık bırakırsam dış dünyadan null set edilebilir.
        public ICollection<Ticket> Tickets { get; }
    }
}
