using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Xml;
using Newtonsoft.Json;

public class StudentReport
{
    public string Name { get; set; }
    public List<Grade> Grades { get; set; }

    public StudentReport(Student student)
    {
        Name = student.Name;
        Grades = student.Grades;
    }

    public static void SerializeReportsToJson(List<Student> students, string filePath)
    {
        var reports = new List<StudentReport>();

        foreach (var student in students)
        {
            reports.Add(new StudentReport(student));
        }

        string json = JsonConvert.SerializeObject(reports, Newtonsoft.Json.Formatting.Indented);
        File.WriteAllText(filePath, json);
        Console.WriteLine($"Отчет успешно сохранен в {filePath}");
    }
}