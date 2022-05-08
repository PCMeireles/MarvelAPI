using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarvelAPI.Models
{
    public class HeroiPersistent
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string modified { get; set; }

    }

    public class HeroiViewModel
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Descrição { get; set; }
        public string Modificado { get; set; }
    }
}