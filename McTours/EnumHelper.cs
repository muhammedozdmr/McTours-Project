using McTours.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McTours
{
    public static class EnumHelper
    {
        public static readonly Dictionary<FuelType, string> FuelTypeNames = new Dictionary<FuelType, string>()
        {
            [FuelType.Diesel] = "Dizel",
            [FuelType.Gasoline] = "Benzin",
            [FuelType.Electricity] = "Elektrik"
        };
        public static IEnumerable<ValueNameObject> FuelTypesGetAll()
        {
            var all = new List<ValueNameObject>();
            foreach (var item in FuelTypeNames)
            {
                all.Add(new ValueNameObject()
                {
                    Value = (int)item.Key,
                    Name = item.Value
                });
            }

            //var all = new List<ValueNameObject>()
            //{
            //    new ValueNameObject()
            //    {
            //        Value=(int)FuelType.Diesel,
            //        Name = FuelTypeNames[FuelType.Diesel]
            //    },
            //      new ValueNameObject()
            //    {
            //        Value=(int)FuelType.Gasoline,
            //        Name = FuelTypeNames[FuelType.Gasoline]
            //    },

            //    new ValueNameObject()
            //    {
            //        Value=(int)FuelType.Electricity,
            //        Name = FuelTypeNames[FuelType.Electricity]
            //    },
            //};
            return all;
        }
        public static readonly Dictionary<SeatingType, string> SeatingTypeNames = new Dictionary<SeatingType, string>()
        {
            [SeatingType.Normal] = "Normal",
            [SeatingType.Single] = "2+1 Tekli Koltuk",
            [SeatingType.Vip] = "Geniş Koltuk"
        };
        public static IEnumerable<ValueNameObject> SeatingTypesGetAll()
        {
            var all = new List<ValueNameObject>();
            foreach (var item in SeatingTypeNames)
            {
                all.Add(new ValueNameObject()
                {
                    Value = (int)item.Key,
                    Name = item.Value
                });
            }
            return all;
        }
        public static readonly Dictionary<Utility, string> UtilitiesTypeNames = new Dictionary<Utility, string>()
        {
            [Utility.Enternet] = "İnternet",
            [Utility.Socket] = "Priz",
            [Utility.Toilet] = "Tuvalet",
            [Utility.TV] = "Televizyon",
            [Utility.USB] = "USB",
        };
        public static IEnumerable<ValueNameObject> UtilitiesTypeGetAll()
        {
            var all = new List<ValueNameObject>();
            foreach (var item in UtilitiesTypeNames)
            {
                all.Add(new ValueNameObject()
                {
                    Value = (int)item.Key,
                    Name = item.Value
                });
            }
            return all;
        }
        public static string GetUtilitiesNames(this Utility utilities)
        {
            var stringBuilder = new StringBuilder();
            foreach (var item in EnumHelper.UtilitiesTypeNames)
            {
                if (utilities.HasFlag(item.Key))
                {
                    if (stringBuilder.Length > 0)
                    {
                        stringBuilder.Append(", ");
                    }
                    stringBuilder.Append(item.Value);
                }
            }
            return stringBuilder.ToString();
        }

        public static readonly Dictionary<Gender, string> GenderNames = new Dictionary<Gender, string>()
        {
            [Gender.Female] = "Kadın",
            [Gender.Male] = "Erkek",
        };

        public static IEnumerable<ValueNameObject> GendersGetAll()
        {
            var all = new List<ValueNameObject>();

            foreach (var item in GenderNames)
            {
                all.Add(new ValueNameObject()
                {
                    Value = (int)item.Key,
                    Name = item.Value
                });
            }
            return all;
        }

        public static string GetGenderName(this Gender gender)
        {
            var stringBuilder = new StringBuilder();

            foreach (var item in GenderNames)
            {
                if (gender.HasFlag(item.Key))
                {
                    if (stringBuilder.Length > 0)
                    {
                        stringBuilder.Append(", ");
                    }

                    stringBuilder.Append(item.Value);
                }
            }

            return stringBuilder.ToString();
        }

    }
}


    //TODO: Hocanın kodlarına bak

