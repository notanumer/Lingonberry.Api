﻿using Lingonberry.Api.Domain.Users;

namespace Lingonberry.Api.Domain.Locations;

public class Department : BaseDomain
{
    public ICollection<Group>? Groups { get; set; }

    public ICollection<Division>? Divisions { get; set; }
}
