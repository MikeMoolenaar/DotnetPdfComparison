using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes;
using MigraDoc.Rendering;
using PdfSharp.Drawing;
using PdfSharp.Fonts;
using PdfSharp.Pdf;
using QuestPDF.Helpers;
using Colors = MigraDoc.DocumentObjectModel.Colors;

namespace PdfStuff;

public static class PdfSharp
{
    public static void Generate()
    {
        GlobalFontSettings.FontResolver = new CustomFontResolver();
        var document = new Document();
        
        document.Info.Title = "Created with PDFsharp";
        var page = document.AddSection();
        var p = page.AddParagraph("Hello PDF!!!");
        p.Format.Font.Size = new Unit(36, UnitType.Point);
        p.Format.Font.Bold = true;
        p.Format.Font.Color = Colors.SlateBlue;
        p.Format.SpaceAfter = new Unit(1, UnitType.Centimeter);

        var frame = page.AddTextFrame();
        frame.FillFormat.Color = Colors.SlateBlue;
        frame.Margin = new Unit(20, UnitType.Point);
        frame.Width = new Unit(16, UnitType.Centimeter);
        frame.Height = new Unit(5, UnitType.Centimeter);
        var pFrame = frame.AddParagraph(Placeholders.LoremIpsum());
        pFrame.Format.Font.Size = new Unit(10, UnitType.Point);

        var p2 = page.AddParagraph(Placeholders.LoremIpsum());
        p2.Format.Font.Size = new Unit(10, UnitType.Point);
        p2.Format.SpaceBefore = new Unit(1, UnitType.Centimeter);
        p2.Format.SpaceAfter = new Unit(1, UnitType.Centimeter);

        var image = page.AddImage("base64:" + Convert.ToBase64String(Placeholders.Image(200, 100)));
        image.Width = new Unit(16, UnitType.Centimeter);
        image.Height = new Unit(10, UnitType.Centimeter);
        
        var style = document.Styles[StyleNames.Normal]!;
        style.Font.Name = "JetBrains Mono";
        var pdfRenderer = new PdfDocumentRenderer
        {
            Document = document,
            PdfDocument =
            {
                PageLayout = PdfPageLayout.SinglePage,
                ViewerPreferences =
                {
                    FitWindow = true
                }
            }
        };

        pdfRenderer.RenderDocument();
        pdfRenderer.PdfDocument.Save("hello2.pdf");
    }
}

internal class CustomFontResolver : IFontResolver
{
    public FontResolverInfo? ResolveTypeface(string familyName, bool isBold, bool isItalic)
    {
        return new FontResolverInfo(familyName, isBold, isItalic);
    }

    public byte[]? GetFont(string faceName)
    {
        var stream = File.Open("/home/mike/.local/share/fonts/fonts/ttf/JetBrainsMonoNL-Regular.ttf", FileMode.Open);

        var bytes = new byte[stream.Length];
        stream.Read(bytes, 0, bytes.Length);
        return bytes;
    }
}