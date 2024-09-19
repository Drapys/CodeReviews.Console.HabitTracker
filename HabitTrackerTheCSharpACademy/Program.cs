using HabitTrackerTheCSharpACademy.Models;

namespace HabitTrackerTheCSharpACademy;

class Program
{
    public Database db = new Database();

    static void Main(string[] args)
    {
        var Program = new Program();

        Program.DisplayMenu();
    }

    void DisplayMenu()
    {
        Console.WriteLine("Main menu: ");
        Console.WriteLine($"1 - Create habit");
        Console.WriteLine($"2 - Show habits");
        Console.WriteLine($"3 - Update habit");
        Console.WriteLine($"4 - Delete habit");
        Console.WriteLine(" ");
        ProccessAnswer();
    }

    void ProccessAnswer()
    {
        try
        {
            Console.WriteLine("Choose an action 1-4");
            ContinueFromMenu(int.Parse(Console.ReadLine()));
        }
        catch (FormatException ex)
        {
            Console.WriteLine("Choose 1-4 from menu.");
            DisplayMenu();
            ContinueFromMenu(int.Parse(Console.ReadLine()));
        }
    }
    void ContinueFromMenu(int menuItem)
    {
        switch (menuItem)
        {
            case 1:
                Console.WriteLine("Choose a name for your habit: ");
                CreateHabit(Console.ReadLine());
                DisplayMenu();
                break;
            case 2:
                ShowHabits();
                DisplayMenu();
                break;
            case 3:
                UpdateHabit();
                DisplayMenu();
                break;
            case 4:
                DisplayMenu();
                break;
            default:
                DisplayMenu();
                ContinueFromMenu(int.Parse(Console.ReadLine()));
                break;

        }
    }

    void ShowHabits()
    {
        Console.WriteLine("Habit list: ");
        Console.WriteLine();
        List<Habit> allHabits = db.GetHabits();
        foreach(Habit hab in allHabits)
        {
            Console.WriteLine(hab.Name);
        }
        Console.WriteLine();
     
    }

    void CreateHabit(string name)
    {
        if (name == null)
        {
            Console.WriteLine("Write a name for you habit.");
            CreateHabit(Console.ReadLine());
        }
        else
        {
            Habit newHabit = new Habit()
            {
                Name = name
            };
            db.CreateHabit(newHabit);
            

        }

    }

    void UpdateHabit()
    {
   
        Console.WriteLine("Choose a habit to update by full name:");
        ShowHabits();
        Console.WriteLine();
        db.UpdateHabit(Console.ReadLine());

    }


}