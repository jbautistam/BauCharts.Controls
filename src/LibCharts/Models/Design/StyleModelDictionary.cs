namespace Bau.Libraries.LibCharts.Models.Design;

/// <summary>
///		Diccionario de <see cref="StyleModel"/>
/// </summary>
public class StyleModelDictionary
{
	/// <summary>
	///		Añade un <see cref="StyleModel"/> al diccionario
	/// </summary>
	public StyleModel Add(string name)
	{
		// Si no existe, añade el estilo, si existe, lo devuelve
		if (!Styles.TryGetValue(name, out StyleModel? style)) // ... no utiliza el GetStyle porque sería una llamada infinita
		{
			// Asigna el estilo
			style = new StyleModel();
			// Añade el estilo a la colección
			Styles.Add(name, style);
		}
		// Devuelve el estilo
		return style;
	}

	/// <summary>
	///		Añade un <see cref="StyleModel"/> al diccionario
	/// </summary>
	public StyleModel Add(string name, StyleModel style)
	{
		// Si no existe, añade el estilo, si existe, lo devuelve
		if (Styles.ContainsKey(name))
			Styles[name] = style;
		else
			Styles.Add(name, style);
		// Devuelve el estilo
		return style;
	}

	/// <summary>
	///		Obtiene el <see cref="StyleModel"/> del diccionario o el estilo predeterminado
	/// </summary>
	public StyleModel GetStyle(string name)
	{
		// Añade el estilo predeterminado si no existe
		if (Styles.Count == 0 || !Styles.ContainsKey(DesignModel.Default))
		{
			StyleModel style = Add(DesignModel.Default);

				// Crea el estilo
				style.CreatePallete(DesignModel.DefaultStyles.Default);
		}
		// Obtiene el estilo
		if (Styles.ContainsKey(name))
			return Styles[name];
		else 
			return Styles[DesignModel.Default];
	}

	/// <summary>
	///		Indizador de estilos
	/// </summary>
	public StyleModel? this[string name]
	{
		get { return GetStyle(name); }
		set 
		{ 
			if (value is not null)
				Add(name, value); 
		}
	}

	/// <summary>
	///		Diccionario de <see cref="StyleModel"/>
	/// </summary>
	private Dictionary<string, StyleModel> Styles { get; } = new(StringComparer.CurrentCultureIgnoreCase);
}
