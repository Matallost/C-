using BackEnd;
using System.Collections.Generic;
using System.Linq;

public class EquipmentManager
{
    public List<Employee> employees = new List<Employee>();
    public List<Computer> computers = new List<Computer>();
    public List<Printer> printers = new List<Printer>();

    public void AddEmployee(Employee employee)
    {
        employees.Add(employee);
    }

    public void RemoveEmployee(int employeeId)
    {
        employees.RemoveAll(e => e.Id == employeeId);
    }

    public void AddComputer(Computer computer)
    {
        computers.Add(computer);
    }

    public void RemoveComputer(int computerId)
    {
        computers.RemoveAll(c => c.Id == computerId);
    }

    public void AddPrinter(Printer printer)
    {
        printers.Add(printer);
    }

    public void RemovePrinter(int printerId)
    {
        printers.RemoveAll(p => p.Id == printerId);
    }

    public int CalculateComputersNeeded()
    {
        return employees.Count;
    }

    public int CalculatePrintersNeeded()
    {
        return (int)Math.Ceiling((double)employees.Count / 5); // Один принтер на 5 сотрудников
    }

    public void EvaluateEquipmentForReplacement()
    {
        foreach (var computer in computers)
        {
            if (computer.NeedsReplacement())
            {
                System.Console.WriteLine($"Компьютер {computer.Model} требует замены.");
            }
        }
    }

    public List<Computer> GetComputers() => computers;

    public List<Printer> GetPrinters() => printers;

    public List<Employee> GetEmployees() => employees;

    public IEnumerable<Employee> FilterEmployeesByName(string name)
    {
        return employees.Where(e => e.Name.Contains(name));
    }

    public IEnumerable<Computer> SortComputersByPurchaseDate()
    {
        return computers.OrderBy(c => c.UpgradeDate);
    }

    public IEnumerable<Printer> SortPrintersByModel()
    {
        return printers.OrderBy(p => p.Model);
    }
}

