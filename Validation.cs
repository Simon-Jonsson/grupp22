using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grupp22_projekt
{
    public class Validation
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
    }
}