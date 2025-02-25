using System.Data;

public enum Subject
{
    Math,
    Physics,
    Chemistry,
    Biology,
    History
}

public struct Grade
{
    public Subject Subject { get; set; }
    public int Score { get; set; }
    public DateTime Date { get; set; }

    public Grade(Subject subject, int score, DateTime date)
    {
        Subject = subject;
        Score = score;
        Date = date;
    }
}

class Program
{
    static void Main(string[] args)
    {
        string[] courses = new string[10];
        string[,] students = new string[10, 15];
        string[,] studentIds = new string[10, 15];
        List<Grade>[,] studentGrades = new List<Grade>[10, 15];

        for (int i = 0; i < 10; i++)
            for (int j = 0; j < 15; j++)
                studentGrades[i, j] = new List<Grade>();

        int courseCount = 1;

        while (true)
        {
            Console.WriteLine("1) Добавить курс");
            Console.WriteLine("2) Записать студента");
            Console.WriteLine("3) Показать студентов на курсе");
            Console.WriteLine("4) Удалить студента");
            Console.WriteLine("5) Добавить оценку студенту");
            Console.WriteLine("6) Показать оценки студента");
            Console.WriteLine("7) Выход");
            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    if (courseCount < 10)
                    {
                        Console.Write("Введите название курса: ");
                        courses[courseCount] = Console.ReadLine();
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
                    if (int.TryParse(Console.ReadLine(), out int courseNumber) && courseNumber >= 0 && courseNumber < courseCount)
                    {
                        for (int i = 0; i < 15; i++)
                        {
                            if (string.IsNullOrEmpty(students[courseNumber, i]))
                            {
                                Console.Write("Введите имя студента: ");
                                students[courseNumber, i] = Console.ReadLine();

                                Console.Write("Введите ID студента: ");
                                studentIds[courseNumber, i] = Console.ReadLine();

                                Console.WriteLine($"Студент '{students[courseNumber, i]}' записан на курс.");
                                break;
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Некорректный номер курса.");
                    }
                    break;

                case "3":
                    Console.Write("Введите номер курса: ");
                    if (int.TryParse(Console.ReadLine(), out int displayCourse) && displayCourse >= 0 && displayCourse < courseCount)
                    {
                        Console.WriteLine($"Студенты на курсе '{courses[courseCount - 2]}':");
                        for (int i = 0; i < 15; i++)
                        {
                            if (!string.IsNullOrEmpty(students[displayCourse, i]))
                            {
                                Console.WriteLine($"{i + 1}: {students[displayCourse, i]} (ID: {studentIds[displayCourse, i]})");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Некорректный номер курса.");
                    }
                    break;

                case "4":
                    Console.Write("Введите номер курса: ");
                    if (int.TryParse(Console.ReadLine(), out int courseToRemoveFrom) && courseToRemoveFrom >= 0 && courseToRemoveFrom < courseCount)
                    {
                        Console.Write("Введите ID студента для удаления: ");
                        string studentIdToSearch = Console.ReadLine();
                        bool found = false;

                        for (int i = 0; i < 15; i++)
                        {
                            if (studentIds[courseToRemoveFrom, i] == studentIdToSearch)
                            {
                                found = true;
                                students[courseToRemoveFrom, i] = null;
                                studentIds[courseToRemoveFrom, i] = null;
                                studentGrades[courseToRemoveFrom, i].Clear();
                                Console.WriteLine("Студент успешно удален.");
                                break;
                            }
                        }

                        if (!found)
                        {
                            Console.WriteLine("Студент с таким ID не найден.");
                        }
                    }
                    break;

                case "5":
                    Console.Write("Введите номер курса: ");
                    if (int.TryParse(Console.ReadLine(), out int courseForGrade) && courseForGrade >= 0 && courseForGrade < courseCount)
                    {
                        Console.Write("Введите ID студента: ");
                        string idForGrade = Console.ReadLine();
                        int studentIndex = -1;

                        for (int i = 0; i < 15; i++)
                        {
                            if (studentIds[courseForGrade, i] == idForGrade)
                            {
                                studentIndex = i;
                                break;
                            }
                        }

                        if (studentIndex != -1)
                        {
                            Console.Write("Введите предмет (Math, Physics, Chemistry, Biology, History): ");
                            if (Enum.TryParse<Subject>(Console.ReadLine(), true, out Subject subjectForGrade))
                            {
                                Console.Write("Введите оценку: ");
                                if (int.TryParse(Console.ReadLine(), out int score))
                                {
                                    Console.WriteLine("Введите дату (yyyy-mm-dd): ");
                                    if (DateTime.TryParse(Console.ReadLine(), out var date))
                                    {
                                        studentGrades[courseForGrade, studentIndex].Add(new Grade(subjectForGrade, score, date));
                                        Console.WriteLine("Оценка добавлена.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Неверный формат даты.");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Некорректное значение оценки.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Некорректное название предмета.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Студент с таким ID не найден.");
                        }
                    }
                    break;

                case "6":
                    Console.Write("Введите номер курса: ");
                    if (int.TryParse(Console.ReadLine(), out int courseForGrades) && courseForGrades >= 0 && courseForGrades < courseCount)
                    {
                        Console.Write("Введите ID студента: ");
                        string idForCheckGrades = Console.ReadLine();
                        int gradingStudentIndex = -1;

                        for (int i = 0; i < 15; i++)
                        {
                            if (studentIds[courseForGrades, i] == idForCheckGrades)
                            {
                                gradingStudentIndex = i;
                                break;
                            }
                        }

                        if (gradingStudentIndex != -1)
                        {
                            Console.WriteLine($"Оценки студента '{students[courseForGrades, gradingStudentIndex]}':");
                            foreach (var grade in studentGrades[courseForGrades, gradingStudentIndex])
                            {
                                Console.WriteLine($"Предмет: {grade.Subject}, Оценка: {grade.Score}, Дата: {grade.Date.ToShortDateString()}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Студент с таким ID не найден.");
                        }
                    }
                    break;

                case "7":
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("Некорректный выбор.");
                    break;
            }
        }
    }
}