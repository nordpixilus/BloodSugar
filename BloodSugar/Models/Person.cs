﻿using NetDayHospital.Core.Controls.ListBloodSugar;
using System.Collections.Generic;

namespace BloodSugar.Models
{
    public class Person
    {
        public string FullName { get; set; } = string.Empty;

        public string BirthDateFull { get; set; } = string.Empty;

        public List<Sugar> Sugars { get; set; } = new();

        //internal void InitSugars(int length = 11)
        //{
        //    for (int i = 1; i < length; i++)
        //    {
        //        Sugars.Add(new Sugar());
        //    }
        //}
    }
}
