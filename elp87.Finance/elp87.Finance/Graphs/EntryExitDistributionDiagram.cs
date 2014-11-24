using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace elp87.Finance.Graphs
{
    public abstract class EntryExitDistributionDiagram : Diagram
    {
        protected EntryExitDistributionDiagram(Grid grid) : base(grid) 
        {
            this._fillingType = FillingTypes.MinToMaxGradient;
        }

        protected override string GetBlockTooltipContent(DiagramCategoryData category)
        {
            StringBuilder ttContent = new StringBuilder();

            string titleNotation = category.Title.ToString();
            ttContent.Append(titleNotation + '\n');

            string equityNotation = "П/У - " + Math.Round(category.Value, 2).ToString();
            ttContent.Append(equityNotation + '\n');

            foreach (var attachedData in category.AttachedData)
            {
                ttContent.Append((string)attachedData + '\n');
            }
            return ttContent.ToString();
        }

        protected static DiagramCategoryData GetCategoryForTradeSystem(IEnumerable<ISysTrade> curCategoryTrades, object title)
        {
            Money value = curCategoryTrades.Sum(trade => trade.Profit.Value);
            Money winProfit = curCategoryTrades.Where(trade => trade.Profit > 0).Sum(trade => trade.Profit.Value);
            int winCount = curCategoryTrades.Where(trade => trade.Profit > 0).Count();
            Money loseProfit = curCategoryTrades.Where(trade => trade.Profit <= 0).Sum(trade => trade.Profit.Value);
            int loseCount = curCategoryTrades.Where(trade => trade.Profit <= 0).Count();
            double profitFactor;
            try
            {
                profitFactor = Math.Round(winProfit / loseProfit, 2);
            }
            catch (DivideByZeroException)
            {
                if (winProfit > 0) profitFactor = Double.PositiveInfinity;
                else if (winProfit == 0) profitFactor = Double.NaN;
                else profitFactor = Double.NegativeInfinity;
            }

            string tradesCountNotation = "Кол-во сделок - " + curCategoryTrades.Count().ToString();
            string winNotation = "Прибыльных - " + winCount.ToString() + "(" + winProfit.ToString() + ")";
            string loseNotation = "Убыточных - " + loseCount.ToString() + "(" + loseProfit.ToString() + ")";
            string profitFactorNotation = "Профит-фактор - " + profitFactor.ToString();
            return new DiagramCategoryData()
            {
                Title = title,
                Value = Convert.ToDouble(value.Value),
                AttachedData = new object[] { tradesCountNotation, winNotation, loseNotation, profitFactorNotation }
            };
        }

        public enum CalcTypes
        {
            EntryDate,
            ExitDate
        }
    }
}
