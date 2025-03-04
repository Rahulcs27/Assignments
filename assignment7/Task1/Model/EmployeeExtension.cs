using System;

public static class EmployeeExtensions
{
    public static int GetExperience(this Employee employee)
    {
        return DateTime.Now.Year - employee.JoiningDate.Year;
    }
}
