using System.Windows.Controls;

namespace elp87.Finance.Graphs
{
    public abstract class TimeDistributionDiagram : EntryExitDistributionDiagram
    {
        protected const int HoursInDay = 24;

        protected TimeDistributionDiagram(Grid grid)
            : base(grid)
        {
            this._diagramType = DiagramTypes.VerticalBlocks;
        }
    }
}
