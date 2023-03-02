using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McTours.VehicleDefinitions
{
    public class VehicleDefinitionDto
    {
        //Bir sınıfta koleksiyon var ise onun constract metodunda instance ı alınmalı
        //Interfaceleri doğrudan instance edilemez bir interface ancak implement edilmiş bir sınıftan instancelanabilir.
        public VehicleDefinitionDto()
        {
            Utilities = new List<Utility>();
        }
        public int Id { get; set; }
        public int VehicleModelId { get; set; }
        //entityde make ıd yok bunu sadece dtoda gösteiyorum veri değişikliği sağlamak update ekranında kullanmak için
        public int VehicleMakeId { get; set; }
        public short Year { get; set; }
        public SeatingType SeatingType { get; set; }
        public int LineCount { get; set; }
        public FuelType FuelType { get; set; }
        //Utility bir koleksiyon dizi olarak görüyor ve create esnasında seçimlerin tamamını getirsin diye böyle yapıyoruz
        public ICollection<Utility> Utilities { get; set; }
    }
}
