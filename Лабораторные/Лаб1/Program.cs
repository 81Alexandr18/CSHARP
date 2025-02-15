using System;
using System.Collections.Generic;

public class Course
{
    public string Name { get; set; }
    public int Capacity { get; set; }
    public List<Student> EnrolledStudents { get; private set; }

    public Course(string name, int capacity)
    {
        Name = name;
        Capacity = capacity;
        EnrolledStudents = new List<Student>();
    }

    public bool Enroll(Student student)
    {
        if (EnrolledStudents.Count < Capacity)
        {
            EnrolledStudents.Add(student);
            return true;
        }
        return false;
    }

    public void Unenroll(Student student)
    {
        EnrolledStudents.Remove(student);
    }

    public void ListStudents()
    {
        if (EnrolledStudents.Count == 0)
        {
            Console.WriteLine("На этот курс не записано ни одного студента");
            return;
        }

        Console.WriteLine($"Студенты, обучающиеся в {Name}:");
        foreach (var student in EnrolledStudents)
        {
            Console.WriteLine(student.Name);
        }
    }
}

public class Student
{
    public string Name { get; set; }

    public Student(string name)
    {
        Name = name;
    }
}

public class EnrollmentSystem
{
    private List<Course> courses = new List<Course>();

    public void AddCourse(string name, int capacity)
    {
        courses.Add(new Course(name, capacity));
        Console.WriteLine($"Курс '{name}' успешно добавлен");
    }

    public void ViewCourse(string name)
    {
        var course = courses.Find(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        if (course != null)
        {
            Console.WriteLine($"Курс: {course.Name}, Вместимость: {course.Capacity}, Зачислены: {course.EnrolledStudents.Count}");
        }
        else
        {
            Console.WriteLine("Курс не найден");
        }
    }

    public void RemoveCourse(string name)
    {
        var course = courses.Find(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        if (course != null)
        {
            courses.Remove(course);
            Console.WriteLine($"Курс '{name}' успешно удален");
        }
        else
        {
            Console.WriteLine("Курс не найден");
        }
    }

    public void EnrollStudent(string courseName, Student student)
    {
        var course = courses.Find(c => c.Name.Equals(courseName, StringComparison.OrdinalIgnoreCase));
        if (course != null)
        {
            if (course.Enroll(student))
            {
                Console.WriteLine($"{student.Name} был зачислен в '{course.Name}'.");
            }
            else
            {
                Console.WriteLine("Курс заполнен");
            }
        }
        else
        {
            Console.WriteLine("Курс не найден");
        }
    }

    public void ShowStudentsInCourse(string courseName)
    {
        var course = courses.Find(c => c.Name.Equals(courseName, StringComparison.OrdinalIgnoreCase));
        if (course != null)
        {
            course.ListStudents();
        }
        else
        {
            Console.WriteLine("Курс не найден");
        }
    }

    public void UnenrollStudent(string courseName, Student student)
    {
        var course = courses.Find(c => c.Name.Equals(courseName, StringComparison.OrdinalIgnoreCase));
        if (course != null)
        {
            course.Unenroll(student);
            Console.WriteLine($"{student.Name} был снят с регистрации на курс '{course.Name}'.");
        }
        else
        {
            Console.WriteLine("Курс не найден");
        }
    }
}

public class Program
{
    static void Main(string[] args)
    {
        var enrollmentSystem = new EnrollmentSystem();

        while (true)
        {
            Console.WriteLine("Меню:");
            Console.WriteLine("1. Добавить курс");
            Console.WriteLine("2. Просмотреть курс");
            Console.WriteLine("3. Удалить курс");
            Console.WriteLine("4. Зарегистрировать студента на курс");
            Console.WriteLine("5. Показать список студентов на курсе");
            Console.WriteLine("6. Удалить студента с курса");
            Console.WriteLine("7. Выйти");
            Console.Write("Введите номер варианта: ");

            var option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    Console.Write("Введите название курса: ");
                    var courseName = Console.ReadLine();
                    Console.Write("Введите пропускную способность курса: ");
                    var capacity = int.Parse(Console.ReadLine());
                    enrollmentSystem.AddCourse(courseName, capacity);
                    break;

                case "2":
                    Console.Write("Введите название курса: ");
                    courseName = Console.ReadLine();
                    enrollmentSystem.ViewCourse(courseName);
                    break;

                case "3":
                    Console.Write("Введите название курса: ");
                    courseName = Console.ReadLine();
                    enrollmentSystem.RemoveCourse(courseName);
                    break;

                case "4":
                    Console.Write("Введите название курса: ");
                    courseName = Console.ReadLine();
                    Console.Write("Введите имя студента: ");
                    var studentName = Console.ReadLine();
                    var student = new Student(studentName);
                    enrollmentSystem.EnrollStudent(courseName, student);
                    break;

                case "5":
                    Console.Write("Введите название курса: ");
                    courseName = Console.ReadLine();
                    enrollmentSystem.ShowStudentsInCourse(courseName);
                    break;

                case "6":
                    Console.Write("Введите название курса: ");
                    courseName = Console.ReadLine();
                    Console.Write("Введите имя студента: ");
                    studentName = Console.ReadLine();
                    student = new Student(studentName);
                    enrollmentSystem.UnenrollStudent(courseName, student);
                    break;

                case "7":
                    return;

                default:
                    Console.WriteLine("Недопустимый параметр. Пожалуйста, попробуйте снова.");
                    break;
            }
        }
    }
}