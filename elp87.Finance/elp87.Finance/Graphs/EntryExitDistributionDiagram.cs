using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace elp87.Finance.Graphs
{
    public abstract class EntryExitDistributionDiagram : Diagram
    {
        protected EntryExitDistributionDiagram(Grid grid) : base(grid) 
        {
            this._fillingType = FillingTypes.MinToMaxGradient;
        }

        protected override string GetBlockTooltipContent(DiagramCategoryData category)
        {
            StringBuilder ttContent = new StringBuilder();

            string titleNotation = category.Title.ToString();
            ttContent.Append(titleNotation + '\n');

            string equityNotation = "П/У - " + Math.Round(category.Value, 2).ToString();
            ttContent.Append(equityNotation + '\n');

            foreach (var attachedData in category.AttachedData)
            {
                ttContent.Append((string)attachedData + '\n');
            }
            return ttContent.ToString();
        }

        public enum CalcTypes
        {
            EntryDate,
            ExitDate
        }
    }
}
