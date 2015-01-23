using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace InOneBoat.Validators
{
    class PassValid : IValidate
    {
        private string text = "";
        public bool check(string str)
        {
            text = "";
            Regex rgx = new Regex(@"(?=^.{8,}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$");
            if (rgx.IsMatch(str))
            {
                return true;
            }
            else
            {
                text = "Неверный формат пароля: Строчные и прописные латинские буквы, цифры, спецсимволы. Минимум 8 символов";
                return false;
            }
        }

        public string getCause()
        {
            return text;
        }
    }
}
