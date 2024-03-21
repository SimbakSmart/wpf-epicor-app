﻿

using Epicor.Core.Models;
using Epicor.Infraestructure.Data;
using Epicor.Infraestructure.Helpers;
using Epicor.Infraestructure.Interfaces;
using Epicor.Infraestructure.Utils;
using System.Data;
using System.Data.Odbc;

namespace Epicor.Infraestructure.Services
{
    public class QueueServices : IServices<Queues>, IDisposable
    {
        private OdbcConnection con = null;

        public async void Dispose()
        {
           if (con != null)  
                await con.CloseAsync();
        }

        public async Task<List<Queues>> GetTotalsAsync(FiltersParams filters = null)
        {
            List<Queues> _list = null;
            string _query = string.Empty;
            try
            {

                if (filters != null)
                {
                    _query = QueuesQueriesStrings.GRAN_TOTAL_TOTAL_CLOSE_AND_TOTAL_OPEN_WITH_FILTERS;
                }
                else
                {
                    _query = QueuesQueriesStrings.GRAN_TOTAL_TOTAL_CLOSE_AND_TOTAL_OPEN;
                }
                

                using (OdbcConnection con = new OdbcConnection(DBContext.GetConnectionString))
                {
                    await con.OpenAsync();
                    using (OdbcCommand com = new OdbcCommand(_query, con))
                    {
                        if (filters != null)
                        {
                            com.CommandType = CommandType.Text;
                            com.Parameters.Add("@StartDate", OdbcType.DateTime).Value = filters.StartDate;
                            com.Parameters.Add("@EndDate", OdbcType.DateTime).Value = filters.EndDate;
                            com.Parameters.Add("@StartDateClosed", OdbcType.DateTime).Value = filters.StartDate;
                            com.Parameters.Add("@EndDateClosed", OdbcType.DateTime).Value = filters.EndDate;
                            com.Parameters.Add("@StartDateOpen", OdbcType.DateTime).Value = filters.StartDate;
                            com.Parameters.Add("@EndDateOpen", OdbcType.DateTime).Value = filters.EndDate;
                        }

                        using (OdbcDataReader reader = com.ExecuteReader())
                        {
                            
                            _list = new List<Queues>();
                            while (reader.Read())
                            {
                                _list.Add(new Queues.QueuesBuilder()
                                                .WithTotal(Convert.ToInt32(reader["Total"]))
                                                .WithTotalOpen(Convert.ToInt32(reader["TotalOpen"]))
                                                .WithTotalClosed(Convert.ToInt32(reader["TotalClosed"]))
                                               .Build()
                                               );
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

        public async Task<List<Queues>> GetTotalsByResponsableAsync(FiltersParams filters = null)
        {
            List<Queues> _list = null;
            string _query = string.Empty;
            try
            {

                if (filters != null)
                {
                    _query = QueuesQueriesStrings.TOTAL_OPEN_BY_QUEUES_RESPONSABLES_WITH_FILTERS;
                }
                else
                {
                    _query = QueuesQueriesStrings.TOTAL_OPEN_BY_QUEUES_RESPONSABLES;
                }

              
                using (OdbcConnection con = new OdbcConnection(DBContext.GetConnectionString))
                {
                    await con.OpenAsync();
                    using (OdbcCommand com = new OdbcCommand(_query, con))
                    {
                        if (filters != null)
                        {
                            com.CommandType = CommandType.Text;
                            com.Parameters.Add("@StartDate", OdbcType.DateTime).Value = filters.StartDate;
                            com.Parameters.Add("@EndDate", OdbcType.DateTime).Value = filters.EndDate;
                        }

                        using (OdbcDataReader reader = com.ExecuteReader())
                        {

                            _list = new List<Queues>();
                            while (reader.Read())
                            {
                                _list.Add(new Queues.QueuesBuilder()
                                                .WithName(reader["Queue"].ToString())
                                                .WithTotal(Convert.ToInt32(reader["Total"]))
                                               .Build()
                                               );
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

        public async Task<List<Queues>> GetTotalsByUrgencyAsync(FiltersParams filters = null)
        {
            List<Queues> _list = null;
            string _query = string.Empty;
            try
            {

                //if (filters != null)
                //{
                //    _query = QueuesQueriesStrings.TOTAL_OPEN_BY_QUEUES_RESPONSABLES_WITH_FILTERS;
                //}
                //else
                //{
                //    _query = QueuesQueriesStrings.TOTAL_OPEN_BY_QUEUES_RESPONSABLES;
                //}

                _query = QueuesQueriesStrings.TOTAL_OPEN_BY_URGENCY;

                using (OdbcConnection con = new OdbcConnection(DBContext.GetConnectionString))
                {
                    await con.OpenAsync();
                    using (OdbcCommand com = new OdbcCommand(_query, con))
                    {
                        //if (filters != null)
                        //{
                        //    com.CommandType = CommandType.Text;
                        //    com.Parameters.Add("@StartDate", OdbcType.DateTime).Value = filters.StartDate;
                        //    com.Parameters.Add("@EndDate", OdbcType.DateTime).Value = filters.EndDate;
                        //}

                        using (OdbcDataReader reader = com.ExecuteReader())
                        {

                            _list = new List<Queues>();
                            while (reader.Read())
                            {
                                _list.Add(new Queues.QueuesBuilder()
                                                .WithUrgency(reader["Urgency"].ToString())
                                                .WithTotal(Convert.ToInt32(reader["Total"]))
                                               .Build()
                                               );
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
    }
}
