using BloodSugar.Models;
using ClipoardPerson;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using NetDayHospital.Core.Controls.DateStartEnd;
using NetDayHospital.Core.Controls.DateStartEnd.Messages;
using NetDayHospital.Core.Controls.ListBloodSugar;
using NetDayHospital.Core.Models;
using NetDayHospital.Core.Models.Table.System;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BloodSugar;

internal partial class MainWindowModel : BaseViewModel, IRecipient<ComplectDateStartEndMessege>
{
    public MainWindowModel()
    {
        ListSugar = new ListBloodSugarViewModel();
        WeakReferenceMessenger.Default.RegisterAll(this);

        FullName = string.Empty;
        BirthDateFull = string.Empty;
        getClipoardPerson = new GetClipoardPerson();
        InitDecimal();
        //InitSugars();
        InitFractions();
        StartMonitorGetRecordsAsync();
    }

    [ObservableProperty]
    DateStartEndViewModel _DateStartEndViewModel = new();


    [ObservableProperty]
    private ListBloodSugarViewModel _ListSugar;

    [ObservableProperty]
    private List<Item> _Fractions = new();

    private void InitFractions()
    {
        for (int i = 0; i < 10; i++)
        {
            Fractions.Add(new Item { Name = i.ToString() });
        }
    }

    [ObservableProperty]
    private List<Item> _Decimals = new();

    private void InitDecimal()
    {
        for (int i = 3; i < 9; i++)
        {
            Decimals.Add(new Item { Name = i.ToString() });
        }
    }



    //public List<Item> Times2 { get; set; } = new();

    private List<string> InitTimes()
    {
        string[] times = { "15.00", "17.00", "19.00", "21.00", "15.00", "20.00", "07.00",
                "15.00", "20.00", "07.00", "15.00" };

        return times.ToList();
    }

    private static List<Parser> InitParsers()
    {
        List<Parser> parsers = new()
            {
                new Parser { Key = "FullName", Pattern = @"^(?<FullName>([а-яА-Я]+ ?){2,5}),? (Д|д)ата", Level = 3 },
                new Parser { Key = "BirthDateFull", Pattern = @"^.+рождения(:|,|\.|;)?\s?(?<BirthDateFull>.{18,20})(:|,|\.|;)?\s(П|п)ол", Level = 2 }
            };

        return parsers;
    }

    GetClipoardPerson getClipoardPerson;

    //List<Parser> parsers;

    /// <summary>
    /// Ожидание получения данных.
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

    //private Person person;

    #region Поле FullName

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required]
    [RegularExpression(@"^([А-Я][а-я]+ ?){2,5}", ErrorMessage = "Invalid Social Security Number.")]
    private string _FullName;

    //partial void OnFullNameChanged(string value)
    //{
    //    person.FullName = value;
    //}

    #endregion

    #region Поле BirthDateFull Дата рождения и возраст    

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required]
    private string _BirthDateFull;

    //partial void OnBirthDateFullChanged(string value)
    //{
    //    person.BirthDateFull = value;
    //}

    #endregion

    //[ObservableProperty]
    //private ObservableCollection<Sugar> _SugarsCol;

    //[ObservableProperty]
    //public ICollectionView _SugarsView;

    //[ObservableProperty]
    //private List<Sugar> _Sugars = new();

    //internal void InitSugars()
    //{
    //    for (int i = 1; i < 12; i++)
    //    {
    //        Sugars.Add(new Sugar { Row = i.ToString() });
    //    }

    //}

    public void Receive(ComplectDateStartEndMessege message)
    {

        ListSugar.Update(
            start: DateStartEndViewModel.SelectedDateStart!.Value,
            end: DateStartEndViewModel.SelectedDateEnd!.Value
            );


        //List<string> times = InitTimes();
        //List<string> dates = StringHelper
        //    .CreateDiabetDates(
        //     start: DateStartEndViewModel.DateStart!.Value,
        //     end: DateStartEndViewModel.DateEnd!.Value
        //     );


        //for (int i = 1; i < 12; i++)
        //{
        //    Sugars.Add(new Sugar { Row = i.ToString(), DateOnly = dates[i-1], TimeOnly = times[i-1] });
        //}

        //SugarsCol = new(Sugars);

        //SugarsView = CollectionViewSource.GetDefaultView(SugarsCol);

        //PeopleCol = new(this.personService.Persons);

        //InitSugars();
        //for (int i = 1; i < 11; i++)
        //{
        //    Sugars.Add(new Sugar { Row = i.ToString(), DateOnly = dates[i-1], TimeOnly = times[i-1] });
        //}
    }

}
