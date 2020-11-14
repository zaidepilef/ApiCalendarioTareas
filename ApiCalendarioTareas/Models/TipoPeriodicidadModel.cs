using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCalendarioTareas.Models
{
    public class TipoPeriodicidadModel
    {
        public int idTipoPeriodicidad { get; set; }
        public string descTipoPeriodicidad { get; set; }
        public string message { get; set; }

        public TipoPeriodicidadModel()
        {
            idTipoPeriodicidad = 0;
            descTipoPeriodicidad = string.Empty;
            message = string.Empty;
        }

    }
}