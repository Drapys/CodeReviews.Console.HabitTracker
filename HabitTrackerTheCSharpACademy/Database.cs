using HabitTrackerTheCSharpACademy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabitTrackerTheCSharpACademy
{
    public class Database
    {
        HabitContext db = new HabitContext();

        /// <summary>
        /// Initializes the Database object and ensures the database is created.
        /// </summary>
        public Database()
        {
            db.Database.EnsureCreated();
        }

        /// <summary>
        /// Creates a new habit in the database. If the habit already exists, it will not be created.
        /// </summary>
        /// <param name="habit">The Habit object to be added to the database.</param>
        public void CreateHabit(Habit habit)
        {
            if (db.Habits.Contains(habit))
            {
                Console.WriteLine("Habit was not created, because it already exists.");
            }
            else
            {
                db.Habits.Add(habit);
                db.SaveChanges();
                Console.WriteLine($"Habit {habit.Name} was created. What do you wish to do now?");
            }
        }

        /// <summary>
        /// Retrieves all habits stored in the database.
        /// </summary>
        /// <returns>List of Habit objects.</returns>
        public List<Habit> GetHabits()
        {
            List<Habit> list = new();
            list = db.Habits.ToList();
            return list;
        }

        /// <summary>
        /// Updates the name and unit of an existing habit.
        /// Prompts the user for the new habit details.
        /// </summary>
        /// <param name="HabitName">The name of the habit to be updated.</param>
        public void UpdateHabit(string HabitName)
        {
            Habit habit = db.Habits.FirstOrDefault(b => b.Name == HabitName);

            if (habit != null)
            {
                Console.WriteLine("Write a new name for your habit.");
                string newname = Console.ReadLine();
                Console.WriteLine("Write the new unit");
                string unit = Console.ReadLine();
                habit.Name = newname;
                habit.Unit = unit;

                db.Habits.Update(habit);

                Console.WriteLine($"Habit {HabitName} was updated to {habit.Name}.");

                db.SaveChanges();
            }
        }

        /// <summary>
        /// Finds and returns a habit by its name.
        /// </summary>
        /// <param name="name">The name of the habit to be retrieved.</param>
        /// <returns>The Habit object if found, otherwise null.</returns>
        public Habit FindHabitByName(string name)
        {
            Habit habit = db.Habits.FirstOrDefault(b => b.Name == name);
            return habit;
        }

        /// <summary>
        /// Deletes a habit and its occurrences from the database.
        /// </summary>
        /// <param name="name">The name of the habit to be deleted.</param>
        /// <returns>True if the habit was successfully deleted, otherwise false.</returns>
        public bool DeleteHabit(string name)
        {
            List<Occurence> occurences = new();

            var query = from occurence in db.Occurences
                        where occurence.Habitid == FindHabitByName(name).Id
                        select occurence;

            occurences = query.ToList();
            db.RemoveRange(occurences);
            db.SaveChanges();

            Habit habit = FindHabitByName(name);
            if (habit != null)
            {
                db.Habits.Remove(habit);
                db.SaveChanges();
                Console.WriteLine($"Habit {name} was deleted.");
                return true;
            }
            else
            {
                Console.WriteLine("No habit was deleted.");
                return false;
            }
        }

        /// <summary>
        /// Retrieves an occurrence of a habit based on the habit name and date.
        /// </summary>
        /// <param name="name">The name of the habit.</param>
        /// <param name="date">The date of the occurrence in the format DD/MM/YYYY.</param>
        /// <returns>An Occurence object representing the habit's occurrence for the specified date.</returns>
        public Occurence GetOccurenceByNameAndDate(string name, string date)
        {
            Occurence occurence = new();

            var query = from g in db.Occurences
                        where g.Habitid == FindHabitByName(name).Id && g.Date == date
                        select g;
            occurence = query.FirstOrDefault();

            return occurence;
        }

        /// <summary>
        /// Logs a habit occurrence by prompting the user for the number of occurrences and the date.
        /// </summary>
        /// <param name="name">The name of the habit to log.</param>
        public void LogHabit(string name)
        {
            Habit habit = FindHabitByName(name);
            int numberOfOccurences = 0;

            Console.WriteLine("How many units of " + habit.Unit + " do you want to add to your habit " + habit.Name + "?");
            try
            {
                numberOfOccurences = int.Parse(Console.ReadLine());
            }
            catch (FormatException fe)
            {
                Console.WriteLine("Number of occurrences was not in the correct format, try again.");
            }
            finally
            {
                if (numberOfOccurences == 0)
                {
                    LogHabit(name);
                }
            }

            Occurence occurence = new();

            Console.WriteLine("What date do you want to log the habit? (Enter in correct format: DD/MM/YYYY");
            occurence.Date = Console.ReadLine();
            occurence.Occurences = numberOfOccurences;
            occurence.Habitid = FindHabitByName(name).Id;

            db.Occurences.Add(occurence);
            db.SaveChanges();
            Console.WriteLine("Successfully logged.");
        }
    }
}
