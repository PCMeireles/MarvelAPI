using MarvelAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarvelAPI.Servico.Interface
{
    interface IRequestService
    {
        Task<List<HeroiViewModel>> ListarHerois(int pagina, string nomecomecacom = "");

        Task<HeroiDetalhePersistent> DetalharHeroi(int id);

    }
}
