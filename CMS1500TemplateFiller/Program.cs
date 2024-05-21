using PdfSharp.Fonts;
using PdfSharp.Pdf;
using PdfSharp.Pdf.AcroForms;
using PdfSharp.Pdf.IO;
using System;

namespace CMS1500TemplateFiller
{
	class Program
	{
		static void Main(string[] args)
		{
			GlobalFontSettings.FontResolver = new CustomFontResolver();

			string templatePath = @"C:\source\CMS1500TemplateFiller\CMS1500TemplateFiller\pdf\template.pdf"; // Path to your PDF template
			string outputPath = @"C:\source\CMS1500TemplateFiller\CMS1500TemplateFiller\pdf\output.pdf"; // Path for the output PDF

			// Load the PDF document
			PdfDocument document = PdfReader.Open(templatePath, PdfDocumentOpenMode.Modify);

			// Check if the document has an AcroForm
			if (document.AcroForm != null)
			{
				//Export out all fields in acroform
				//foreach (var field in document.AcroForm.Fields)
				//{
				//	Console.WriteLine(field);
				//}

				// Ensure the form fields are not read-only
				document.AcroForm.Elements.SetBoolean("/NeedAppearances", true);

				// Get the text field by name
				PdfTextField firstNameField = (PdfTextField)(document.AcroForm.Fields["pt_name"]);

				if (firstNameField != null)
				{
					// Set the value of the text field
					firstNameField.Value = new PdfString("John Doe");
				}
				else
				{
					Console.WriteLine("Text field 'Patient Name' not found.");
				}
			}
			else
			{
				Console.WriteLine("No AcroForm found in the document.");
			}

			document.Save(outputPath);
			Console.WriteLine("PDF saved to " + outputPath);

		}
	}
}
