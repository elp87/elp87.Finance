using System;
using System.Text;
using System.Windows.Controls;

namespace elp87.Finance.Graphs
{
    public abstract class DayOfWeekDistributionDiagram : EntryExitDistributionDiagram
    {
        protected const int DaysInWeekCount = 7;
                
        protected DayOfWeekDistributionDiagram(Grid grid)
            : base(grid)
        {
            this._diagramType = DiagramTypes.HorizontalBlocks;
        }
    }
}
