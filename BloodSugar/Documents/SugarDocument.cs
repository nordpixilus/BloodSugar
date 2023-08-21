using BloodSugar.Models;
using NetDayHospital.Core.Controls.ListBloodSugar;
using NetDayHospital.Core.Documents;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using static System.Net.Mime.MediaTypeNames;

namespace BloodSugar.Documents
{
    public class SugarDocument : BaseDocument
    {
        private Person person;

        public SugarDocument(Person person)
        {
            this.person = person;
            AddText();
            AddTable();            
        }

        private void AddText()
        {
            Paragraph paragraphPerson = new();
            paragraphPerson.TextAlignment = TextAlignment.Center;
            paragraphPerson.FontWeight = FontWeights.Bold;

            paragraphPerson.Inlines.Add(new LineBreak());
            paragraphPerson.Inlines.Add(new Run($"{person.FullName}, дата рождения: {person.BirthDateFull}"));
            paragraphPerson.Inlines.Add(new LineBreak());
            paragraphPerson.Inlines.Add(new LineBreak());

            Blocks.Add(paragraphPerson);

            Blocks.Add(new Paragraph());

            Paragraph paragraphTableName = new();
            paragraphTableName.Inlines.Add(new Run(@"Уровень сахара крови."));
            paragraphTableName.Inlines.Add(new LineBreak());
            paragraphTableName.TextAlignment = TextAlignment.Center;
            paragraphTableName.FontWeight = FontWeights.Bold;
            Blocks.Add(paragraphTableName);
        }

        private void AddTable()
        {
            Table table = new()
            {
                FontSize = 19,
                FontFamily = new FontFamily("Times New Roman"),
                Background = Brushes.Black, CellSpacing = 2,
            };

            TableColumn column1 = new();
            TableColumn column2 = new();

            table.Columns.Add(column1);
            table.Columns.Add(column2);

            TableRow row = new();
            row.Cells.Add(CellHeader("Дата, время"));
            row.Cells.Add(CellHeader(@"Уровень сахара крови (ммоль\л)"));
            TableRowGroup RowGroupHeader = new();
            RowGroupHeader.Rows.Add(row);
            table.RowGroups.Add(RowGroupHeader);

            row = new();
            row.Cells.Add(CellContent(person.Sugars, "left"));
            row.Cells.Add(CellContent(person.Sugars, "riht"));
            TableRowGroup RowGroupContent = new();
            RowGroupContent.Rows.Add(row);
            table.RowGroups.Add(RowGroupContent);

            Blocks.Add(table);

            
        }

        private TableCell CellHeader(string text)
        {
            TableCell tableCell = new()
            {
                Background = Brushes.White,
                TextAlignment = TextAlignment.Center,
                Padding = new Thickness(0, 0, 0, 15)
            };

            //tableCell.Blocks.Add(new LineBreak());

            Paragraph paragraph = new();

            paragraph.Inlines.Add(new LineBreak());
            //paragraph = new(new Run(text));
            paragraph.Inlines.Add(new Run(text));
            //paragraph.Inlines.Add(new Run("\n"));
            //paragraph.MinWidowLines = 30;
            //Padding = new Thickness(0,10,0,10)

            tableCell.Blocks.Add(paragraph);
            return tableCell;
        }

        private TableCell CellContent(List<Sugar> sugars, string column)
        {
            TableCell tableCell = new()
            {
                Background = Brushes.White,
                TextAlignment = TextAlignment.Center
            };

            Paragraph paragraph = new();

            paragraph.Inlines.Add(new LineBreak());

            //tableCell.Blocks.Add(paragraph);
            //tableCell.Blocks.Add(new Paragraph());
            if (column == "left")
            {
                foreach (var item in sugars)
                {
                    //Paragraph paragraph = new();
                    //paragraph.Inlines.Add(new LineBreak());
                    paragraph.Inlines.Add(new Run($"{item.DateOnly} {item.TimeOnly}\r\n"));
                    paragraph.LineHeight = 30;
                    tableCell.Blocks.Add(paragraph);
                    //tableCell.Blocks.Add(new Paragraph(new Run($"{item.DateOnly} {item.TimeOnly}")));
                }                
            }
            else
            {
                foreach (var item in sugars)
                {
                    //Paragraph paragraph = new();
                    //paragraph.Inlines.Add(new LineBreak());
                    paragraph.Inlines.Add(new Run($"{item.Level}\r\n"));
                    tableCell.Blocks.Add(paragraph);
                    //tableCell.Blocks.Add(new Paragraph(new Run(item.Level)));
                }
            }

            tableCell.Blocks.Add(paragraph);

            tableCell.Blocks.Add(new Paragraph());
            tableCell.Blocks.Add(new Paragraph());

            return tableCell;
        }
        
        private static Paragraph TextParagraphCenter(string text)
        {
            return new Paragraph(new Run(text))
            {
                TextAlignment = TextAlignment.Center
            };
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
