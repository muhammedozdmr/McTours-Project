using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McTours.Business
{
    //internal sadece bu proje içinde bussiness içinde erişiliyor.
    internal class ValidationResult
    {
        private List<string> _errors = new List<string>();
        public bool HasErrors => _errors.Any();
        public IEnumerable<string> Errors => _errors; //readonly olmuş oldu direkt return ediyor.
        //{
        //    get
        //    {
        //        return _errors;
        //    }
        //}
        public string ErrorString
        {
            get
            {
                return string.Join(Environment.NewLine, Errors);
            }
        }
        public void AddError(string error)
        {
            _errors.Add(error);
        }
    }
}
