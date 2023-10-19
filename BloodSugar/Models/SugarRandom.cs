using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BloodSugar.Models;

class SugarRandom
{
    private Random sugarRandom;
    private List<double> points = new();
    private List<double> bottomPoints = new();
    private double nextPoint = 0;
    private int top = 0;

    public SugarRandom()
    {
        sugarRandom = new Random();
        bottomPoints = AddBottonPoints(5.9, 14);
        top = sugarRandom.Next(2, 5);
    }

    public List<double> GetPoints()
    {
        return points;
    }

    public void FirstDay(int level)
    {
        points.Clear();

        switch (level)
        {
            case 1: CreateSuperTop(); break;
            case 2: CreateTop(); break;
            case 3: CreateNormal(); break;
        }

        //double v = GetRandomNumberInRange(9.9, 9.7);
        
       

        

        //double nightPoint = points.Last();
        //double[] bottomPoints = { 4.5, 4.6, 4.7, 4.8, 4.9, 5.0, 5.1, 5.2, 5,3, 5.4, 5.5, 5.6, 5.7, 5.8, 5.9 };        
        //points.Add(bottomPoints[sugarRandom.Next(bottomPoints.Length)]);

        

        // true: на понижение
        // false: на уровне
        //if (GetRondomBool())
        //{

        //}
        //else
        //{

        //}
        
        //var randomBool = GetRondomBool();
        //MessageBox.Show(randomBool.ToString());
        //MessageBox.Show(num[sugarRandom.Next(num.Length)].ToString());
    }

    private void CreateSuperTop()
    {
        CreateFirstDayPoints(startPoint: 9.9, startPointCount: 4, startPeriodPoint: 9.9, startPeriodCount: 6);
        CreateFor();
        
        //CreatePoints(startPeriodPoint: nextPoint, startPeriodCount: 6);
        //CreatePoints(startPeriodPoint: nextPoint, startPeriodCount: 6);
        //CreatePoints(startPeriodPoint: nextPoint, startPeriodCount: 6);
        //CreatePoints(startPeriodPoint: nextPoint, startPeriodCount: 6);
        //CreatePoints(startPeriodPoint: nextPoint, startPeriodCount: 6);


            //// Создание первой точки
            //List<double> startRange = AddBottonPoints(9.9, 4);        
            //points.Add(startRange[sugarRandom.Next(startRange.Count)]);
            ////List<double> startPoints = new() { 9.9, 9.8, 9.7, 9.6 };

            //// Создание значений для первых 3 точек
            //List<double> firstPoints = AddBottonPoints(9.9, 6);
            //points.Add(firstPoints[sugarRandom.Next(firstPoints.Count)]);
            //points.Add(firstPoints[sugarRandom.Next(firstPoints.Count)]);
            //points.Add(firstPoints[sugarRandom.Next(firstPoints.Count)]);

            //List<double> firstPoints = new();
            //double startPoint = points[0];
            //for (int i = 0; i < 6; i++)
            //{
            //    double x = Math.Round(startPoint - 0.1, 1);
            //    firstPoints.Add(x);
            //    startPoint = x;
            //}

            // Добавление следующих 5 точек из диапазона
            //for (int i = 0; i < 5; i++)
            //{
            //    int count = firstPoints.Count;
            //    points.Add(firstPoints[sugarRandom.Next(count)]);
            //    MessageBox.Show(points[i].ToString());
            //}
    }    

    private void CreateTop()
    {
        CreateFirstDayPoints(startPoint: 8.5, startPointCount: 4, startPeriodPoint: 8.7, startPeriodCount: 6);
        CreateFor();
        //CreatePoints(startPeriodPoint: 8.7, startPeriodCount: 6);
    }

    private void CreateNormal()
    {
        CreateFirstDayPoints(startPoint: 6.2, startPointCount: 4, startPeriodPoint: 6.4, startPeriodCount: 6);
        CreateFor();
        //CreatePoints(startPeriodPoint: 6.4, startPeriodCount: 6);
    }

    private void CreateFor()
    {
        for (int i = 1; i < 6; i++)
        {
            if (top == i)
            {
                nextPoint = Math.Round(nextPoint + 0.6, 1);
            }
            else
            {
                nextPoint = Math.Round(nextPoint - 0.2, 1);
            }

            CreatePoints(startPeriodPoint: nextPoint, startPeriodCount: 6);
        }
    }

    /// <summary>
    /// Создание точек первого дня.
    /// </summary>
    /// <param name="startPoint">Первая точка.</param>
    /// <param name="startPointCount">Количество точек для выбора первой точки.</param>
    /// <param name="startPeriodPoint">Верхняя точка для создания диапазона выбора.</param>
    /// <param name="startPeriodCount">Количество точек для создания диапазона выбора.</param>
    private void CreateFirstDayPoints(double startPoint, int startPointCount, double startPeriodPoint, int startPeriodCount)
    {
        // Создание первой точки
        List<double> startRange = AddBottonPoints(startPoint, startPointCount);
        points.Add(startRange[sugarRandom.Next(startRange.Count)]);
        double lastPoint1 = points.Last();

        // Создание значений для первых 3 точек
        List<double> firstPoints = AddBottonPoints(startPeriodPoint, startPeriodCount);
        
        points.Add(firstPoints[sugarRandom.Next(firstPoints.Count)]);
        double lastPoint2 = points.Last();
        points.Add(firstPoints[sugarRandom.Next(firstPoints.Count)]);
        double lastPoint3 = points.Last();
        points.Add(firstPoints[sugarRandom.Next(firstPoints.Count)]);
        double lastPoint4 = points.Last();
        double point = ((lastPoint1 + lastPoint2 + lastPoint3 + lastPoint4) / 4);
        nextPoint = Math.Round(point, 1);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="startPeriodPoint">Верхняя точка для создания диапазона выбора.</param>
    /// <param name="startPeriodCount">Количество точек для создания диапазона выбора.</param>
    private void CreatePoints(double startPeriodPoint, int startPeriodCount)
    {        
        List<double> firstPoints = AddBottonPoints(startPeriodPoint, startPeriodCount);

        points.Add(firstPoints[sugarRandom.Next(firstPoints.Count)]);
        double lastPoint1 = points.Last();

        points.Add(firstPoints[sugarRandom.Next(firstPoints.Count)]);
        double lastPoint2 = points.Last();

        double point = ((lastPoint1 + lastPoint2) / 2);
        nextPoint = Math.Round(point, 1);

        points.Add(bottomPoints[sugarRandom.Next(bottomPoints.Count)]);
    }
    

    private double GetRandomNumberInRange(double minNumber, double maxNumber)
    {
        return sugarRandom.NextDouble() * (maxNumber - minNumber) + minNumber;
    }

    private bool GetRondomBool()
    {
        return sugarRandom.Next(2) == 1;
    }

    private static List<double> AddBottonPoints(double value, int count)
    {
        List<double> points = new() { value };

        for (int i = 0; i < count; i++)
        {
            //MessageBox.Show(points[i].ToString());
            double x = Math.Round(points[i] - 0.1, 1);
            points.Add(x);            
        }

        return points;
    }

    private static List<double> AddTopPoints(double value, int count)
    {
        List<double> points = new() { value };

        for (int i = 0; i < count; i++)
        {
            MessageBox.Show(points[i].ToString());
            double x = Math.Round(points[i] + 0.1, 1);
            points.Add(x);

        }

        return points;
    }
}
