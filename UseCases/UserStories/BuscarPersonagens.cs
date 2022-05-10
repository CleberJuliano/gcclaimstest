using Entities.Dtos;
using UseCases.Interfaces;

namespace UseCases.UserStories
{
    public class BuscarPersonagens
    {
        private readonly IPersonagemServices _personagemServices;

        public BuscarPersonagens(IPersonagemServices personagemServices)
        {
            _personagemServices = personagemServices;
        }

        public async Task<ResponseDto> Executar(int pagina, string parametroDeBusca)
        {
            return await _personagemServices.BuscarPaginado(pagina, parametroDeBusca);
        }
    }
}
