

namespace Epicor.Infraestructure.Utils
{
    public static class QueueQueryString
    {

        public static string TOTAL_QUEUE = @"SELECT COUNT(*) AS [Total]
                                            FROM SupportCall  
                                            WHERE YEAR(OpenDate) >= 2020";
    }
}
