using System;
using System.Collections.Generic;

namespace Test.Models;

public partial class AllieviLezione
{
    public int IdLezione { get; set; }

    public int Allievo { get; set; }

    public virtual Allievo AllievoNavigation { get; set; } = null!;

    public virtual Lezioni IdLezioneNavigation { get; set; } = null!;
}
