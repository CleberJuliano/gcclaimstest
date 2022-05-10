using Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases.Interfaces
{
    public interface IPersonagemFavoritoJsonServices
    {
        ICollection<PersonagemFavorito> BuscarTodos();
        void Favoritar(int id);
        void Desfavoritar(int id);
    }
}
