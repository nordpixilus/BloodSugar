using CommunityToolkit.Mvvm.ComponentModel;
using NetDayHospital.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClipoardPerson;
using NetDayHospital.Core.Models.Table.System;
using NetDayHospital.Core.Models.Table;
using Microsoft.VisualBasic;

namespace DayHospitalBloodSugar
{
    internal partial class MainWindowModel : BaseViewModel
    {
        public MainWindowModel()
        {
            //person = new Person();
            //person.InitSugars();
            getClipoardPerson = new GetClipoardPerson();
            InitDecimal();
            InitFractions();
            InitSugars();
            InitTimes();
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

        public List<string> Times { get; set; } = new();

        private void InitTimes()
        {
            string[] times = { "06.00", "07.00", "08.00", "09.00", "10.00", "11.00", "12.00", "13.00",
                "14.00", "15.00", "16.00", "17.00", "18.00", "19.00", "20.00", "21.00", "22.00",
                "23.00", "24.00", "01.00", "02.00", "03.00", "04.00", "05.00", };

            foreach (var item in times)
            {
                Times.Add(item);
            }
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

        public List<Sugar> Sugars { get; set; } = new();

        internal void InitSugars(int length = 11)
        {
            for (int i = 1; i < length; i++)
            {
                Sugars.Add(new Sugar { Row = i.ToString()});
            }
        }
    }    
}
