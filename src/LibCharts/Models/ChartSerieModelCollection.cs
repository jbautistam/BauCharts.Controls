using Bau.Libraries.LibCharts.Models.Structs;

namespace Bau.Libraries.LibCharts.Models;

/// <summary>
///		Colección de <see cref="ChartSerieModel"/>
/// </summary>
public class ChartSerieModelCollection : List<ChartSerieModel>
{
	public ChartSerieModelCollection(ChartModel chart)
	{
		Chart = chart;
	}

	/// <summary>
	///		Normaliza los datos de los gráficos
	/// </summary>
	public void Normalize()
	{
		double totalBarWidths = GetBarsTotalWidth();

			// Normaliza las series
			NormalizeBars(totalBarWidths);
	}

	/// <summary>
	///		Normaliza las barras
	/// </summary>
	private void NormalizeBars(double barsWidth)
	{
		double offset = 0;

			// Normaliza la X de los elementos de las series
			foreach (ChartSerieModel serie in this)
			{
				// Añade el ancho si es una barra
				if (serie.Type == ChartSerieModel.ChartSerieType.Bars)
				{
					double step = offset;

						// Asigna el valor X
						foreach (ChartSeriePointModel item in serie.Items)
						{
							// Asigna el valor
							item.X = step;
							// Pasa al siguiente valor
							step = step + barsWidth;
						}
						// Añade el paso con el ancho de la barra
						offset += Chart.Design.Styles.GetStyle(serie.Style).Width + Chart.Configuration.GapBetweenBars;
				}
			}
	}

	/// <summary>
	///		Obtiene el ancho total de las barras
	/// </summary>
	private double GetBarsTotalWidth()
	{
		int barsCount = 0;
		double width = 0;

			// Calcula el ancho de las barras
			foreach (ChartSerieModel serie in this)
				if (serie.Type == ChartSerieModel.ChartSerieType.Bars)
				{
					// Añade el ancho
					width += Chart.Design.Styles.GetStyle(serie.Style).Width + Chart.Configuration.GapBetweenBars;
					// Añade el número de series
					barsCount++;
				}
			// Añade la separación entre series
			if (barsCount > 0)
				width += (barsCount - 1) * Chart.Configuration.GapBetweenSeries;
			// Devuelve el ancho
			return width;
	}

	/// <summary>
	///		Cuenta las series de un tipo de elemento
	/// </summary>
	public int CountType(ChartSerieModel.ChartSerieType type)
	{
		int count = 0;

			// Cuenta el número de tipos
			foreach (ChartSerieModel serie in this)
				if (serie.Type == type)
					count++;
			// Devuelve el número de elementos
			return count;
	}

	/// <summary>
	///		Obtiene el rectángulo con los límites del gráfico
	/// </summary>
	public RectangleModel GetLimits() => new RectangleModel(new PointModel(GetMinimumX(), GetMaximumY()), new PointModel(GetMaximumX(), GetMinimumY()));

	/// <summary>
	///		Obtiene el valor Y máximo de las series
	/// </summary>
	public double GetMaximumY()
	{
		double? maximum = null;

			// Obtiene el máximo
			foreach (ChartSerieModel serie in this)
			{
				double maxSerie = serie.GetMaximumY();

					if (maximum is null || maximum < maxSerie)
						maximum = maxSerie;
			}
			// Devuelve el máximo localizado
			return maximum ?? 0;
	}

	/// <summary>
	///		Obtiene el valor Y mínimo de las series
	/// </summary>
	public double GetMinimumY()
	{
		double? minimum = null;

			// Obtiene el mínimo
			foreach (ChartSerieModel serie in this)
			{
				double minSerie = serie.GetMinimumY();

					if (minimum is null || minimum > minSerie)
						minimum = minSerie;
			}
			// Devuelve el mínimo localizado
			return minimum ?? 0;
	}

	/// <summary>
	///		Obtiene el valor X máximo de las series
	/// </summary>
	public double GetMaximumX()
	{
		double? maximum = null;

			// Obtiene el máximo
			foreach (ChartSerieModel serie in this)
			{
				double maxSerie = serie.GetMaximumX();

					if (maximum is null || maximum < maxSerie)
						maximum = maxSerie;
			}
			// Devuelve el máximo localizado
			return maximum ?? 0;
	}

	/// <summary>
	///		Obtiene el valor X mínimo de las series
	/// </summary>
	public double GetMinimumX()
	{
		double? minimum = null;

			// Obtiene el mínimo
			foreach (ChartSerieModel serie in this)
			{
				double minSerie = serie.GetMinimumX();

					if (minimum is null || minimum > minSerie)
						minimum = minSerie;
			}
			// Devuelve el mínimo localizado
			return minimum ?? 0;
	}

	/// <summary>
	///		Gráfico al que pertenece la serie
	/// </summary>
	public ChartModel Chart { get; }
}
