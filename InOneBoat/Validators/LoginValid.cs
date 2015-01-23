using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace InOneBoat.Validators
{
    class LoginValid : IValidate
    {
        private string text = "";
        public bool check(string str)
        {
            text = "";
            Regex rgx = new Regex(@"^[a-zA-Z][a-zA-Z0-9-_\.]{1,20}$");
            if (rgx.IsMatch(str))
            {
                return true;
            }
            else
            {
                text = "Неверный формат логина: 2-20 символов, латинские буквы, циыры, первая буква";
                return false;
            }
        }

        public string getCause()
        {
            return text;
        }
    }
}
