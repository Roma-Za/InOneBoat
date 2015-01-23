using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace InOneBoat.Validators
{
    class EmailValid : IValidate
    {
        private string text = "";
        public bool check(string str)
        {
            text = "";
            Regex rgx = new Regex(@"^([a-z0-9_-]+\.)*[a-z0-9_-]+@[a-z0-9_-]+(\.[a-z0-9_-]+)*\.[a-z]{2,6}$");
            if (rgx.IsMatch(str))
            {
                return true;
            }
            else
            {
                text = "Неверный формат ел. почты ";
                return false;
            }
        }

        public string getCause()
        {
            return text;
        }
    }
}
