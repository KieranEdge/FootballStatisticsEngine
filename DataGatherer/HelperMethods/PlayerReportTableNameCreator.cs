using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGatherer.HelperMethods
{
    public static class PlayerReportTableNameCreator
    {
        public static string TableNameCreator(int startYear, int endYear, string teamName)
        {
            return $"Player_Reports_{teamName}_{startYear}_{endYear}";
        }
    }
}
