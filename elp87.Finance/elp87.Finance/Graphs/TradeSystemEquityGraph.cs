using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using System.Collections.Generic;

namespace elp87.Finance.Graphs
{
    public sealed class TradeSystemEquityGraph : SimpleGraph, IGraph
    {
        public TradeSystemEquityGraph(Grid grid, TradeSystem system)
            : base(grid)
        {
            GraphData mainGraph = new GraphData(
                system.TradeList.Select(trade => new PointData() { Date = trade.ExitDateTime, Value = trade.CumProfit }).ToList(),
                new GraphProperty() { Fill = Brushes.Green, MedianLineStroke = Brushes.Black, HasToolTip = true, Opacity = 0.7 }
                );

            List<ISysTrade> longTrades = system.TradeList.Where(trade => trade.IsLong).ToList();
            List<ISysTrade> longTradesClone = new List<ISysTrade>(longTrades.Select(trade => (ISysTrade)trade.Clone()));
            TradeSystem longSystem = new TradeSystem(longTradesClone);
            longSystem.CalcTradeProperties();
            GraphData longTradesGraph = new GraphData(
                longSystem.TradeList.Select(trade => new PointData() { Date = trade.ExitDateTime, Value = trade.CumProfit }).ToList(),
                new GraphProperty() { Stroke = Brushes.DarkBlue }
                );

            List<ISysTrade> shortTrades = system.TradeList.Where(trade => trade.IsLong == false).ToList();
            List<ISysTrade> shortTradesClone = new List<ISysTrade>(shortTrades.Select(trade => (ISysTrade)trade.Clone()));
            TradeSystem shortSystem = new TradeSystem(shortTradesClone);
            shortSystem.CalcTradeProperties();
            GraphData shortTradeGraph = new GraphData(
                shortSystem.TradeList.Select(trade => new PointData() { Date = trade.ExitDateTime, Value = trade.CumProfit }).ToList(),
                new GraphProperty() { Stroke = Brushes.DarkRed }
                );

            GraphData drawDownGraph = new GraphData(
                system.TradeList.Select(trade => new PointData() { Date = trade.ExitDateTime, Value = -trade.DrawDown }).ToList(),
                new GraphProperty() { Stroke = Brushes.Red, StrokeThickness = 2 }
                );

            this._graphs = new GraphData[] { mainGraph, longTradesGraph, shortTradeGraph, drawDownGraph };
        }
    }
}
