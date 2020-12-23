using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCalendarioTareas.Models
{
    public class UsuariosModel
    {
        public string codUsuario { get; set; }

        public string nombreCompleto { get; set; }

        public string message { get; set; }

        public UsuariosModel()
        {
            codUsuario = string.Empty;
            nombreCompleto = string.Empty;
            message = string.Empty;
        }

    }
}
