using System.Windows.Controls;

using ScottPlot;
using ScottPlot.WPF;
using Bau.Libraries.LibCharts.Models;
using Bau.Libraries.LibCharts.Models.Design;
using Bau.Libraries.LibCharts.Models.Structs;

namespace Bau.Controls.BauCharts;

/// <summary>
///		Control para visualización de un gráfico
/// </summary>
public partial class Chart : UserControl
{
	public Chart()
	{
		InitializeComponent();
	}

	/// <summary>
	///		Limpia el gráfico
	/// </summary>
	public void ClearChart()
	{
		wpfPlot.Plot.Clear();
	}

	/// <summary>
	///		Dibuja un gráfico
	/// </summary>
	public void DrawChart(ChartModel chart)
	{
		// Limpia el dibujo
		ClearChart();
		// Configura el canvas
		ConfigureCanvas(wpfPlot, true, true);
		// Dibuja el gráfico
		Draw(wpfPlot, chart);
		// Datos adicionales del gráfico
		wpfPlot.Plot.ShowLegend();
		AssignAxis(wpfPlot, chart);
	}

	/// <summary>
	///		Asigna los ejes
	/// </summary>
	private void AssignAxis(WpfPlot wpfPlot, ChartModel chart)
	{
		switch (chart.Type)
		{
			case ChartModel.ChartType.XY:
					wpfPlot.Plot.Axes.SetLimits(ConvertRectangle(chart.Series.GetLimits()));
				break;
			case ChartModel.ChartType.Pie:
				break;
			case ChartModel.ChartType.Radar:
				break;
		}
	}

	/// <summary>
	///		Convierte un rectángulo
	/// </summary>
	private CoordinateRect ConvertRectangle(RectangleModel rectangle) => new CoordinateRect(rectangle.Left, rectangle.Right, rectangle.Bottom, rectangle.Top);

	/// <summary>
	///		Configura un control de dibujo
	/// </summary>
	private void ConfigureCanvas(WpfPlot wpfPlot, bool panning, bool zoom)
	{
	/*
		wpfPlot.Configuration.Pan = panning;
		wpfPlot.Configuration.RightClickDragZoom = zoom;
		wpfPlot.Configuration.ScrollWheelZoom = zoom;
	*/
	}

	/// <summary>
	///		Dibuja un gráfico
	/// </summary>
	private void Draw(WpfPlot canvas, ChartModel chart)
	{
		// Transforma el modelo en un gráfico
		switch (chart.Type)
		{
			case ChartModel.ChartType.XY:
					CreateXYChart(chart, canvas.Plot);
				break;
			case ChartModel.ChartType.Pie:
					CreatePieChart(chart, canvas.Plot);
				break;
		}
		// Asigna los títulos, leyenda...
		PostProcess(chart, canvas.Plot);
		// Dibuja el gráfico
		canvas.Refresh();
	}

	/// <summary>
	///		Transforma el modelo en un gráfico de barras
	/// </summary>
	private void CreateXYChart(ChartModel chart, Plot plot)
	{
		double offset = 0;
			
			// Normaliza los datos
			chart.Series.Normalize();
			// Añade primero las series de barras
			//DrawBars(chart, plot, offset);
			//foreach (ChartSerieModel serie in chart.Series)
			//	if (serie.Type == ChartSerieModel.ChartSerieType.Bars)
			//	{
			//		StyleModel style = chart.Design.Styles.GetStyle(serie.Style);
			//		double step = style.Width;
			//	}
			// Añade el resto de las series
			foreach (ChartSerieModel serie in chart.Series)
				switch (serie.Type)
				{
					case ChartSerieModel.ChartSerieType.Scatter:
							AddScatterSerie(serie, 0, 1, chart.Design.Styles.GetStyle(serie.Style), plot);
						break;
					case ChartSerieModel.ChartSerieType.Bars:
							AddBarSerie(serie, chart.Design.Styles.GetStyle(serie.Style), plot);
						break;
					case ChartSerieModel.ChartSerieType.Lines:
							AddLineSerie(serie, 0, 1, chart.Design.Styles.GetStyle(serie.Style), plot);
						break;
				}
		//if (chart.Series.CountType(ChartSerieModel.ChartSerieType.Scatter) > 1)
		//	TransformScatterSerie(chart, plot);
		//else if (chart.Series.CountType(ChartSerieModel.ChartSerieType.Bars) > 1)
		//	TransformBarGroup(chart, plot);
		//else
		//	TransformBarSerie(chart, plot);
	}

	///// <summary>
	/////		Dibuja los gráficos de barras
	///// </summary>
	//private double DrawBars(ChartModel chart, Plot plot, double offset)
	//{
	//	int countBars = chart.Series.CountType(ChartSerieModel.ChartSerieType.Bars);
	//	double bars
	//}

	/// <summary>
	///		Trasforma una serie de grupos de barras
	/// </summary>
	private void TransformBarGroup(ChartModel chart, Plot plot)
	{
		int series = chart.Series.CountType(ChartSerieModel.ChartSerieType.Bars);
		int group = 1;
		List<Bar> bars = [];

			// Crea las barras de las series
			foreach (ChartSerieModel serie in chart.Series)
			{
				StyleModel? style = chart.Design.Styles.GetStyle(serie.Style);
				int position = group;

					// Elemento de la serie
					foreach (ChartSeriePointModel item in serie.Items)
					{
						// Añade la barra
						bars.Add(new Bar()
										{
											Position = position,
											Value = item.Y,
											FillColor = GetColor(style, 0)
										}
								);
						// Incrementa la posición
						position += series;
					}
					// Incrementa el número de grupo
					group++;
			}
			// Añade las barras al gráfico
			plot.Add.Bars(bars);
	}

	/// <summary>
	///		Añade una serie con una barra
	/// </summary>
	private void AddBarSerie(ChartSerieModel serie, StyleModel style, Plot plot)
	{
		int index = 0;

			// Añade las barras
			foreach (ChartSeriePointModel point in serie.Items)
				plot.Add.Bar(new Bar()
									{
										Position = point.X,
										Size = style.Width,
										Value = point.Y,
										FillColor = GetColor(style, index++)
									}
							);
	}

	/// <summary>
	///		Transforma el modelo en un gráfico de barras apiladas
	/// </summary>
	private void TransformStackedBars(ChartModel chart, Plot plt)
	{
/*
		double barWidth = (double) (1.0 / chart.Series.Count) - 0.1;
		double offset = -1 * barWidth / chart.Series.Count;

			// Crea los datos del gráfico de barras
			foreach (ChartSerieModel serie in chart.Series)
			{
				double[] ticks = GetXTicks(serie.Items.Count);

					// Añade el gráfico
					plt.PlotBar(ticks, serie.GetYItems(), label: serie.Name, barWidth: barWidth, xOffset: offset, 
								horizontal: chart.Orientation == ChartModel.OrientationType.Horizontal);
					// Incrementa el desplazamiento de la barra
					offset += barWidth;
			}
			// Personaliza el dibujo
			switch (chart.Orientation)
			{
				case ChartModel.OrientationType.Horizontal:
						// Asigna los ejes
						plt.YAxis.SetOffset(0);
						plt.Grid(enable: false, lineStyle: LineStyle.Dot);
						// Aplica los ticks del eje y las etiquetas
						plt.YTicks(chart.GetLabels());
					break;
				case ChartModel.OrientationType.Vertical:
						// Asigna los ejes
						plt.XAxis.SetOffset(0);
						plt.Grid(enable: false, lineStyle: LineStyle.Dot);
						// Aplica los ticks del eje y las etiquetas
						plt.XTicks(chart.GetLabels());
					break;
			}
*/
	}

	/// <summary>
	///		Transforma el modelo en un gráfico de líneas
	/// </summary>
	private void TransformLines(ChartModel chart, Plot plt)
	{
/*
		foreach (ChartSerieModel serie in chart.Series)
			plt.PlotScatter(serie.GetXItems(), serie.GetYItems());
*/
	}

	/// <summary>
	///		Añade una serie de líneas entre puntos
	/// </summary>
	private void AddScatterSerie(ChartSerieModel serie, double start, double step, StyleModel? style, Plot plot)
	{
		double offset = start;

			// Añade las líneas
			for (int index = 0; index < serie.Items.Count; index++)
			{
				// Añade la línea entre puntos
				plot.Add.Scatter(offset, serie.Items[index].Y, GetColor(style, index));
				// Incrementa el desplazamiento de la línea
				offset += step;
			}
	}

	/// <summary>
	///		Añade una serie de líneas
	/// </summary>
	private void AddLineSerie(ChartSerieModel serie, double start, double step, StyleModel? style, Plot plot)
	{
		double offset = start;

			// Añade las líneas
			for (int index = 0; index < serie.Items.Count; index++)
			{
				// Añade la línea
				plot.Add.Scatter(offset, serie.Items[index].Y, GetColor(style, index));
				// Incrementa el desplazamiento de la línea
				offset += step;
			}
	}

	/// <summary>
	///		Transforma el modelo en un gráfico de área
	/// </summary>
	private void TransformArea(ChartModel chart, Plot plt)
	{
/*
		foreach (ChartSerieModel serie in chart.Series)
			plt.PlotFill(serie.GetXItems(), serie.GetYItems(), serie.Name);
*/
	}

	/// <summary>
	///		Transforma un modelo en un gráfico de área entre curvas (sólo funciona con dos series)
	/// </summary>
	private void TransformAreaBetweenCurves(ChartModel chart, Plot plt)
	{
/*
		// Pinta el relleno entre las dos series
		plt.PlotFill(chart.Series[0].GetXItems(), chart.Series[0].GetYItems(),
					 chart.Series[1].GetXItems(), chart.Series[1].GetYItems(), fillAlpha: .5);
		// Pinta las líneas de cada serie
		plt.PlotScatter(chart.Series[0].GetXItems(), chart.Series[0].GetYItems(), System.Drawing.Color.Black);
		plt.PlotScatter(chart.Series[1].GetXItems(), chart.Series[1].GetYItems(), System.Drawing.Color.Black);
*/
	}

	/// <summary>
	///		Transforma un gráfico de tarta
	/// </summary>
	private void CreatePieChart(ChartModel chart, Plot plt)
	{
		// Añade las series
		foreach (ChartSerieModel serie in chart.Series)
		{
			StyleModel? style = chart.Design.Styles.GetStyle(serie.Style);
			int index = 0;
			List<PieSlice> slices = new();
				
				// Añade los trozos de tarta
				foreach (ChartSeriePointModel item in serie.Items)
					slices.Add(new PieSlice()
										{
											FillColor = GetColor(style, index++),
											Value = item.Y
										}
								);
				// Añade las series al gráfico
				plt.Add.Pie(slices);
		}
		/*
		pie.ExplodeFraction = .1;
		*/
	}

	/// <summary>
	///		Obtiene un color de un estilo
	/// </summary>
	private Color GetColor(StyleModel? style, int index)
	{
		if (style is null)
			return Convert(System.Drawing.Color.RebeccaPurple);
		else
			return Convert(style.Pallete[index % style.Pallete.Count]);

		// Convierte System.Drawing.Color en Color
		Color Convert(System.Drawing.Color color) => Color.FromARGB((uint) color.ToArgb());
	}

	/// <summary>
	///		Transforma un gráfico de radar
	/// </summary>
	private void TransformRadar(ChartModel chart, Plot plt)
	{
/*
		double [ , ] values = new double[chart.Series.Count, chart.Labels.Count];
		string [] groups = new string[chart.Series.Count];

			// Asigna los nombres de los grupos
			for (int index = 0; index < chart.Series.Count; index++)
				groups[index] = chart.Series[index].Name;
			// Asigna los valores
			for (int serie = 0; serie < chart.Series.Count; serie++)
				for (int index = 0; index < chart.Series[serie].Items.Count; index++)
					values[serie, index] = chart.Series[serie].Items[index].Y;
			// Dibja el radar
			plt.PlotRadar(values, chart.GetLabels(), groups);
*/
	}

	/// <summary>
	///		Crea un gráfico de heatmap
	/// </summary>
	private void TransformHeatmap(ChartModel chart, Plot plt)
	{
/*
		double [ , ] values = new double[chart.Series.Count, chart.Labels.Count];

			// Asigna los valores
			for (int serie = 0; serie < chart.Series.Count; serie++)
				for (int index = 0; index < chart.Series[serie].Items.Count; index++)
					values[serie, index] = chart.Series[serie].Items[index].Y;
			// Dibuja el gráfico
			plt.PlotHeatmap(values);
*/
	}

	/// <summary>
	///		Postproceso del gráfico: leyenda, textos...
	/// </summary>
	private void PostProcess(ChartModel chart, Plot plot)
	{
		// Asigna la leyenda
		if (chart.Legend.Location != ChartLegendModel.LocationType.None)
			plot.ShowLegend(ConvertLocation(chart.Legend.Location));
		// Asigna los títulos
		plot.Title(chart.Title);
		// Dependiendo de si es una tarta o no, muestra las etiquetas y el grid
		if (chart.Type == ChartModel.ChartType.Pie || chart.Type == ChartModel.ChartType.Radar)
		{
			plot.HideGrid();
			/*
			plot.Frameless(false);
			plot.XAxis.Ticks(false);
			plot.YAxis.Ticks(false);
			*/
		}
		else
		{
			plot.ShowGrid();
			// Etiquetas de los ejes
			/*
			plot.XLabel(chart.XLabel);
			plot.YLabel(chart.YLabel);
			*/
			// Grid
			/*
			plot.XAxis.ManualTickSpacing(2);
			plot.XAxis.Line(true, System.Drawing.Color.Black, 2);
			plot.YAxis.ManualTickSpacing(2);
			plot.YAxis.Line(true, System.Drawing.Color.Black, 2);
			*/
		}
		// Colores de fondo del gráfico y la barra
		// plot.Style(figureBackground: System.Drawing.Color.LightBlue, dataBackground: System.Drawing.Color.LightYellow);
	}

	/// <summary>
	///		Convierte la ubicación de la leyenda
	/// </summary>
	private Alignment ConvertLocation(ChartLegendModel.LocationType location)
	{
		return location switch
					{
						ChartLegendModel.LocationType.LowerCenter => Alignment.LowerCenter,
						ChartLegendModel.LocationType.LowerLeft => Alignment.LowerLeft,
						ChartLegendModel.LocationType.LowerRight => Alignment.LowerRight,
						ChartLegendModel.LocationType.MiddleLeft => Alignment.MiddleLeft,
						ChartLegendModel.LocationType.MiddleRight => Alignment.MiddleRight,
						ChartLegendModel.LocationType.UpperCenter => Alignment.UpperCenter,
						ChartLegendModel.LocationType.UpperLeft => Alignment.UpperLeft,
						ChartLegendModel.LocationType.UpperRight => Alignment.UpperRight,
						_ => Alignment.UpperLeft,
					};
	}

	/// <summary>
	///		Obtiene los ticks del eje horizontal
	/// </summary>
	private double[] GetXTicks(int count)
	{
		double[] ticks = new double[count];

			// Asigna los elementos
			for (int index = 0; index < count; index++)
				ticks[index] = index;
			// Devuelve la lista de ticks
			return ticks;
	}
}