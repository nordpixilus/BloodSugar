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
    const string FIELD_REGUIRED_ERROR_MESSAGE = "Заполните поле:\nПример:\n";
    private readonly SugarRandom sugarRandom;
    private int selectDays;

    public MainWindowModel()
    {
        WeakReferenceMessenger.Default.RegisterAll(this);
        sugarRandom = new SugarRandom();
        InitRadioButton(1);
        selectDays = 7;
        UpdateBoolTable = false;
        FullName = string.Empty;
        BirthDateFull = string.Empty;
        StartMonitorGetRecordsAsync();       
    }

    #region Подключение работы с буфером обмена.

    private readonly GetClipoardPerson getClipoardPerson = new();
    #endregion

    #region Подключение блока выбора дат.
    [ObservableProperty]
    DateStartEndViewModel _DateStartEndViewModel = new();
    #endregion

    #region Подключение блока таблицы.

    [ObservableProperty]
    private ListBloodSugarViewModel _ListBloodSugarViewModel = new();
    #endregion

    #region Кнопки выбора вариантов сложности сахаров.

    [ObservableProperty]
    private bool _RadioButton1 = false;

    partial void OnRadioButton1Changed(bool value)
    {
        if (value is true)
        {
            CreateValueSugar(1);
        }
    }

    [ObservableProperty]
    private bool _RadioButton2 = false;

    partial void OnRadioButton2Changed(bool value)
    {
        if (value is true)
        {
            CreateValueSugar(2);
        }
    }

    [ObservableProperty]
    private bool _RadioButton3 = false;

    partial void OnRadioButton3Changed(bool value)
    {
        if (value is true)
        {
            CreateValueSugar(3);
        }
    }

    #region Метод инициализации RadioButton
    /// <summary>
    /// Инициализации включения RadioButton
    /// </summary>
    /// <param name="num"></param>
    private void InitRadioButton(int num)
    {
        switch (num)
        {
            case 1: RadioButton1 = true; break;
            case 2: RadioButton2 = true; break;
            case 3: RadioButton3 = true; break;
        }
    }
    #endregion

    #endregion

    #region Кнопка изменения варианта списка.

    [ObservableProperty]
    private bool _CountListChecked;

    partial void OnCountListCheckedChanged(bool value)
    {
        if (value)
        {
            selectDays = 0;
        }
        else
        {
            selectDays = 7;
        }

        UpdateTable();
    }
    #endregion

    #region Свойство UpdateBoolTable

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(PrintCommand))]
    private bool _UpdateBoolTable; 
    #endregion

    #region Поле FullName

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = FIELD_REGUIRED_ERROR_MESSAGE + "Иванов Иван Иванович")]
    [RegularExpression(@"^([А-Я][а-я]+ ?){2,5}", ErrorMessage = "Только русские буквы. \n От 2 до 5 слов.")]
    [NotifyCanExecuteChangedFor(nameof(PrintCommand))]
    private string _FullName;
    #endregion

    #region Поле BirthDateFull Дата рождения и возраст

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = FIELD_REGUIRED_ERROR_MESSAGE + "01.01.1960 (70 лет)")]
    [RegularExpression(@"^\d\d\.\d\d\.\d\d\d\d \(\d\d ((Г|г)ода?|(Л|л)ет)\)",
        ErrorMessage = "01.01.1960 (70 лет)\nВарианты:\nГод, Года, Лет\nгод, года, лет")]
    [NotifyCanExecuteChangedFor(nameof(PrintCommand))]
    private string _BirthDateFull;
    #endregion    

    private void CreateValueSugar(int level)
    {
        sugarRandom.FirstDay(level);
        UpdateTable();
    }

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

    private bool PrintClick()
    {
        return !HasErrors && UpdateBoolTable;
    }

    [RelayCommand(CanExecute = nameof(PrintClick))]
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

    private void UpdateTable()
    {
        if (UpdateBoolTable)
        {
            ListBloodSugarViewModel.UpdateTable(
            start: DateStartEndViewModel.SelectedDateStart!.Value,
            end: DateStartEndViewModel.SelectedDateEnd!.Value,
            rows: selectDays,
            sugar: sugarRandom.GetPoints()
            );
        }
    }

    public void Receive(ComplectDateStartEndMessege message)
    {
        UpdateBoolTable = message.Value;
        UpdateTable();
    }
}
