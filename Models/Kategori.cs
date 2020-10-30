using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Kategori
    {
        public string Name { get; set; }

        public Kategori (string _name)
        {
            Name = _name;

        }

        //En tom konstruktor så att man kan serializera/ desarializera
        public Kategori()
        {

        }

    }
}
