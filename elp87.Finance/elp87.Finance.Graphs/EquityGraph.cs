/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Input;

namespace elp87.Finance.Graphs
{
    public class EquityGraph : IGraph
    {
        #region Contants
        const double _sideBlockWidth = 70;
        #endregion

        #region Fields
        private TradeSystem _tradeSystem;
        private Grid _grid;
        private double _actualGridWidth;
        #endregion

        #region Constructors
        private EquityGraph(Grid grid)
        {
            this._grid = grid;
            this._grid.SizeChanged += new SizeChangedEventHandler(grid_SizeChanged);
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
        #region Public
        public void DrawGraph()
        {
            this._grid.Children.Clear();

            DateTime minDate = this._tradeSystem.TradeList.Min(trade => trade.EntryDateTime);
            DateTime maxDate = this._tradeSystem.TradeList.Max(trade => trade.ExitDateTime);
            TimeSpan tRange = maxDate - minDate;
            double timeRange = tRange.TotalHours;
            double maxProfit = this.maxGraphValue();
            if (maxProfit < 0) maxProfit = 0;
            double profitRange = maxProfit - this.minGraphValue();
            double zeroLevel = (maxProfit / profitRange) * this._grid.ActualHeight;
            this._actualGridWidth = this._grid.ActualWidth - _sideBlockWidth;

            double horizontLineHeight = this.getHorizontalCell(profitRange);
            this.drawSideBlock(this._grid, this._actualGridWidth);
            this.drawHorizontLines(horizontLineHeight, this._grid, profitRange, maxProfit, this._actualGridWidth);
            this.drawMedianLine(maxProfit, profitRange, this._grid, this._actualGridWidth);
            this.drawMonthLines(minDate, timeRange, this._grid, this._actualGridWidth);
            this.drawAllTradesEquity(zeroLevel, minDate, timeRange, this._grid, profitRange, maxProfit, this._actualGridWidth);
            this.drawLongTradesEquity(zeroLevel, minDate, timeRange, this._grid, profitRange, maxProfit, this._actualGridWidth);
            this.drawShortTradesEquity(zeroLevel, minDate, timeRange, this._grid, profitRange, maxProfit, this._actualGridWidth);
            this.drawDownEquity(zeroLevel, minDate, timeRange, this._grid, profitRange, maxProfit, this._actualGridWidth);
        }
        #endregion

        #region Private
        private double maxGraphValue()
        {
            double maxValue = 0;
            double maxProfit = this._tradeSystem.TradeList.Max(trade => trade.CumProfit);
            double maxLongValue = this.calcOneTypeProfitList(true).Max();
            double maxShortValue = this.calcOneTypeProfitList(false).Max();
            maxValue = Math.Max(maxLongValue, maxShortValue);
            maxValue = Math.Max(maxValue, maxProfit);
            return maxValue;
        }

        private List<double> calcOneTypeProfitList(bool type)
        {
            List<double> profitList = new List<double>();
            double cumProfit = 0;
            foreach (Trade trade in this._tradeSystem.TradeList)
            {
                if (trade.IsLong == type)
                {
                    cumProfit += trade.Profit;
                    profitList.Add(cumProfit);
                }
            }
            if (profitList.Count == 0) { profitList.Add(0); }
            return profitList;
        }

        private double minGraphValue()
        {
            double minValue = 0;
            double maxDD = -this._tradeSystem.TradeList.Max(trade => trade.DrawDown);
            double minLongValue = this.calcOneTypeProfitList(true).Min();
            double minShortValue = this.calcOneTypeProfitList(false).Min();
            minValue = Math.Min(minLongValue, minShortValue);
            minValue = Math.Min(minValue, maxDD);
            return minValue;
        }

        private double getHorizontalCell(double range)
        {
            double cell = 0.01;
            if (range > 0.1) cell = 0.05;
            if (range > 1) cell = 0.5;
            if (range > 10) cell = 5;
            if (range > 100) cell = 50;
            if (range > 1000) cell = 500;
            if (range > 10000) cell = 5000;
            if (range > 100000) cell = 50000;
            if (range > 1000000) cell = 500000;
            return cell;
        }

        private void drawSideBlock(Grid tabGrid, double gridWidth)
        {
            Rectangle sideBlock = new Rectangle();
            sideBlock.Stroke = Brushes.DarkGray;
            sideBlock.Fill = Brushes.LightGray;
            sideBlock.Width = 70;
            sideBlock.Height = tabGrid.ActualHeight;
            tabGrid.Children.Add(sideBlock);
            sideBlock.Margin = new Thickness(gridWidth, 0, 0, 0);
        }

        private void drawHorizontLines(double lineHeight, Grid tabGrid, double profitRange, double maxProfit, double gridWidth)
        {
            const int _labelTextOffsetPosition = 15;

            double minValue = maxProfit - profitRange;
            double maxGraphValue = maxProfit - (maxProfit % lineHeight);
            double minGraphValue = minValue - (minValue % lineHeight);
            for (double i = minGraphValue; i <= maxGraphValue; i += lineHeight)
            {
                Line horLine = new Line();
                horLine.Stroke = Brushes.LightGray;
                horLine.X1 = 0;
                horLine.X1 = gridWidth;
                horLine.Y1 = ((maxProfit - i) / profitRange) * tabGrid.ActualHeight;
                horLine.Y2 = horLine.Y1;
                tabGrid.Children.Add(horLine);

                Label numLabel = new Label();
                numLabel.Content = i.ToString("0,0", System.Globalization.CultureInfo.CreateSpecificCulture("ru-RU"));
                tabGrid.Children.Add(numLabel);
                numLabel.Margin = new Thickness(gridWidth, horLine.Y1 - _labelTextOffsetPosition, 0, 0);
            }
        }

        private void drawMedianLine(double maxProfit, double profitRange, Grid tabGrid, double gridWidth)
        {
            double min = Math.Min(this._tradeSystem.TradeList.Min(trade => trade.CumProfit), 0);
            double max = Math.Max(this._tradeSystem.TradeList.Max(trade => trade.CumProfit), 0);
            Line medianLine = new Line();
            medianLine.Stroke = Brushes.Black;
            medianLine.X1 = 0;
            medianLine.Y1 = ((maxProfit - min) / profitRange) * tabGrid.ActualHeight;
            medianLine.X2 = gridWidth;
            medianLine.Y2 = ((maxProfit - max) / profitRange) * tabGrid.ActualHeight;
            tabGrid.Children.Add(medianLine);

            Label txtLabel = new Label();
            txtLabel.Content = maxProfit.ToString("0,0.0", System.Globalization.CultureInfo.CreateSpecificCulture("ru-RU"));
            txtLabel.Margin = new Thickness(gridWidth, 0, 0, 0);
            txtLabel.VerticalAlignment = VerticalAlignment.Top;
            txtLabel.HorizontalAlignment = HorizontalAlignment.Left;
            txtLabel.Foreground = Brushes.White;
            txtLabel.Background = Brushes.Black;
            txtLabel.Height = 23;
            tabGrid.Children.Add(txtLabel);
        }

        private void drawMonthLines(DateTime minDate, double timeRange, Grid tabGrid, double gridWidth)
        {
            int vertLineMiss = 0;
            DateTime zeroMinDate = new DateTime(minDate.Year, minDate.Month, minDate.Day, 0, 0, 0);
            int vertLineOffset = ((int)timeRange / 25000) + 1;
            for (int i = 0; i < timeRange; i++)
            {
                TimeSpan vertLineSpan = new TimeSpan(i, 0, 0);
                DateTime curDay = zeroMinDate + vertLineSpan;
                if (curDay.Day == 1 && curDay.Hour == 0)
                {
                    vertLineMiss++;
                    if (vertLineMiss == vertLineOffset)
                    {
                        Line vertLine = new Line();
                        vertLine.Stroke = Brushes.Gray;
                        vertLine.X1 = (i / timeRange) * gridWidth;
                        vertLine.Y1 = 0;
                        vertLine.X2 = vertLine.X1;
                        vertLine.Y2 = tabGrid.ActualHeight;
                        tabGrid.Children.Add(vertLine);
                        vertLineMiss = 0;
                    }
                }
            }
        }

        private void drawAllTradesEquity(double zeroLevel, DateTime minDate, double timeRange, Grid tabGrid, double profitRange, double maxProfit, double gridWidth)
        {
            double x0, y0;

            Polyline equityLine = new Polyline();
            equityLine.Stroke = Brushes.Green;
            equityLine.Fill = Brushes.Green;
            equityLine.Opacity = 0.7;
            equityLine.Points = new PointCollection();
            x0 = 0;
            y0 = zeroLevel;
            equityLine.Points.Add(new Point(x0, y0));
            foreach (ISysTrade curTrade in this._tradeSystem.TradeList)
            {
                double x1, y1;
                TimeSpan tPosition = curTrade.ExitDateTime - minDate;
                double timePosition = tPosition.TotalHours;
                x1 = (timePosition / timeRange) * gridWidth;
                y1 = ((maxProfit - curTrade.CumProfit) / profitRange) * tabGrid.ActualHeight;
                Point equityPoint = new Point(x1, y1);
                equityLine.Points.Add((Point)equityPoint);
            }
            x0 = gridWidth;
            y0 = (maxProfit / profitRange) * tabGrid.ActualHeight;
            equityLine.Points.Add(new Point(x0, y0));
            equityLine.MouseEnter += new MouseEventHandler(equityLine_MouseEnter);
            tabGrid.Children.Add(equityLine);

            Label txtLabel = new Label();
            txtLabel.Content = this._tradeSystem.TradeList.Last().CumProfit.ToString("0,0.0", System.Globalization.CultureInfo.CreateSpecificCulture("ru-RU"));
            txtLabel.Margin = new Thickness(gridWidth, ((maxProfit - this._tradeSystem.TradeList.Last().CumProfit) / profitRange) * tabGrid.ActualHeight, 0, 0);
            txtLabel.VerticalAlignment = VerticalAlignment.Top;
            txtLabel.HorizontalAlignment = HorizontalAlignment.Left;
            txtLabel.Foreground = Brushes.White;
            txtLabel.Background = Brushes.DarkGreen;
            txtLabel.Height = 23;
            tabGrid.Children.Add(txtLabel);
        }

        private void drawLongTradesEquity(double zeroLevel, DateTime minDate, double timeRange, Grid tabGrid, double profitRange, double maxProfit, double gridWidth)
        {
            double x0, y0;
            List<ISysTrade> longTradeList;
            double curProfit = 0;
            Polyline longTradesEquityLine = new Polyline();
            longTradesEquityLine.Stroke = Brushes.Blue;
            longTradesEquityLine.Points = new PointCollection();
            x0 = 0;
            y0 = zeroLevel;
            longTradesEquityLine.Points.Add(new Point(x0, y0));
            longTradeList = this._tradeSystem.TradeList.FindAll(trade => trade.IsLong);
            foreach (Trade curTrade in longTradeList)
            {
                double x1, y1;
                curProfit += curTrade.Profit;
                TimeSpan tPosition = curTrade.ExitDateTime - minDate;
                double timePosition = tPosition.TotalHours;
                x1 = (timePosition / timeRange) * gridWidth;
                y1 = ((maxProfit - curProfit) / profitRange) * tabGrid.ActualHeight;
                Point equityPoint = new Point(x1, y1);
                longTradesEquityLine.Points.Add(equityPoint);
            }
            tabGrid.Children.Add(longTradesEquityLine);

            Label txtLabel = new Label();
            txtLabel.Content = curProfit.ToString("0,0.0", System.Globalization.CultureInfo.CreateSpecificCulture("ru-RU"));
            txtLabel.Margin = new Thickness(gridWidth, ((maxProfit - curProfit) / profitRange) * tabGrid.ActualHeight, 0, 0);
            txtLabel.VerticalAlignment = VerticalAlignment.Top;
            txtLabel.HorizontalAlignment = HorizontalAlignment.Left;
            txtLabel.Foreground = Brushes.White;
            txtLabel.Background = Brushes.Blue; ;
            txtLabel.Height = 23;
            tabGrid.Children.Add(txtLabel);
        }

        private void drawShortTradesEquity(double zeroLevel, DateTime minDate, double timeRange, Grid tabGrid, double profitRange, double maxProfit, double gridWidth)
        {
            double x0, y0;
            List<ISysTrade> shortTradeList;
            double curProfit = 0;
            Polyline longTradesEquityLine = new Polyline();
            longTradesEquityLine.Stroke = Brushes.Brown;
            longTradesEquityLine.Points = new PointCollection();
            x0 = 0;
            y0 = zeroLevel;
            longTradesEquityLine.Points.Add(new Point(x0, y0));
            shortTradeList = this._tradeSystem.TradeList.FindAll(trade => !trade.IsLong);
            foreach (Trade curTrade in shortTradeList)
            {
                double x1, y1;
                curProfit += curTrade.Profit;
                TimeSpan tPosition = curTrade.ExitDateTime - minDate;
                double timePosition = tPosition.TotalHours;
                x1 = (timePosition / timeRange) * gridWidth;
                y1 = ((maxProfit - curProfit) / profitRange) * tabGrid.ActualHeight;
                Point equityPoint = new Point(x1, y1);
                longTradesEquityLine.Points.Add(equityPoint);
            }
            tabGrid.Children.Add(longTradesEquityLine);

            Label txtLabel = new Label();
            txtLabel.Content = curProfit.ToString("0,0.0", System.Globalization.CultureInfo.CreateSpecificCulture("ru-RU"));
            txtLabel.Margin = new Thickness(gridWidth, ((maxProfit - curProfit) / profitRange) * tabGrid.ActualHeight, 0, 0);
            txtLabel.VerticalAlignment = VerticalAlignment.Top;
            txtLabel.HorizontalAlignment = HorizontalAlignment.Left;
            txtLabel.Foreground = Brushes.White;
            txtLabel.Background = Brushes.Brown; ;
            txtLabel.Height = 23;
            tabGrid.Children.Add(txtLabel);
        }

        private void drawDownEquity(double zeroLevel, DateTime minDate, double timeRange, Grid tabGrid, double profitRange, double maxProfit, double gridWidth)
        {
            double x0, y0;
            Polyline drawDawnLine = new Polyline();
            drawDawnLine.Stroke = Brushes.Red;
            drawDawnLine.StrokeThickness = 2;
            drawDawnLine.Points = new PointCollection();
            x0 = 0;
            y0 = zeroLevel;
            drawDawnLine.Points.Add(new Point(x0, y0));
            foreach (ISysTrade curTrade in this._tradeSystem.TradeList)
            {
                double x1, y1;
                TimeSpan tPosition = curTrade.ExitDateTime - minDate;
                double timePosition = tPosition.TotalHours;
                x1 = (timePosition / timeRange) * gridWidth;
                y1 = ((maxProfit + curTrade.DrawDown) / profitRange) * tabGrid.ActualHeight;
                drawDawnLine.Points.Add(new Point(x1, y1));
            }
            tabGrid.Children.Add(drawDawnLine);

            Label txtLabel = new Label();
            txtLabel.Content = this._tradeSystem.TradeList.Last().DrawDown.ToString("0,0.0", System.Globalization.CultureInfo.CreateSpecificCulture("ru-RU"));
            txtLabel.Margin = new Thickness(gridWidth, (((maxProfit + this._tradeSystem.TradeList.Last().DrawDown) / profitRange) * tabGrid.ActualHeight) - 23, 0, 0);
            txtLabel.VerticalAlignment = VerticalAlignment.Top;
            txtLabel.HorizontalAlignment = HorizontalAlignment.Left;
            txtLabel.Foreground = Brushes.White;
            txtLabel.Background = Brushes.Red;
            txtLabel.Height = 23;
            tabGrid.Children.Add(txtLabel);
        }
        #endregion

        #region Event Handlers
        private void grid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (this._tradeSystem != null && this._tradeSystem.TradeList.Count != 0)
            {
                this.DrawGraph();
            }
        }

        private void equityLine_MouseEnter(object sender, MouseEventArgs e)
        {
            double x;
            string ttContent;
            ToolTip tt = new ToolTip();
            x = e.GetPosition((Polyline)sender).X;
            DateTime maxDate = this._tradeSystem.TradeList.Max(trade => trade.ExitDateTime);
            DateTime minDate = this._tradeSystem.TradeList.Min(trade => trade.EntryDateTime);
            TimeSpan tRange = maxDate - minDate;
            double timeRange = tRange.TotalDays;
            int xDay = Convert.ToInt32((x * timeRange) / this._actualGridWidth);
            TimeSpan xDaySpan = new TimeSpan(xDay, 0, 0, 0);
            DateTime thisDay = minDate + xDaySpan;
            ttContent = thisDay.Day + "." + thisDay.Month + "." + thisDay.Year + ":";

            var getInfo = from trade in this._tradeSystem.TradeList
                          where ((trade.ExitDateTime).Date == thisDay.Date)
                          select trade.CumProfit;
            foreach (double profits in getInfo)
            {
                ttContent += '\n' + profits.ToString();
            }

            tt.Content = ttContent;
            ((Polyline)sender).ToolTip = tt;
        }
        #endregion
        #endregion
    }
}
*/