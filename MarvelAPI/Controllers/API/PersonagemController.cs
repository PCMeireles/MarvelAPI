using MarvelAPI.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Linq;
using MarvelAPI.Servico;
using MarvelAPI.Servico.Interface;
using System.IO;

namespace MarvelAPI.Controllers.API
{
    public class PersonagemController : ApiController
    {
        private readonly IRequestService _requestService;

        public PersonagemController()
        {
            _requestService = new RequestService();
        }

        [HttpGet]
        public HttpResponseMessage Listar(int pagina = 0, bool meusFavoritos = false, string comecacom = "")
        {
            try
            {
                if (meusFavoritos)
                {
                    if (pagina != 0)
                        return Request.CreateResponse(HttpStatusCode.OK, new List<HeroiViewModel>());

                    //consultar na nossa base de dados
                    var path = ((System.Web.HttpContextWrapper)Request.Properties["MS_HttpContext"]).Server.MapPath("~/dataBase.JSON");

                    if (!File.Exists(path))
                        File.Create(path).Close();

                    var dados = JsonConvert.DeserializeObject<List<Favorito>>(File.ReadAllText(path));
                    var herois = dados.Select(c => c.Heroi).Select(c => new HeroiViewModel()
                    {
                        Codigo = c.id,
                        Descrição = c.description,
                        Modificado = c.modified,
                        Nome = c.name
                    }).ToList();

                    if (!string.IsNullOrEmpty(comecacom))
                        herois.RemoveAll(c => !c.Nome.StartsWith(comecacom));

                    return Request.CreateResponse(HttpStatusCode.OK, herois);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, _requestService.ListarHerois(pagina, comecacom).Result);
                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage Detalhar(int id)
        {

            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, _requestService.DetalharHeroi(id).Result);

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }

        }

        [HttpPut]
        public HttpResponseMessage Favoritar(int id)
        {
            try
            {
                var path = ((System.Web.HttpContextWrapper)Request.Properties["MS_HttpContext"]).Server.MapPath("~/dataBase.JSON");

                if (!File.Exists(path))
                    File.Create(path).Close();

                var dados = JsonConvert.DeserializeObject<List<Favorito>>(File.ReadAllText(path));

                if (dados == null)
                    dados = new List<Favorito>();

                if (dados.Count > 5)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Você pode favoritar apenas 5 personagens");

                if(dados.Any(c=>c.Codigo == id))
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Esse personagem já está em sua lista de favoritos");

                dados.Add(new Favorito() { Codigo = id, DataCadastro = DateTime.Now, Heroi = _requestService.DetalharHeroi(id).Result });

                File.WriteAllText(path, JsonConvert.SerializeObject(dados));
                return Request.CreateResponse(HttpStatusCode.OK, "Dados atualizados com sucesso!");
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }

        }

        [HttpDelete]
        public HttpResponseMessage RemoverFavorito(int id)
        {

            var path = ((System.Web.HttpContextWrapper)Request.Properties["MS_HttpContext"]).Server.MapPath("~/dataBase.JSON");

            if (!File.Exists(path))
                File.Create(path).Close();

            var dados = JsonConvert.DeserializeObject<List<Favorito>>(File.ReadAllText(path));

            dados.RemoveAll(c => c.Codigo == id);

            File.WriteAllText(path, JsonConvert.SerializeObject(dados));
            return Request.CreateResponse(HttpStatusCode.OK, "Dados atualizados com sucesso!");
        }
    }
}
