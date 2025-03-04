using System;

public class Employee
{
    public string Name { get; set; }
    public DateTime JoiningDate { get; set; }

    public Employee(string name, DateTime joiningDate)
    {
        Name = name;
        JoiningDate = joiningDate;
    }
}
