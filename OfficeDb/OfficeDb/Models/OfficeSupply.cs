using System;
using System.Collections.Generic;

namespace OfficeDb.Models;

public partial class OfficeSupply
{
    public int ItemId { get; set; }

    public string ItemName { get; set; } = null!;

    public string Department { get; set; } = null!;

    public int Quantity { get; set; }
}
