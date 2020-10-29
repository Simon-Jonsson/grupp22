using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Kategori
    {
        public string Namn { get; set; }

        public Kategori (string _namn)
        {
            Namn = _namn;

        }

        public Kategori()
        {

        }

    }
}
