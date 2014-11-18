using elp87.Helpers.Colors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace elp87.Finance.Graphs
{
    public abstract class Diagram : IGraph
    {
        #region Fields
        private Grid _grid;
        
        protected List<DiagramCategoryData> _categories;
        protected DiagramTypes _diagramType;
        protected FillingTypes _fillingType;
        protected Nullable<double> _catAxis;
        #endregion        

        protected Diagram(Grid grid)            
        {
            this._grid = grid;
        }        

        #region Methods
        public void DrawGraph()
        {
            this._grid.Children.Clear();

            if (this._categories.Count > 0)
            {
                double maxValue = this.GetMaxValue();
                double minValue = this.GetMinValue();
                double valueRange = maxValue - minValue;

                this.DrawZeroLine(maxValue, valueRange);
                for (int i = 0; i < this._categories.Count; i++)
                {
                    this.DrawBlock(this._categories[i], i, maxValue, valueRange);
                }
            }
        }

        #region Private
        private double GetMaxValue()
        {
            return this._categories.Max(category => category.Value);
        }

        private double GetMinValue()
        {
            return this._categories.Min(category => category.Value);
        }

        private void DrawZeroLine(double maxValue, double valueRange)
        {
            Line zeroLine = new Line();
            zeroLine.Stroke = Brushes.Black;

            switch (this._diagramType)
            {
                case DiagramTypes.VerticalBlocks:
                    {
                        zeroLine.X1 = 0;
                        zeroLine.X2 = this._grid.ActualWidth;
                        zeroLine.Y1 = (maxValue / valueRange) * this._grid.ActualHeight;
                        zeroLine.Y2 = (maxValue / valueRange) * this._grid.ActualHeight;
                        break;
                    }
                case DiagramTypes.HorizontalBlocks:
                    {
                        zeroLine.X1 = (maxValue / valueRange) * this._grid.ActualWidth;
                        zeroLine.X2 = (maxValue / valueRange) * this._grid.ActualWidth;
                        zeroLine.Y1 = 0;
                        zeroLine.Y2 = this._grid.ActualHeight;
                        break;
                    }
            }

            this._grid.Children.Add(zeroLine);
        }

        private void DrawBlock(DiagramCategoryData diagramCategoryData, int i, double maxValue, double valueRange)
        {
            Rectangle block = new Rectangle();
            switch (this._diagramType)
            {
                case DiagramTypes.VerticalBlocks:
                    {                        
                        block = CreateRectangle(
                            ((i + .25) / this._categories.Count) * this._grid.ActualWidth,
                            ((i + .75) / this._categories.Count) * this._grid.ActualWidth,
                            (maxValue / valueRange) * this._grid.ActualHeight,
                            ((maxValue - diagramCategoryData.Value) / valueRange) * this._grid.ActualHeight
                            );
                                                
                        break;
                    }
                case DiagramTypes.HorizontalBlocks:
                    {
                        block = CreateRectangle(
                            (maxValue / valueRange) * this._grid.ActualWidth,
                            ((maxValue - diagramCategoryData.Value) / valueRange) * this._grid.ActualWidth,
                            ((i + .25) / this._categories.Count) * this._grid.ActualHeight,
                            ((i + .75) / this._categories.Count) * this._grid.ActualHeight
                            );
                        break;
                    }
            }

            switch (this._fillingType)
            {
                case FillingTypes.MinToMaxGradient:
                    {
                        SolidColorBrush blockBrush = ColorGradient.FromRedToGreen(maxValue - valueRange, maxValue, diagramCategoryData.Value);
                        block.Fill = blockBrush;
                        block.Stroke = blockBrush;
                        break;
                    }
                case FillingTypes.AroundAxisFilling:
                    {
                        SolidColorBrush blockBrush;
                        if (this._catAxis == null) throw new AxisNullValueException();
                        double axisValue = this._catAxis.Value;
                        try
                        {
                            double categoryValue = Convert.ToDouble(diagramCategoryData.Title);
                            if (categoryValue < axisValue) { blockBrush = Brushes.Red; }
                            else if (categoryValue == axisValue) { blockBrush = Brushes.Black; }
                            else { blockBrush = Brushes.Green; }
                            
                        }
                        catch (Exception)
                        {
                            blockBrush = Brushes.Black;
                        }
                        block.Fill = blockBrush;
                        block.Stroke = blockBrush;
                        break;
                    }
            }
            this._grid.Children.Add(block);
        }

        private static Rectangle CreateRectangle(double x1, double x2, double y1, double y2)
        {
            Rectangle rect = new Rectangle();
            double left = Math.Min(x1, x2);
            double top = Math.Min(y1, y2);
            double width = Math.Abs(x1 - x2);
            double height = Math.Abs(y1 - y2);

            rect.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            rect.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            rect.Width = width;
            rect.Height = height;
            rect.Margin = new System.Windows.Thickness(left, top, 0, 0);

            return rect;
        }
        #endregion
        #endregion

        #region Enums
        protected enum DiagramTypes
        {
            HorizontalBlocks,
            VerticalBlocks
        }

        protected enum FillingTypes
        {
            MinToMaxGradient,
            AroundAxisFilling
        }        
        #endregion
    }
}
