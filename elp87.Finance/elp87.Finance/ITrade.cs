using System;

namespace elp87.Finance
{
    public interface ITrade
    {
        DateTime EntryDateTime { get; set; }
        DateTime ExitDateTime { get; set; }

        double EntryPrice { get; set; }
        double ExitPrice { get; set; }

        int Count { get; set; }

        double EntryVolume { get; }
        double ExitVolume { get; }

        double Profit { get; }
        double ProfitPC { get; }

        string InstrumentName { get; set; }

        bool IsLong { get; set; }
    }
}
