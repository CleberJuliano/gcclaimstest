using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Model
{
    public class PersonagemFavorito
    {
        public int Id { get; set; }

        public PersonagemFavorito(int id)
        {
            Id = id;
        }
    }
}
