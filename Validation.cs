using Business_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grupp22_projekt
{
    public class Validation : OurExceptions
    {
        public virtual bool isInputLetters(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (!char.IsLetter(input[i]))
                {
                    return false;
                }
            }
            return true;
        }

        public virtual bool isInputEmpty(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override string IsInputEmpty(string input)
        {
            if (isInputEmpty(input) == true)
            {
                return "Texten är tom";
            }

            return IsInputEmpty(input);
        }
    }
}