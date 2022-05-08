using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarvelAPI.Models
{
    public class Favorito
    {
        public int Codigo { get; set; }
        public DateTime DataCadastro { get; set; }
        public HeroiDetalhePersistent Heroi { get; set; }
    }
}