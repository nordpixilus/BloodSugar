using BloodSugar.Documents;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace BloodSugar
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //private void Button_Click_print(object sender, RoutedEventArgs e)
        //{
        //    // Create a PrintDialog  
        //    PrintDialog printDlg = new PrintDialog();
        //    // Create a FlowDocument dynamically.  
        //    FlowDocument doc = new SugarDocument();
        //    doc.Name = "FlowDoc";
            
        //    // Create IDocumentPaginatorSource from FlowDocument  
        //    IDocumentPaginatorSource idpSource = doc;
        //    // Call PrintDocument method to send document to printer  
        //    printDlg.PrintDocument(idpSource.DocumentPaginator, "Hello WPF Printing.");

        //    //if (printDlg.ShowDialog() ?? false)
        //    //{
        //    //    printDlg.PrintDocument(idpSource.DocumentPaginator, "Hello WPF Printing.");
        //    //}
        //}
    }
}
