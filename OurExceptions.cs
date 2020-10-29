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
            this.Message = "Felmeddelande";
        }

        public OurExceptions(string message, Exception inner) :
            base(message, inner)
        {

        }

        public string Message { get; set; }
 

        private string Message1()
        {
            return "Felmeddelande";
        }

        //public bool NeedsUpdate
        //{
        //    get
        //    {
        //        // Om nästa uppdatering är innan nuvarande klockslag så ska en uppdatering ske
        //        // dvs metoden NeedsUpdate ska returnera true
        //        return NextUpdate <= DateTime.Now;
        //    }
        //}


        public virtual string IsInputEmpty(string input)
        {
            return Message1();
        }
    }
}
