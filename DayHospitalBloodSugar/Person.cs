﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayHospitalBloodSugar
{
    internal class Person
    {
        public string FullName { get; set; } = string.Empty;

        public string BirthDateFull { get; set; } = string.Empty;

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