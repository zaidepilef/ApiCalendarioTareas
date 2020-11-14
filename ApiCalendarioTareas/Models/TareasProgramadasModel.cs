using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCalendarioTareas.Models
{
    public class TareasProgramadasModel
    {

        public int idTareaProgramada { get; set; }
        public int codPeriodicidadProceso { get; set; }
        public string nombrePeriodicidad  { get; set; }
        public string nombreAplicativo { get; set; }
        public string url { get; set; }
        public int codActivo { get; set; }
        public string message { get; set; }
        public TareasProgramadasModel()
        {
            idTareaProgramada = 0;
            codPeriodicidadProceso = 0;
            nombrePeriodicidad = string.Empty;
            nombreAplicativo = string.Empty;
            url = string.Empty;
            codActivo = 0;
            message = string.Empty;
        }

    }
}
