using System.Data;
using System.Collections.Generic;
using Grabe;

namespace Program
{
    public class Student
    {
        public string Name { get; private set; }
        public string Id { get; private set; }
        public List<Grade> Grades { get; private set; }

        public Student(string name, string id)
        {
            Name = name;
            Id = id;
            Grades = new List<Grade>();
        }

        public void AddGrade(Grade grade)
        {
            Grades.Add(grade);
        }
    }
    public class Course
    {
        public string Name { get; private set; }
        public List<Student> Students { get; private set; }

        public Course(string name)
        {
            Name = name;
            Students = new List<Student>();
        }

        public void AddStudent(Student student)
        {
            Students.Add(student);
        }

        public void RemoveStudent(string studentId)
        {
            Students.RemoveAll(s => s.Id == studentId);
        }

        public void DisplayStudents()
        {
            foreach (var student in Students)
            {
                Console.WriteLine($"Name: {student.Name}, ID: {student.Id}");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Course[] courses = new Course[10];
            int courseCount = 1;

            while (true)
            {
                Console.WriteLine("1) Добавить курс");
                Console.WriteLine("2) Записать студента");
                Console.WriteLine("3) Показать студентов на курсе");
                Console.WriteLine("4) Удалить студента");
                Console.WriteLine("5) Добавить оценку студенту");
                Console.WriteLine("6) Показать оценки студента");
                Console.WriteLine("0) Выход");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        if (courseCount < courses.Length)
                        {
                            Console.Write("Введите название курса: ");
                            string courseName = Console.ReadLine();
                            courses[courseCount] = new Course(courseName);
                            courseCount++;
                            Console.WriteLine("Данный курс имеет номер {0}", courseCount - 1);
                            Console.WriteLine("Курс добавлен.");
                        }
                        else
                        {
                            Console.WriteLine("Достигнуто максимальное количество курсов.");
                        }
                        break;
                    case "2":
                        Console.Write("Введите номер курса: ");
                        int courseIndex = int.Parse(Console.ReadLine());
                        if (courseIndex >= 0 && courseIndex < courseCount)
                        {
                            Console.Write("Введите имя студента: ");
                            string studentName = Console.ReadLine();
                            Console.Write("Введите ID студента: ");
                            string studentId = Console.ReadLine();
                            Console.WriteLine($"Студент '{studentName}' записан на курс.");
                            courses[courseIndex].AddStudent(new Student(studentName, studentId));

                        }
                        else
                        {
                            Console.WriteLine("Некорректный ID курса.");
                        }
                        break;
                    case "3":
                        Console.Write("Введите номер курса: ");
                        courseIndex = int.Parse(Console.ReadLine());
                        if (courseIndex >= 0 && courseIndex < courseCount)
                        {
                            courses[courseIndex].DisplayStudents();
                        }
                        else
                        {
                            Console.WriteLine("Некорректный ID курса.");
                        }
                        break;
                    case "4":
                        Console.Write("Введите номер курса: ");
                        courseIndex = int.Parse(Console.ReadLine());
                        if (courseIndex >= 0 && courseIndex < courseCount)
                        {
                            Console.Write("Введите ID студента для удаления: ");
                            string studentIdToRemove = Console.ReadLine();
                            courses[courseIndex].RemoveStudent(studentIdToRemove);
                        }
                        else
                        {
                            Console.WriteLine("Некорректный ID курса.");
                        }
                        break;
                    case "5":
                        Console.Write("Введите номер курса: ");
                        courseIndex = int.Parse(Console.ReadLine());
                        if (courseIndex >= 0 && courseIndex < courseCount)
                        {
                            Console.Write("Введите ID студента: ");
                            string studentId = Console.ReadLine();
                            var student = courses[courseIndex].Students.Find(s => s.Id == studentId);
                            if (student != null)
                            {
                                Console.Write("Введите предмет (Math, Physics, Chemistry, Biology, History): ");
                                Subject subject = (Subject)Enum.Parse(typeof(Subject), Console.ReadLine());
                                Console.Write("Введите оценку: ");
                                int score = int.Parse(Console.ReadLine());

                                Console.WriteLine("Введите дату (yyyy-mm-dd): ");
                                if (DateTime.TryParse(Console.ReadLine(), out var date))
                                {
                                    student.AddGrade(new Grade(subject, score, date));
                                    Console.WriteLine("Оценка добавлена.");
                                }
                                else
                                {
                                    Console.WriteLine("Неверный формат даты.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Студент не найден.");
                            }
                        }
                        break;
                    case "6":
                        Console.Write("Введите номер курса: ");
                        courseIndex = int.Parse(Console.ReadLine());
                        if (courseIndex >= 0 && courseIndex < courseCount)
                        {
                            Console.Write("Введите ID студента: ");
                            string studentId = Console.ReadLine();
                            var student = courses[courseIndex].Students.Find(s => s.Id == studentId);
                            if (student != null)
                            {
                                Console.WriteLine("Оценки студента:");
                                foreach (var grade in student.Grades)
                                {
                                    Console.WriteLine($"Предмет: {grade.Subject}, Оценка: {grade.Score}, Дата: {grade.Date}");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Студент не найден.");
                            }
                        }
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Некорректный выбор.");
                        break;
                }
            }
        }
    }
}