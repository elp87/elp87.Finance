using System.Collections.Generic;

namespace elp87.Finance.Graphs
{
    public abstract class Diagram : IGraph
    {
        #region Fields
        protected List<DiagramCategoryData> _categories;
        protected DiagramType _diagramType;
        #endregion

        #region Methods
        public void DrawGraph()
        {
            
        }
        #endregion

        #region Enums
        public enum DiagramType
        {
            HorizontalBlocs,
            VerticalBlocs
        } 
        #endregion
    }
}
