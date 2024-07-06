namespace Bau.Libraries.LibCharts.Models;

/// <summary>
///		Parámetros de configuración general
/// </summary>
public class ChartConfigurationModel
{
	/// <summary>
	///		Separación entre barras
	/// </summary>
	public double GapBetweenBars { get; set; } = 5;

	/// <summary>
	///		Separación entre series de barras
	/// </summary>
	public double GapBetweenSeries { get; set; } = 10;
}
