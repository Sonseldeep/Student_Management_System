using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
public record Student(int Id, string Name, string Grade);

public class StudentManager
{
    private readonly List<Student> _students = new();
    private int _nextId = 1;

    public void AddStudent(string? name, string? grade)
    {
        string fullName = string.IsNullOrWhiteSpace(name) ? "Unknown" : name;

        string grage = string.IsNullOrWhiteSpace(grade) ? "N/A" : grade;

        _students.Add(new Student(_nextId++, fullName, grage));
        Console.WriteLine("Student added successfully.");

        public void DisplayAllStudents()
    {
        if (_students.Count == 0)
        {
            Console.WriteLine("No students found.");
            return;
        }
        foreach (var student in _students)
        {
            System.Console.WriteLine(student);
        }
    }


}

public class StudentManagementSystem
{
    public static void Main(string[] args)
    {

    }
}

}