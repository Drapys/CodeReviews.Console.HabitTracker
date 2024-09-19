using System;
using System.Collections.Generic;

namespace HabitTrackerTheCSharpACademy.Models;

public partial class Occurence
{
    public int Id { get; set; }

    public string Date { get; set; } = null!;

    public int Occurences { get; set; }

    public int Habitid { get; set; }

    public string Unit { get; set; } = null!;
}
