using Npgsql;
using System.Collections;
using System.Data;

namespace PostgreSQL_DotNet_Core.Helper
{
    public class SPDataAccess
    {
        public NpgsqlConnection m_Conn;
        private Hashtable m_Erroobj;

        public SPDataAccess(string connectionString)
        {
            m_Conn = new NpgsqlConnection(connectionString);
            m_Erroobj = new Hashtable();
        }

        public DataSet? GetDataSet(NpgsqlCommand cmd)
        {
            try
            {
                NpgsqlDataAdapter adp = new NpgsqlDataAdapter();
                adp.SelectCommand = cmd;
                cmd.CommandTimeout = 0; // Infinite timeout matching your code
                cmd.Connection = m_Conn;

                DataSet ds = new DataSet();
                adp.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                //ErrorTrackingExtension.SetError(ex);
                return null;
            }
        }

        public bool ExecuteCommand(NpgsqlCommand cmd)
        {
            cmd.Connection = m_Conn;
            cmd.CommandTimeout = 120;
            try
            {
                if (m_Conn.State != ConnectionState.Open)
                {
                    m_Conn.Open();
                }
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                //ErrorTrackingExtension.SetError(ex);
                return false;
            }
            finally
            {
                m_Conn.Close();
            }
        }
    }
}
