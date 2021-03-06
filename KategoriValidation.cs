﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace grupp22_projekt
{
    public class KategoriValidation : OurExceptions
    { 

        //Kollar ifall kategorinamnet är tomt
        public override bool isInputEmpty(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw (new OurExceptions("Vänligen fyll i kategorirutan"));
            }
            else
            {
                return false;
            }
        }

        //Kollar så att man har klickat på en kategori som finns i våran lista
        public override bool isItemNull(ListBox listBox)
        {
            if (listBox.SelectedItem == null)
            {
                throw (new OurExceptions("Vänligen välj en kategori som finns i listan"));
            }

            return false;
        }

        //Kollar så att inputen endast består av bokstäver
        public bool isInputLetters(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (!char.IsLetter(input[i]))
                {
                    throw (new OurExceptions("Vänligen använd bokstäver"));
                }
            }
            return false;
        }

    }
}