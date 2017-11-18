using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Augustine.ScreenDimmer
{
    internal class GlobalHotkeyParser
	{
		internal static GlobalHotKey Parse(string rawValue)
		{
			KeyModifiers modifiers = KeyModifiers.NONE;
			Keys key = Keys.None;
			string trimmed = rawValue.Trim();
            string description = "";
            var splitted = trimmed.Split(',');
            if (splitted.Length >= 2) {
                description = splitted[1];
            } 
			var keys = splitted[0].Split('+');
			foreach (var item in keys)
			{
				// modifiers
				switch (item.ToLowerInvariant())
				{
					case "alt":
						modifiers |= KeyModifiers.MOD_ALT;
						break;
					case "ctrl":
						modifiers |= KeyModifiers.MOD_CONTROL;
						break;
					case "shift":
						modifiers |= KeyModifiers.MOD_SHIFT;
						break;
					case "win":
						modifiers |= KeyModifiers.MOD_WIN;
						break;
					default:
						// key
						if (!Enum.TryParse<Keys>(item, true, out key))
						{
                            int numericValue;
							// try to parse numeric value
							if (int.TryParse(item, out numericValue))
							{
								if (Enum.IsDefined(typeof(Keys), numericValue))
								{
									key = (Keys)numericValue;
								}
							}
						}
						break;
				}

			}
			return new GlobalHotKey(modifiers, key, description);
		}
	}
}
