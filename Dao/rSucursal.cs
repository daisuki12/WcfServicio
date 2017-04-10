using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WcfServicio.Entidad;

namespace WcfServicio.Dao
{
    public class rSucursal
    {
        public SqlConnection ConexionSQL()
        {
            string ConnString = ConfigurationManager.ConnectionStrings["Cipsa"].ConnectionString;
            return new SqlConnection(ConnString);
        }

        public List<mSucursal> ListaSucursalesBanco(int idBanco)
        {
            List<mSucursal> objLista = null;
            SqlConnection Conn = ConexionSQL();
            using (Conn)
            {
                try
                {
                    Conn.Open();
                    SqlCommand command = new SqlCommand()
                    {
                        CommandType = CommandType.StoredProcedure,
                        CommandText = "PA_MANT_SUCURSAL",
                        Connection = Conn
                    };
                    command.Parameters.Add("@PINT_ID_BANCO", SqlDbType.Int).Value = idBanco;
                    command.Parameters.Add("@PVCH_ACCION", SqlDbType.VarChar, 3).Value = "SEB";
                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleResult);

                    if (reader != null)
                    {
                        mSucursal modelo = null;
                        objLista = new List<mSucursal>();
                        int posIdsucursal = reader.GetOrdinal("ID_SUCURSAL");
                        int posNombreSucursal = reader.GetOrdinal("NOM_SUCURSAL");
                        int posDireccion = reader.GetOrdinal("DIRECCION");

                        while (reader.Read())
                        {
                            modelo = new mSucursal();
                            modelo.idSucursal = reader.GetInt32(posIdsucursal);
                            modelo.NombreSucursal = reader.GetString(posNombreSucursal);
                            modelo.Direccion = reader.GetString(posDireccion);
                            objLista.Add(modelo);
                        }
                    }

                    reader.Close();
                    reader.Dispose();
                    command.Dispose();
                }
                catch (SqlException Ex)
                {

                }
                finally
                {
                    if (Conn.State == ConnectionState.Open)
                    {
                        Conn.Close();
                        Conn.Dispose();
                    }
                }
                return objLista;
            }
        }
    }
}