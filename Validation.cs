using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grupp22_projekt
{
    public class Validation
    {
        public bool isInputLetters(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }
            for (int i = 0; i < input.Length; i++)
            {
                if (!char.IsLetter(input[i]))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
