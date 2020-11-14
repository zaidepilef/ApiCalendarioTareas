using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCalendarioTareas.Models
{
    public class CalendarioTareasProgramadasModel
    {

        public string nombreAplicativo { get; set; }
        public int codPeriodicidadProceso { get; set; }
        public List<string> semanas { get; set; } //Cambios
        public List<string> meses { get; set; }
        public List<string> dias { get; set; }
        public List<string> fechasEspecificas { get; set; }
        public string hora { get; set; }
        public int intervalo { get; set; }


        //public int idCalendario { get; set; }
        //public int idTareaProgramada { get; set; }

        //public string nombrePeriodicidad { get; set; }

        //public string url { get; set; }
        // public int codActivo { get; set; }
        //public string usuarioCracion { get; set; }
        //public DateTime fechaCreacion { get; set; }
        public string message { get; set; }


        public CalendarioTareasProgramadasModel()
        {
            //idCalendario = 0;
            //idTareaProgramada = 0;
            codPeriodicidadProceso = 0;
            //nombrePeriodicidad = string.Empty;
            nombreAplicativo = string.Empty;
            semanas = new List<string>(); //Cambios
            meses = new List<string>();
            dias = new List<string>();
            fechasEspecificas = new List<string>();
            hora = string.Empty;
            intervalo = 0;
            //url = string.Empty;
            //usuarioCracion = string.Empty;
            message = string.Empty;
        }

    }
}