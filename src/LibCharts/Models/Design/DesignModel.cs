namespace Bau.Libraries.LibCharts.Models.Design;

/// <summary>
///		Datos de diseño
/// </summary>
public class DesignModel
{
	// Constantes públicas
	public const string Default = nameof(Default);
	public const string Secondary = nameof(Secondary);
	// Enumerados públics
	public enum DefaultStyles
	{
		Default = 1,
		Secondary
	}

	/// <summary>
	///		Estilos
	/// </summary>
	public StyleModelDictionary Styles { get; } = new();
}
