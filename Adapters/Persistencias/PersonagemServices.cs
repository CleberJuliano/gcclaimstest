using Adapters.Helpers;
using Entities.Dtos;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using UseCases.Interfaces;

namespace Adapters.Persistencias
{
    public class PersonagemServices : IPersonagemServices
    {
        private readonly string _uri;
        private readonly string _path;
        private readonly string _publicKey;
        private readonly string _privateKey;
        private readonly int _resultLimit;
        private readonly IPersonagemFavoritoJsonServices _personagemFavoritoJsonServices;

        public PersonagemServices(
            IConfiguration configuration,
            IPersonagemFavoritoJsonServices personagemFavoritoJsonServices)
        {
            _uri = configuration["MarvelApi:uri"];
            _path = configuration["MarvelApi:path"];
            _resultLimit = Convert.ToInt32(configuration["Config:resultLimit"]);
            _publicKey = configuration["Config:publicKey"];
            _privateKey = configuration["Config:privateKey"];
            _personagemFavoritoJsonServices = personagemFavoritoJsonServices;
        }
        public async Task<ResponseDto> BuscarPaginado(int pagina, string parametroDeBusca)
        {
            string ts = DateTime.Now.ToString();
            string hash = Criptografia.GerarMd5(ts, _publicKey, _privateKey);
            using (var client = new HttpClient())
            {
                var rota = $"{_uri}{_path}/characters?ts={ts}&apikey={_publicKey}&hash={hash}&offset={((pagina - 1) * _resultLimit)}&limit={_resultLimit}&orderBy=name";

                if (!string.IsNullOrEmpty(parametroDeBusca))
                    rota = $"{rota}&name={parametroDeBusca}";

                HttpResponseMessage httpResponseMessage = await client.GetAsync(rota);

                if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var content = await httpResponseMessage.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<ResponseDto>(content);

                    var personagensFavoritos = _personagemFavoritoJsonServices.BuscarTodos();
                    result.data.results.ToList().ForEach(x => x.favorito = personagensFavoritos.Any(t => t.Id == x.id));

                    return result;
                }
            }
            return null;
        }

        public async Task<PersonagemDto> BuscarPorId(int id)
        {
            string ts = DateTime.Now.ToString();
            string hash = Criptografia.GerarMd5(ts, _publicKey, _privateKey);
            using (var client = new HttpClient())
            {
                var rota = $"{_uri}{_path}/characters/{id}?ts={ts}&apikey={_publicKey}&hash={hash}";

                HttpResponseMessage httpResponseMessage = await client.GetAsync(rota);

                if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var content = await httpResponseMessage.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<ResponseDto>(content);
                    return result.data.results.FirstOrDefault();
                }
            }
            return null;
        }
    }
}
