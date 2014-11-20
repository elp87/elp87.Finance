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

        protected override string GetBlockTooltipContent(DiagramCategoryData category)
        {
            string titleNotation = category.Title.ToString() + "%";
            string valueNotation = category.Value.ToString();
            return titleNotation + " - " + valueNotation;
        }
    }
}
