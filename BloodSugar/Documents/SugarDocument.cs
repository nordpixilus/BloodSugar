using BloodSugar.Models;
using NetDayHospital.Core.Controls.ListBloodSugar;
using NetDayHospital.Core.Documents;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace BloodSugar.Documents;

public class SugarDocument : BaseDocument
{
    private readonly Person person;
    private readonly Table table = new();

    public SugarDocument(Person person)
    {
        this.person = person;
    }

    public void Create()
    {
        AddText();
        AddTable();
    }

    private void AddText()
    {
        Paragraph paragraphPerson = new()
        {
            TextAlignment = TextAlignment.Center,
            FontWeight = FontWeights.Bold,
            FontFamily = new FontFamily("Times New Roman")
        };

        paragraphPerson.Inlines.Add(new LineBreak());
        paragraphPerson.Inlines.Add(new LineBreak());
        paragraphPerson.Inlines.Add(new Run($"{person.FullName}, дата рождения: {person.BirthDateFull}"));
        paragraphPerson.Inlines.Add(new LineBreak());
        paragraphPerson.Inlines.Add(new LineBreak());

        Blocks.Add(paragraphPerson);

        Paragraph paragraphTableName = new();
        paragraphTableName.Inlines.Add(new Run(@"Колебания сахара крови  (ммоль\л)"));
        paragraphTableName.Inlines.Add(new LineBreak());
        paragraphTableName.TextAlignment = TextAlignment.Center;
        paragraphTableName.FontWeight = FontWeights.Bold;
        Blocks.Add(paragraphTableName);
    }

    private void AddTable()
    {
        table.FontSize = 19;
        table.FontFamily = new FontFamily("Times New Roman");
        table.CellSpacing = 2;
        table.Padding = new Thickness(1);
        table.Background = Brushes.Black;
        table.Margin = new Thickness(150, 0, 150, 0);

        table.Columns.Add(new TableColumn());
        table.Columns.Add(new TableColumn());
        table.Columns.Add(new TableColumn());

        AddTableHeader();
        AddTableContent();
    }

    private void AddTableHeader()
    {
        TableRow rowHeader = new();

        rowHeader.Cells.Add(AddCellHeader("Дата"));
        rowHeader.Cells.Add(AddCellHeader("Время"));
        rowHeader.Cells.Add(AddCellHeader(@"Уровень сахара"));

        TableRowGroup RowGroupHeader = new();
        RowGroupHeader.Rows.Add(rowHeader);
        table.RowGroups.Add(RowGroupHeader);
    }

    private void AddTableContent()
    {
        TableRowGroup RowGroupContent = new();
        foreach (Sugar item in person.Sugars)
        {
            TableRow rowitem = new();
            rowitem.Cells.Add(AddCellContent(item.DateOnly));
            rowitem.Cells.Add(AddCellContent(item.TimeOnly));
            rowitem.Cells.Add(AddCellContent(item.Level));
            RowGroupContent.Rows.Add(rowitem);
        }

        table.RowGroups.Add(RowGroupContent);

        Blocks.Add(table);
    }

    private static TableCell AddCellContent(string text)
    {
        Thickness thickness = new(0, 10, 0, 10);
        return AddCell(text, thickness);
    }

    private static TableCell AddCellHeader(string text)
    {
        Thickness thickness = new(0, 5, 0, 15);
        return AddCell(text, thickness);
    }

    private static TableCell AddCell(string text, Thickness thickness)
    {
        return new TableCell(new Paragraph(new Run(text)))
        {
            Background = Brushes.White,
            TextAlignment = TextAlignment.Center,
            Padding = thickness,
            FontWeight = FontWeights.Bold,

        };
    }
}

//SugarDocument.CreateDocument();
//CreateDocument();


//var file = File.ReadAllBytes(@"Temp\demo.pdf");
//var printQueue = LocalPrintServer.GetDefaultPrintQueue();

//using (var job = printQueue.AddJob())
//using (var stream = job.JobStream)
//{
//    stream.Write(file, 0, file.Length);
//}


//System.Windows.Documents.FixedDocument fixedDocument;
//using (FileStream pdfFile = new FileStream(@"Temp\demo.pdf", System.IO.FileMode.Open, FileAccess.Read))
//{
//    Document document = new Document(pdfFile);
//    RenderSettings renderSettings = new RenderSettings();
//    ConvertToWpfOptions renderOptions = new ConvertToWpfOptions { ConvertToImages = false };
//    renderSettings.RenderPurpose = RenderPurpose.Print;
//    renderSettings.ColorSettings.TransformationMode = ColorTransformationMode.HighQuality;
//    //convert the pdf with the rendersettings and options to a fixed-size document which can be printed
//    fixedDocument = document.ConvertToWpf(renderSettings, renderOptions);
//}
//printDialog.PrintDocument(fixedDocument.DocumentPaginator, "Print");


//PrintDialog printDialog = new PrintDialog();
//if (printDialog.ShowDialog() == true)
//{
//    //IDocumentPaginatorSource idp = PurposeDoc;
//    //printDialog.PrintDocument(idp.DocumentPaginator, "C:\Users\Nikola\Documents\kjhkjh.pdf");

//    //PurposeDoc.PagePadding = new Thickness(25, 15, 20, 15);
//    //PurposeDoc.ColumnGap = 0;
//    //PurposeDoc.ColumnWidth = printDialog.PrintableAreaWidth;

//    //var paginator = ((IDocumentPaginatorSource)PurposeDoc).DocumentPaginator;
//    //paginator.PageSize = new Size(PurposeDoc.PageWidth, PurposeDoc.PageHeight);

//    printDialog.PrintTicket.PageOrientation = System.Printing.PageOrientation.Landscape;

//    printDialog.PrintDocument(paginator, "Purpose Doc");
//}
