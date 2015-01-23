using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InOneBoat
{
    class Validator
    {
        public string Text { set; get; }
        private List<IValidate> Validators = new List<IValidate>();
        private string result = "";

        public Validator(string text) {
            Text = text;
        }
        public Validator()
        {
        }
        public void addValidator(IValidate v) {
            Validators.Add(v);
        }
 
        public bool check() {
            foreach (var item in Validators)
            {
                if (!item.check(Text)) {
                    result += item.getCause();
                    return false;
                }
            }
            return true;
        }
        public string getCause() {
            return result;
        }
        public void setText(string text) {
            Text = text;
            result = "";
        }
    }
}
