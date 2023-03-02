using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McTours.Domain
{
    public class VehicleDefinition
    {
        public VehicleDefinition()
        {

        }
        public int Id { get; set; }
        public int VehicleModelId { get; set; }
        public short Year { get; set; }
        public SeatingType SeatingType { get; set; }
        public int LineCount { get; set; }
        //Bu tarz readonly propertyleri domain katmanında yapmak mantıklıdır.
        //Hatta şöyleki değişmeyecek davranışları da yani metodları da domainde get edebililirsin. Mesela her seferin iptal durumu vardır.
        //Bu veriyi silmez oluşturulma esnasında bileti iptal etmeye yarar iptal edilen bilet tekrar işleme alınamaz yeni bilet alınması gerek. İşte bu işlem
        //domain katmanında yapılmalı DDD için önemlidir. Domain Driven Design Fakat bunu CRUD ile karıştırma çünkü onlar
        //service katmanında olmak zorunda çünkü veri ekleyip veri siliyorlar.
        public int TotalSeatCount
        {
            get
            {
                int seatCountPerLine;
                switch (SeatingType)
                {
                    case SeatingType.Normal:
                        seatCountPerLine = 3;
                        break;
                    case SeatingType.Vip:
                        seatCountPerLine = 2;
                        break;
                    case SeatingType.Single:
                        seatCountPerLine = 1;
                        break;
                    default:
                        seatCountPerLine = 0;
                        break;
                }
                return seatCountPerLine * LineCount;
            }
        }
        public FuelType FuelType { get; set; }
        public Utility Utilities { get; set; }

        //Navigation Property
        public VehicleModel VehicleModel { get; set; }
        public IEnumerable<Vehicle> Vehicles { get; set; }  
    }
}
