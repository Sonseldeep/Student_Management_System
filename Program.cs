using System;
using System.Collections.Generic;
using System.IO.Compression;
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
    }
    public void DisplayAllStudents()
    {
        if (_students.Count == 0)
        {
            Console.WriteLine("No students found.");
            return;
        }
        foreach (var student in _students)
        {
            Console.WriteLine(student);
        }
    }

    public void UpdateStudent(int id, string? newName, string? newGrade)
    {
        var index = _students.FindIndex(s => s.Id == id);
        if (index == -1)
        {
            Console.WriteLine($"No student found with ID : {id}");
            return;
        }

        var student = _students[index];
        _students[index] = student with
        {
            Name = string.IsNullOrWhiteSpace(newName) ? student.Name : newName,
            Grade = string.IsNullOrWhiteSpace(newGrade) ? student.Grade : newGrade
        };
        Console.WriteLine("Student updated successfully.");

    }

    public void DeleteStudent(int id)
    {
        bool removed = _students.RemoveAll(s => s.Id == id) > 0;
        System.Console.WriteLine(removed ? "Student with Id: {id} deleted successfully." : $"No student found with ID: {id}");

    }




}

public class StudentManagementSystem
{
    public static void Main(string[] args)
    {

    }
}

