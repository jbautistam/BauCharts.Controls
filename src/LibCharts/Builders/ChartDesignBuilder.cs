using System.Drawing;
using Bau.Libraries.LibCharts.Models.Design;

namespace Bau.Libraries.LibCharts.Builders;

/// <summary>
///		Generador del diseño del gráfico
/// </summary>
public class ChartDesignBuilder
{
	public ChartDesignBuilder(ChartBuilder builder, DesignModel design)
	{
		Builder = builder;
		Design = design;
	}

	/// <summary>
	///		Añade un color a una paleta
	/// </summary>
	public ChartDesignBuilder WithColor(string name, Color color)
	{
		// Añade los colores de la paleta
		Design.Styles.Add(name).Pallete.Add(color);
		// Devuelve el generador
		return this;
	}

	/// <summary>
	///		Vuelve al generador padre
	/// </summary>
	public ChartBuilder Back() => Builder;

	/// <summary>
	///		Generador padre
	/// </summary>
	public ChartBuilder Builder { get; }

	/// <summary>
	///		Diseño
	/// </summary>
	public Models.Design.DesignModel Design { get; }
}
