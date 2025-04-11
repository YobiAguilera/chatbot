using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cap_Comunes
{
    public class ccConexionSql
    {
        private int retorno;
        public SqlCommand comando;
        private DataTable tabla;

        private SqlConnection con { get; set; }
        private string cdDatabase { get; set; }
        private string cdServer { get; set; }
        private string cdUser { get; set; }
        private string cdPass { get; set; }
        private string cdPort { get; set; }

        public ccConexionSql()
        {

        }
        public void datosConexion(string storeProcedure)
        {
            if (storeProcedure.Contains("pa_cob_")) datosConexionCob();
            else
            {
                if (storeProcedure.Contains("pa_sat_")) datosConexionSAT();
                else datosConexionBase();
            }
        }
        public void datosConexionBase()
        {
            this.cdDatabase = ConfigurationManager.AppSettings["dataBaseConfig"];

            if (ConfigurationManager.AppSettings["productivo"].ToString().Equals("true"))
            {
                this.cdServer = ConfigurationManager.AppSettings["serverConfig"];
                this.cdUser = ConfigurationManager.AppSettings["userConfig"];
                this.cdPass = ConfigurationManager.AppSettings["passConfig"];
                this.cdPort = ConfigurationManager.AppSettings["portConfig"];
            }
            else
            {
                this.cdServer = ConfigurationManager.AppSettings["serverConfigV"];
                this.cdUser = ConfigurationManager.AppSettings["userConfigV"];
                this.cdPass = ConfigurationManager.AppSettings["passConfigV"];
                this.cdPort = ConfigurationManager.AppSettings["portConfigV"];
            }
        }
        public void datosConexionSAT()
        {
            this.cdDatabase = ConfigurationManager.AppSettings["dataBaseSATConfig"];
            if(ConfigurationManager.AppSettings["productivo"].ToString().Equals("true"))
            {
                this.cdServer = ConfigurationManager.AppSettings["serverConfig"];
                this.cdUser = ConfigurationManager.AppSettings["userConfig"];
                this.cdPass = ConfigurationManager.AppSettings["passConfig"];
                this.cdPort = ConfigurationManager.AppSettings["portConfig"];
            }
            else
            {
                this.cdServer = ConfigurationManager.AppSettings["serverConfigV"];
                this.cdUser = ConfigurationManager.AppSettings["userConfigV"];
                this.cdPass = ConfigurationManager.AppSettings["passConfigV"];
                this.cdPort = ConfigurationManager.AppSettings["portConfigV"];
            }
        }
        public void datosConexionCob()
        {
            this.cdDatabase = ConfigurationManager.AppSettings["dataBaseConfigCob"];
            if (ConfigurationManager.AppSettings["productivo"].ToString().Equals("true"))
            {
                this.cdServer = ConfigurationManager.AppSettings["serverConfig"];
                this.cdUser = ConfigurationManager.AppSettings["userConfig"];
                this.cdPass = ConfigurationManager.AppSettings["passConfig"];
                this.cdPort = ConfigurationManager.AppSettings["portConfig"];
            }
            else
            {
                this.cdServer = ConfigurationManager.AppSettings["serverConfigV"];
                this.cdUser = ConfigurationManager.AppSettings["userConfigV"];
                this.cdPass = ConfigurationManager.AppSettings["passConfigV"];
                this.cdPort = ConfigurationManager.AppSettings["portConfigV"];
            }
        }

        public SqlConnection conectar()
        
        {
            try
            {
                con = new SqlConnection("Data Source=" + cdServer + "," + cdPort + ";Network Library=DBMSSOCN;Initial Catalog=" + cdDatabase + ";User Id = " + cdUser + "; Password = " + cdPass + ";");
                if (con.State == ConnectionState.Closed)
                    con.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("\n" + ex.Message, "No se pudo establecer conexion con la base",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return con;
        }
        public SqlConnection desconectar()
        {
            if (con.State == ConnectionState.Open)
                con.Close();
            return con;
        }
        public void probarConexion(string server, string database, string user, string pass, string port)
        {
            con = new SqlConnection("Data Source=" + server + "," + port + ";Network Library=DBMSSOCN;Initial Catalog=" + database + ";User Id = " + user + "; Password = " + pass + ";");
            if (con.State == ConnectionState.Closed)
                con.Open();
            con.Close();
        }
        public int retornos(string storeProcedure)
        {
            retorno = 0;
            datosConexion(storeProcedure);
            comando.Connection = conectar();
            comando.CommandText = storeProcedure;
            comando.CommandType = CommandType.StoredProcedure;
            SqlParameter retParam = new SqlParameter();
            retParam.SqlDbType = System.Data.SqlDbType.Int;
            retParam.ParameterName = "@Resultado";
            retParam.Direction = System.Data.ParameterDirection.ReturnValue;
            comando.Parameters.Add(retParam);
            comando.ExecuteNonQuery();
            retorno = (int)comando.Parameters["@Resultado"].Value;
            comando.Dispose();
            desconectar();
            return retorno;
        }
        public DataTable consultas(string storeProcedure)
        {
            tabla = new DataTable();
            datosConexion(storeProcedure);
            comando.Connection = conectar();
            comando.CommandText = storeProcedure;
            comando.CommandType = CommandType.StoredProcedure;
            comando.CommandTimeout = 120;
            comando.ExecuteNonQuery();
            using (var da = new SqlDataAdapter(comando))
            {
                comando.CommandType = CommandType.StoredProcedure;
                da.Fill(tabla);
            }
            comando.Dispose();
            desconectar();
            return tabla;
        }
        public int retornosuchasConexiones(string storeProcedure)
        {
            retorno = 0;
            datosConexion(storeProcedure);
            //using (SqlConnection conn = new SqlConnection(conectar().ToString()))
            using (SqlConnection conn = conectar())
            {
                using (SqlCommand cmd = new SqlCommand(storeProcedure, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter retParam = new SqlParameter();
                    retParam.SqlDbType = System.Data.SqlDbType.Int;
                    retParam.ParameterName = "@Resultado";
                    retParam.Direction = System.Data.ParameterDirection.ReturnValue;
                    cmd.Parameters.Add(retParam);
                    conn.Close();
                    conn.Open();
                    using (DbDataReader dr = cmd.ExecuteReader()) // Ejecuta el comando que quieras :)  
                    {
                        retorno = (int)cmd.Parameters["@Resultado"].Value;
                    }
                    conn.Close();
                }
            }
            return retorno;
        }

        DataRow dr;
        public byte[] retornoBytes(string storeProcedure)
        {
            byte[] retorno = new byte[0];
            datosConexion(storeProcedure);
            //using (SqlConnection conn = new SqlConnection(conectar().ToString()))
            using (SqlConnection conn = conectar())
            {
                using (SqlCommand cmd = new SqlCommand(storeProcedure, conn))
                {
                    DataSet ds = new DataSet();
                    datosConexion(storeProcedure);
                    comando.Connection = conectar();
                    comando.CommandText = storeProcedure;
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.CommandTimeout = 120;
                    comando.ExecuteNonQuery();
                    using (var da = new SqlDataAdapter(comando))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        da.Fill(ds,"IMG");
                    }

                    dr = ds.Tables["IMG"].Rows[0];
                    retorno = (byte[])dr["img"];

                    comando.Dispose();
                    desconectar();
                    //return tabla;
                }
            }
            return retorno;
        }
        public DataTable consultasS(string query)
        {
            tabla = new DataTable();
            datosConexionBase();
            SqlCommand comando = new SqlCommand();
            comando.Connection = conectar();
            comando.CommandText = query;
            comando.CommandType = CommandType.Text;
            comando.CommandTimeout = 120;
            comando.ExecuteNonQuery();
            using (var da = new SqlDataAdapter(comando))
            {
                comando.CommandType = CommandType.Text;
                da.Fill(tabla);
            }
            comando.Dispose();
            desconectar();
            return tabla;
        }
    }
}
