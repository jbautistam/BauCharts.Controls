namespace Bau.Libraries.LibCharts.Models;

/// <summary>
///		Clase con los datos de una serie
/// </summary>
public class ChartSerieModel
{
	/// <summary>
	///		Tipo de gráfico de la serie
	/// </summary>
	public enum ChartSerieType
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
		Heatmap,
		/// <summary>Gráfico de líneas entre puntos</summary>
		Scatter
	}

	public ChartSerieModel(string name, string style, ChartSerieType type)
	{
		Name = name;
		Style = style;
		Type = type;
	}

	/// <summary>
	///		Obtiene un array con los valores de los elementos
	/// </summary>
	public double[] GetXItems()
	{
		double[] values = [Items.Count];

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
		double[] values = [Items.Count];

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
				if (maximum is null || maximum < point.Y)
					maximum = point.Y;
			// Devuelve el valor máximo
			return maximum ?? 0;
	}

	/// <summary>
	///		Obtiene el valor mínimo del eje Y
	/// </summary>
	public double GetMinimumY()
	{
		double? minimum = null;

			// Busca el valor máximo
			foreach (ChartSeriePointModel point in Items)
				if (minimum is null || minimum > point.Y)
					minimum = point.Y;
			// Devuelve el valor mínimo
			return minimum ?? 0;
	}

	/// <summary>
	///		Obtiene el valor máximo del eje X
	/// </summary>
	public double GetMaximumX()
	{
		double? maximum = null;

			// Busca el valor máximo
			foreach (ChartSeriePointModel point in Items)
				if (maximum is null || maximum < point.X)
					maximum = point.X;
			// Devuelve el valor máximo
			return maximum ?? 0;
	}

	/// <summary>
	///		Obtiene el valor mínimo del eje X
	/// </summary>
	public double GetMinimumX()
	{
		double? minimum = null;

			// Busca el valor máximo
			foreach (ChartSeriePointModel point in Items)
				if (minimum is null || minimum > point.X)
					minimum = point.X;
			// Devuelve el valor mínimo
			return minimum ?? 0;
	}

	/// <summary>
	///		Nombre de la serie
	/// </summary>
	public string Name { get; }

	/// <summary>
	///		Estilo de la serie
	/// </summary>
	public string Style { get; }

	/// <summary>
	///		Tipo de la serie
	/// </summary>
	public ChartSerieType Type { get; }

	/// <summary>
	///		Elementos
	/// </summary>
	public List<ChartSeriePointModel> Items { get; } = [];
}