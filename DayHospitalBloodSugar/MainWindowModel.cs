using CommunityToolkit.Mvvm.ComponentModel;
using NetDayHospital.Core.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ClipoardPerson;
using NetDayHospital.Core.Models.Table.System;
using NetDayHospital.Core.Controls.DateStartEnd;
using CommunityToolkit.Mvvm.Messaging;
using NetDayHospital.Core.Messages;
using System.Windows;
using System;
using System.Globalization;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;

namespace DayHospitalBloodSugar
{
    internal partial class MainWindowModel : BaseViewModel, IRecipient<ComplectDateStartEndMessege>
    {
        [ObservableProperty]
        DateStartEndViewModel _DateStartEndViewModel = new();

        public MainWindowModel()
        {
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

        [ObservableProperty]
        private ObservableCollection<Sugar> _SugarsCol;

        [ObservableProperty]
        public ICollectionView _SugarsView;

        [ObservableProperty]
        private List<Sugar> _Sugars = new();

        //internal void InitSugars()
        //{
        //    for (int i = 1; i < 12; i++)
        //    {
        //        Sugars.Add(new Sugar { Row = i.ToString() });
        //    }

        //}

        public void Receive(ComplectDateStartEndMessege message)
        {
            List<string> times = InitTimes();
            //List<string> dates = StringHelper
            //    .CreateDiabetDates(
            //     start: DateStartEndViewModel.DateStart!.Value,
            //     end: DateStartEndViewModel.DateEnd!.Value
            //     );
            

            //for (int i = 1; i < 12; i++)
            //{
            //    Sugars.Add(new Sugar { Row = i.ToString(), DateOnly = dates[i-1], TimeOnly = times[i-1] });
            //}

            SugarsCol = new(Sugars);

            SugarsView = CollectionViewSource.GetDefaultView(SugarsCol);

            //PeopleCol = new(this.personService.Persons);

            //InitSugars();
            //for (int i = 1; i < 11; i++)
            //{
            //    Sugars.Add(new Sugar { Row = i.ToString(), DateOnly = dates[i-1], TimeOnly = times[i-1] });
            //}
        }
    }
}
