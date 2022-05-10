using Entities.Dtos;

namespace WEB.ViewsModels.PersonagemViewModel
{
    public class ListarPersonagemViewModel
    {
        public ListarPersonagemViewModel(
            int pagina, 
            int totalDeRegistros, 
            int limiteDeRegistros,
            ICollection<PersonagemDto> personagens)
        {
            Pagina = pagina;
            TotalDeRegistros = totalDeRegistros;
            LimiteDeRegistros = limiteDeRegistros;
            Personagens = personagens;
        }

        public int Pagina { get; set; }
        public int TotalDeRegistros { get; set; }
        public int LimiteDeRegistros { get; set; }
        public int TotalDePaginas { get; set; }
        public ICollection<PersonagemDto> Personagens { get; set; } = new List<PersonagemDto>();

        public void CalcularPagina()
        {
            this.TotalDePaginas = (TotalDeRegistros / LimiteDeRegistros);
        }

        
    }
}
