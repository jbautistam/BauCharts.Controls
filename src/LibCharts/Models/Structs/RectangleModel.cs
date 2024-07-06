namespace Bau.Libraries.LibCharts.Models.Structs;

/// <summary>
///		Datos de un rectángulo
/// </summary>
public struct RectangleModel
{
	public RectangleModel(double left, double top, double right, double bottom)
	{
		Left = left;
		Top = top;
		Right = right;
		Bottom = bottom;
	}

	public RectangleModel(PointModel start, PointModel end)
	{
		// Guarda las posiciones izquierda y derecha
		if (start.X < end.X)
		{
			Left = start.X;
			Right = end.X;
		}
		else
		{
			Left = end.X;
			Right = start.X;
		}
		// Guarda las posiciones superior e inferior
		if (start.Y > end.Y)
		{
			Top = start.Y;
			Bottom = end.Y;
		}
		else
		{
			Top = end.Y;
			Bottom = start.Y;
		}
	}

	/// <summary>
	///		Posición izquierda
	/// </summary>
	public double Left { get; }

	/// <summary>
	///		Posición superior
	/// </summary>
	public double Top { get; }

	/// <summary>
	///		Posición derecha
	/// </summary>
	public double Right { get; }

	/// <summary>
	///		Posición inferior
	/// </summary>
	public double Bottom { get; }

	/// <summary>
	///		Ancho del rectángulo
	/// </summary>
	public double Width => Right - Left;

	/// <summary>
	///		Altura del rectángulo
	/// </summary>
	public double Height => Top - Bottom;
}
