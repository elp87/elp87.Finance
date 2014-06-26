using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using Telerik.Charting;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.ChartView;

namespace elp87.Finance.Graphs.Telerik
{
    public class EquityGraph : IGraph
    {
        private TradeSystem _tradeSystem;
        private Grid _grid;
        RadCartesianChart _graph;

        #region Constructors
        private EquityGraph(Grid grid)
        {
            this._grid = grid;
            this._graph = new RadCartesianChart();
            this._graph.HorizontalAxis = new DateTimeCategoricalAxis();

            this._graph.VerticalAxis = new LinearAxis();
        }

        public EquityGraph(Grid grid, TradeSystem tradeSystem)
            : this(grid)
        {
            this._tradeSystem = tradeSystem;
            this._tradeSystem.CalcTradeProperties();
        }

        public EquityGraph(Grid grid, List<ISysTrade> tradeList)
            : this(grid)
        {
            this._tradeSystem = new TradeSystem();
            this._tradeSystem.TradeList = tradeList;
            this._tradeSystem.CalcTradeProperties();
        }
        #endregion

        #region Methods
        public void DrawGraph()
        {
            this.AddEquityLineData();
            this.AddLongTradeLineData();
            this.AddShortTradeLineData();
            this.AddDrawDownLineData();

            ((DateTimeCategoricalAxis)this._graph.HorizontalAxis).MajorTickInterval = 500;
            this._grid.Children.Add(this._graph);
        }

        #region Private
        private void AddEquityLineData()
        {

            DateTime maxDate = this._tradeSystem.TradeList.Max(trade => trade.ExitDateTime);
            DateTime minDate = this._tradeSystem.TradeList.Min(trade => trade.ExitDateTime);
            double tradePeriod = (maxDate - minDate).TotalDays;

            AreaSeries barSeries = new AreaSeries();
            barSeries.Fill = new SolidColorBrush(Colors.Green);
            foreach (ISysTrade trade in this._tradeSystem.TradeList)
            {
                barSeries.DataPoints.Add(new CategoricalDataPoint()
                {
                    Category = trade.ExitDateTime,
                    Label = trade.ExitDateTime,
                    Value = trade.CumProfit
                });
            }

            this._graph.Series.Clear();
            this._graph.Series.Add(barSeries);
        }

        private void AddLongTradeLineData()
        {
            DateTime minDate = this._tradeSystem.TradeList.Min(trade => trade.ExitDateTime);

            LineSeries series = new LineSeries();
            series.Stroke = new SolidColorBrush(Colors.DarkBlue);
            double profit = 0;
            foreach (ISysTrade trade in this._tradeSystem.TradeList)
            {
                if (trade.IsLong)
                {
                    profit += trade.Profit;
                    series.DataPoints.Add(new CategoricalDataPoint()
                    {
                        Category = trade.ExitDateTime,
                        Value = profit
                    });
                }
            }
            this._graph.Series.Add(series);
        }

        private void AddShortTradeLineData()
        {
            DateTime minDate = this._tradeSystem.TradeList.Min(trade => trade.ExitDateTime);

            LineSeries series = new LineSeries();
            series.Stroke = new SolidColorBrush(Colors.Red);
            double profit = 0;
            foreach (ISysTrade trade in this._tradeSystem.TradeList)
            {
                if (!trade.IsLong)
                {
                    profit += trade.Profit;
                    series.DataPoints.Add(new CategoricalDataPoint()
                    {
                        Category = trade.ExitDateTime,
                        Value = profit
                    });
                }
            }
            this._graph.Series.Add(series);
        }

        private void AddDrawDownLineData()
        {
            DateTime minDate = this._tradeSystem.TradeList.Min(trade => trade.ExitDateTime);

            LineSeries series = new LineSeries();
            series.Stroke = new SolidColorBrush(Colors.Red);
            series.StrokeThickness = 3;
            foreach (ISysTrade trade in this._tradeSystem.TradeList)
            {
                series.DataPoints.Add(new CategoricalDataPoint()
                {
                    Category = trade.ExitDateTime,
                    Value = -trade.DrawDown
                });
            }
            this._graph.Series.Add(series);
        }
        #endregion
        #endregion
    }
}
