using System;

namespace elp87.Finance
{
    public interface ITrade
    {
        DateTime EntryDateTime { get; set; }
        DateTime ExitDateTime { get; set; }

        Money EntryPrice { get; set; }
        Money ExitPrice { get; set; }

        int Count { get; set; }

        Money EntryVolume { get; }
        Money ExitVolume { get; }

        Money Profit { get; }
        double ProfitPC { get; }

        string InstrumentName { get; set; }

        bool IsLong { get; set; }
    }
}
