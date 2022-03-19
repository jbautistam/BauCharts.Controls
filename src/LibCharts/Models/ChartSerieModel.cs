using System;
using System.Collections.Generic;

namespace Bau.Libraries.LibCharts.Models
{
	/// <summary>
	///		Clase con los datos de una serie
	/// </summary>
	public class ChartSerieModel
	{
		/// <summary>
		///		Obtiene un array con los valores de los elementos
		/// </summary>
		public double[] GetXItems()
		{
			double[] values = new double[Items.Count];

				// Asigna los elementos al array
				for (int index = 0; index < Items.Count; index++)
					values[index] = Items[index].X;
				// Devuelve los elementos
				return values;
		}

		/// <summary>
		///		Obtiene un array con los valores de los elementos
		/// </summary>
		public double[] GetYItems()
		{
			double[] values = new double[Items.Count];

				// Asigna los elementos al array
				for (int index = 0; index < Items.Count; index++)
					values[index] = Items[index].Y;
				// Devuelve los elementos
				return values;
		}

		/// <summary>
		///		Obtiene el valor máximo del eje y
		/// </summary>
		public double GetMaximumY()
		{
			double? maximum = null;

				// Busca el valor máximo
				foreach (ChartSeriePointModel point in Items)
					if (maximum == null || maximum < point.Y)
						maximum = point.Y;
				// Devuelve el valor máximo
				return maximum ?? 0;
		}

		/// <summary>
		///		Nombre de la serie
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		///		Elementos
		/// </summary>
		public List<ChartSeriePointModel> Items { get; } = new List<ChartSeriePointModel>();
	}
}
