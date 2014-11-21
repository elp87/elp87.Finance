using System;
using System.Text;
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

        protected override string GetBlockTooltipContent(DiagramCategoryData category)
        {
            StringBuilder ttContent = new StringBuilder();

            string dayNotation = (string)category.Title;
            ttContent.Append(dayNotation + '\n');

            string equityNotation = "П/У - " + Math.Round(category.Value, 2).ToString();
            ttContent.Append(equityNotation + 'n');

            foreach (var attachedData in category.AttachedData)
            {
                ttContent.Append((string)attachedData + 'n');
            }
            return ttContent.ToString();
        }
        
        protected enum CalcTypes
        {
            EntryDate,
            ExitDate
        }
    }
}
