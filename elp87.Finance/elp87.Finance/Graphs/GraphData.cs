using System.Collections.Generic;

namespace elp87.Finance.Graphs
{
    public class GraphData
    {
        public GraphData(List<PointData> points, GraphProperty property)
        {
            this.Points = points;
            this.Property = property;
        }

        public List<PointData> Points { get; set; }

        public GraphProperty Property { get; set; }
    }
}
