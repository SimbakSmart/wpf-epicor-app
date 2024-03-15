

using Epicor.Core;
using Epicor.Infraestructure.Data;
using Epicor.Infraestructure.Intefaces;
using Epicor.Infraestructure.Utils;
using System.Data.Odbc;


namespace Epicor.Infraestructure.Services
{
    public class QueueService : IQueueService, IDisposable
    {
        private OdbcConnection con= null;
     
        public async Task<int> TotalSupportCallOpenAsync()
        {
            int _total = 0;
            try
            {
                using (OdbcConnection con = new OdbcConnection(DBContext.ConnectionString))
                {
                    await con.OpenAsync();
                    using (OdbcCommand com = new OdbcCommand(QueueQueryString.TOTAL_SUPPORTCALL_OPEN, con))
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


        public async Task<List<Queues>> TotalActivesOpenByQueueAsync()
        {
            List<Queues> _list = null;
            try
            {
                using (OdbcConnection con = new OdbcConnection(DBContext.ConnectionString))
                {
                    await con.OpenAsync();
                    using (OdbcCommand com = new OdbcCommand(QueueQueryString.TOTAL_OPEN_BY_QUEUE, con))
                    {


                        using (OdbcDataReader reader = com.ExecuteReader())
                        {
                            _list = new List<Queues>();
                            while (reader.Read())
                            {
                                _list.Add(new Queues(reader["Queue"].ToString(), Convert.ToInt32(reader["Total"])));
                            }
                            reader.Close();
                        }

                    }
                }
            }

            catch
            {
                return null;
            }
            return _list;
        }

        public  List<Queues> TotalActivesOpenByQueue()
        {
            List<Queues> _list = null;
            try
            {
                using (OdbcConnection con = new OdbcConnection(DBContext.ConnectionString))
                {
                    con.Open();
                    using (OdbcCommand com = new OdbcCommand(QueueQueryString.TOTAL_OPEN_BY_QUEUE, con))
                    {


                        using (OdbcDataReader reader = com.ExecuteReader())
                        {
                            _list = new List<Queues>();
                            while (reader.Read())
                            {
                                _list.Add(new Queues(reader["Queue"].ToString(), Convert.ToInt32(reader["Total"])));
                            }
                            reader.Close();
                        }

                    }
                }
            }

            catch
            {
                return null;
            }
            return _list;
        }



        public void Dispose()
        {
            if (con != null) con.Close();
        }

    }
}
