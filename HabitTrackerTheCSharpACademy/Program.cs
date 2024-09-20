using HabitTrackerTheCSharpACademy.Models;

namespace HabitTrackerTheCSharpACademy
{
    class Program
    {
        public Database db = new Database();

        /// <summary>
        /// Entry point of the application. Initializes the Program and displays the main menu.
        /// </summary>
        /// <param name="args">Command line arguments</param>
        static void Main(string[] args)
        {
            var Program = new Program();
            Program.DisplayMenu();
        }

        /// <summary>
        /// Displays the main menu options and processes user input for habit actions.
        /// </summary>
        void DisplayMenu()
        {
            Console.WriteLine("Main menu: ");
            Console.WriteLine($"1 - Create habit");
            Console.WriteLine($"2 - Show habits");
            Console.WriteLine($"3 - Update habit");
            Console.WriteLine($"4 - Delete habit");
            Console.WriteLine($"5 - Log habit");
            Console.WriteLine(" ");
            ProccessAnswer();
        }

        /// <summary>
        /// Processes the user's menu selection and directs to the corresponding action.
        /// Handles invalid input with error handling.
        /// </summary>
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

        /// <summary>
        /// Continues with the menu selection and calls the appropriate method based on user input.
        /// </summary>
        /// <param name="menuItem">The menu option selected by the user.</param>
        void ContinueFromMenu(int menuItem)
        {
            switch (menuItem)
            {
                case 1:
                    CreateHabit();
                    DisplayMenu();
                    break;
                case 2:
                    ShowHabits();
                    OccurencesResponse();
                    DisplayMenu();
                    break;
                case 3:
                    UpdateHabit();
                    DisplayMenu();
                    break;
                case 4:
                    RemoveHabit();
                    DisplayMenu();
                    break;
                case 5:
                    LogHabit();
                    DisplayMenu();
                    break;
                default:
                    DisplayMenu();
                    ContinueFromMenu(int.Parse(Console.ReadLine()));
                    break;
            }
        }

        /// <summary>
        /// Logs an occurrence for a selected habit. Prompts the user to enter the habit name.
        /// </summary>
        void LogHabit()
        {
            Console.WriteLine("What habit do you wanna log? Write full name");
            ShowHabits();
            string name = Console.ReadLine();
            if (name != null)
            {
                db.LogHabit(name);
            }
            else
            {
                LogHabit();
            }
        }

        /// <summary>
        /// Displays all habits stored in the database.
        /// </summary>
        void ShowHabits()
        {
            Console.WriteLine("Habit list: ");
            Console.WriteLine();
            List<Habit> allHabits = db.GetHabits();
            foreach (Habit hab in allHabits)
            {
                Console.WriteLine(hab.Name);
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Asks if the user wants to see occurrences of a specific habit and calls the method to show occurrences.
        /// </summary>
        void OccurencesResponse()
        {
            Console.WriteLine("Do you wanna see occurences of a habit?");
            Console.WriteLine("1 - Yes");
            Console.WriteLine("2 - No");

            int response = int.Parse(Console.ReadLine());

            switch (response)
            {
                case 1:
                    ShowOccurences();
                    break;
                case 2:
                    break;
                default:
                    Console.WriteLine("Not valid answer, redirected to main menu.");
                    break;
            }
        }

        /// <summary>
        /// Displays occurrences of a habit based on the habit's name and date.
        /// </summary>
        void ShowOccurences()
        {
            Console.WriteLine("Write the full name of your habit");
            ShowHabits();
            string name = Console.ReadLine();
            Console.WriteLine("Write the date in the correct format. (DD/MM/YY");
            string date = Console.ReadLine();
            Occurence oc = new();
            oc = db.GetOccurenceByNameAndDate(name, date);

            if (oc == null)
            {
                Console.WriteLine("Your habit does not exist, you entered a wrong date, or there is no occurence, try again.");
                ShowOccurences();
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Occurence:");
                Console.WriteLine($"{name} - {oc.Occurences} {db.FindHabitByName(name).Unit}");
            }
        }

        /// <summary>
        /// Displays the available units for habits (e.g., hours of sleep, cups).
        /// </summary>
        void ShowUnits()
        {
            Console.WriteLine("1 - 8 hours of sleep");
            Console.WriteLine("2 - something");
            Console.WriteLine("3 - Cups");
        }

        /// <summary>
        /// Prompts the user for a habit name and unit, then creates a new habit in the database.
        /// </summary>
        void CreateHabit()
        {
            string name = "";
            int Unit = 0;
            Console.WriteLine("Choose a name for your habit: ");
            name = Console.ReadLine();
            Console.WriteLine("Choose a unit for your habit");
            ShowUnits();

            if (name == null)
            {
                Console.WriteLine("Write a name for you habit.");
                CreateHabit();
            }
            else
            {
                string unitString = "";
                switch (Unit)
                {
                    case 1:
                        unitString = "8 hours of sleep";
                        break;
                    case 2:
                        unitString = "something";
                        break;
                    case 3:
                        unitString = "Cups";
                        break;
                }
                Habit newHabit = new Habit()
                {
                    Name = name,
                    Unit = unitString
                };
                db.CreateHabit(newHabit);
            }
        }

        /// <summary>
        /// Prompts the user to select a habit to update and sends the updated data to the database.
        /// </summary>
        void UpdateHabit()
        {
            Console.WriteLine("Choose a habit to update by its full name:");
            ShowHabits();
            Console.WriteLine();
            db.UpdateHabit(Console.ReadLine());
        }

        /// <summary>
        /// Prompts the user to select a habit to delete and attempts to remove it from the database.
        /// </summary>
        void RemoveHabit()
        {
            Console.WriteLine("Choose a habit to delete by its full name.");
            ShowHabits();
            Console.WriteLine();
            if (!db.DeleteHabit(Console.ReadLine()))
            {
                RemoveHabit();
            }
        }
    }
}
