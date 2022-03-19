using System;
using System.Collections.Generic;

using Bau.Libraries.LibCharts.Models;

namespace Bau.Libraries.LibCharts.Builders
{
	/// <summary>
	///		Generador de <see cref="ChartSerieModel"/>
	/// </summary>
	public class ChartSerieBuilder
	{
		public ChartSerieBuilder(ChartBuilder builder, string name)
		{
			Builder = builder;
			Serie = new ChartSerieModel
							{
								Name = name
							};
			Builder.Chart.Series.Add(Serie);
		}

		/// <summary>
		///		Añade un elemento a la serie
		/// </summary>
		public ChartSerieBuilder WithItem(double x, double y)
		{
			// Añade el elemento
			Serie.Items.Add(new ChartSeriePointModel
										{
											X = x,
											Y = y
										}
						   );
			// Devuelve el generador
			return this;
		}

		/// <summary>
		///		Añade una serie de elementos aleatorios
		/// </summary>
		public ChartSerieBuilder WithRandomItems(int points, double minValue, double maxValue)
		{
			Random rnd = new Random();

				// Añade los elementos
				for (int index = 0; index < points; index++)
					Serie.Items.Add(new ChartSeriePointModel
											{
												X = index,
												Y = minValue + rnd.NextDouble() * maxValue
											}
									);
				// Devuelve el generador
				return this;
		}

		/// <summary>
		///		Devuelve el generador de gráficos
		/// </summary>
		public ChartBuilder Back()
		{
			return Builder;
		}

		/// <summary>
		///		Generador de gráficos
		/// </summary>
		private ChartBuilder Builder { get; }

		/// <summary>
		///		Datos de la serie
		/// </summary>
		private ChartSerieModel Serie { get; }
	}
}
