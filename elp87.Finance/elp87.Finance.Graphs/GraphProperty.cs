﻿using System.Windows.Media;

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
    }
}
