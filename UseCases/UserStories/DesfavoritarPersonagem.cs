using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.Interfaces;

namespace UseCases.UserStories
{
    public class DesfavoritarPersonagem
    {
        private readonly IPersonagemFavoritoJsonServices _personagemFavoritoJsonServices;

        public DesfavoritarPersonagem(IPersonagemFavoritoJsonServices personagemServices)
        {
            _personagemFavoritoJsonServices = personagemServices;
        }

        public void Executar(int id)
        {
            _personagemFavoritoJsonServices.Desfavoritar(id);
        }
    }
}
