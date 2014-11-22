using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace elp87.Finance.Graphs
{
    public abstract class TimeDistributionDiagram : EntryExitDistributionDiagram
    {
        protected TimeDistributionDiagram(Grid grid)
            : base(grid)
        {
            this._diagramType = DiagramTypes.VerticalBlocks;
        }
    }
}
