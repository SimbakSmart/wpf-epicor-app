




namespace Epicor.Infraestructure.Data
{


    public static  class DBContext
    {
        private static string _connectionString = "Driver={SQL Server};Server=128.17.50.183;Database=EpicorITSMApplication;Uid=Indicador;Pwd=50p0rte2013;";

        public static string ConnectionString { get {  return _connectionString; }  }
    }

    //public  class DBContext
    //{

    //    private static readonly string _strConnecion =
    //      ConfigurationManager.ConnectionStrings["EpicorConnection"].ConnectionString;
    //    private static SqlConnection con = null;
    //    public static string GetConnectionString()
    //    {
    //        return _strConnecion;
    //    }
    //}


}
