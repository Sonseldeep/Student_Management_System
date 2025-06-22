using System;
using System.Collections.Generic;
using System.Linq;

public record Student(int Id, string Name, string Grade);

public class StudentManager
{
    private readonly List<Student> _students = new();
    private int _nextId = 1;

    public void AddStudent(string? name, string? grade)
    {
        string finalName = string.IsNullOrWhiteSpace(name) ? "Unknown" : name.Trim();
        string finalGrade = string.IsNullOrWhiteSpace(grade) ? "N/A" : grade.Trim();
        _students.Add(new Student(_nextId++, finalName, finalGrade));
        Console.WriteLine("Student added successfully.");
    }

    public void DisplayAllStudents()
    {
        if (_students.Count == 0)
        {
            Console.WriteLine("No students found.");
            return;
        }
        foreach (var student in _students)
            Console.WriteLine(student);
    }

    public void UpdateStudent(int id, string? newName, string? newGrade)
    {
        var index = _students.FindIndex(s => s.Id == id);
        if (index == -1)
        {
            Console.WriteLine($"No student found with ID: {id}");
            return;
        }
        var student = _students[index];
        _students[index] = student with
        {
            Name = string.IsNullOrWhiteSpace(newName) ? student.Name : newName.Trim(),
            Grade = string.IsNullOrWhiteSpace(newGrade) ? student.Grade : newGrade.Trim()
        };
        Console.WriteLine("Student updated successfully.");
    }

    public void DeleteStudent(int id)
    {
        bool removed = _students.RemoveAll(s => s.Id == id) > 0;
        Console.WriteLine(removed
            ? "Student deleted successfully."
            : $"No student found with ID: {id}");
    }

    public void SearchStudentsByName(string? name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("Please enter a valid name to search.");
            return;
        }
        var results = _students
            .Where(s => s.Name.Equals(name.Trim(), StringComparison.OrdinalIgnoreCase))
            .ToList();

        if (results.Count == 0)
        {
            Console.WriteLine($"No students found with name: {name.Trim()}");
            return;
        }
        Console.WriteLine("\n--- SEARCH RESULTS ---");
        foreach (var student in results)
            Console.WriteLine(student);
    }
}

public static class InputHelper
{
    public static int ReadInt(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            var input = Console.ReadLine();
            if (int.TryParse(input, out int number))
                return number;
            Console.WriteLine("Invalid input. Please enter a valid number.");
        }
    }

    public static string? ReadString(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine();
    }
}

public class StudentManagementSystem
{
    private readonly StudentManager _manager = new();

    public void Run()
    {
        bool running = true;
        while (running)
        {
            DisplayMenu();
            int choice = InputHelper.ReadInt("Enter your choice: ");
            running = HandleMenuChoice(choice);
        }
    }

    private static void DisplayMenu()
    {
        Console.WriteLine("\n--- STUDENT MANAGEMENT SYSTEM ---");
        Console.WriteLine("1. Add Student");
        Console.WriteLine("2. View All Students");
        Console.WriteLine("3. Update Student");
        Console.WriteLine("4. Delete Student");
        Console.WriteLine("5. Search Student by Name");
        Console.WriteLine("6. Exit");
    }

    private bool HandleMenuChoice(int choice) => choice switch
    {
        1 => AddStudent(),
        2 => ViewAllStudents(),
        3 => UpdateStudent(),
        4 => DeleteStudent(),
        5 => SearchStudent(),
        6 => Exit(),
        _ => InvalidChoice()
    };

    private bool AddStudent()
    {
        string? name = InputHelper.ReadString("Enter student name: ");
        string? grade = InputHelper.ReadString("Enter student grade: ");
        _manager.AddStudent(name, grade);
        return true;
    }

    private bool ViewAllStudents()
    {
        _manager.DisplayAllStudents();
        return true;
    }

    private bool UpdateStudent()
    {
        int id = InputHelper.ReadInt("Enter student ID to update: ");
        string? newName = InputHelper.ReadString("Enter new name (leave blank to keep current): ");
        string? newGrade = InputHelper.ReadString("Enter new grade (leave blank to keep current): ");
        _manager.UpdateStudent(id, newName, newGrade);
        return true;
    }

    private bool DeleteStudent()
    {
        int id = InputHelper.ReadInt("Enter student ID to delete: ");
        _manager.DeleteStudent(id);
        return true;
    }

    private bool SearchStudent()
    {
        string? name = InputHelper.ReadString("Enter student name to search: ");
        _manager.SearchStudentsByName(name);
        return true;
    }

    private bool Exit()
    {
        Console.WriteLine("Exiting program. Goodbye!");
        return false;
    }

    private bool InvalidChoice()
    {
        Console.WriteLine("Invalid choice. Please try again.");
        return true;
    }

    public static void Main()
    {
        new StudentManagementSystem().Run();
    }
}

