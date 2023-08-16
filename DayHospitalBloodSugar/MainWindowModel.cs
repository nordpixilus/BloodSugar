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

namespace DayHospitalBloodSugar
{
    internal partial class MainWindowModel : BaseViewModel
    {
        public MainWindowModel()
        {
            //person = new Person();
            //person.InitSugars();
            getClipoardPerson = new GetClipoardPerson();
            
            InitSugars();
            StartMonitorGetRecordsAsync();
        }

        private List<Parser> InitParsers()
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
                Sugars.Add(new Sugar());
            }
        }
    }    
}
