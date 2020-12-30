using ApiCalendarioTareas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCalendarioTareas.IService
{
    public interface ICalendarioTareasProgramadasService
    {

        List<UsuariosModel> ListarUsuarios();

        Task<List<AplicativosModel>> ListarAplicativos();

        Task<List<TareasProgramadasModel>> ListarTareasProgramadas();

        Task<TareasProgramadasModel> BuscarTareaProgramdaById(int idTareaProgramda);

        Task<TareasProgramadasModel> BuscarTareaProgramdaByNombre(string nombreAplicativo);

        Task<CalendarioTareasProgramadasModel> BuscarCalendarioTareaProgramdaByIdTarea(int idTareaProgramda);

        Task<TareasProgramadasModel> InsertarTareaProgramada(TareasProgramadasModel tareaProgamada);

        Task<CalendarioTareasProgramadasModel> InsertarCalendarioTareaProgramada(CalendarioTareasProgramadasModel objCalendario);

        Task<List<TipoPeriodicidadModel>> ListarTipoPeriodicidad();

        Task<bool> Eliminar(int idTareaProgramada);

    }
}
