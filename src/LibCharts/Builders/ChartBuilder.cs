using System;
using System.Collections.Generic;

using Bau.Libraries.LibCharts.Models;

namespace Bau.Libraries.LibCharts.Builders
{
	/// <summary>
	///		Builder de <see cref="ChartModel"/>
	/// </summary>
	public class ChartBuilder
	{
		public ChartBuilder(string title, ChartModel.ChartType type, ChartModel.OrientationType orientation)
		{
			Chart = new ChartModel
							{
								Title = title,
								Type = type,
								Orientation = orientation
							};
		}

		/// <summary>
		///		Añade una etiqueta al eje X
		/// </summary>
		public ChartBuilder WithXLabel(string label)
		{
			// Añade la etiqueta
			Chart.XLabel = label;
			// Devuelve el generador
			return this;
		}

		/// <summary>
		///		Añade una etiqueta al gráfico
		/// </summary>
		public ChartBuilder WithYLabel(string label)
		{
			// Añade la etiqueta
			Chart.YLabel = label;
			// Devuelve el generador
			return this;
		}

		/// <summary>
		///		Añade una etiqueta al gráfico
		/// </summary>
		public ChartBuilder WithLabel(string label)
		{
			// Añade la etiqueta
			Chart.Labels.Add(label);
			// Devuelve el generador
			return this;
		}

		/// <summary>
		///		Añade etiquetas al gráfico
		/// </summary>
		public ChartBuilder WithLabels(List<string> labels)
		{
			// Añade las etiquetas
			Chart.Labels.AddRange(labels);
			// Devuelve el generador
			return this;
		}

		/// <summary>
		///		Asigna la ubicación de la leyenda
		/// </summary>
		public ChartBuilder WithLegend(ChartModel.LegendLocationType location)
		{
			// Asigna la ubicación
			Chart.LegendLocation = location;
			// Devuelve el generador
			return this;
		}

		/// <summary>
		///		Añade una serie al gráfico
		/// </summary>
		public ChartSerieBuilder WithSerie(string name)
		{
			return new ChartSerieBuilder(this, name);
		}

		/// <summary>
		///		Genera el gráfico
		/// </summary>
		public ChartModel Build()
		{
			return Chart;
		}

		/// <summary>
		///		Gráfico
		/// </summary>
		internal ChartModel Chart { get; }
	}
}
