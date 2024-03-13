using System;

class Classes_first
{
    static void Main(string[] args)
    {

        // Получение данных о резервуарах, установках и заводах
        var tanks = GetTanks();
        var units = GetUnits();
        var factories = GetFactories();

        int choice = -1; // переменная для выбора действий


        while (choice != 0) 
        {

            Console.WriteLine("\nВыберите действие:\n1 - Поиск резервуара\n2 - Вывод информации о всех объектах\n3 - Выход\n");

            // Проверка ввода
            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.Write("Неверный ввод\n");
                break; 
            }

            switch (choice)
            {
                case 1:
                   
                    Console.WriteLine("\nВведите название резервуара:");

                    string searchTankName = Console.ReadLine();             // имя резервуара
                    var foundTank = FindTank(tanks, searchTankName);        // поиск

                    if (foundTank != null)
                    {
                        var unit = FindUnit(units, tanks, foundTank.tankName);
                        var factoryOfUnit = FindFactory(factories, unit);

                        Console.WriteLine($"Резервуар {foundTank.tankName} принадлежит установке {unit.unitName}, который принадлежит заводу {factoryOfUnit.factoryName}");
                    }
                    else
                    {
                        Console.WriteLine($"Резервуар с названием '{searchTankName}' не найден.");
                    }
                    break;

                case 2:
                    // общий объем резервуаров
                    var totalVolume = GetTotalVolume(tanks);
                    Console.WriteLine($"\nОбщий объем резервуаров: {totalVolume}");

                    // о каждом резервуаре, его установке и заводе
                    Console.WriteLine("Вывод всех резервуаров:");
                    foreach (var tank in tanks)
                    {
                        var unit = FindUnit(units, tanks, tank.tankName); // поиск установки по резервуару
                        var factoryOfUnit = FindFactory(factories, unit); // поиск завода по установке
                        Console.WriteLine($"Резервуар {tank.tankName} принадлежит установке {unit.unitName}, который принадлежит заводу {factoryOfUnit.factoryName}");
                    }
                    break;

                case 3:
                    return;


                default:
                    Console.WriteLine("Неверный ввод");
                    break;
            }
        }
    }

    // Метод для получения данных о резервуарах
    public static Tank[] GetTanks()
    {
        return new Tank[]
        {
            new Tank(1, "Резервуар 1", "Надземный - вертикальный", 1500, 2000, 1),
            new Tank(2, "Резервуар 2", "Надземный - горизонтальный", 2500, 3000, 1),
            new Tank(3, "Дополнительный резервуар 24", "Надземный - горизонтальный", 3000, 3000, 2),
            new Tank(4, "Резервуар 35", "Надземный - вертикальный", 3000, 3000, 2),
            new Tank(5, "Резервуар 47", "Подземный - двустенный", 4000, 5000, 2),
            new Tank(6, "Резервуар 256", "Подводный", 500, 500, 3)
        };
    }

    // Метод получения данных об установках
    public static Unit[] GetUnits()
    {
        return new Unit[]
        {
            new Unit(1, "ГФУ-2", "Газофракционирующая установка", 1),
            new Unit(2, "АВТ-6", "Атмосферно-вакуумная установка", 1),
            new Unit(3, "АВТ-10", "Атмосферно-вакуумная установка", 2)
        };
    }

    // Метод получения данных о заводах
    public static Factory[] GetFactories()
    {
        return new Factory[]
        {
            new Factory(1, "НПЗ№1", "Первый нефтеперерабатывающий завод"),
            new Factory(2, "НПЗ№2", "Второй нефтеперерабатывающий завод")
        };
    }

    // Метод для поиска резервуара по наименованию
    public static Tank FindTank(Tank[] tanks, string tankName)
    {
        foreach (var tank in tanks)
        {
            if (tank.tankName == tankName)
            {
                return tank;
            }
        }
        return null;
    }

    // Метод для поиска установки по имени резервуара
    public static Unit FindUnit(Unit[] units, Tank[] tanks, string unitName)
    {
        foreach (var tank in tanks)
        {
            if (tank.tankName == unitName)
            {
                foreach (var unit in units)
                {
                    if (unit.Unit_Id == tank.UnitId)
                    {
                        return unit;
                    }
                }
            }
        }
        return null;
    }

    // Метод поиска завода по установке
    public static Factory FindFactory(Factory[] factories, Unit unit)
    {
        foreach (var factory in factories)
        {
            if (factory.Factory_Id == unit.FactoryId)
            {
                return factory;
            }
        }
        return null;
    }

    // Метод вычисления общего объема всех резервуаров
    public static int GetTotalVolume(Tank[] tanks)
    {
        int totalVolume = 0;

        foreach (var tank in tanks)
        {
            totalVolume += tank.Tank_Volume;
        }
        return totalVolume;
    }
}

//класс представляющий установку
public class Unit
{
    public int Unit_Id { get; }
    public string unitName { get; }
    public string Unit_Desc { get; }
    public int FactoryId { get; }
    // конструктор с параметрами
    public Unit(int id, string name, string description, int factoryId)
    {
        Unit_Id = id;
        unitName = name;
        Unit_Desc = description;
        FactoryId = factoryId;
    }
}

// Класс, представляющий завод
public class Factory
{
    public int Factory_Id { get; }
    public string factoryName { get; }
    public string Factory_Desc { get; }



    public Factory(int id, string name, string description)
    {
        Factory_Id = id;
        factoryName = name;
        Factory_Desc = description;
    }
}

// Класс, представляющий резервуар
public class Tank
{
    public int Tank_Id { get; }
    public string tankName { get; }
    public string Tank_Desc { get; }
    public int Tank_Volume { get; }
    public int Tank_MaxVolume { get; }
    public int UnitId { get; }
    // конструктор с параметрами
    public Tank(int id, string name, string description, int volume, int maxVolume, int unitId)
    {
        Tank_Id = id;
        tankName = name;
        Tank_Desc = description;
        Tank_Volume = volume;
        Tank_MaxVolume = maxVolume;
        UnitId = unitId;
    }
}