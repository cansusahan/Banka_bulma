using System;
using System.Collections.Generic;

namespace son.Models;

public partial class Banka
{
    public int Id { get; set; }

    public int? Bin { get; set; }

    public int? BankaKodu { get; set; }

    public string? BankaAdi { get; set; }

    public string? Tip { get; set; }

    public string? AltTip { get; set; }

  

    internal static object Where(Func<object, bool> value)
    {
        throw new NotImplementedException();
    }
}
