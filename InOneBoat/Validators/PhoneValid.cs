using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace InOneBoat.Validators
{
    class PhoneValid : IValidate
    {
        private string text = "";
        public bool check(string str)
        {
            text = "";
            Regex rgx = new Regex(@"^((8|\+7)[\- ]?)?(\(?\d{3,4}\)?[\- ]?)?[\d\- ]{5,10}$");
            if (rgx.IsMatch(str))
            {
                return true;
            }
            else
            {
                text = "Неверный формат телефонного номера ";
                return false;
            }
        }

        public string getCause()
        {
            return text;
        }
    }
}