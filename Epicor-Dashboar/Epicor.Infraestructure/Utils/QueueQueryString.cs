

namespace Epicor.Infraestructure.Utils
{
    public static class QueueQueryString
    {

        public static string TOTAL_SUPPORTCALL_OPEN = @"SELECT COUNT(*) AS [Total]
                                            FROM SupportCall  
                                            WHERE YEAR(OpenDate) >= 2020 AND Closed=0";

        public static string TOTAL_OPEN_BY_QUEUE = @"SELECT
                                                CASE
                                                WHEN Q.Name IS NULL THEN 'ASIGNADAS A DESARROLLADOR'
                                                ELSE
                                                REPLACE(Q.Name,'_',' ')
                                                END AS [Queue],
                                                COUNT(*) AS Total
                                                FROM SupportCall AS SC
                                                LEFT JOIN Queue AS Q  ON Q.QueueID = SC.AssignToQueueID
                                                WHERE Closed =0 AND YEAR(OpenDate) >= 2020
                                                GROUP BY  Q.Name ORDER BY TOTAL DESC";
    }
}
