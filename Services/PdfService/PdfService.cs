using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;
using TestProgrammasy.DTOs;

namespace TestProgrammasy.Services.PdfService
{
    public class PdfService : IPdfService
    {
        public async Task<byte[]> GenerateTestResultPdfAsync(TestResultDTO result)
        {
            using var document = new PdfDocument();
            var page = document.AddPage();
            var gfx = XGraphics.FromPdfPage(page);

            // Fontlar
            var titleFont = new XFont("Times New Roman", 24, XFontStyleEx.Bold);
            var headerFont = new XFont("Times New Roman", 16, XFontStyleEx.Bold);
            var normalFont = new XFont("Times New Roman", 12);
            var valueFont = new XFont("Times New Roman", 12, XFontStyleEx.Bold);

            // Reňkler
            var headerColor = XBrushes.DarkBlue;
            var textColor = XBrushes.Black;
            var lineColor = new XPen(XColors.DarkBlue, 1);

            // Logo we umumy dizaýn
            DrawHeader(gfx, result, titleFont, headerColor);
            DrawDecorationLines(gfx, lineColor, page.Width);

            // Umumy maglumat
            double yPosition = 150;
            DrawStudentInfo(gfx, result, headerFont, normalFont, valueFont, textColor, ref yPosition);

            // Netije
            DrawResults(gfx, result, headerFont, normalFont, valueFont, textColor, lineColor, ref yPosition);

            // Aşaky düşündiriş
            DrawFooter(gfx, normalFont, textColor, page.Width);

            using var ms = new MemoryStream();
            document.Save(ms);
            return ms.ToArray();
        }

        private void DrawHeader(XGraphics gfx, TestResultDTO result, XFont titleFont, XBrush headerColor)
        {
            // Logo çyzmak üçin (üýtgedip bilersiňiz)
            var logo = XImage.FromFile("wwwroot/images/school_logo.png");
            gfx.DrawImage(logo, 50, 20, 60, 60);

            // Mekdebiň ady
            gfx.DrawString("Berkarar mekdebi", titleFont, headerColor,
                new XRect(150, 20, 400, 30), XStringFormats.CenterLeft);

            // Test netijesi ady
            gfx.DrawString("Test Netijesi Hasabaty", titleFont, headerColor,
                new XRect(150, 50, 400, 30), XStringFormats.CenterLeft);
        }

        private void DrawDecorationLines(XGraphics gfx, XPen lineColor, double pageWidth)
        {
            // Bezeg çyzyklar
            gfx.DrawLine(lineColor, 50, 100, pageWidth - 50, 100);
            gfx.DrawLine(lineColor, 50, 102, pageWidth - 50, 102);
        }

        private void DrawStudentInfo(XGraphics gfx, TestResultDTO result,
    XFont headerFont, XFont normalFont, XFont valueFont, XBrush textColor, ref double yPosition)
        {
            var leftMargin = 50;
            var columnWidth = 200;
            var tempY = yPosition;

            gfx.DrawString("Okuwçynyň Maglumatlary:", headerFont, textColor,
                new XPoint(leftMargin, yPosition));
            yPosition += 30;

            // Birinji sütün
            DrawInfoField(gfx, "Okuwçynyň ady:", result.UserId, normalFont, valueFont,
                textColor, leftMargin, ref yPosition);

            tempY = yPosition - 20;
            // Ikinji sütün
            DrawInfoField(gfx, "Synpy:", "9-njy synp", normalFont, valueFont,
                textColor, leftMargin + columnWidth, ref tempY);

            // Birinji sütün
            DrawInfoField(gfx, "Test ady:", result.TestTitle, normalFont, valueFont,
                textColor, leftMargin, ref yPosition);

            tempY = yPosition - 20;
            // Ikinji sütün
            DrawInfoField(gfx, "Dersi:", result.TestTitle, normalFont, valueFont,
                textColor, leftMargin + columnWidth, ref tempY);

            // Birinji sütün
            DrawInfoField(gfx, "Sene:", result.CompletedDate.ToString("dd.MM.yyyy"), normalFont, valueFont,
                textColor, leftMargin, ref yPosition);

            tempY = yPosition - 20;
            // Ikinji sütün
            DrawInfoField(gfx, "Wagty:", result.CompletedDate.ToString("HH:mm"), normalFont, valueFont,
                textColor, leftMargin + columnWidth, ref tempY);
        }

        private void DrawResults(XGraphics gfx, TestResultDTO result,
            XFont headerFont, XFont normalFont, XFont valueFont, XBrush textColor, XPen lineColor, ref double yPosition)
        {
            yPosition += 30;
            gfx.DrawString("Test Netijesi:", headerFont, textColor, new XPoint(50, yPosition));
            yPosition += 30;

            // Netije grafigi
            DrawResultGraph(gfx, result, lineColor, ref yPosition);

            // Jikme-jik netijeler
            var leftMargin = 50;
            DrawInfoField(gfx, "Dogry jogaplar:", $"{result.EarnedPoints}/{result.TotalPoints}",
                normalFont, valueFont, textColor, leftMargin, ref yPosition);
            DrawInfoField(gfx, "Göterim:", $"{result.Percentage}%", normalFont, valueFont,
                textColor, leftMargin, ref yPosition);
            DrawInfoField(gfx, "Baha:", result.Grade, normalFont, valueFont,
                textColor, leftMargin, ref yPosition);
        }

        private void DrawResultGraph(XGraphics gfx, TestResultDTO result, XPen lineColor, ref double yPosition)
        {
            var graphWidth = 400;
            var graphHeight = 20;
            var leftMargin = 50;

            // Progress bar background
            gfx.DrawRectangle(new XSolidBrush(XColor.FromArgb(230, 230, 230)),
                leftMargin, yPosition, graphWidth, graphHeight);

            // Progress bar fill
            var fillWidth = (result.Percentage / 100) * graphWidth;
            gfx.DrawRectangle(new XSolidBrush(GetGradeColor(result.Grade)),
                leftMargin, yPosition, fillWidth, graphHeight);

            // Progress bar border
            gfx.DrawRectangle(lineColor, leftMargin, yPosition, graphWidth, graphHeight);

            // Percentage text
            var percentageFont = new XFont("Times New Roman", 10, XFontStyleEx.Bold);
            gfx.DrawString($"{result.Percentage}%", percentageFont, XBrushes.White,
                new XRect(leftMargin, yPosition, fillWidth, graphHeight),
                XStringFormats.Center);

            yPosition += graphHeight + 20;
        }

        private XColor GetGradeColor(string grade)
        {
            return grade switch
            {
                "A" => XColor.FromArgb(46, 204, 113),
                "B" => XColor.FromArgb(52, 152, 219),
                "C" => XColor.FromArgb(241, 196, 15),
                "D" => XColor.FromArgb(230, 126, 34),
                _ => XColor.FromArgb(231, 76, 60)
            };
        }

        private void DrawInfoField(XGraphics gfx, string label, string value,
            XFont labelFont, XFont valueFont, XBrush textColor, double x, ref double y)
        {
            gfx.DrawString(label, labelFont, textColor, new XPoint(x, y));
            gfx.DrawString(value, valueFont, textColor, new XPoint(x + 120, y));
            y += 20;
        }

        private void DrawFooter(XGraphics gfx, XFont font, XBrush textColor, double pageWidth)
        {
            var footerText = "Bu resmi test netije hasabatydyr.";
            var footerText2 = "Mekdep müdirliginiň möhüri we goly bilen tassyklanmalydyr.";

            var y = 700; // Footer position
            gfx.DrawString(footerText, font, textColor,
                new XRect(0, y, pageWidth, 20), XStringFormats.Center);
            gfx.DrawString(footerText2, font, textColor,
                new XRect(0, y + 20, pageWidth, 20), XStringFormats.Center);

            // Gol we möhür üçin ýer
            var signatureY = y + 60;
            gfx.DrawString("Müdiriň goly: _________________", font, textColor, new XPoint(50, signatureY));
            gfx.DrawString("Möhür ýeri", font, textColor, new XPoint(400, signatureY));
        }
    }
}
