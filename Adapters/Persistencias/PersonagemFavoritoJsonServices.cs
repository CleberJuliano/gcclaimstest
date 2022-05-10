using Entities.Model;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.Interfaces;

namespace Adapters.Persistencias
{
    public class PersonagemFavoritoJsonServices : IPersonagemFavoritoJsonServices
    {
        private readonly string path;

        public PersonagemFavoritoJsonServices(IHostingEnvironment hostingEnvironment)
        {
            path = Path.Combine(hostingEnvironment.WebRootPath, "database/PersonagensFavoritos.json");
        }
        public ICollection<PersonagemFavorito> BuscarTodos()
        {
            if(File.Exists(path))
            {
                string content = File.ReadAllText(path);
                var personagensFavoritos = JsonConvert.DeserializeObject<ICollection<PersonagemFavorito>>(content);
                return personagensFavoritos;
            }
            return null;
        }

        public void Desfavoritar(int id)
        {
            var favoritosJsonData = File.ReadAllText(path);
            List<PersonagemFavorito> personagensFavoritos = new List<PersonagemFavorito>();

            personagensFavoritos = JsonConvert.DeserializeObject<List<PersonagemFavorito>>(favoritosJsonData);

            if (personagensFavoritos == null)
            {
                personagensFavoritos = new List<PersonagemFavorito>();
            }

            personagensFavoritos.Remove(personagensFavoritos.FirstOrDefault(x=>x.Id == id));

            System.IO.File.WriteAllText(path, JsonConvert.SerializeObject(personagensFavoritos));
        }

        public void Favoritar(int id)
        {
            var favoritosJsonData = File.ReadAllText(path);
            List<PersonagemFavorito> personagensFavoritos = new List<PersonagemFavorito>();

            personagensFavoritos = JsonConvert.DeserializeObject<List<PersonagemFavorito>>(favoritosJsonData);

            if (personagensFavoritos == null)
            {
                personagensFavoritos = new List<PersonagemFavorito>();
            }

            if (personagensFavoritos.Count() >= 5)
                throw new Exception("É permitido adicionar apenas 5 personagens como favorito.");
            personagensFavoritos.Add(new PersonagemFavorito(id));

            System.IO.File.WriteAllText(path, JsonConvert.SerializeObject(personagensFavoritos));
        }
    }
}
