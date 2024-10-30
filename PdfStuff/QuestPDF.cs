using QuestPDF;
using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace PdfStuff;

public static class QuestPDF
{
    public static void Generate()
    {
        Settings.License = LicenseType.Community;

        FontManager.RegisterFont(File.OpenRead("/home/mike/.local/share/fonts/fonts/ttf/JetBrainsMono-Regular.ttf"));

        var doc = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(2, Unit.Centimetre);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(20));

                page.Header()
                    .Text("Hello PDF!!!")
                    .SemiBold().FontSize(36).FontColor(Colors.Blue.Medium)
                    .FontFamily("JetBrains Mono");

                page.Content()
                    .PaddingVertical(1, Unit.Centimetre)
                    .Column(x =>
                    {
                        x.Item().Background(Colors.Blue.Medium)
                            .Padding(20)
                            .Text(Placeholders.LoremIpsum())
                            .FontSize(10);
                        x.Spacing(20);

                        x.Item().Text(Placeholders.LoremIpsum()).FontSize(10).FontFamily("JetBrains Mono");
                        x.Item().Image(Placeholders.Image(200, 100));
                    });

                page.Footer()
                    .AlignCenter()
                    .Text(x =>
                    {
                        x.Span("Page ");
                        x.CurrentPageNumber();
                    });
            });
        });
        doc.GeneratePdf("hello.pdf");
    }
}