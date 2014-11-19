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
    }
}
