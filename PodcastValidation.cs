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

       
        //Kollar ifall podcastnamnet är tomt
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

        //Kollar ifall det är en giltig RSS-URL och throwar fel ifall den inte är giltig
        public bool isNotValidURL(string URL)
        {
            if (ValidationRSS(URL) == false)
            {
                throw (new OurExceptions("Denna URL är inte giltig för RSS"));
            }
            return false;
        }

        //Kollar ifall uppdateringsfrekvens-comboxbox är empty och throwar fel isåfall
        public bool isUppdateringsFrekvensEmpty(ComboBox comboBox)
        {
            if (comboBox.SelectedItem == null)
            {
                throw (new OurExceptions("Vänligen välj en uppdateringsfrekvens"));
            }
            return false;
        }

        //Kollar ifall kategori-comboxbox är empty och throwar fel isåfall
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