using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

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

        public virtual bool isInputEmpty(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw (new OurExceptions("Textrutan är tom"));
            }
            else
            {
                return false;
            }
        }

        public virtual bool isItemNull(ListBox listBox)
        {
            if(listBox.SelectedItem == null)
            {
                throw (new OurExceptions("Inget item valt"));
            }

            return false;
        }

        protected bool ValidationRSS(string URL)
        {
            try
            {
                new XmlDocument().Load(URL);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        



    }
}
