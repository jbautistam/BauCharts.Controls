namespace Bau.Libraries.LibCharts.Models.Design;

/// <summary>
///		Datos de un estilo
/// </summary>
public class StyleModel
{
	/// <summary>
	///		Crea una paleta
	/// </summary>
	public void CreatePallete(DesignModel.DefaultStyles style)
	{
		// Limpia la paleta
		Pallete.Clear();
		// Añade los colores
		switch (style)
		{
			case DesignModel.DefaultStyles.Secondary:
					CreateSecondaryPallete();
				break;
			default:
					CreateDefaultPallete();
				break;
		};
	}

	/// <summary>
	///		Crea la paleta predeterminada
	/// </summary>
	private void CreateDefaultPallete()
	{
		Pallete.Add(System.Drawing.Color.Red);
		Pallete.Add(System.Drawing.Color.Yellow);
		Pallete.Add(System.Drawing.Color.Gray);
		Pallete.Add(System.Drawing.Color.Green);
		Pallete.Add(System.Drawing.Color.Honeydew);
		Pallete.Add(System.Drawing.Color.Pink);
		Pallete.Add(System.Drawing.Color.DeepPink);
		Pallete.Add(System.Drawing.Color.ForestGreen);
		Pallete.Add(System.Drawing.Color.Gold);
	}

	/// <summary>
	///		Crea la paleta secundaria
	/// </summary>
	private void CreateSecondaryPallete()
	{
		Pallete.Add(System.Drawing.Color.Navy);
		Pallete.Add(System.Drawing.Color.Gainsboro);
		Pallete.Add(System.Drawing.Color.LightBlue);
		Pallete.Add(System.Drawing.Color.MediumSeaGreen);
	}

	/// <summary>
	///		Ancho
	/// </summary>
	public double Width { get; set; } = 50;

	/// <summary>
	///		Paleta de colores
	/// </summary>
	public List<System.Drawing.Color> Pallete { get; } = [];
}
