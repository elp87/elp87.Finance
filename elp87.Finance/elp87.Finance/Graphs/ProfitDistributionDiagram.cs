using System.Windows.Controls;

namespace elp87.Finance.Graphs
{
    public abstract class ProfitDistributionDiagram : Diagram
    {
        protected ProfitDistributionDiagram(Grid grid)
            : base(grid)
        {
            this._diagramType = DiagramTypes.VerticalBlocks;
            this._fillingType = FillingTypes.AroundAxisFilling;
            this._catAxis = 0;
        }

    }
}
