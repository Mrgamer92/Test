using System;
using System.Collections.Generic;

namespace Test.Models;

public partial class Insegnanti
{
    public int IdInsegnante { get; set; }

    public string Nome { get; set; } = null!;

    public string Cognome { get; set; } = null!;

    public int Lezione { get; set; }

    public virtual Lezioni? Lezioni { get; set; }
}
