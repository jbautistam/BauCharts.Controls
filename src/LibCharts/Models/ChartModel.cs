namespace Bau.Libraries.LibCharts.Models;

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
		/// <summary>X / Y</summary>
		XY,
		/// <summary>Gráfico de tarta</summary>
		Pie,
		/// <summary>Gráfico de radar</summary>
		Radar
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

	public ChartModel(ChartType type)
	{
		Type = type;
		Series = new ChartSerieModelCollection(this);
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
	public string Title { get; set; } = default!;

	/// <summary>
	///		Tipo de gráfico
	/// </summary>
	public ChartType Type { get; }

	/// <summary>
	///		Orientación
	/// </summary>
	public OrientationType Orientation { get; set; } = OrientationType.Horizontal;

	/// <summary>
	///		Parámetros generales
	/// </summary>
	public ChartConfigurationModel Configuration { get; } = new();

	/// <summary>
	///		Leyenda
	/// </summary>
	public ChartLegendModel Legend { get; } = new();

	/// <summary>
	///		Etiqueta del eje X
	/// </summary>
	public string XLabel { get; set; } = default!;

	/// <summary>
	///		Etiqueta del eje Y
	/// </summary>
	public string YLabel { get; set; } = default!;

	/// <summary>
	///		Datos de diseño
	/// </summary>
	public Design.DesignModel Design { get; } = new();

	/// <summary>
	///		Series
	/// </summary>
	public ChartSerieModelCollection Series { get; }

	/// <summary>
	///		Etiquetas de los puntos
	/// </summary>
	public List<string> Labels { get; } = [];
}
