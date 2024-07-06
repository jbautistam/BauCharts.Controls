namespace Bau.Libraries.LibCharts.Models.Structs;

/// <summary>
///		Datos de un punto
/// </summary>
public struct PointModel
{
	public PointModel(double x, double y)
	{
		X = x;
		Y = y;
	}

	/// <summary>
	///		Punto X
	/// </summary>
	public double X { get; }

	/// <summary>
	///		Punto Y
	/// </summary>
	public double Y { get; }
}
