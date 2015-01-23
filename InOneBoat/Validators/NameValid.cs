using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace InOneBoat.Validators
{
    class NameValid : IValidate
    {
        private string text = "";
        public bool check(string str)
        {
            text = "";
            Regex rgx = new Regex(@"[A-ZА-Я][a-zа-я]+([-'][a-zа-я]+)?");
            if (rgx.IsMatch(str))
            {
                return true;
            }
            else 
            {
                text = "Неверное заполнение ФИО ";
                return false;
            }
        }

        public string getCause()
        {
            return text;
        }
    }
}
