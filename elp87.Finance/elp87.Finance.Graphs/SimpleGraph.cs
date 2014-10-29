using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace elp87.Finance.Graphs
{
    public class SimpleGraph : IGraph
    {
        #region Contants
        const double _sideBlockWidth = 70;
        #endregion

        #region Fields
        private GraphData[] _graphs;
        private Grid _grid;
        private double _actualGridWidth;
        #endregion

        #region Constructors
        protected SimpleGraph(Grid grid)
        {
            this._grid = grid;
            this._grid.SizeChanged += new SizeChangedEventHandler(grid_SizeChanged);
        }

        public SimpleGraph(Grid grid, GraphData graph)
            : this(grid)
        {
            this._graphs = new GraphData[] { graph };
        }

        public SimpleGraph(Grid grid, GraphData[] graphs)
            : this(grid)
        {
            this._graphs = graphs;
        }
        #endregion

        #region Methods
        #region Public
        public void DrawGraph()
        {
            this._grid.Children.Clear();

            DateTime minDate = this.GetMinDate();
            DateTime maxDate = this.GetMaxDate();
            TimeSpan tRange = maxDate - minDate;
            double timeRange = tRange.TotalHours;

            Money minValue = this.GetMinValue();
            Money maxValue = this.GetMaxValue();
            Money profitRange = maxValue - minValue;

            double zeroLevel = (maxValue / profitRange) * this._grid.ActualHeight;
            this._actualGridWidth = this._grid.ActualWidth - _sideBlockWidth;

            double horizontLineHeight = this.GetHorizontalCell(profitRange);
            this.DrawSideBlock(this._grid, this._actualGridWidth);
            this.DrawHorizontLines(horizontLineHeight, this._grid, profitRange, maxValue, this._actualGridWidth);
            this.DrawMonthLines(minDate, timeRange, this._grid, this._actualGridWidth);

            foreach (GraphData graph in this._graphs)
            {
                this.DrawGraphLine(graph, zeroLevel, minDate, timeRange, profitRange, maxValue, this._actualGridWidth);
            }
        }        
        #endregion

        #region Private
        protected DateTime GetMinDate()
        {
            return this._graphs.Min(graph => graph.Points.Min(point => point.Date));
        }

        protected DateTime GetMaxDate()
        {
            return this._graphs.Max(graph => graph.Points.Max(point => point.Date));
        }

        protected Money GetMinValue()
        {
            return this._graphs.Min(graph => graph.Points.Min(point => point.Value));
        }

        protected Money GetMaxValue()
        {
            return this._graphs.Max(graph => graph.Points.Max(point => point.Value));
        }

        protected double GetHorizontalCell(Money range)
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

        private void DrawSideBlock(Grid tabGrid, double gridWidth)
        {
            Rectangle sideBlock = new Rectangle();
            sideBlock.Stroke = Brushes.DarkGray;
            sideBlock.Fill = Brushes.LightGray;
            sideBlock.Width = 70;
            sideBlock.Height = tabGrid.ActualHeight;
            tabGrid.Children.Add(sideBlock);
            sideBlock.Margin = new Thickness(gridWidth, 0, 0, 0);
        }

        private void DrawHorizontLines(double lineHeight, Grid tabGrid, Money profitRange, Money maxProfit, double gridWidth)
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

        private void DrawMonthLines(DateTime minDate, double timeRange, Grid tabGrid, double gridWidth)
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

        private void DrawGraphLine(GraphData graph, double zeroLevel, DateTime minDate, double timeRange, Money profitRange, Money maxValue, double gridWidth)
        {
            double x0, y0;

            Polyline equityLine = new Polyline();

            if (graph.Property.Stroke != null)
            {
                equityLine.Stroke = graph.Property.Stroke;
            }
            else
            {
                equityLine.Stroke = Brushes.Black;
            }
            if (graph.Property.Fill != null)
            {
                equityLine.Fill = graph.Property.Fill;
            }
            equityLine.StrokeThickness = graph.Property.StrokeThickness;
            equityLine.Opacity = graph.Property.Opacity;

            equityLine.Points = new PointCollection();

            x0 = 0;
            y0 = zeroLevel;
            equityLine.Points.Add(new Point(x0, y0));

            foreach (PointData point in graph.Points)
            {
                double x1, y1;
                TimeSpan tPosition = point.Date - minDate;
                double timePosition = tPosition.TotalHours;
                x1 = (timePosition / timeRange) * gridWidth;
                y1 = ((maxValue - point.Value) / profitRange) * this._grid.ActualHeight;
                Point equityPoint = new Point(x1, y1);
                equityLine.Points.Add((Point)equityPoint);
            }

            x0 = gridWidth;
            y0 = (maxValue / profitRange) * this._grid.ActualHeight;
            equityLine.Points.Add(new Point(x0, y0));

            this._grid.Children.Add(equityLine);
        }
        #endregion

        #region Event Handlers
        private void grid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            throw new NotImplementedException();
        } 
        #endregion 
        #endregion        
    }
}
