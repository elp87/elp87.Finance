namespace elp87.Finance
{
    public class SysTrade : Trade, ISysTrade
    {
        public double CumProfit { get; set; }

        public double CumProfitPC { get; set; }

        public double ContractProfit { get; set; }

        public double ContractProfitPC { get; set; }

        public double DrawDown { get; set; }

        public double DrawDownPC { get; set; }

        public bool NewContract { get; set; }
    }
}
