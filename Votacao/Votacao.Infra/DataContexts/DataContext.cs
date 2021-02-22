using Microsoft.Extensions.Options;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Votacao.Infra.DataContexts
{
    public class DataContext : IDisposable
    {
        public SqlConnection SQLConnection { get; set; }

        public DataContext(IOptions<SettingsInfra> options)
        {
            try
            {
                SQLConnection = new SqlConnection(options.Value.ConnectionString);
                SQLConnection.Open();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void Dispose()
        {
            try
            {
                if (SQLConnection.State != ConnectionState.Closed)
                {
                    SQLConnection.Close();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
