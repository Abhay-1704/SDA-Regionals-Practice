using System;
using System.Collections.Generic;

namespace FleetDb.Models;

public partial class Vehicle
{
    public int VehicleId { get; set; }

    public string LicensePlate { get; set; } = null!;

    public string ModelName { get; set; } = null!;

    public DateTime LastServiceDate { get; set; }

    public int Mileage { get; set; }

    public bool RequiresMaintenance { get; set; }
}
