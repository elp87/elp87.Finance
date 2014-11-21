using System.Windows.Controls;

namespace elp87.Finance.Graphs
{
    public abstract class DayOfWeekDistributionDiagram : Diagram
    {
        protected CalcTypes _calcType;

        protected DayOfWeekDistributionDiagram(Grid grid)
            : base(grid)
        {
            this._diagramType = DiagramTypes.HorizontalBlocks;
            this._fillingType = FillingTypes.MinToMaxGradient;
        }

        protected enum CalcTypes
        {
            EntryDate,
            ExitDate
        }
    }
}
