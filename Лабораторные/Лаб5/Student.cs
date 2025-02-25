using System.Collections.Generic;
using System.Diagnostics;

public class Student
{
    public string Name { get; private set; }
    public List<Grade> Grades { get; private set; }

    public Student(string name)
    {
        Name = name;
        Grades = new List<Grade>();
    }

    public void AddGrade(Grade grade)
    {
        Grades.Add(grade);
    }
}