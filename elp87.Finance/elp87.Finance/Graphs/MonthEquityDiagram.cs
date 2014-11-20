using System;
using System.Windows.Controls;

namespace elp87.Finance.Graphs
{
    public abstract class MonthEquityDiagram : Diagram
    {
        protected MonthEquityDiagram(Grid grid)
            : base(grid)
        {
            this._diagramType = DiagramTypes.VerticalBlocks;
            this._fillingType = FillingTypes.MinToMaxGradient;
        }

        protected override string GetBlockTooltipContent(DiagramCategoryData category)
        {
            DateTime date = (DateTime)category.Title;
            string monthNotation = date.ToString("MMMM yyyy");
            string valueNotation = Math.Round(category.Value, 2).ToString() + "%";
            return monthNotation + ": " + valueNotation;
        }
    }
}
