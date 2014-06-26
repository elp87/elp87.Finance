namespace elp87.Finance
{
    public interface ISysTrade : ITrade
    {
        double CumProfit { get; set; }
        double CumProfitPC { get; set; }

        double ContractProfit { get; set; }
        double ContractProfitPC { get; set; }

        double DrawDown { get; set; }
        double DrawDownPC { get; set; }

        bool NewContract { get; set; }
    }
}
