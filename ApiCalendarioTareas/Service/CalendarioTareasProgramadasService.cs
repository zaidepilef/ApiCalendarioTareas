using ApiCalendarioTareas.IService;
using ApiCalendarioTareas.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCalendarioTareas.Service
{
    public class CalendarioTareasProgramadasService : ICalendarioTareasProgramadasService
    {

        SqlConnection sqlCon = null;
        SqlCommand sqlCom = null;

        AplicativosModel _oAplicativos = new AplicativosModel();
        TareasProgramadasModel _oTareasProgramadas = new TareasProgramadasModel();
        CalendarioTareasProgramadasModel _oCalendariosTareasProgramada = new CalendarioTareasProgramadasModel();

        public CalendarioTareasProgramadasService(IConfiguration configuration)
        {
            sqlCon = new SqlConnection(configuration.GetConnectionString("Cancelaciones"));
        }


        #region TareasProgramadas

        public async Task<List<AplicativosModel>> ListarAplicativos()
        {
            List<AplicativosModel> _lstAplicativos = new List<AplicativosModel>();
            try
            {
                //sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Cancelaciones"].ToString());
                sqlCon.Open();
                sqlCom = new SqlCommand("SP_ListarAplicativos", sqlCon);
                sqlCom.CommandType = CommandType.StoredProcedure;
                sqlCon.Close();

                sqlCon.Open();
                SqlDataReader reader = await sqlCom.ExecuteReaderAsync();

                while (reader.Read()) _lstAplicativos.Add(this.MappingAplicativos(reader));
            }
            catch (Exception ex)
            {
                _lstAplicativos.Clear();
                _lstAplicativos.Add(new AplicativosModel() { message = ex.Message.Split('~')[0] });
            }
            finally
            {
                sqlCom.Dispose();
                sqlCon.Close();
            }
            return _lstAplicativos;
        }
        public async Task<List<TareasProgramadasModel>> ListarTareasProgramadas()
        {
            List<TareasProgramadasModel> _lstTareasProgramadas = new List<TareasProgramadasModel>();
            try
            {
                sqlCon.Open();
                sqlCom = new SqlCommand("SP_ListarTareasProgramadas", sqlCon);
                sqlCom.CommandType = CommandType.StoredProcedure;
                sqlCon.Close();

                sqlCon.Open();
                SqlDataReader reader = await sqlCom.ExecuteReaderAsync();
                while (reader.Read()) _lstTareasProgramadas.Add(this.MappingTareasProgramadas(reader));
            }
            catch (Exception ex)
            {
                _lstTareasProgramadas.Clear();
                _lstTareasProgramadas.Add(new TareasProgramadasModel() { message = ex.Message.Split('~')[0] });
            }
            finally
            {
                sqlCom.Dispose();
                sqlCon.Close();
            }
            return _lstTareasProgramadas;
        }
        public async Task<TareasProgramadasModel> BuscarTareaProgramdaById(int idTareaProgramda)
        {

            try
            {
                sqlCon.Open();
                sqlCom = new SqlCommand("SP_BuscarTareaProgramadaById", sqlCon);
                sqlCom.Parameters.AddWithValue("@IdTareaProgramada", idTareaProgramda);
                sqlCom.CommandType = CommandType.StoredProcedure;
                sqlCon.Close();

                sqlCon.Open();
                SqlDataReader reader = await sqlCom.ExecuteReaderAsync();
                while (reader.Read()) _oTareasProgramadas = this.MappingTareasProgramadas(reader);
            }
            catch (Exception ex)
            {
                _oTareasProgramadas = (new TareasProgramadasModel() { message = ex.Message.Split('~')[0] });
            }
            finally
            {
                sqlCom.Dispose();
                sqlCon.Close();
            }
            return _oTareasProgramadas;
        }
        public async Task<TareasProgramadasModel> BuscarTareaProgramdaByNombre(string nombreAplicativo)
        {

            try
            {
                sqlCon.Open();
                sqlCom = new SqlCommand("SP_BuscarTareaProgramadaByNombre", sqlCon);
                sqlCom.Parameters.AddWithValue("@NombreAplicativo", nombreAplicativo);
                sqlCom.CommandType = CommandType.StoredProcedure;
                sqlCon.Close();

                sqlCon.Open();
                SqlDataReader reader = await sqlCom.ExecuteReaderAsync();
                while (reader.Read()) _oTareasProgramadas = this.MappingTareasProgramadas(reader);
            }
            catch (Exception ex)
            {
                _oTareasProgramadas = (new TareasProgramadasModel() { message = ex.Message.Split('~')[0] });
            }
            finally
            {
                sqlCom.Dispose();
                sqlCon.Close();
            }
            return _oTareasProgramadas;
        }
        public async Task<bool> ActualizarTareaProgramada(TareasProgramadasModel tareaProgamada)
        {

            bool update = false;

            try
            {

                sqlCon.Open();
                sqlCom = new SqlCommand("SP_UpdateTareaProgramada", sqlCon);
                sqlCom.Parameters.AddWithValue("@IdTareaProgramada", tareaProgamada.idTareaProgramada);
                sqlCom.Parameters.AddWithValue("@CodPeriodicidadProceso", tareaProgamada.codPeriodicidadProceso);
                sqlCom.Parameters.AddWithValue("@Nombre", tareaProgamada.nombreAplicativo);
                sqlCom.Parameters.AddWithValue("@Url", tareaProgamada.url);
                sqlCom.Parameters.AddWithValue("@CodActivo", tareaProgamada.codActivo);

                sqlCom.Parameters.Add("@Cantidad", SqlDbType.Int).Direction = ParameterDirection.Output;
                sqlCom.CommandType = CommandType.StoredProcedure;

                await sqlCom.ExecuteNonQueryAsync();

                int cantidad = int.Parse((sqlCom.Parameters["@Cantidad"].Value).ToString());

                if (cantidad == 1)
                {
                    update = true;
                }

            }
            catch (Exception ex)
            {
                update = false;
            }
            finally
            {
                sqlCom.Dispose();
                sqlCon.Close();
            }


            return update;
        }
        public async Task<bool> EliminarTareaProgramadaById(TareasProgramadasModel tareaProgamada)
        {

            bool delete = false;

            try
            {

                sqlCon.Open();
                sqlCom = new SqlCommand("SP_DeleteTareaProgramdaById", sqlCon);
                sqlCom.Parameters.AddWithValue("@IdTareaProgramada", tareaProgamada.idTareaProgramada);
                sqlCom.Parameters.Add("@Cantidad", SqlDbType.Int).Direction = ParameterDirection.Output;
                sqlCom.CommandType = CommandType.StoredProcedure;

                await sqlCom.ExecuteNonQueryAsync();

                int cantidad = int.Parse((sqlCom.Parameters["@Cantidad"].Value).ToString());

                if (cantidad == 1)
                {
                    delete = true;
                }

            }
            catch (Exception ex)
            {
                delete = false;
            }
            finally
            {
                sqlCom.Dispose();
                sqlCon.Close();
            }

            return delete;
        }
        public async Task<TareasProgramadasModel> InsertarTareaProgramada(TareasProgramadasModel tareaProgamada)
        {

            try
            {

                sqlCon.Open();
                sqlCom = new SqlCommand("SP_InsertTareaProgramada", sqlCon);
                //sqlCom.Parameters.AddWithValue("@CodPeriodicidadProceso", tareaProgamada.codPeriodicidadProceso);
                //sqlCom.Parameters.AddWithValue("@Nombre", tareaProgamada.nombreAplicativo);
                //sqlCom.Parameters.AddWithValue("@Url", tareaProgamada.url);
                //sqlCom.Parameters.AddWithValue("@CodActivo", tareaProgamada.codActivo);

                sqlCom.Parameters.AddWithValue("@CodPeriodicidadProceso", 1);
                sqlCom.Parameters.AddWithValue("@Nombre", "Test");
                sqlCom.Parameters.AddWithValue("@Url", "asd");
                sqlCom.Parameters.AddWithValue("@CodActivo", 1);

                sqlCom.Parameters.Add("@IdInsertado", SqlDbType.Int).Direction = ParameterDirection.Output;
                sqlCom.CommandType = CommandType.StoredProcedure;

                await sqlCom.ExecuteNonQueryAsync();

                tareaProgamada.idTareaProgramada = int.Parse((sqlCom.Parameters["@IdInsertado"].Value).ToString());

            }
            catch (Exception ex)
            {
                tareaProgamada.idTareaProgramada = 0;
                tareaProgamada.message = ex.Message.Split('~')[0];
            }
            finally
            {
                sqlCom.Dispose();
                sqlCon.Close();
            }
            tareaProgamada.message = "OK";
            return tareaProgamada;
        }

        #endregion

        #region CalendarioTareasProgramadas


        public async Task<bool> ActualizarCalendarioTareaProgramada(CalendarioTareasProgramadasModel calendarioTareaProgamada)
        {
            bool update = false;

            try
            {
                sqlCon.Open();
                sqlCom = new SqlCommand("SP_UpdateCaledarioTareaProgramda", sqlCon);
                //sqlCom.Parameters.AddWithValue("@IdCalendario", calendarioTareaProgamada.idCalendario);
                //sqlCom.Parameters.AddWithValue("@Mes", calendarioTareaProgamada.mes);
                //sqlCom.Parameters.AddWithValue("@Dia", calendarioTareaProgamada.dia);
                //sqlCom.Parameters.AddWithValue("@Hora", calendarioTareaProgamada.hora);
                //sqlCom.Parameters.AddWithValue("@Intervalo", calendarioTareaProgamada.intervalo);
                //sqlCom.Parameters.AddWithValue("@UsuarioCreacion", calendarioTareaProgamada.usuarioCracion);
                //sqlCom.Parameters.AddWithValue("@CodActivo", calendarioTareaProgamada.codActivo);
                sqlCom.Parameters.Add("@Cantidad", SqlDbType.Int).Direction = ParameterDirection.Output;
                sqlCom.CommandType = CommandType.StoredProcedure;

                await sqlCom.ExecuteNonQueryAsync();

                int cantidad = int.Parse((sqlCom.Parameters["@Cantidad"].Value).ToString());

                if (cantidad == 1)
                {
                    update = true;
                }

            }
            catch (Exception ex)
            {
                update = false;
            }
            finally
            {
                sqlCom.Dispose();
                sqlCon.Close();
            }


            return update;
        }
        

        public async Task<bool> EliminarCalendarioTareaProgramadaById(CalendarioTareasProgramadasModel calendariotareaProgamada)
        {

            bool delete = false;

            try
            {

                sqlCon.Open();
                sqlCom = new SqlCommand("SP_DeleteCalendarioTareaProgramdaById", sqlCon);
                //sqlCom.Parameters.AddWithValue("@IdTareaProgramada", calendariotareaProgamada.idCalendario);
                sqlCom.Parameters.Add("@Cantidad", SqlDbType.Int).Direction = ParameterDirection.Output;
                sqlCom.CommandType = CommandType.StoredProcedure;

                await sqlCom.ExecuteNonQueryAsync();

                int cantidad = int.Parse((sqlCom.Parameters["@Cantidad"].Value).ToString());

                if (cantidad == 1)
                {
                    delete = true;
                }

            }
            catch (Exception ex)
            {
                delete = false;
            }
            finally
            {
                sqlCom.Dispose();
                sqlCon.Close();
            }

            return delete;
        }
        

        public async Task<bool> EliminarCalendarioTareaProgramadaByIdTarea(CalendarioTareasProgramadasModel calendariotareaProgamada)
        {

            bool delete = false;

            try
            {

                sqlCon.Open();
                sqlCom = new SqlCommand("SP_DeleteCalendarioTareaProgramdaByIdTarea", sqlCon);
                //sqlCom.Parameters.AddWithValue("@IdTarea", calendariotareaProgamada.idTareaProgramada);
                sqlCom.Parameters.Add("@Cantidad", SqlDbType.Int).Direction = ParameterDirection.Output;
                sqlCom.CommandType = CommandType.StoredProcedure;

                await sqlCom.ExecuteNonQueryAsync();

                int cantidad = int.Parse((sqlCom.Parameters["@Cantidad"].Value).ToString());

                if (cantidad == 1)
                {
                    delete = true;
                }

            }
            catch (Exception ex)
            {
                delete = false;
            }
            finally
            {
                sqlCom.Dispose();
                sqlCon.Close();
            }

            return delete;
        }
        
        
        public async Task<CalendarioTareasProgramadasModel> InsertarCalendarioTareaProgramada(CalendarioTareasProgramadasModel calendarioTareaProgamada)
        {

            try
            {
                sqlCon.Open();
                sqlCom = new SqlCommand("SP_InsertCalendarioTareaProgramada", sqlCon);
                //sqlCom.Parameters.AddWithValue("@IdTareaProgramada", calendarioTareaProgamada.idTareaProgramada);
                //sqlCom.Parameters.AddWithValue("@Mes", calendarioTareaProgamada.mes);
                //sqlCom.Parameters.AddWithValue("@Dia", calendarioTareaProgamada.dia);
                //sqlCom.Parameters.AddWithValue("@Hora", calendarioTareaProgamada.hora);
                //sqlCom.Parameters.AddWithValue("@Intervalo", calendarioTareaProgamada.intervalo);
                //sqlCom.Parameters.AddWithValue("@UsuarioCreacion", calendarioTareaProgamada.usuarioCracion);
                sqlCom.Parameters.AddWithValue("@IdTareaProgramada", 1);
                sqlCom.Parameters.AddWithValue("@Mes", 1);
                sqlCom.Parameters.AddWithValue("@Dia", 1);
                sqlCom.Parameters.AddWithValue("@Hora", 1);
                sqlCom.Parameters.AddWithValue("@Intervalo", 1);
                sqlCom.Parameters.AddWithValue("@UsuarioCreacion", "Test");

                sqlCom.Parameters.Add("@IdInsertado", SqlDbType.Int).Direction = ParameterDirection.Output;
                sqlCom.CommandType = CommandType.StoredProcedure;

                await sqlCom.ExecuteNonQueryAsync();

                //calendarioTareaProgamada.idCalendario = int.Parse((sqlCom.Parameters["@IdInsertado"].Value).ToString());

            }
            catch (Exception ex)
            {
                //calendarioTareaProgamada.idCalendario = 0;
                calendarioTareaProgamada.message = ex.Message.Split('~')[0];
            }
            finally
            {
                sqlCom.Dispose();
                sqlCon.Close();
            }

            calendarioTareaProgamada.message = "OK";
            return calendarioTareaProgamada;
        }
        

        public async Task<CalendarioTareasProgramadasModel> BuscarCalendarioTareaProgramdaByIdTarea(int idTareaProgramda)
        {
            CalendarioTareasProgramadasModel retorno = new CalendarioTareasProgramadasModel();
            List<CalendarioTareasProgramadasModel> _lstCalendariotareaProgramadas = new List<CalendarioTareasProgramadasModel>();

            try
            {

                sqlCon.Open();
                sqlCom = new SqlCommand("SP_BuscarCalendarioByIdTarea", sqlCon);
                sqlCom.Parameters.AddWithValue("@IdTareaProgramada", idTareaProgramda);
                sqlCom.CommandType = CommandType.StoredProcedure;
                sqlCon.Close();

                sqlCon.Open();
                SqlDataReader reader = await sqlCom.ExecuteReaderAsync();
                while (reader.Read()) _lstCalendariotareaProgramadas.Add(this.MappingCalendarioTareasProgramadas(reader));


                if (idTareaProgramda == 1)
                {
                    retorno.nombreAplicativo = "Cuadratura Diaria";
                    retorno.codPeriodicidadProceso = 1;
                    retorno.semanas.Add("1"); retorno.semanas.Add("3"); retorno.semanas.Add("5"); //Cambios
                    retorno.meses.Add("0");
                    retorno.dias.Add("0");
                    retorno.fechasEspecificas.Add("0");
                    retorno.hora = "19:00";
                    retorno.intervalo = 0;
                }


                if (idTareaProgramda == 2)
                {
                    retorno.nombreAplicativo = "Cuadratura Semanal";
                    retorno.codPeriodicidadProceso = 2;
                    retorno.semanas.Add("0"); //Cambios
                    retorno.meses.Add("0");
                    retorno.dias.Add("5");
                    retorno.fechasEspecificas.Add("0");
                    retorno.hora = "14:00";
                    retorno.intervalo = 0;

                }


                if (idTareaProgramda == 3)
                {
                    retorno.nombreAplicativo = "Cuadratura Mensual";
                    retorno.codPeriodicidadProceso = 3;
                    retorno.semanas.Add("0"); //Cambios
                    retorno.meses.Add("1"); retorno.meses.Add("4"); retorno.meses.Add("8"); retorno.meses.Add("12");
                    retorno.dias.Add("15");
                    retorno.fechasEspecificas.Add("0");
                    retorno.hora = "14:00";
                    retorno.intervalo = 0;
                }

                if (idTareaProgramda == 4)
                {
                    retorno.nombreAplicativo = "Cuadratura Intervalo";
                    retorno.codPeriodicidadProceso = 4;
                    retorno.semanas.Add("0"); //Cambios
                    retorno.meses.Add("0");
                    retorno.dias.Add("0");
                    retorno.fechasEspecificas.Add("0");
                    retorno.hora = "0";
                    retorno.intervalo = 1;
                }

                if (idTareaProgramda == 1002)
                {
                    retorno.nombreAplicativo = "Cuadratura Fecha";
                    retorno.codPeriodicidadProceso = 1002;
                    retorno.semanas.Add("0"); //Cambios
                    retorno.meses.Add("0");
                    retorno.dias.Add("0");
                    retorno.fechasEspecificas.Add("2012-11-12"); retorno.fechasEspecificas.Add("2012-11-13");
                    retorno.hora = "14";
                    retorno.intervalo = 1;
                }


            }
            catch (Exception ex)
            {
                _lstCalendariotareaProgramadas.Clear();
                _oCalendariosTareasProgramada = (new CalendarioTareasProgramadasModel() { message = ex.Message.Split('~')[0] });
            }
            finally
            {
                sqlCom.Dispose();
                sqlCon.Close();
            }
            return retorno;
        }


        public async Task<List<TipoPeriodicidadModel>> ListarTipoPeriodicidad()
        {
            List<TipoPeriodicidadModel> _lstTipoPeriodicidad = new List<TipoPeriodicidadModel>();
            try
            {
                //sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Cancelaciones"].ToString());
                sqlCon.Open();
                sqlCom = new SqlCommand("SP_ListarTipoPeriodicidad", sqlCon);
                sqlCom.CommandType = CommandType.StoredProcedure;
                sqlCon.Close();

                sqlCon.Open();
                SqlDataReader reader = await sqlCom.ExecuteReaderAsync();

                while (reader.Read()) _lstTipoPeriodicidad.Add(this.MappingTipoPeriodicidad(reader));
            }
            catch (Exception ex)
            {
                _lstTipoPeriodicidad.Clear();
                _lstTipoPeriodicidad.Add(new TipoPeriodicidadModel() { message = ex.Message.Split('~')[0] });
            }
            finally
            {
                sqlCom.Dispose();
                sqlCon.Close();
            }
            return _lstTipoPeriodicidad;
        }

        #endregion

        #region Mappings

        private AplicativosModel MappingAplicativos(SqlDataReader reader)
        {
            return new AplicativosModel()
            {
                idTareaProgramada = (int)reader["id_tarea_programada"],
                nombre = reader["nombre"].ToString()
            };
        }
        
        private TareasProgramadasModel MappingTareasProgramadas(SqlDataReader reader)
        {
            return new TareasProgramadasModel()
            {
                idTareaProgramada = (int)reader["id_tarea_programada"],
                codPeriodicidadProceso = (int)reader["cod_periodicidad_proceso"],
                nombrePeriodicidad = reader["nombreperiodicidad"].ToString(),
                nombreAplicativo = reader["nombreaplicativo"].ToString(),
                url = reader["url"].ToString()
            };
        }
        
        private CalendarioTareasProgramadasModel MappingCalendarioTareasProgramadas(SqlDataReader reader)
        {
            return new CalendarioTareasProgramadasModel()
            {
                //idCalendario = (int)reader["id_calendario"],
                //idTareaProgramada = (int)reader["id_tarea_programada"],
                //mes = (int)reader["mes"],
                //dia = (int)reader["dia"],
                //hora = (int)reader["hora"],
                //intervalo = (int)reader["intervalo"],
                //fechaCreacion = (DateTime)reader["fecha_creacion"],
                //usuarioCracion = reader["usuario_creacion"].ToString(),
                //codPeriodicidadProceso = (int)reader["cod_periodicidad_proceso"],
                //nombrePeriodicidad = reader["nombreperiodicidad"].ToString(),
                //nombreAplicativo = reader["nombreaplicativo"].ToString(),
                //url = reader["url"].ToString()
            };
        }

        private TipoPeriodicidadModel MappingTipoPeriodicidad(SqlDataReader reader)
        {
            return new TipoPeriodicidadModel()
            {
                idTipoPeriodicidad = (int)reader["cod_periodicidad_proceso"],
                descTipoPeriodicidad = reader["nombre"].ToString()
            };
        }


        #endregion



    }
}
