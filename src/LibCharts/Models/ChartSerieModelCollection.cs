using System;
using System.Collections.Generic;

namespace Bau.Libraries.LibCharts.Models
{
	/// <summary>
	///		Colección de <see cref="ChartSerieModel"/>
	/// </summary>
	public class ChartSerieModelCollection : List<ChartSerieModel>
	{
		/// <summary>
		///		Obtiene el valor máximo de las series
		/// </summary>
		public double GetMaximumValue()
		{
			double? maximum = null;

				// Obtiene el máximo
				foreach (ChartSerieModel serie in this)
				{
					double maxSerie = serie.GetMaximumY();

						if (maximum == null || maximum < maxSerie)
							maximum = maxSerie;
				}
				// Devuelve el máxmo localizado
				return maximum ?? 0;
		}
	}
}
