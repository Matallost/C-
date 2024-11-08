using BackEnd;
using System.Diagnostics;
using System.Reflection;

internal class Program
{
    private static void Main(string[] args)
    {
        EquipmentManager manager = new EquipmentManager();
        string filePath = "equipmentData.xml";

        // Попытка загрузить данные из XML
        try
        {
            EquipmentData data = XmlUtility.Load<EquipmentData>(filePath);
            foreach (var employee in data.Employees)
            {
                manager.AddEmployee(employee);
            }
            foreach (var computer in data.Computers)
            {
                manager.AddComputer(computer);
            }
            foreach (var printer in data.Printers)
            {
                manager.AddPrinter(printer);
            }
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Файл данных не найден. Начинаем с пустой базы.");
        }

        Console.WriteLine("Добро пожаловать в систему управления оборудованием!");

        while (true)
        {
            Console.WriteLine("\nЧто нужно сделать:");
            Console.WriteLine("1. Добавить сотрудника.");
            Console.WriteLine("2. Удалить сотрудника.");
            Console.WriteLine("3. Добавить компьютер.");
            Console.WriteLine("4. Удалить компьютер.");
            Console.WriteLine("5. Добавить принтер.");
            Console.WriteLine("6. Удалить принтер.");
            Console.WriteLine("7. Сортировка компьютеров.");
            Console.WriteLine("8. Сортировка принтеров.");
            Console.WriteLine("9. Изменение данных о сотруднике или компьютере.");
            Console.WriteLine("10. Проверка на необходимость замены оборудования.");
            Console.WriteLine("11. Вывести данные о всех сотрудниках");
            Console.WriteLine("0. Выход");

            Console.Write("Выберите действие: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddEmployee(manager);
                    break;
                case "2":
                    RemoveEmployee(manager);
                    break;
                case "3":
                    AddComputer(manager);
                    break;
                case "4":
                    RemoveComputer(manager);
                    break;
                case "5":
                    AddPrinter(manager);
                    break;
                case "6":
                    RemovePrinter(manager);
                    break;
                case "7":
                    SortComputers(manager);
                    break;
                case "8":
                    SortPrinters(manager);
                    break;
                case "9":
                    ModifyData(manager);
                    break;
                case "10":
                    manager.EvaluateEquipmentForReplacement();
                    break;
                case "11":
                    ShowEmployeeData(manager);
                    break;
                case "0":
                    SaveData(manager, filePath);
                    return; // Выход из программы
                default:
                    Console.WriteLine("Неверный выбор. Пожалуйста, попробуйте еще раз.");
                    break;
            }

        }

        static void AddEmployee(EquipmentManager manager)
        {
            Console.Write("Введите ID сотрудника: ");
            int id = int.Parse(Console.ReadLine());
            Console.Write("Введите имя сотрудника: ");
            string name = Console.ReadLine();
            manager.AddEmployee(new Employee { Id = id, Name = name });
            Console.WriteLine("Сотрудник добавлен.");
        }

        static void RemoveEmployee(EquipmentManager manager)
        {
            Console.Write("Введите ID сотрудника для удаления: ");
            int id = int.Parse(Console.ReadLine());
            manager.RemoveEmployee(id);
            Console.WriteLine("Сотрудник удален.");
        }

        static void AddComputer(EquipmentManager manager)
        {
            Console.Write("Введите ID компьютера: ");
            int id = int.Parse(Console.ReadLine());
            Console.Write("Введите модель компьютера: ");
            string model = Console.ReadLine();
            Console.Write("Введите дату покупки (YYYY-MM-DD): ");
            DateTime purchaseDate = DateTime.Parse(Console.ReadLine());
            manager.AddComputer(new Computer { Id = id, Model = model, UpgradeDate = purchaseDate });
            Console.WriteLine("Компьютер добавлен.");
        }

        static void RemoveComputer(EquipmentManager manager)
        {
            Console.Write("Введите ID компьютера для удаления: ");
            int id = int.Parse(Console.ReadLine());
            manager.RemoveComputer(id);
            Console.WriteLine("Компьютер удален.");
        }

        static void AddPrinter(EquipmentManager manager)
        {
            Console.Write("Введите ID принтера: ");
            int id = int.Parse(Console.ReadLine());
            Console.Write("Введите модель принтера: ");
            string model = Console.ReadLine();
            manager.AddPrinter(new Printer { Id = id, Model = model });
            Console.WriteLine("Принтер добавлен.");
        }

        static void RemovePrinter(EquipmentManager manager)
        {
            Console.Write("Введите ID принтера для удаления: ");
            int id = int.Parse(Console.ReadLine());
            manager.RemovePrinter(id);
            Console.WriteLine("Принтер удален.");
        }

        static void SortComputers(EquipmentManager manager)
        {
            var sortedComputers = manager.SortComputersByPurchaseDate().ToList();
            Console.WriteLine("Компьютеры отсортированы по дате покупки:");
            foreach (var computer in sortedComputers)
            {
                Console.WriteLine($"ID: {computer.Id}, Модель: {computer.Model}, Дата покупки: {computer.UpgradeDate}");
            }
        }

        static void SortPrinters(EquipmentManager manager)
        {
            var sortedPrinters = manager.SortPrintersByModel().ToList();
            Console.WriteLine("Принтеры отсортированы по модели:");
            foreach (var printer in sortedPrinters)
            {
                Console.WriteLine($"ID: {printer.Id}, Модель: {printer.Model}");
            }
        }
        static void ShowEmployeeData(EquipmentManager manager)
        {
            foreach(var employee in manager.employees)
            {
                Console.WriteLine($"ID: {employee.Id}, : Имя: {employee.Name}");
            }
        }

        static void ModifyData(EquipmentManager manager)
        {
            Console.Write("Введите ID сотрудника или компьютера для изменения: ");
            int id = int.Parse(Console.ReadLine());
            Console.Write("Введите новое имя или данные (в случае изменения компьютера): ");
            string newData = Console.ReadLine();
            foreach(var employee in manager.employees)
            {
                if (employee.Id == id)
                {
                    employee.Name = newData;
                }
            }
            Console.WriteLine("Данные изменены."); 
        }

        static void SaveData(EquipmentManager manager, string filePath)
        {
            EquipmentData equipmentData = new EquipmentData
            {
                Employees = manager.GetEmployees(),
                Computers = manager.GetComputers(),
                Printers = manager.GetPrinters()
            };
            XmlUtility.Save(equipmentData, filePath);
            Console.WriteLine("Данные успешно сохранены в XML.");
        }
}
}
