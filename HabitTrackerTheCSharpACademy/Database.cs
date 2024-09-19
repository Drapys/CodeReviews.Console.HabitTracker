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
        public Database()
        {
            db.Database.EnsureCreated();
        }

        public void CreateHabit(Habit habit)
        {

            if (db.Habits.Contains(habit))
            {
                Console.WriteLine("Habit was not created, because it already exists");

            }
            else
            {
                db.Habits.Add(habit);
                db.SaveChanges();
                Console.WriteLine($"Habit {habit.Name} was created. What do you wish to do now?");
            }
        }

        public List<Habit> GetHabits()
        {
            List<Habit> list = new();
            list = db.Habits.ToList();
            return list;
        }

        public void UpdateHabit(string HabitName)
        {
            Habit Habit = db.Habits.FirstOrDefault(b => b.Name == HabitName);

            if (Habit != null) {
                
                Console.WriteLine("Write a new name for your habit.");
                string newname = Console.ReadLine();

                Habit.Name = newname;
                db.Habits.Update(Habit);

                Console.WriteLine($"Habit {HabitName} was updated to {Habit.Name}.");

                db.SaveChanges();
            }


           
        }

       public Habit FindHabitByName(string name)
        {
            Habit habit = null;
            habit = db.Habits.FirstOrDefault(b => b.Name == name);
            return habit;
        }

        public void DeleteHabit(string name)
        {
            Habit habit = db.Habits.FirstOrDefault(b => b.Name == name);
            if (habit != null) { 
            db.Habits.Remove(habit);
                db.SaveChanges();
            }
        }

    }
}
