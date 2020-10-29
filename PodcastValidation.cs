using Business_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace grupp22_projekt
{
    public class PodcastValidation : OurExceptions
    {

       

        public override bool isInputEmpty(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw (new OurExceptions("Vänligen fyll i ett podcast namn"));
            }
            else
            {
                return false;
            }
        }

        public bool isNotValidURL(string URL)
        {
            if (ValidationRSS(URL) == false)
            {
                throw (new OurExceptions("Denna URL är inte giltig för RSS"));
            }
            return false;
        }

        public bool isUppdateringsFrekvensEmpty(ComboBox comboBox)
        {
            if (comboBox.SelectedItem == null)
            {
                throw (new OurExceptions("Vänligen välj en uppdateringsfrekvens"));
            }
            return false;
        }

        public bool isKategoriEmpty(ComboBox comboBox)
        {
            if (comboBox.SelectedItem == null)
            {
                throw (new OurExceptions("Vänligen välj en kategori"));
            }
            return false;
        }

    }
}