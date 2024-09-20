Habit Tracker Program Documentation
Namespace: HabitTrackerTheCSharpACademy
This namespace contains the main logic for the Habit Tracker console application.

Class: Program
This is the entry point class for the Habit Tracker application. It manages user interactions through a menu-driven console interface and interacts with the Database to manage habits.

Fields
Database db: This instance interacts with the data layer for habit management. It allows the creation, reading, updating, deletion, and logging of habits.
Methods
static void Main(string[] args)

Description: This is the entry point of the application. It initializes the Program class and invokes the DisplayMenu method.
void DisplayMenu()

Description: Displays the main menu options to the user. Options include creating, showing, updating, and deleting habits, as well as logging habit occurrences.
void ProccessAnswer()

Description: Takes user input from the menu (1-5), and directs the user to perform the selected action.
Error Handling: If input is invalid, prompts the user to choose a valid menu option (1-5).
void ContinueFromMenu(int menuItem)

Description: Determines the action based on the menu option selected by the user.
Options:
1: Create a new habit.
2: Show habits.
3: Update an existing habit.
4: Delete a habit.
5: Log an occurrence for a habit.
void LogHabit()

Description: Allows the user to log occurrences for a habit by entering the habit name. If the name is not valid, the user is prompted to re-enter.
void ShowHabits()

Description: Retrieves and displays a list of all habits stored in the database.
void OccurencesResponse()

Description: Asks the user if they want to see occurrences of a habit. If yes, it calls the ShowOccurences method.
void ShowOccurences()

Description: Displays the occurrence of a specific habit based on its name and date. It prompts the user for both the habit's name and date, then fetches the occurrence data from the database.
void ShowUnits()

Description: Displays available units for the habits (e.g., 8 hours of sleep, Cups).
void CreateHabit()

Description: Prompts the user for a habit name and unit, then creates a new habit in the database.
void UpdateHabit()

Description: Prompts the user to select a habit to update and passes the input to the database for modification.
void RemoveHabit()

Description: Prompts the user to select a habit to delete and attempts to remove it from the database.
Usage Workflow
The program starts and displays a main menu with several options (create, show, update, delete, or log a habit).
The user selects an option and follows the prompts.
Depending on the user input, habits can be created, modified, or deleted.
Logging occurrences and showing habit data is supported.



Namespace: HabitTrackerTheCSharpACademy
This namespace contains the Database class, which handles the interactions with the underlying data storage using Entity Framework for managing habits and their occurrences.

Class: Database
This class is responsible for managing the database operations for the Habit Tracker application. It includes methods for creating, updating, deleting, and logging habits, as well as retrieving habit data and occurrences.

Constructor: Database()
Description: The constructor ensures that the database is created if it doesn't already exist when the Database object is instantiated.


    db.Database.EnsureCreated();

Methods
void CreateHabit(Habit habit)
Description: Adds a new habit to the database if it doesn't already exist. If the habit already exists, it prevents duplication.

Parameters:

habit: The Habit object to be added to the database.
Error Handling: If the habit already exists, a message is displayed.

List<Habit> GetHabits()
Description: Retrieves and returns a list of all habits stored in the database.

Returns: A list of Habit objects from the database.

void UpdateHabit(string HabitName)
Description: Updates the name and unit of an existing habit in the database.

Parameters:

HabitName: The name of the habit to be updated.
Prompts: The method prompts the user for the new name and unit of the habit.

Habit FindHabitByName(string name)
Description: Searches the database for a habit by its name and returns the Habit object if found.

Parameters:

name: The name of the habit to search for.
Returns: The Habit object if found, otherwise null.

bool DeleteHabit(string name)
Description: Deletes a habit and its associated occurrences from the database based on the provided habit name.

Parameters:

name: The name of the habit to be deleted.
Returns: true if the habit was successfully deleted, false if no habit was deleted.

Occurence GetOccurenceByNameAndDate(string name, string date)
Description: Retrieves an occurrence of a habit based on the habit name and date.

Parameters:

name: The name of the habit.
date: The date of the occurrence (in the format DD/MM/YYYY).
Returns: The Occurence object for the specified habit and date, or null if no occurrence is found.

void LogHabit(string name)
Description: Logs a new occurrence for a specified habit. The user is prompted to enter the number of occurrences and the date.

Parameters:

name: The name of the habit for which an occurrence is being logged.
Prompts: The user is prompted for the number of units to log and the date.

Error Handling: If the number of occurrences is not in the correct format, the user is asked to try again.

Usage Workflow
The Database class interacts with the HabitContext for all CRUD operations.
It includes methods for managing both habits and their occurrences, ensuring that the database is updated accordingly.
User input is prompted when creating or updating habits and logging occurrences, with appropriate error handling in place for invalid input.


