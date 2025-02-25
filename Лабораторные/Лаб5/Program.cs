using System;
using System.Collections.Generic;
using System.Diagnostics;

public class Program
{
    public static void Main(string[] args)
    {
        Console.Write("Введите количество студентов: ");
        int studentCount = int.Parse(Console.ReadLine());
        List<Student> students = new List<Student>();

        for (int i = 0; i < studentCount; i++)
        {
            Console.Write($"Введите имя студента {i + 1}: ");
            string studentName = Console.ReadLine();
            Student student = new Student(studentName);

            Console.Write("Введите количество оценок: ");
            int gradeCount = int.Parse(Console.ReadLine());

            for (int j = 0; j < gradeCount; j++)
            {
                Console.Write($"Введите предмет для оценки {j + 1}: ");
                string subject = Console.ReadLine();

                Console.Write($"Введите оценку для предмета {subject}: ");
                int value = int.Parse(Console.ReadLine());

                Console.Write($"Введите дату получения оценки (в формате ГГГГ-ММ-ДД): ");
                DateTime date = DateTime.Parse(Console.ReadLine());

                Grade grade = new Grade(subject, value, date);
                student.AddGrade(grade);
            }

            students.Add(student);
        }

        string filePath = "student_reports.json";
        StudentReport.SerializeReportsToJson(students, filePath);
    }
}
public class Grade
{
    public string Subject { get; set; }
    public int Value { get; set; }
    public DateTime Date { get; set; }

    public Grade(string subject, int value, DateTime date)
    {
        Subject = subject;
        Value = value;
        Date = date;
    }
}