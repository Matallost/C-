using BackEnd;
using System.Collections.Generic;

public class EquipmentData
{
    public List<Employee> Employees { get; set; } = new List<Employee>();
    public List<Computer> Computers { get; set; } = new List<Computer>();
    public List<Printer> Printers { get; set; } = new List<Printer>();
}

