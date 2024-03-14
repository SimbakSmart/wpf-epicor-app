

using Epicor.Infraestructure.Data;
using Epicor.Infraestructure.Intefaces;
using Epicor.Infraestructure.Utils;
using System.Data.Odbc;
using System.Data.SqlClient;

namespace Epicor.Infraestructure.Services
{
    public class QueueService : IQueueService, IDisposable
    {
        //private SqlConnection con= null;
        private OdbcConnection con= null;
        //public void Dispose()
        //{
        //    if(con != null) con.Close();
        //}

        //public async Task<int> GetTotalAsync()
        //{
        //    int _total = 0;  

        //    try
        //    {
        //        using ( con = new SqlConnection(DBContext.GetConnectionString()))
        //        {
        //            await con.OpenAsync();
        //            using (SqlCommand com = new SqlCommand(QueueQueryString.TOTAL_QUEUE, con))
        //            {
        //                _total = (int)await com.ExecuteScalarAsync();
        //            }
        //        }
        //    }
        //    catch {
        //    _total = 0;
        //    }
        //    return _total;// Task.FromResult(_total);
        //}
        

        public async Task<int> GetTotalAsync()
        {
            int _total = 0;
            try
            {
                using (OdbcConnection con = new OdbcConnection(DBContext.ConnectionString))
                {
                    await con.OpenAsync();
                    using (OdbcCommand com = new OdbcCommand(QueueQueryString.TOTAL_QUEUE, con))
                    {
                        _total = (int)await com.ExecuteScalarAsync();
                    }
                }
            }

            catch 
            {
                _total = 0;
            }
            return _total;
        }

        public void Dispose()
        {
            if (con != null) con.Close();
        }
    }
}
