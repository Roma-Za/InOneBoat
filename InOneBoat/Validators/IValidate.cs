using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InOneBoat
{
    interface IValidate
    {
        bool check(string str);
        string getCause();
    }
}
