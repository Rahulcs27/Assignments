using System;
using System.Collections.Generic;

public class WorkshopManager
{
    public Dictionary<int, string> Workshops { get; private set; }
    public Dictionary<string, HashSet<int>> Registrations { get; private set; }

    public WorkshopManager()
    {
        Workshops = new Dictionary<int, string>
        {
            { 1, "AI Workshop" },
            { 2, "Cybersecurity" },
            { 3, "Data Science" }
        };

        Registrations = new Dictionary<string, HashSet<int>>
        {
            { "AI Workshop", new HashSet<int>() },
            { "Cybersecurity", new HashSet<int>() },
            { "Data Science", new HashSet<int>() }
        };
    }

    public bool RegisterStudent(int workshopChoice, int studentID)
    {
        if (!Workshops.ContainsKey(workshopChoice))
            return false;

        string selectedWorkshop = Workshops[workshopChoice];

        if (!Registrations[selectedWorkshop].Add(studentID))
            return false; // Student already registered

        return true;
    }

    public void DisplayRegistrations()
    {
        Console.WriteLine("\n===== Workshop Registrations =====");
        foreach (var workshop in Registrations)
        {
            Console.WriteLine($" {workshop.Key} ({workshop.Value.Count} students)");
            foreach (var studentID in workshop.Value)
                Console.WriteLine($"   - Student ID: {studentID}");
        }
    }
}
