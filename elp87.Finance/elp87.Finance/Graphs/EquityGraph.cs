using System;
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
        private List<PointData> _points;
        private Grid _grid;
        private double _actualGridWidth;
        #endregion

        #region Constructors
        protected EquityGraph(Grid grid)
        {
            this._grid = grid;
            //this._grid.SizeChanged += new SizeChangedEventHandler(grid_SizeChanged);
        }

        public EquityGraph(Grid grid, List<PointData> points)
            : this(grid)
        {
            this._points = points;
        }
        #endregion

        #region Methods
        #region Public
        public void DrawGraph()
        {
            this._grid.Children.Clear();

            DateTime minDate = this._points.Min(point => point.Date);
            DateTime maxDate = this._points.Max(point => point.Date);
            TimeSpan tRange = maxDate - minDate;
            double timeRange = tRange.TotalHours;

            Money maxValue = this.maxGraphValue();
            Money minValue = this.minGraphValue();
            Money profitRange = maxValue - minValue;

            double zeroLevel = (maxValue / profitRange) * this._grid.ActualHeight;
            this._actualGridWidth = this._grid.ActualWidth - _sideBlockWidth;

            double horizontLineHeight = this.getHorizontalCell(profitRange);
            this.drawSideBlock(this._grid, this._actualGridWidth);
            this.drawHorizontLines(horizontLineHeight, this._grid, profitRange, maxValue, this._actualGridWidth);
            this.drawMedianLine(minValue, maxValue, profitRange, this._grid, this._actualGridWidth);
            this.drawMonthLines(minDate, timeRange, this._grid, this._actualGridWidth);
            this.drawAllPoints(zeroLevel, minDate, timeRange, this._grid, profitRange, maxValue, this._actualGridWidth);

            // TODO: Draw DD
        }  
        #endregion

        #region Private
        protected Money maxGraphValue()
        {            
            Money max = Math.Max(this._points.Max(point => point.Value).Value, 0);
            return max;
        }

        protected Money minGraphValue()
        {
            Money min = Math.Min(this._points.Max(point => point.Value).Value, 0);
            return min;
        }

        protected double getHorizontalCell(Money range)
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

        private void drawHorizontLines(double lineHeight, Grid tabGrid, Money profitRange, Money maxProfit, double gridWidth)
        {
            const int _labelTextOffsetPosition = 15;

            Money minValue = maxProfit - profitRange;
            double maxGraphValue = Convert.ToDouble(maxProfit.Value) - (Convert.ToDouble(maxProfit.Value) % lineHeight);
            double minGraphValue = Convert.ToDouble(minValue.Value) - (Convert.ToDouble(minValue.Value) % lineHeight);
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

        private void drawMedianLine(Money minProfit, Money maxProfit, Money profitRange, Grid tabGrid, double gridWidth)
        {
            Line medianLine = new Line();
            medianLine.Stroke = Brushes.Black;
            medianLine.X1 = 0;
            medianLine.Y1 = ((maxProfit - minProfit) / profitRange) * tabGrid.ActualHeight;
            medianLine.X2 = gridWidth;
            medianLine.Y2 = ((maxProfit - maxProfit) / profitRange) * tabGrid.ActualHeight;
            tabGrid.Children.Add(medianLine);

            Label txtLabel = new Label();
            txtLabel.Content = maxProfit.Value.ToString("0,0.0", System.Globalization.CultureInfo.CreateSpecificCulture("ru-RU"));
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

        private void drawAllPoints(double zeroLevel, DateTime minDate, double timeRange, Grid tabGrid, Money profitRange, Money maxProfit, double gridWidth)
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
            foreach (PointData point in this._points)
            {
                double x1, y1;
                TimeSpan tPosition = point.Date - minDate;
                double timePosition = tPosition.TotalHours;
                x1 = (timePosition / timeRange) * gridWidth;
                y1 = ((maxProfit - point.Value) / profitRange) * tabGrid.ActualHeight;
                Point equityPoint = new Point(x1, y1);
                equityLine.Points.Add((Point)equityPoint);
            }
            x0 = gridWidth;
            y0 = (maxProfit / profitRange) * tabGrid.ActualHeight;
            equityLine.Points.Add(new Point(x0, y0));
            equityLine.MouseEnter += new MouseEventHandler(equityLine_MouseEnter);
            tabGrid.Children.Add(equityLine);

            Label txtLabel = new Label();
            txtLabel.Content = this._points.Last().Value.ToString();//"0,0.0", System.Globalization.CultureInfo.CreateSpecificCulture("ru-RU"));
            txtLabel.Margin = new Thickness(gridWidth, ((maxProfit - this._points.Last().Value) / profitRange) * tabGrid.ActualHeight, 0, 0);
            txtLabel.VerticalAlignment = VerticalAlignment.Top;
            txtLabel.HorizontalAlignment = HorizontalAlignment.Left;
            txtLabel.Foreground = Brushes.White;
            txtLabel.Background = Brushes.DarkGreen;
            txtLabel.Height = 23;
            tabGrid.Children.Add(txtLabel);
        }        
        #endregion   
     
        #region EventHandlers
        private void equityLine_MouseEnter(object sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }
        #endregion
        #endregion
    }
}
