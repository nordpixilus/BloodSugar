using BloodSugar.Models;
using NetDayHospital.Core.Documents;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace BloodSugar.Documents
{
    public class SugarDocument : BaseDocument
    {
        private Person person;

        //public static FlowDocument Create(Person person)
        //{
        //    //Directory.CreateDirectory("Temp");
        //    //FileInfo fileInf = new FileInfo(@"Temp\demo.pdf");

        //    // Create a FlowDocument  
        //    FlowDocument doc = new FlowDocument();
        //    // Create a Section  
        //    Section sec = new Section();
        //    // Create first Paragraph  
        //    Paragraph p1 = new Paragraph();


        //    // Create and add a new Bold, Italic and Underline  
        //    Bold bld = new Bold();
        //    bld.Inlines.Add(new Run("First Paragraph"));
        //    Italic italicBld = new Italic();
        //    italicBld.Inlines.Add(bld);
        //    Underline underlineItalicBld = new Underline();
        //    underlineItalicBld.Inlines.Add(italicBld);
        //    // Add Bold, Italic, Underline to Paragraph  
        //    p1.Inlines.Add(underlineItalicBld);
        //    // Add Paragraph to Section  
        //    sec.Blocks.Add(p1);
        //    // Add Section to FlowDocument  
        //    doc.Blocks.Add(sec);
        //    return doc;



        //}
       

        public SugarDocument(Person person)
        {
            this.person = person;
            CreateFlowDocument();
        }

        private void CreateFlowDocument()
        {
            // Create a FlowDocument  
            //FlowDocument doc = new BaseDocument();
            //doc.ColumnWidth = double.PositiveInfinity;
            //doc.Blocks.Add(new Block());
            //doc.TextAlignment = TextAlignment.Center;

            Paragraph pFio = TextParagraphBold("Фатеева Юлия Николаевна, дата рождения: 30.07.1960 (63 года)");
            Section s1 = SectionEnterParagraph(1);
            Paragraph pDoc = TextParagraphBold("Колебания сахара крови");

            Blocks.Add(pFio);
            Blocks.Add(s1);
            Blocks.Add(pDoc);

            //this.Blocks


            //// Create a Section  
            //Section sec = new Section();
            //// Create first Paragraph  
            //Paragraph p1 = new Paragraph();
            //// Create and add a new Bold, Italic and Underline  
            //Bold bld = new Bold();
            //bld.Inlines.Add(new Run("First Paragraph"));
            //Italic italicBld = new Italic();
            //italicBld.Inlines.Add(bld);
            //Underline underlineItalicBld = new Underline();
            //underlineItalicBld.Inlines.Add(italicBld);
            //// Add Bold, Italic, Underline to Paragraph  
            //p1.Inlines.Add(underlineItalicBld);
            //// Add Paragraph to Section  
            //sec.Blocks.Add(p1);
            //// Add Section to FlowDocument  
            //doc.Blocks.Add(sec);




            //return doc;
        }

        private Section SectionEnterParagraph(int num = 1)
        {
            Section sec = new Section();
            for (int i = 0; i < num; i++)
            {
                Paragraph p = new();
                sec.Blocks.Add(p);
            }

            return sec;
        }

        private Paragraph TextParagraphBold(string text)
        {
            Bold bld = new();
            Run run = new Run();
            bld.Inlines.Add(new Run(text));
            //Underline underlineItalicBld = new();
            //underlineItalicBld.Inlines.Add(bld);
            Paragraph p = new();
            //TextAlignment textAlignment = new TextAlignment();

            p.TextAlignment = TextAlignment.Center;
            p.Inlines.Add(bld);
            return p;
        }

        private static TableColumn CreateItemColumnSize(int size)
        {
            return new TableColumn() { Width = new GridLength(size) };
        }

        private void CreateDocument2()
        {
            FlowDocument doc = new FlowDocument();

            Paragraph p = new Paragraph(new Run("Hello, world!"));
            p.FontSize = 36;
            doc.Blocks.Add(p);

            p = new Paragraph(new Run("The ultimate programming greeting!"));
            p.FontSize = 14;
            p.FontStyle = FontStyles.Italic;
            p.TextAlignment = TextAlignment.Left;
            p.Foreground = Brushes.Gray;
            doc.Blocks.Add(p);

            PrintDialog pd = new PrintDialog();
            doc.PageHeight = pd.PrintableAreaHeight;
            doc.PageWidth = pd.PrintableAreaWidth;
            doc.PagePadding = new Thickness(50);
            doc.ColumnGap = 0;
            doc.ColumnWidth = pd.PrintableAreaWidth;

            IDocumentPaginatorSource dps = doc;
            pd.PrintDocument(dps.DocumentPaginator, "flow doc");
        }
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
