using System.Windows.Media;

namespace elp87.Finance.Graphs
{
    public class GraphProperty
    {
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
    }
}
