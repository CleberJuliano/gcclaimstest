using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.Interfaces;

namespace UseCases.UserStories
{
    public class DetalharPersonagem
    {
        private readonly IPersonagemServices _personagemServices;

        public DetalharPersonagem(IPersonagemServices personagemServices)
        {
            _personagemServices = personagemServices;
        }

        public async Task<PersonagemDto> Executar(int id)
        {
            return await _personagemServices.BuscarPorId(id);
        }
    }
}
