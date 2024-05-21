using PdfSharp.Fonts;
using System;
using System.IO;
using System.Reflection;

namespace CMS1500TemplateFiller
{
	public class CustomFontResolver : IFontResolver
	{
		public byte[] GetFont(string faceName)
		{
			string resource = null;

			switch (faceName)
			{
				case "CourierNew":
					resource = "CMS1500TemplateFiller.font.cour.ttf"; //Update with your namespace and font file path, make sure the font is embedded resource
					break;
					// Add more cases for other fonts as needed
			}

			if (resource != null)
			{
				using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resource))
				{
					if (stream == null)
						throw new FileNotFoundException($"Font resource '{resource}' not found.");

					byte[] fontData = new byte[stream.Length];
					stream.Read(fontData, 0, fontData.Length);
					return fontData;
				}
			}

			throw new ArgumentException($"Font '{faceName}' not found.");
		}

		public FontResolverInfo ResolveTypeface(string familyName, bool isBold, bool isItalic)
		{
			// Map your font family to internal PDFsharp font names
			if (familyName == "Courier New")
			{
				if (isBold)
				{
					if (isItalic)
						return new FontResolverInfo("CourierNew-BoldItalic");
					return new FontResolverInfo("CourierNew-Bold");
				}

				if (isItalic)
					return new FontResolverInfo("CourierNew-Italic");

				return new FontResolverInfo("CourierNew");
			}

			// Handle more font families as needed
			throw new ArgumentException($"Font family '{familyName}' not found.");
		}
	}
}