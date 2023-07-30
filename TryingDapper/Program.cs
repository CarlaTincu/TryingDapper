using System.Collections.Generic;
using System;
using System.Data.SqlTypes;
using System.Data.Common;
using TryingDapper;
using System.Data.SqlClient;
using Dapper;

public class Program
{
    private static readonly string connectionString = "Data Source=CARLA\\SQLEXPRESS;Initial Catalog=Employees;Trusted_Connection=True;TrustServerCertificate=True;";
    public static void Main()
    {
        List<Employee> employees = GetAllEmployees();
        foreach (var employee in employees)
        {
            Console.WriteLine($"ID : {employee.Id}, Name : {employee.LastName}");
        }
    }
    public static List<Employee> GetAllEmployees()
    {
        string query = "SELECT id, FirstName, LastName FROM Employees";
        using(var conn = new SqlConnection(connectionString))
        {
            conn.Open();
            List<Employee> employees = conn.Query<Employee>(query).ToList();
            return employees;
        }
    }
}
