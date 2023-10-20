using System;
using System.Collections.Generic;
using System.Linq;

namespace BloodSugar.Models;

class SugarRandom
{
    private readonly Random sugarRandom;
    private readonly int firstCount = 4;
    private List<double> workRange = new();
    private List<double> bottomRange = new();
    private readonly List<double> points = new();

    private int startNum = 0;
    private int jump = 0;

    public SugarRandom()
    {
        sugarRandom = new Random();
    }

    public List<double> GetPoints()
    {
        return points;
    }

    public void FirstDay(int level)
    {
        workRange.Clear();
        bottomRange.Clear();
        points.Clear();

        jump = sugarRandom.Next(2, 5);
        startNum = 0;

        switch (level)
        {
            case 1: CreateSuperTop(); break;
            case 2: CreateTop(); break;
            case 3: CreateNormal(); break;
        }

        CreateFirstDayPoints();
        CreateForPoints();
    }

    private void CreateSuperTop()
    {
        workRange = CterateListRange(top: 9.9, bottom: 8.3);
        bottomRange = CterateListRange(top: 6.2, bottom: 6.0);
    }

    private void CreateTop()
    {
        workRange = CterateListRange(top: 8.5, bottom: 7.5);
        bottomRange = CterateListRange(top: 5.9, bottom: 5.0);
    }

    private void CreateNormal()
    {
        workRange = CterateListRange(top: 6.2, bottom: 5.3);
        bottomRange = CterateListRange(top: 4.3, bottom: 4.1);
    }

    /// <summary>
    /// Создание списка значение от большого до малого.
    /// </summary>
    /// <param name="top">Наибольшее значение</param>
    /// <param name="bottom">Наименьшее значение</param>
    /// <returns>Возврат списка double значений</returns>
    private static List<double> CterateListRange(double top, double bottom)
    {
        List<double> subList = new();
        while (top > bottom)
        {
            subList.Add(top);
            top = Math.Round(top - 0.1, 1);
        }
        return subList;
    }

    /// <summary>
    /// Создание точек первого дня.
    /// </summary>
    /// <param name="startPoint">Первая точка.</param>
    /// <param name="startPointCount">Количество точек для выбора первой точки.</param>
    /// <param name="startPeriodPoint">Верхняя точка для создания диапазона выбора.</param>
    /// <param name="startPeriodCount">Количество точек для создания диапазона выбора.</param>
    private void CreateFirstDayPoints()
    {
        // Создание первой точки
        List<double> subList = workRange.GetRange(startNum, firstCount);
        points.Add(subList[sugarRandom.Next(subList.Count)]);
        double point1 = points.Last();

        // Создание значений для первых 3 точек
        subList = workRange.GetRange(0, firstCount + 3);
        points.Add(subList[sugarRandom.Next(subList.Count)]);
        double point2 = points.Last();

        points.Add(subList[sugarRandom.Next(subList.Count)]);
        double point3 = points.Last();

        points.Add(subList[sugarRandom.Next(subList.Count)]);
        double point4 = points.Last();

        double point = ((point1 + point2 + point3 + point4) / 4);
        int num = workRange.IndexOf(Math.Round(point, 1));
        startNum = num >= 4 ? num : 3;
    }

    private void CreateForPoints()
    {
        for (int i = 1; i < 6; i++)
        {
            if (jump == i)
            {
                List<double> subList = workRange.GetRange(startNum - 2, 3);

                points.Add(subList[sugarRandom.Next(subList.Count)]);

                points.Add(subList[sugarRandom.Next(subList.Count)]);
            }
            else
            {
                List<double> subList = workRange.GetRange(startNum, ValidateCount(startNum, 4));

                points.Add(subList[sugarRandom.Next(subList.Count)]);
                double point1 = points.Last();

                points.Add(subList[sugarRandom.Next(subList.Count)]);
                double point2 = points.Last();

                double point = ((point1 + point2) / 2);
                startNum = workRange.IndexOf(Math.Round(point, 1)) + 2;
            }

            points.Add(bottomRange[sugarRandom.Next(bottomRange.Count)]);

            if (startNum > workRange.Count - 4)
            {
                startNum = workRange.Count - 4;
            }
        }
    }

    private int ValidateCount(int start, int count)
    {
        int workCount = workRange.Count;
        int end = start + count;
        while (end > workCount)
        {
            count--;
            end = start + count;
        }
        return count;
    }
}
