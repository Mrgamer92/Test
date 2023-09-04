using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Test.Models;

public partial class Lezioni
{
    public int IdLezioni { get; set; } = 1;

    [DisplayName ("Codice Corso")]
    public string CodCorso { get; set; } = null!;

    public string Corso { get; set; } = null!;

    public int Ore { get; set; }

    public virtual ICollection<AllieviLezione> AllieviLeziones { get; set; } = new List<AllieviLezione>();

    public virtual Insegnanti IdLezioniNavigation { get; set; } = null!;
}
