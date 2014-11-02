using System.Windows.Media;

namespace elp87.Finance.Graphs
{
    public class GraphProperty
    {
        public GraphProperty()
        {
            this.Stroke = null;
            this.Fill = null;
            this.Opacity = 1;
            this.StrokeThickness = 1;
            this.HasToolTip = false;
            this.MedianLineStroke = null;
        }

        /// <summary>
        /// Возвращает или задает объект <see cref="System.Windows.Media.Brush"/>, определяющий способ рисования линии графика
        /// </summary>
        public Brush Stroke { get; set; }

        /// <summary>
        /// Возвращает или задает объект <see cref="System.Windows.Media.Brush"/>, определяющий способ рисования внутренней части графика
        /// </summary>
        public Brush Fill { get; set; }

        /// <summary>
        /// Возвращает или задает коэффициент непрозрачности графика
        /// </summary>
        public double Opacity { get; set; }

        /// <summary>
        /// Возвращает или задает толщину линии графика
        /// </summary>
        public double StrokeThickness { get; set; }

        /// <summary>
        /// Возвращает или задает наличие объекта подсказки, отображаемого для этого графика
        /// </summary>
        public bool HasToolTip { get; set; }

        /// <summary>
        /// Возвращает или задает объект <see cref="System.Windows.Media.Brush"/>, определяющий способ рисования линии медианы        
        /// </summary>
        /// <remarks>
        /// Если свойство равно null, линия медианы не строится
        /// </remarks>
        public Brush MedianLineStroke { get; set; }
    }
}
