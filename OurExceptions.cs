using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer
{
    public class OurExceptions : Exception
    {
        public OurExceptions()
        {

        }

        public OurExceptions(string message) :
            base (message)
        {

        }

        public OurExceptions(string message, Exception inner) :
            base(message, inner)
        {

        }

        private string Message ()
        {
            return "Felmeddelande";
        }

        public virtual string IsInputEmpty(OurExceptions ex, string input)
        {
            return Message();
        }
    }
}
