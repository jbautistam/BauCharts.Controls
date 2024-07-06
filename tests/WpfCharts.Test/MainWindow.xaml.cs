using System.Windows;

using Bau.Libraries.LibCharts.Builders;
using Bau.Libraries.LibCharts.Models;
using Bau.Libraries.LibCharts.Models.Design;

namespace WpfCharts.Test;

/// <summary>
///		Ventana de pruebas
/// </summary>
public partial class MainWindow : Window
{
	// Variables privadas
	private Random _rnd = new();

	public MainWindow()
	{
		InitializeComponent();
	}

	/// <summary>
	///		Muestra un gráfico de tarta
	/// </summary>
	private void ShowChart()
	{
		if (Chart is not null)
			chrtGraph.DrawChart(Chart);
	}

	/// <summary>
	///		Genera un gráfico de barras
	/// </summary>
	private void GenerateBars()
	{
		ChartBuilder builder = new("Sample bars", ChartModel.ChartType.XY, ChartModel.OrientationType.Vertical);

			// Genera una serie
			Chart = builder
						.WithSerie("First", DesignModel.Default, ChartSerieModel.ChartSerieType.Bars)
								.WithRandomItems(10, 1, 100)
							.Back()
					.Build();
			// Muestra el gráfico
			ShowChart();
	}

	/// <summary>
	///		Genera un grupo de gráfico de barras
	/// </summary>
	private void GenerateBarGroups()
	{
		ChartBuilder builder = new("Sample bar groups", ChartModel.ChartType.XY, ChartModel.OrientationType.Vertical);

			// Genera una serie
			Chart = builder
						.WithSerie("First", DesignModel.Default, ChartSerieModel.ChartSerieType.Bars)
								.WithRandomItems(10, 1, 100)
							.Back()
						.WithSerie("Second", DesignModel.Secondary, ChartSerieModel.ChartSerieType.Bars)
								.WithRandomItems(10, 1, 100)
							.Back()
						.WithSerie("Terd", DesignModel.Secondary, ChartSerieModel.ChartSerieType.Lines)
								.WithRandomItems(10, 1, 100)
							.Back()
					.Build();
			// Muestra el gráfico
			ShowChart();
	}

	/// <summary>
	///		Genera un gráfico de untos
	/// </summary>
	private void GenerateScatter()
	{
		ChartBuilder builder = new("Sample bars", ChartModel.ChartType.XY, ChartModel.OrientationType.Vertical);

			// Genera una serie
			Chart = builder
						.WithSerie("First", DesignModel.Default, ChartSerieModel.ChartSerieType.Scatter)
								.WithRandomItems(10, 1, 100)
							.Back()
					.Build();
			// Muestra el gráfico
			ShowChart();
	}

	/// <summary>
	///		Genera un gráfico de tarta
	/// </summary>
	private void GeneratePie()
	{
		ChartBuilder builder = new("Sample pie", ChartModel.ChartType.Pie, ChartModel.OrientationType.Horizontal);

			// Genera una serie
			Chart = builder
						.WithSerie("First", DesignModel.Default, ChartSerieModel.ChartSerieType.Bars)
								.WithRandomItems(10, 1, 100)
							.Back()
					.Build();
			// Muestra el gráfico
			ShowChart();
	}

	/// <summary>
	///		Genera un gráfico de tarta
	/// </summary>
	private void GeneratePieWithSeries()
	{
		ChartBuilder builder = new("Sample pie with series", ChartModel.ChartType.Pie, ChartModel.OrientationType.Horizontal);

			// Genera una serie
			Chart = builder
						.WithSerie("First", DesignModel.Default, ChartSerieModel.ChartSerieType.Pie)
								.WithRandomItems(10, 1, 100)
							.Back()
						.WithSerie("Second", DesignModel.Secondary, ChartSerieModel.ChartSerieType.Pie)
								.WithRandomItems(6, 1, 100)
							.Back()
					.Build();
			// Muestra el gráfico
			ShowChart();
	}

	/// <summary>
	///		Comprueba los datos
	/// </summary>
	private bool ValidateData()
	{
		bool validated = false;

			// Comprueba los datos de la serie
			if (string.IsNullOrWhiteSpace(txtSerieTitle.Text))
				MessageBox.Show("Introduzca el título de la serie");
			else if (string.IsNullOrWhiteSpace(cboSerieType.Text))
				MessageBox.Show("Seleccione el tipo de serie");
			else
				validated = true;
			// Devuelve el valor que indica si los datos son correctos
			return validated;
	}

	/// <summary>
	///		Añade una serie
	/// </summary>
	private void AddSerie()
	{
		if (ValidateData())
		{
			// Genera el gráfico
			if (Chart is null)
			{
				// Genera el gráfico
				Chart = new ChartModel(ChartModel.ChartType.XY);
				// Añade los estilos
				Chart.Design.Styles.Add("Default").CreatePallete(DesignModel.DefaultStyles.Default);
				Chart.Design.Styles.Add("Secondary").CreatePallete(DesignModel.DefaultStyles.Secondary);
				Chart.Design.Styles.Add("OneColorRed").Pallete.Add(System.Drawing.Color.Red);
				Chart.Design.Styles.Add("OneColorGreen").Pallete.Add(System.Drawing.Color.Green);
			}
			// Asigna las propiedades del gráfico
			if (!string.IsNullOrWhiteSpace(txtChartTitle.Text))
				Chart.Title = txtChartTitle.Text;
			// Añade la serie
			Chart.Series.Add(GetSerie(cboStyle.Text));
		}
	}

	/// <summary>
	///		Obtiene una serie
	/// </summary>
	private ChartSerieModel GetSerie(string style)
	{
		ChartSerieModel serie = new(txtSerieTitle.Text, style, GetSerieType());

			// Añade los elementos a la serie
			AddRandomItems(serie, 6, 3, 20);
			// Devuelve la serie
			return serie;

		// Obtiene el tipo de la serie
		ChartSerieModel.ChartSerieType GetSerieType()
		{
			// Obtiene el tipo de serie del combo
			if (!string.IsNullOrWhiteSpace(cboSerieType.Text))
			{
				if (cboSerieType.Text.Equals("Bar", StringComparison.CurrentCultureIgnoreCase))
					return ChartSerieModel.ChartSerieType.Bars;
				else if (cboSerieType.Text.Equals("Line", StringComparison.CurrentCultureIgnoreCase))
					return ChartSerieModel.ChartSerieType.Pie;
				else if (cboSerieType.Text.Equals("Scatter", StringComparison.CurrentCultureIgnoreCase))
					return ChartSerieModel.ChartSerieType.Scatter;
				else if (cboSerieType.Text.Equals("Line", StringComparison.CurrentCultureIgnoreCase))
					return ChartSerieModel.ChartSerieType.Pie;
			}
			// Si ha llegado hasta aquí es porque no ha encontrado nada
			return ChartSerieModel.ChartSerieType.Bars;
		}
	}

	/// <summary>
	///		Añade una serie de elementos aleatorios a una serie
	/// </summary>
	private void AddRandomItems(ChartSerieModel serie, int points, double minValue, double maxValue)
	{
		for (int index = 0; index < points; index++)
			serie.Items.Add(new ChartSeriePointModel
									{
										X = index,
										Y = minValue + _rnd.NextDouble() * maxValue
									}
							);
	}

	/// <summary>
	///		Limpia las series
	/// </summary>
	private void ClearSeries()
	{
		if (Chart is not null)
			Chart.Series.Clear();
	}

	private void cmdBars_Click(object sender, RoutedEventArgs e)
	{
		GenerateBars();
	}

	private void cmdBarsGroup_Click(object sender, RoutedEventArgs e)
	{
		GenerateBarGroups();
	}

	private void cmdPie_Click(object sender, RoutedEventArgs e)
	{
		GeneratePie();
	}

	private void cmdPie2_Click(object sender, RoutedEventArgs e)
	{
		GeneratePieWithSeries();
	}

	private void cmdScatter_Click(object sender, RoutedEventArgs e)
	{
		GenerateScatter();
	}

	private void cmdAddSerie_Click(object sender, RoutedEventArgs e)
	{
		AddSerie();
		ShowChart();
    }

	private void cmdClearSeries_Click(object sender, RoutedEventArgs e)
	{
		ClearSeries();
		ShowChart();
	}

	/// <summary>
	///		Datos del gráfico actual
	/// </summary>
	private ChartModel? Chart { get; set; }
}