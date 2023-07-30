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
            Console.WriteLine($"First Name : {employee.FirstName},Last Name : {employee.LastName}");
        }
        Console.WriteLine("Do you want to add an Employee? Please press y if yes, otherwise n");
        string choice = Console.ReadLine();
        switch(choice)
        {
            case "y":
                AddEmployee();
                employees = GetAllEmployees();
                foreach (var employee in employees)
                {
                    Console.WriteLine($"First Name : {employee.FirstName},Last Name : {employee.LastName}");
                }
                break;
            case "n":
                Console.WriteLine("Ok! Thank you for your effort!");
                break;
            default: Console.WriteLine("The letter/sentence you entered is not an option! Try again!"); break;
        }
    }
    public static List<Employee> GetAllEmployees()
    {
        string query = "SELECT FirstName, LastName FROM Employees";
        using(var conn = new SqlConnection(connectionString))
        {
            conn.Open();
            List<Employee> employees = conn.Query<Employee>(query).ToList();
            return employees;
        } 
    }
    public static void AddEmployee()
    {
        Console.WriteLine("Please introduce the first name:");
        string firstName = Console.ReadLine();
        Console.WriteLine("Please introduce the last name:");
        string lastName = Console.ReadLine();
        Employee newEmployee = new Employee
        {
            FirstName = firstName,
            LastName = lastName
        };
        using(SqlConnection conn = new SqlConnection(connectionString))
        {
            try
            {
                conn.Open();
                string insertQuery = "INSERT INTO Employees (FirstName,LastName) " +
                                        "VALUES (@FirstName, @LastName);";


                //int insertedEmployeeId = conn.ExecuteScalar<int>(insertQuery, newEmployee);
                conn.Execute(insertQuery, newEmployee);
               // Console.WriteLine($"Employee added with ID :{insertedEmployeeId}");
            }
            catch (Exception ex) { Console.WriteLine("ERROR ADDING EMPLOYEE!"); }
        }

    }
}
