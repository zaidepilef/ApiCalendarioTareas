using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCalendarioTareas.Models
{
    public class AplicativosModel
    {

        public int idTareaProgramada { get; set; }

        public string nombre { get; set; }

        public string message { get; set; }

        public AplicativosModel()
        {
            idTareaProgramada = 0;
            nombre = string.Empty;
            message = string.Empty;
        }

    }
}
