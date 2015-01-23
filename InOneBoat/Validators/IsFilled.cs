using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InOneBoat.Validators
{
    class IsFilled : IValidate
    {
        private string text = "";
        public bool check(string str)
        {
            text = "";
            if (str != "") return true;
            else {
                text = "Пустое поле";
                return false; } 
        }

        public string getCause()
        {
            return text;
        }
    }
}
