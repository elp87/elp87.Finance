using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using elp87.Helpers;

namespace elp87.Finance
{
    public static class TSLabReport
    {
        public static List<ISysTrade> ReadReport(string reportFileName)
        {
            const int _dealTypeColumnIndex = 0;
            const int _numberColumnIndex = 2;
            const int _entryDateColumnIndex = 5;
            const int _entryPriceColumnIndex = 6;
            const int _exitDateColumnIndex = 9;
            const int _exitPriceColumnIndex = 10;

            List<TsLabTrade> CSVFileTrades = new List<TsLabTrade>();
            CSVReader csv = new CSVReader(reportFileName, CSVFileTrades);

            csv.AddColumn("TsLabDealType", _dealTypeColumnIndex);
            csv.AddColumn("Count", _numberColumnIndex);
            csv.AddColumn("TsLabEntryDateTime", _entryDateColumnIndex);
            csv.AddColumn("TsLabEntryPrice", _entryPriceColumnIndex);
            csv.AddColumn("TsLabExitDateTime", _exitDateColumnIndex);
            csv.AddColumn("TsLabExitPrice", _exitPriceColumnIndex);
            CSVFileTrades = (List<TsLabTrade>)csv.finalList;
            CSVFileTrades.RemoveAll(trade => trade.ExitPrice.Equals(0));

            IEnumerable<ISysTrade> itradeEnumerable = (IEnumerable<TsLabTrade>)CSVFileTrades;

            return itradeEnumerable.ToList();
        }
    }
}
