using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class PersonagemDto
    {
        public int? id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public DateTime? modified { get; set; }
        public ImagemDoPersonagemDto thumbnail { get; set; }

        [NotMapped]
        public bool favorito { get; set; }
    }
}
