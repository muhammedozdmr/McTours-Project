using McTours.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McTours.Business.Services
{
    //Amaç create-update gibi sayfalarda comboboxları bizim istediğimiz gibi türkçe getirmek için fakat arka planda yine enum değerleriyle tutacak
    public class FuelTypeService
    {
        //Yöntem 3 FuelTypeHelper
        //Yöntem 2
        private readonly IEnumerable<ValueNameObject> _fuelTypeCollection = new List<ValueNameObject>()
        {
            new ValueNameObject(){Value = (int)FuelType.Diesel, Name="Dizel"},
            new ValueNameObject(){Value = (int)FuelType.Gasoline, Name="Benzin"},
            new ValueNameObject(){Value = (int)FuelType.Electricity, Name="Elektrik"},
        };
        //Yöntem 1
        //private readonly Dictionary<FuelType, ValueNameObject> _fuelTypes = new Dictionary<FuelType, ValueNameObject>()
        //{
        //    [FuelType.Diesel] = new ValueNameObject()
        //    {
        //        Value = (int)FuelType.Diesel,
        //        Name = "Dizel"
        //    },
        //    [FuelType.Gasoline] = new ValueNameObject()
        //    {
        //        Value = (int)FuelType.Gasoline,
        //        Name = "Benzin"
        //    },
        //    [FuelType.Electricity] = new ValueNameObject()
        //    {
        //        Value = (int)FuelType.Electricity,
        //        Name = "Elektrik"
        //    },
        //};
        public ValueNameObject GetByValue(int value)
        {
            return _fuelTypeCollection.FirstOrDefault(item => item.Value == value);
            //var fuelTypeObject = default(ValueNameObject);
            //foreach(var item in _fuelTypes)
            //{
            //    if(item.Key == (FuelType)value)
            //    {
            //        fuelTypeObject = item.Value;
            //        break;
            //    }
            //}
            //return fuelTypeObject;
        }

        public IEnumerable<ValueNameObject> GetAll()
        {
            //return _fuelTypes.Select(item => item.Value).ToList();
            return _fuelTypeCollection;
        }
}
}
