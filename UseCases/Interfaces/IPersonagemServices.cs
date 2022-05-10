using Entities.Dtos;

namespace UseCases.Interfaces
{
    public interface IPersonagemServices
    {
        Task<ResponseDto> BuscarPaginado(int pagina, string parametroDeBusca);
        Task<PersonagemDto> BuscarPorId(int id);
    }
}
