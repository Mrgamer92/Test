using System;
using System.Collections.Generic;

namespace Test.Models;

public partial class Allievo
{
    public int IdAllievo { get; set; }

    public string Nome { get; set; } = null!;

    public string Cognome { get; set; } = null!;

    public string CorsoScelto { get; set; } = null!;

    public virtual AllieviLezione? AllieviLezione { get; set; }
}
