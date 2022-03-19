using System;
using System.Collections.Generic;

namespace Bau.Libraries.LibCharts.Models
{
	/// <summary>
	///		Clase con los datos de un gráfico
	/// </summary>
	public class ChartModel
	{
		/// <summary>
		///		Tipo de gráfico
		/// </summary>
		public enum ChartType
		{
			/// <summary>Gráfico de barras</summary>
			Bars,
			/// <summary>Barras apiladas</summary>
			StackedBar,
			/// <summary>Líneas</summary>
			Lines,
			/// <summary>Areas</summary>
			Areas,
			/// <summary>Area entre curvas</summary>
			AreasBetweenCurve,
			/// <summary>Tarta</summary>
			Pie,
			/// <summary>Gráfico de radar</summary>
			Radar,
			/// <summary>Gráfico de mapa de calor</summary>
			Heatmap
		}

		/// <summary>
		///		Orientación
		/// </summary>
		public enum OrientationType
		{
			/// <summary>Horizontal</summary>
			Horizontal,
			/// <summary>Vertical</summary>
			Vertical
		}

		/// <summary>
		///		Ubicación de la leyenda
		/// </summary>
		public enum LegendLocationType
		{
			None,
			LowerCenter,
			LowerLeft,
			LowerRight,
			MiddleLeft,
			MiddleRight,
			UpperCenter,
			UpperLeft,
			UpperRight,
		}

		/// <summary>
		///		Obtiene una lista de etiquetas
		/// </summary>
		public string[] GetLabels()
		{
			string [] values = new string[Labels.Count];

				// Asigna las etiquetas
				for (int index = 0; index < Labels.Count; index++)
					values[index] = Labels[index];
				// Devuelve las etiquetas
				return values;
		}

		/// <summary>
		///		Título
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		///		Tipo de gráfico
		/// </summary>
		public ChartType Type { get; set; } = ChartType.Bars;

		/// <summary>
		///		Orientación
		/// </summary>
		public OrientationType Orientation { get; set; } = OrientationType.Horizontal;

		/// <summary>
		///		Ubicación de la leyenda
		/// </summary>
		public LegendLocationType LegendLocation { get; set; } = LegendLocationType.None;

		/// <summary>
		///		Etiqueta del eje X
		/// </summary>
		public string XLabel { get; set; }

		/// <summary>
		///		Etiqueta del eje Y
		/// </summary>
		public string YLabel { get; set; }

		/// <summary>
		///		Series
		/// </summary>
		public ChartSerieModelCollection Series { get; } = new ChartSerieModelCollection();

		/// <summary>
		///		Etiquetas de los puntos
		/// </summary>
		public List<string> Labels { get; } = new List<string>();
	}
}
