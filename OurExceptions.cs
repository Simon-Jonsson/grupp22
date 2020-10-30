using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace grupp22_projekt
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

        //Virtual-metod som vi skriver över i våra valideringsklasser
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

        //Virtual-metod som vi skriver över i våra valideringsklasser
        public virtual bool isItemNull(ListBox listBox)
        {
            if(listBox.SelectedItem == null)
            {
                throw (new OurExceptions("Inget item valt"));
            }

            return false;
        }

        //Kollar ifall man kan ladda URLen för denna RSS feed. Går inte det är det inte en RSS feed och returnerar false, annars true
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
