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
        protected enum DiagramType
        {
            HorizontalBlocs,
            VerticalBlocs
        }

        protected enum FillingTypes
        {
            MinToMaxGradient,
            AroundAxisFilling
        }
        #endregion
    }
}
