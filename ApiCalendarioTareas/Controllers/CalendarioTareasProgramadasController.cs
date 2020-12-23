using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiCalendarioTareas.IService;
using ApiCalendarioTareas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ApiCalendarioTareas.Controllers
{
    [ApiController]
    //[Route("[controller]")]
    [Route("api/[controller]")]
    public class CalendarioTareasProgramadasController : ControllerBase
    {
        private ICalendarioTareasProgramadasService _oCalendarioTareasProgramadasService;

        public CalendarioTareasProgramadasController(ICalendarioTareasProgramadasService oCalendarioTareasProgramadasService)
        {
            _oCalendarioTareasProgramadasService = oCalendarioTareasProgramadasService;
        }


        [HttpGet]
        [Route("[action]")]
        public async Task<IEnumerable<TareasProgramadasModel>> ListarTareasProgramadas()
        {
            return await _oCalendarioTareasProgramadasService.ListarTareasProgramadas();
        }


        [HttpGet]
        [Route("[action]")]
        public async Task<IEnumerable<AplicativosModel>> ListarAplicativos()
        {
            return await _oCalendarioTareasProgramadasService.ListarAplicativos();
        }

        [HttpGet]
        [Route("[action]/{idTareaProgramada}")]
        public async Task<TareasProgramadasModel> BuscarTareaProgramdaById(int idTareaProgramada)
        {
            return await _oCalendarioTareasProgramadasService.BuscarTareaProgramdaById(idTareaProgramada);
        }

        [HttpGet]
        [Route("[action]/{nombreAplicativo}")]
        public async Task<TareasProgramadasModel> BuscarTareaProgramdaByNombre(string nombreAplicativo)
        {
            return await _oCalendarioTareasProgramadasService.BuscarTareaProgramdaByNombre(nombreAplicativo);
        }

       
        [HttpPost]
        public async Task<CalendarioTareasProgramadasModel> Post([FromBody] CalendarioTareasProgramadasModel oCalendario)
        {

            return await _oCalendarioTareasProgramadasService.InsertarCalendarioTareaProgramada(oCalendario);
        }


        [HttpPost]
        [Route("[action]")]
        public async Task<CalendarioTareasProgramadasModel> Editar([FromBody] CalendarioTareasProgramadasModel oCalendario)
        {
            return await _oCalendarioTareasProgramadasService.InsertarCalendarioTareaProgramada(oCalendario);
        }


        [HttpGet]
        [Route("[action]/{idTareaProgramada}")]
        public async Task<CalendarioTareasProgramadasModel> BuscarCalendarioTareaProgramdaByIdTarea(int idTareaProgramada)
        {
            return await _oCalendarioTareasProgramadasService.BuscarCalendarioTareaProgramdaByIdTarea(idTareaProgramada);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IEnumerable<TipoPeriodicidadModel>> ListarTipoPeriodicidad()
        {
            return await _oCalendarioTareasProgramadasService.ListarTipoPeriodicidad();
        }


        [HttpDelete]
        [Route("[action]/{idTareaProgramada}")]
        public async Task<bool> Eliminar(int idTareaProgramada)
        {
            return await _oCalendarioTareasProgramadasService.Eliminar(idTareaProgramada);
        }

        [Route("[action]")]
        public List<UsuariosModel> ListarUsuarios()
        {
            return _oCalendarioTareasProgramadasService.ListarUsuarios();
        }
        List<UsuariosModel> ListarUsuarios();

    }
}
