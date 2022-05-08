using MarvelAPI.Models;
using MarvelAPI.Servico.Interface;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MarvelAPI.Servico
{
    public class RequestService : IRequestService
    {
        private readonly string apiKey, privateKey, baseUrl;

        public RequestService()
        {
            apiKey = "025f9275378f7028e08abe9d74804ac0";
            privateKey = "afd4ba72ed371d29c6ef3f5324f6ab6c6ceda6f8";
            baseUrl = "https://gateway.marvel.com:443/";
        }

        public Task<List<HeroiViewModel>> ListarHerois(int pagina, string nomecomecacom = "")
        {
            string contentResponse = "";
            if (nomecomecacom == "")
            {
                contentResponse = this.Chamar($"v1/public/characters?orderBy=name&offset={pagina}");
            }
            else
            {
                contentResponse = this.Chamar($"v1/public/characters?nameStartsWith={nomecomecacom}&orderBy=name&offset={pagina}");
            }

            List<HeroiPersistent> herois = JsonConvert.DeserializeObject<List<HeroiPersistent>>(contentResponse);
            return Task.FromResult(herois.Select(c => new HeroiViewModel() { Codigo = c.id, Nome = c.name, Descrição = c.description, Modificado = c.description }).ToList());
        }

        public Task<HeroiDetalhePersistent> DetalharHeroi(int id)
        {
            var herois = JsonConvert.DeserializeObject<List<HeroiDetalhePersistent>>(this.Chamar($"v1/public/characters/{id}?"));
            return Task.FromResult(herois.First());
        }

        private string Chamar(string action)
        {
            var ts = DateTime.Now.Ticks;
            var TextBytes = System.Text.Encoding.UTF8.GetBytes((ts + privateKey + apiKey));
            var md5Bytes = System.Security.Cryptography.MD5.Create().ComputeHash(TextBytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < md5Bytes.Length; i++)
            {
                sb.Append(md5Bytes[i].ToString("x2"));
            }

            var hash = sb.ToString();

            var client = new RestClient(baseUrl);
            var request = new RestRequest(action + $"&ts={ts}&hash={hash}&apikey={apiKey}", Method.Get);
            var response = client.ExecuteAsync(request).Result;

            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception("Falha ao se comunicar com gateway.marvel");

            var contentResponse = JsonConvert.DeserializeObject<dynamic>(response.Content);

            var dataContent = JsonConvert.SerializeObject(contentResponse.data.results);

            return dataContent;
        }

       
    }
}