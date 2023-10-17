using BloodSugar.Documents;
using BloodSugar.Models;
using ClipoardPerson;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using NetDayHospital.Core.Controls.DateStartEnd;
using NetDayHospital.Core.Controls.DateStartEnd.Messages;
using NetDayHospital.Core.Controls.ListBloodSugar;
using NetDayHospital.Core.Models;
using NetDayHospital.Core.Models.Table.System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Documents;

namespace BloodSugar;

internal partial class MainWindowModel : BaseViewModel, IRecipient<ComplectDateStartEndMessege>
{
    public MainWindowModel()
    {
        WeakReferenceMessenger.Default.RegisterAll(this);
        StartMonitorGetRecordsAsync();

        //FullName = "Фатеева Юлия Николаевна";
        //BirthDateFull = "30.07.1960 (63 года)";
    }

    #region Подключение работы с буфером обмена.

    private readonly GetClipoardPerson getClipoardPerson = new();
    #endregion

    #region Подключение блока выбора дат.
    [ObservableProperty]
    DateStartEndViewModel _DateStartEndViewModel = new();
    #endregion

    #region Подключение блока таблицы дат и количесво сахара в крови.

    [ObservableProperty]
    private ListBloodSugarViewModel _ListBloodSugarViewModel = new(); 
    #endregion

    #region Поле FullName
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required]
    [RegularExpression(@"^([А-Я][а-я]+ ?){2,5}", ErrorMessage = "Invalid Social Security Number.")]
    private string _FullName = string.Empty;

    //partial void OnFullNameChanged(string value)
    //{
    //    person.FullName = value;
    //}
    #endregion

    #region Поле BirthDateFull Дата рождения и возраст
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required]
    private string _BirthDateFull = string.Empty;

    //partial void OnBirthDateFullChanged(string value)
    //{
    //    person.BirthDateFull = value;
    //}
    #endregion

    #region Start

    /// <summary>
    /// Ожидание получения данных из буфера обмена.
    /// </summary>
    private async void StartMonitorGetRecordsAsync()
    {
        List<Parser> parsers = InitParsers();

        getClipoardPerson.InitListRegeps(parsers);

        bool result = await getClipoardPerson.GetRecordsAsync();
        if (result)
        {
            List<Record> fields = getClipoardPerson.GetListFields();
            Dictionary<string, string> records = fields.ToDictionary(a => a.Key, a => a.Result);

            FullName = records["FullName"];
            BirthDateFull = records["BirthDateFull"];
        }
    }

    /// <summary>
    /// Создание списка регулярок.
    /// </summary>
    /// <returns></returns>
    private static List<Parser> InitParsers()
    {
        List<Parser> parsers = new()
            {
                new Parser { Key = "FullName", Pattern = @"^(?<FullName>([а-яА-Я]+ ?){2,5}),? (Д|д)ата", Level = 3 },
                new Parser { Key = "BirthDateFull", Pattern = @"^.+рождения(:|,|\.|;)?\s?(?<BirthDateFull>.{18,20})(:|,|\.|;)?\s(П|п)ол", Level = 2 }
            };

        return parsers;
    } 
    #endregion

    [RelayCommand]
    private void Print()
    {
        Person person = new()
        {
            FullName = FullName,
            BirthDateFull = BirthDateFull,
            Sugars = ListBloodSugarViewModel.SugarsCol.ToList()
        };

        // Create a PrintDialog  
        PrintDialog printDlg = new PrintDialog();
        // Create a FlowDocument dynamically.  
        SugarDocument doc = new(person);
        doc.Create();
        //doc.Name = "FlowDoc";
        // Create IDocumentPaginatorSource from FlowDocument  
        IDocumentPaginatorSource idpSource = doc;
        // Call PrintDocument method to send document to printer  
        //printDlg.PrintDocument(idpSource.DocumentPaginator, "Hello WPF Printing.");

        if (printDlg.ShowDialog() ?? false)
        {
            printDlg.PrintDocument(idpSource.DocumentPaginator, "Hello WPF Printing.");
        }
    }



    public void Receive(ComplectDateStartEndMessege message)
    {

        ListBloodSugarViewModel.Update(
            start: DateStartEndViewModel.SelectedDateStart!.Value,
            end: DateStartEndViewModel.SelectedDateEnd!.Value,
            rows: 5
            );
    }
}
