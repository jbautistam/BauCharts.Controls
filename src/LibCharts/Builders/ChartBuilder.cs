using Bau.Libraries.LibCharts.Models;
using Bau.Libraries.LibCharts.Models.Design;

namespace Bau.Libraries.LibCharts.Builders;

/// <summary>
///		Builder de <see cref="ChartModel"/>
/// </summary>
public class ChartBuilder
{
	public ChartBuilder(string title, ChartModel.ChartType type, ChartModel.OrientationType orientation)
	{
		Chart = new ChartModel(type)
						{
							Title = title,
							Orientation = orientation
						};
		// Genera los datos predeterminados
		AssignDefaultPallete();
		AssignSecondaryPallete();
	}

	/// <summary>
	///		Genera una paleta predeterminada
	/// </summary>
	private void AssignDefaultPallete()
	{
		StyleModel style = Chart.Design.Styles.Add(DesignModel.Default);

			// Añade los colores de la paleta
			style.CreatePallete(DesignModel.DefaultStyles.Default);
	}

	/// <summary>
	///		Genera una paleta secundaria
	/// </summary>
	private void AssignSecondaryPallete()
	{
		StyleModel style = Chart.Design.Styles.Add(DesignModel.Secondary);

			// Añade los colores de la paleta
			style.CreatePallete(DesignModel.DefaultStyles.Secondary);
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
	public ChartBuilder WithLegend(ChartLegendModel.LocationType location)
	{
		// Asigna la ubicación
		Chart.Legend.Location = location;
		// Devuelve el generador
		return this;
	}

	/// <summary>
	///		Añade una serie al gráfico
	/// </summary>
	public ChartSerieBuilder WithSerie(string name, string style, ChartSerieModel.ChartSerieType type) => new(this, name, style, type);

	/// <summary>
	///		Añade datos de diseño al gráfico
	/// </summary>
	public ChartDesignBuilder WithDesign() => new(this, Chart.Design);

	/// <summary>
	///		Genera el gráfico
	/// </summary>
	public ChartModel Build() => Chart;

	/// <summary>
	///		Gráfico
	/// </summary>
	internal ChartModel Chart { get; }
}
