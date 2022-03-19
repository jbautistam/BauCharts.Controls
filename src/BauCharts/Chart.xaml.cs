using System;
using System.Windows.Controls;

using ScottPlot;
using Bau.Libraries.LibCharts.Models;

namespace Bau.Controls.BauCharts
{
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
			wpfPlot.plt.Clear();
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
		}

		/// <summary>
		///		Configura un control de dibujo
		/// </summary>
		private void ConfigureCanvas(WpfPlot wpfPlot, bool panning, bool zoom)
		{
			wpfPlot.Configuration.Pan = panning;
			wpfPlot.Configuration.RightClickDragZoom = zoom;
			wpfPlot.Configuration.ScrollWheelZoom = zoom;
		}

		/// <summary>
		///		Dibuja un gráfico
		/// </summary>
		private void Draw(WpfPlot canvas, ChartModel chart)
		{
			// Transforma el modelo en un gráfico
			switch (chart.Type)
			{
				case ChartModel.ChartType.Bars:
						TransformBars(chart, canvas.plt);
					break;
				case ChartModel.ChartType.StackedBar:
						TransformStackedBars(chart, canvas.plt);
					break;
				case ChartModel.ChartType.Lines:
						TransformLines(chart, canvas.plt);
					break;
				case ChartModel.ChartType.Areas:
						TransformArea(chart, canvas.plt);
					break;
				case ChartModel.ChartType.AreasBetweenCurve:
						TransformAreaBetweenCurves(chart, canvas.plt);
					break;
				case ChartModel.ChartType.Pie:
						TransformPie(chart, canvas.plt);
					break;
				case ChartModel.ChartType.Radar:
						TransformRadar(chart, canvas.plt);
					break;
				case ChartModel.ChartType.Heatmap:
						TransformHeatmap(chart, canvas.plt);
					break;
			}
			// Asigna los títulos, leyenda...
			PostProcess(chart, canvas.plt);
			// Propiedades de dibujo
			// Dibuja el gráfico
			canvas.Render();
		}

		/// <summary>
		///		Transforma el modelo en un gráfico de barras
		/// </summary>
		private void TransformBars(ChartModel chart, Plot plt)
		{
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
		}

		/// <summary>
		///		Transforma el modelo en un gráfico de barras apiladas
		/// </summary>
		private void TransformStackedBars(ChartModel chart, Plot plt)
		{
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
		}

		/// <summary>
		///		Transforma el modelo en un gráfico de líneas
		/// </summary>
		private void TransformLines(ChartModel chart, Plot plt)
		{
			foreach (ChartSerieModel serie in chart.Series)
				plt.PlotScatter(serie.GetXItems(), serie.GetYItems());
		}

		/// <summary>
		///		Transforma el modelo en un gráfico de área
		/// </summary>
		private void TransformArea(ChartModel chart, Plot plt)
		{
			foreach (ChartSerieModel serie in chart.Series)
				plt.PlotFill(serie.GetXItems(), serie.GetYItems(), serie.Name);
		}

		/// <summary>
		///		Transforma un modelo en un gráfico de área entre curvas (sólo funciona con dos series)
		/// </summary>
		private void TransformAreaBetweenCurves(ChartModel chart, Plot plt)
		{
			// Pinta el relleno entre las dos series
			plt.PlotFill(chart.Series[0].GetXItems(), chart.Series[0].GetYItems(),
						 chart.Series[1].GetXItems(), chart.Series[1].GetYItems(), fillAlpha: .5);
			// Pinta las líneas de cada serie
			plt.PlotScatter(chart.Series[0].GetXItems(), chart.Series[0].GetYItems(), System.Drawing.Color.Black);
			plt.PlotScatter(chart.Series[1].GetXItems(), chart.Series[1].GetYItems(), System.Drawing.Color.Black);
		}

		/// <summary>
		///		Transforma un gráfico de tarta
		/// </summary>
		private void TransformPie(ChartModel chart, Plot plt)
		{
			plt.PlotPie(chart.Series[0].GetYItems(), chart.GetLabels());
		}

		/// <summary>
		///		Transforma un gráfico de radar
		/// </summary>
		private void TransformRadar(ChartModel chart, Plot plt)
		{
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
		}

		/// <summary>
		///		Crea un gráfico de heatmap
		/// </summary>
		private void TransformHeatmap(ChartModel chart, Plot plt)
		{
			double [ , ] values = new double[chart.Series.Count, chart.Labels.Count];

				// Asigna los valores
				for (int serie = 0; serie < chart.Series.Count; serie++)
					for (int index = 0; index < chart.Series[serie].Items.Count; index++)
						values[serie, index] = chart.Series[serie].Items[index].Y;
				// Dibuja el gráfico
				plt.PlotHeatmap(values);
		}

		/// <summary>
		///		Postproceso del gráfico: leyenda, textos...
		/// </summary>
		private void PostProcess(ChartModel chart, Plot plt)
		{
			// Asigna la leyenda
			plt.Legend(location: ConvertLocation(chart.LegendLocation));
			// Asigna los títulos
			plt.Title(chart.Title);
			// Dependiendo de si es una tarta o no, muestra las etiquetas y el grid
			if (chart.Type == ChartModel.ChartType.Pie || chart.Type == ChartModel.ChartType.Radar)
			{
				plt.Grid(false);
				plt.Frameless(false);
				plt.XAxis.Ticks(false);
				plt.YAxis.Ticks(false);
			}
			else
			{
				// Etiquetas de los ejes
				plt.XLabel(chart.XLabel);
				plt.YLabel(chart.YLabel);
				// Grid
				plt.XAxis.ManualTickSpacing(2);
				plt.XAxis.Line(true, System.Drawing.Color.Black, 2);
				plt.YAxis.ManualTickSpacing(2);
				plt.YAxis.Line(true, System.Drawing.Color.Black, 2);
			}
			// Colores de fondo del gráfico y la barra
			plt.Style(figureBackground: System.Drawing.Color.LightBlue, dataBackground: System.Drawing.Color.LightYellow);
		}

		/// <summary>
		///		Convierte la ubicación de la leyenda
		/// </summary>
		private Alignment ConvertLocation(ChartModel.LegendLocationType location)
		{
			switch (location)
			{
				case ChartModel.LegendLocationType.LowerCenter:
					return Alignment.LowerCenter;
				case ChartModel.LegendLocationType.LowerLeft:
					return Alignment.LowerLeft;
				case ChartModel.LegendLocationType.LowerRight:
					return Alignment.LowerRight;
				case ChartModel.LegendLocationType.MiddleLeft:
					return Alignment.MiddleLeft;
				case ChartModel.LegendLocationType.MiddleRight:
					return Alignment.MiddleRight;
				case ChartModel.LegendLocationType.UpperCenter:
					return Alignment.UpperCenter;
				case ChartModel.LegendLocationType.UpperLeft:
					return Alignment.UpperLeft;
				case ChartModel.LegendLocationType.UpperRight:
					return Alignment.UpperRight;
				default:
					return Alignment.UpperLeft;
			}
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
}
