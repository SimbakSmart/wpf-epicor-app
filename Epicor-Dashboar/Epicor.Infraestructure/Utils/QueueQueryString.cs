

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

        public static string TOTAL_OPEN_BY_QUEUE_URGENCY = @"SELECT 
                                                    CASE
                                                    WHEN U.Entry IS NULL THEN 'NO DEFINITOS'
                                                    ELSE
                                                     SUBSTRING(U.Entry, 4, LEN(U.Entry)) 
                                                    END
                                                    AS [Urgency],
                                                    COUNT(*) AS [Total]
                                                    FROM SupportCall AS Sc
                                                    LEFT JOIN [EpicorITSMApplication].[dbo].[ValueListEntry] AS U   ON U.ValueListEntryID = Sc.UrgencyID
                                                    WHERE YEAR(Sc.OpenDate) >=2020 AND Sc.Closed=0
                                                    GROUP BY U.Entry  ORDER BY Urgency DESC";

        public static string TOTAL_OPEN_BY_QUEUE_PRIOTIRY = @"SELECT 
                                                            CASE
                                                            WHEN U.Entry IS NULL THEN 'NO DEFINITOS'
                                                            ELSE
                                                             SUBSTRING(U.Entry, 4, LEN(U.Entry)) 
                                                            END
                                                            AS [Priority],
                                                            COUNT(*) AS [Total]
                                                            FROM SupportCall AS Sc
                                                            LEFT JOIN [EpicorITSMApplication].[dbo].[ValueListEntry] AS U   ON U.ValueListEntryID = Sc.PriorityID
                                                            WHERE YEAR(Sc.OpenDate) >=2020 AND Sc.Closed=0
                                                            GROUP BY U.Entry
                                                            ";
    }
}
