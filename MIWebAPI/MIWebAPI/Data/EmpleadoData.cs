
using MIWebAPI.Models;
using System.Data;
using System.Data.SqlClient;

namespace MiWebAPI.Data
{
    public class EmpleadoData
    {

        private readonly string conexion;

        public EmpleadoData(IConfiguration configuration)
        {
            conexion = configuration.GetConnectionString("CadenaSQL")!;
        }

        public async Task<List<Empleado>> Lista()
        {
            List<Empleado> lista = new List<Empleado>();

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_listaEmpleados", con);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        lista.Add(new Empleado
                        {
                            IdEmpleado = Convert.ToInt32(reader["IdEmpleado"]),
                            NombreDescripcion = reader["NombreDescripcion"].ToString(),
                            Direccion = reader["Direccion"].ToString(),
                            Identificacion = reader["Identificacion"].ToString(),
                            Moneda = reader["Moneda"].ToString(),
                            FechaCreacion = reader["FechaCreacion"].ToString()
                        });
                    }
                }
            }
            return lista;
        }

        public async Task<Empleado> Obtener(int Id)
        {
            Empleado objeto = new Empleado();

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_obtenerEmpleado", con);
                cmd.Parameters.AddWithValue("@IdEmpleado", Id);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        objeto = new Empleado
                        {
                            IdEmpleado = Convert.ToInt32(reader["IdEmpleado"]),
                            NombreDescripcion = reader["NombreDescripcion"].ToString(),
                            Direccion = reader["Direccion"].ToString(),
                            Identificacion = reader["Identificacion"].ToString(),
                            Moneda = reader["Moneda"].ToString(),
                            FechaCreacion = reader["FechaCreacion"].ToString()
                        };
                    }
                }
            }
            return objeto;
        }

        public async Task<bool> Crear(Empleado objeto)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(conexion))
            {

                SqlCommand cmd = new SqlCommand("sp_crearEmpleado", con);
                cmd.Parameters.AddWithValue("@NombreDescripcion", objeto.NombreDescripcion);
                cmd.Parameters.AddWithValue("@Direccion", objeto.Direccion);
                cmd.Parameters.AddWithValue("@Moneda", objeto.Moneda);
                cmd.Parameters.AddWithValue("@FechaCreacion", objeto.FechaCreacion);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    await con.OpenAsync();
                    respuesta = await cmd.ExecuteNonQueryAsync() > 0 ? true : false;
                }
                catch
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }

        public async Task<bool> Editar(Empleado objeto)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(conexion))
            {

                SqlCommand cmd = new SqlCommand("sp_editarEmpleado", con);
                cmd.Parameters.AddWithValue("@IdEmpleado", objeto.IdEmpleado);
                cmd.Parameters.AddWithValue("@NombreDescripcion", objeto.NombreDescripcion);
                cmd.Parameters.AddWithValue("@Direccion", objeto.Direccion);
                cmd.Parameters.AddWithValue("@Moneda", objeto.Moneda);
                cmd.Parameters.AddWithValue("@FechaCreacion", objeto.FechaCreacion);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    await con.OpenAsync();
                    respuesta = await cmd.ExecuteNonQueryAsync() > 0 ? true : false;
                }
                catch
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }

        public async Task<bool> Eliminar(int id)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(conexion))
            {

                SqlCommand cmd = new SqlCommand("sp_eliminarEmpleado", con);
                cmd.Parameters.AddWithValue("@IdEmpleado", id);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    await con.OpenAsync();
                    respuesta = await cmd.ExecuteNonQueryAsync() > 0 ? true : false;
                }
                catch
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }
    }
} 
