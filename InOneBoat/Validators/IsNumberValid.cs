using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace InOneBoat.Validators
{
    class IsNumberValid : IValidate
    {
        private string text = "";
        public bool check(string str)
        {
            text = "";
            Regex rgx = new Regex(@"^\d+$");
            if (rgx.IsMatch(str))
            {
                return true;
            }
            else
            {
                text = "Неверное число ";
                return false;
            }
        }

        public string getCause()
        {
            return text;
        }
    }
}
