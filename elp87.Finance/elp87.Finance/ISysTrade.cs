namespace elp87.Finance
{
    public interface ISysTrade : ITrade
    {
        Money CumProfit { get; set; }
        double CumProfitPC { get; set; }

        Money ContractProfit { get; set; }
        double ContractProfitPC { get; set; }

        Money DrawDown { get; set; }
        double DrawDownPC { get; set; }

        bool NewContract { get; set; }
    }
}
