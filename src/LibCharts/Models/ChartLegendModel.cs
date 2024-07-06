namespace Bau.Libraries.LibCharts.Models;

/// <summary>
///		Clase con los datos de la leyenda
/// </summary>
public class ChartLegendModel
{
	/// <summary>
	///		Ubicación de la leyenda
	/// </summary>
	public enum LocationType
	{
		/// <summary>Sin leyenda</summary>
		None,
		/// <summary>Inferior centrada</summary>
		LowerCenter,
		/// <summary>Inferior izquierda</summary>
		LowerLeft,
		/// <summary>Inferior derecha</summary>
		LowerRight,
		/// <summary>Izquierda centrada</summary>
		MiddleLeft,
		/// <summary>Derecha centrada</summary>
		MiddleRight,
		/// <summary>Superior centrada</summary>
		UpperCenter,
		/// <summary>Superior izquierda</summary>
		UpperLeft,
		/// <summary>Superior derecha</summary>
		UpperRight
	}

	/// <summary>
	///		Ubicación
	/// </summary>
	public LocationType Location { get; set; } = LocationType.None;

	/// <summary>
	///		Indica si se debe mostrar la leyenda
	/// </summary>
	public bool Visible { get; set; }
}
